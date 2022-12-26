using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using ProductAPI.Data;
using ProductAPI.Models;
using ProductAPI.Repository.IRepository;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ProductAPI.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
	{

		private readonly ApplicationDbContext _db;
		private readonly IMemoryCache _cache;
		private readonly IList<string> _validSortColumns = new List<string> { "owner", "name", "priceRange", "category", "onSale", "averageRating" };
		
		public ProductRepository(ApplicationDbContext db, IMemoryCache cache) : base(db)
		{
			_db = db;
			_cache = cache;
		}

		public async Task<int> CountAsync()
		{
			return await _db.Products.CountAsync();
		}


		public async Task<IEnumerable<Product>> GetAllAsync(int pageSize, int pageNumber, string sortColumn, bool sortOrder)
		{
			// Validate the pagination parameters
			if (pageSize <= 0)
			{
				throw new ArgumentOutOfRangeException(nameof(pageSize), "The pageSize parameter must be greater than zero.");
			}

			if (pageNumber <= 0)
			{
				throw new ArgumentOutOfRangeException(nameof(pageNumber), "The pageNumber parameter must be greater than zero.");
			}

			// Build the cache key based on the pagination and sorting parameters
			string cacheKey = $"products_{pageSize}_{pageNumber}_{sortColumn}_{sortOrder}";

			// Check if the results are already cached
			if (_cache.TryGetValue(cacheKey, out IEnumerable<Product> products))
			{
				// Return the cached results
				return products;
			}

			// Build the query
			IQueryable<Product> query = _db.Products
				.Include(p => p.Category)  // Include the Category navigation property
				.Include(p => p.Images)  // Include the Images navigation property
				.Include(p => p.Videos)  // Include the Videos navigation property
				.AsQueryable();

			// Implement sorting logic here using the sortColumn and sortOrder parameters
			if (sortColumn == "owner")
			{
				query = sortOrder ? query.OrderBy(p => p.OwnerId) : query.OrderByDescending(p => p.OwnerId);
			}
			else if (sortColumn == "name")
			{
				query = sortOrder ? query.OrderBy(p => p.Name) : query.OrderByDescending(p => p.Name);
			}
			else if (sortColumn == "priceRange")
			{
				query = sortOrder ? query.OrderBy(p => p.Price) : query.OrderByDescending(p => p.Price);
			}
			else if (sortColumn == "category")
			{
				query = sortOrder ? query.OrderBy(p => p.CategoryId) : query.OrderByDescending(p => p.CategoryId);
			}
			else if (sortColumn == "onSale")
			{
				query = sortOrder ? query.OrderBy(p => p.OnSale) : query.OrderByDescending(p => p.OnSale);
			}
			else if (sortColumn == "averageRating")
			{
				query = sortOrder ? query.OrderBy(p => p.AverageRating) : query.OrderByDescending(p => p.AverageRating);
			}

			// Implement pagination logic here using the pageSize and pageNumber parameters
			if (pageSize > 0 && pageNumber > 0)
			{
				query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
			}

			// Execute the query and cache the results
			products = await query.ToListAsync();
			_cache.Set(cacheKey, products, TimeSpan.FromMinutes(5));

			return products;
		}

		public async Task<Product> GetByIdAsync(int id)
		{
			// Build the cache key
			string cacheKey = $"product_{id}";

			// Check if the product is already cached
			if (_cache.TryGetValue(cacheKey, out Product product))
			{
				// Return the cached product
				return product;
			}

			try
			{
				// Query the database for the product
				product = await _db.Products
					.Include(p => p.Category)
					.Include(p => p.Images)
					.Include(p => p.Videos)
					.SingleOrDefaultAsync(p => p.ProductId == id);

				// Validate the product
				if (product == null)
				{
					throw new Exception("Product not found");
				}

				// Set the cache options
				var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(60));

				// Save the product in cache
				_cache.Set(cacheKey, product, cacheEntryOptions);

				return product;
			}
			catch (Exception ex)
			{
				// Log the error
				//_logger.LogError(ex.ToString());

				// Return null to indicate that the product could not be retrieved
				return null;
			}
		}

		public async Task<Product> GetProductByNameAsync(string productName, int cacheDuration, bool tracked = true)
		{
			// Build the cache key
			string cacheKey = $"product_{productName}";

			// Check if the product is already cached
			if (_cache.TryGetValue(cacheKey, out Product product))
			{
				// Return the cached product
				return product;
			}

			// Get the product from the database using Full-Text search
			product = await _db.Products
				.Include(p => p.Category)  // Include the Category navigation property
				.Include(p => p.Images)  // Include the Images navigation property
				.Include(p => p.Videos)  // Include the Videos navigation property
				.Where(p => EF.Functions.ToTsVector("english", p.Name).Matches(EF.Functions.ToTsQuery("english", productName)))
				.FirstOrDefaultAsync();

			if (!tracked)
			{
				_db.Entry(product).State = EntityState.Detached;
			}

			// Cache the product
			_cache.Set(cacheKey, product, TimeSpan.FromHours(cacheDuration));

			return product;
		}

		public async Task<IEnumerable<Product>> GetProductsByOwnerAsync(string ownerId, int pageSize, int pageNumber, string sortColumn, bool sortOrder)
		{
			try
			{
				// Validate the pagination parameters
				if (pageSize <= 0)
				{
					throw new ArgumentOutOfRangeException(nameof(pageSize), "The pageSize parameter must be greater than zero.");
				}

				if (pageNumber <= 0)
				{
					throw new ArgumentOutOfRangeException(nameof(pageNumber), "The pageNumber parameter must be greater than zero.");
				}

				// Build the cache key
				string cacheKey = $"products_by_owner_{ownerId}_{pageSize}_{pageNumber}";

				// Check if the results are already cached
				if (_cache.TryGetValue(cacheKey, out IEnumerable<Product> products))
				{
					// Return the cached results
					return products;
				}

				// Build the query
				IQueryable<Product> query = _db.Products
					.Include(p => p.Category)  // Include the Category navigation property
					.Include(p => p.Images)  // Include the Images navigation property
					.Include(p => p.Videos)  // Include the Videos navigation property
					.AsQueryable();


				// Implement sorting logic here using the sortColumn and sortOrder parameters
				if (sortColumn == "owner")
				{
					query = sortOrder ? query.OrderBy(p => p.OwnerId) : query.OrderByDescending(p => p.OwnerId);
				}
				else if (sortColumn == "name")
				{
					query = sortOrder ? query.OrderBy(p => p.Name) : query.OrderByDescending(p => p.Name);
				}
				else if (sortColumn == "priceRange")
				{
					query = sortOrder ? query.OrderBy(p => p.Price) : query.OrderByDescending(p => p.Price);
				}
				else if (sortColumn == "category")
				{
					query = sortOrder ? query.OrderBy(p => p.CategoryId) : query.OrderByDescending(p => p.CategoryId);
				}
				else if (sortColumn == "onSale")
				{
					query = sortOrder ? query.OrderBy(p => p.OnSale) : query.OrderByDescending(p => p.OnSale);
				}
				else if (sortColumn == "averageRating")
				{
					query = sortOrder ? query.OrderBy(p => p.AverageRating) : query.OrderByDescending(p => p.AverageRating);
				}

				// Execute the query and cache the results
				products = await query.ToListAsync();
				_cache.Set(cacheKey, products, TimeSpan.FromMinutes(5));

				return products;
			}
			catch (Exception ex)
			{
				// Handle the exception
				Console.WriteLine(ex.Message);
				return null;
			}
		}
		
		public async Task<IEnumerable<Product>> GetProductsByAverageRatingAsync(double averageRating, int pageSize = 0, int pageNumber = 1, string sortColumn = "", bool sortOrder = false, bool tracked = true)
		{
			// Build the cache key based on the pagination and sorting parameters
			string cacheKey = $"products_rating_{averageRating}_{pageSize}_{pageNumber}_{sortColumn}_{sortOrder}";

			// Check if the results are already cached
			if (_cache.TryGetValue(cacheKey, out IEnumerable<Product> products))
			{
				// Return the cached results
				return products;
			}

			// Build the query
			IQueryable<Product> query = _db.Products
				.Include(p => p.Category)
				.Include(p => p.Images)
				.Include(p => p.Videos)
				.Where(p => p.AverageRating >= averageRating)  // Filter by averageRating or above
				.AsQueryable();

			// Apply sorting if a valid sortColumn is provided
			if (_validSortColumns.Contains(sortColumn))
			{
				if (sortColumn == "owner")
				{
					query = sortOrder ? query.OrderBy(p => p.OwnerId) : query.OrderByDescending(p => p.OwnerId);
				}
				else if (sortColumn == "name")
				{
					query = sortOrder ? query.OrderBy(p => p.Name) : query.OrderByDescending(p => p.Name);
				}
				else if (sortColumn == "priceRange")
				{
					query = sortOrder ? query.OrderBy(p => p.Price) : query.OrderByDescending(p => p.Price);
				}
				else if (sortColumn == "category")
				{
					query = sortOrder ? query.OrderBy(p => p.CategoryId) : query.OrderByDescending(p => p.CategoryId);
				}
				else if (sortColumn == "onSale")
				{
					query = sortOrder ? query.OrderBy(p => p.OnSale) : query.OrderByDescending(p => p.OnSale);
				}
				else if (sortColumn == "averageRating")
				{
					query = sortOrder ? query.OrderBy(p => p.AverageRating) : query.OrderByDescending(p => p.AverageRating);
				}
			}

			// Apply pagination if pageSize and pageNumber are provided
			if (pageSize > 0 && pageNumber > 0)
			{
				query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
			}

			// Return the query results
			products = await query.ToListAsync();

			// Set the cache options
			var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(60));

			// Save the results in cache
			_cache.Set(cacheKey, products, cacheEntryOptions);

			return products;
		}

		public async Task<IEnumerable<Product>> GetProductsByAvgTimeSpentAsync(int avgTimeSpent, int pageSize = 0, int pageNumber = 1, string sortColumn = "", bool sortOrder = false, bool tracked = true)
		{
			// Build the cache key based on the pagination and sorting parameters
			string cacheKey = $"products_rating_{avgTimeSpent}_{pageSize}_{pageNumber}_{sortColumn}_{sortOrder}";

			// Check if the results are already cached
			if (_cache.TryGetValue(cacheKey, out IEnumerable<Product> products))
			{
				// Return the cached results
				return products;
			}

			// Build the query
			IQueryable<Product> query = _db.Products
				.Include(p => p.Category)
				.Include(p => p.Images)
				.Include(p => p.Videos)
				.Where(p => p.AvgTimeSpent >= avgTimeSpent)  // Filter by averageRating or above
				.AsQueryable();

			// Apply sorting if a valid sortColumn is provided
			if (_validSortColumns.Contains(sortColumn))
			{
				if (sortColumn == "owner")
				{
					query = sortOrder ? query.OrderBy(p => p.OwnerId) : query.OrderByDescending(p => p.OwnerId);
				}
				else if (sortColumn == "name")
				{
					query = sortOrder ? query.OrderBy(p => p.Name) : query.OrderByDescending(p => p.Name);
				}
				else if (sortColumn == "priceRange")
				{
					query = sortOrder ? query.OrderBy(p => p.Price) : query.OrderByDescending(p => p.Price);
				}
				else if (sortColumn == "category")
				{
					query = sortOrder ? query.OrderBy(p => p.CategoryId) : query.OrderByDescending(p => p.CategoryId);
				}
				else if (sortColumn == "onSale")
				{
					query = sortOrder ? query.OrderBy(p => p.OnSale) : query.OrderByDescending(p => p.OnSale);
				}
				else if (sortColumn == "averageRating")
				{
					query = sortOrder ? query.OrderBy(p => p.AverageRating) : query.OrderByDescending(p => p.AverageRating);
				}
			}

			// Apply pagination if pageSize and pageNumber are provided
			if (pageSize > 0 && pageNumber > 0)
			{
				query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
			}

			// Return the query results
			products = await query.ToListAsync();

			// Set the cache options
			var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(60));

			// Save the results in cache
			_cache.Set(cacheKey, products, cacheEntryOptions);

			return products;
		}

		public async Task<IEnumerable<Product>> GetProductsByClickThroughRateAsync(double clickThroughRate, int pageSize = 0, int pageNumber = 1, string sortColumn = "", bool sortOrder = false, bool tracked = true)
		{
			// Build the cache key based on the pagination and sorting parameters
			string cacheKey = $"products_ctr_{clickThroughRate}_{pageSize}_{pageNumber}_{sortColumn}_{sortOrder}";

			// Check if the results are already cached
			if (_cache.TryGetValue(cacheKey, out IEnumerable<Product> products))
			{
				// Return the cached results
				return products;
			}

			// Build the query
			IQueryable<Product> query = _db.Products
				.Include(p => p.Category)
				.Include(p => p.Images)
				.Include(p => p.Videos)
				.Where(p => p.ClickThroughRate >= clickThroughRate)  // Filter by averageRating or above
				.AsQueryable();

			// Apply sorting if a valid sortColumn is provided
			if (_validSortColumns.Contains(sortColumn))
			{
				if (sortColumn == "owner")
				{
					query = sortOrder ? query.OrderBy(p => p.OwnerId) : query.OrderByDescending(p => p.OwnerId);
				}
				else if (sortColumn == "name")
				{
					query = sortOrder ? query.OrderBy(p => p.Name) : query.OrderByDescending(p => p.Name);
				}
				else if (sortColumn == "priceRange")
				{
					query = sortOrder ? query.OrderBy(p => p.Price) : query.OrderByDescending(p => p.Price);
				}
				else if (sortColumn == "category")
				{
					query = sortOrder ? query.OrderBy(p => p.CategoryId) : query.OrderByDescending(p => p.CategoryId);
				}
				else if (sortColumn == "onSale")
				{
					query = sortOrder ? query.OrderBy(p => p.OnSale) : query.OrderByDescending(p => p.OnSale);
				}
				else if (sortColumn == "averageRating")
				{
					query = sortOrder ? query.OrderBy(p => p.AverageRating) : query.OrderByDescending(p => p.AverageRating);
				}
			}

			// Apply pagination if pageSize and pageNumber are provided
			if (pageSize > 0 && pageNumber > 0)
			{
				query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
			}

			// Return the query results
			products = await query.ToListAsync();

			// Set the cache options
			var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(60));

			// Save the results in cache
			_cache.Set(cacheKey, products, cacheEntryOptions);

			return products;
		}

		public async Task<IEnumerable<Product>> GetProductsByConversionRateAsync(double conversionRate, int pageSize = 0, int pageNumber = 1, string sortColumn = "", bool sortOrder = false, bool tracked = true)
		{
			// Build the cache key based on the pagination and sorting parameters
			string cacheKey = $"products_cr_{conversionRate}_{pageSize}_{pageNumber}_{sortColumn}_{sortOrder}";

			// Check if the results are already cached
			if (_cache.TryGetValue(cacheKey, out IEnumerable<Product> products))
			{
				// Return the cached results
				return products;
			}

			// Build the query
			IQueryable<Product> query = _db.Products
				.Include(p => p.Category)
				.Include(p => p.Images)
				.Include(p => p.Videos)
				.Where(p => p.ConversionRate >= conversionRate)  // Filter by averageRating or above
				.AsQueryable();

			// Apply sorting if a valid sortColumn is provided
			if (_validSortColumns.Contains(sortColumn))
			{
				if (sortColumn == "owner")
				{
					query = sortOrder ? query.OrderBy(p => p.OwnerId) : query.OrderByDescending(p => p.OwnerId);
				}
				else if (sortColumn == "name")
				{
					query = sortOrder ? query.OrderBy(p => p.Name) : query.OrderByDescending(p => p.Name);
				}
				else if (sortColumn == "priceRange")
				{
					query = sortOrder ? query.OrderBy(p => p.Price) : query.OrderByDescending(p => p.Price);
				}
				else if (sortColumn == "category")
				{
					query = sortOrder ? query.OrderBy(p => p.CategoryId) : query.OrderByDescending(p => p.CategoryId);
				}
				else if (sortColumn == "onSale")
				{
					query = sortOrder ? query.OrderBy(p => p.OnSale) : query.OrderByDescending(p => p.OnSale);
				}
				else if (sortColumn == "averageRating")
				{
					query = sortOrder ? query.OrderBy(p => p.AverageRating) : query.OrderByDescending(p => p.AverageRating);
				}
			}

			// Apply pagination if pageSize and pageNumber are provided
			if (pageSize > 0 && pageNumber > 0)
			{
				query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
			}

			// Return the query results
			products = await query.ToListAsync();

			// Set the cache options
			var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(60));

			// Save the results in cache
			_cache.Set(cacheKey, products, cacheEntryOptions);

			return products;
		}

		public async Task<IEnumerable<Product>> GetProductsByManufacturerAsync(string manufacturer, int pageSize = 0, int pageNumber = 1, string sortColumn = "", bool sortOrder = false, bool tracked = true)
		{
			// Build the cache key based on the pagination and sorting parameters
			string cacheKey = $"products_manufacturer_{manufacturer}_{pageSize}_{pageNumber}_{sortColumn}_{sortOrder}";

			// Check if the results are already cached
			if (_cache.TryGetValue(cacheKey, out IEnumerable<Product> products))
			{
				// Return the cached results
				return products;
			}

			// Build the query
			IQueryable<Product> query = _db.Products
				.Include(p => p.Category)
				.Include(p => p.Images)
				.Include(p => p.Videos)
				.Where(p => p.Manufacturer == manufacturer)  // Filter by
				.AsQueryable();

			// Apply sorting if a valid sortColumn is provided
			if (_validSortColumns.Contains(sortColumn))
			{
				if (sortColumn == "owner")
				{
					query = sortOrder ? query.OrderBy(p => p.OwnerId) : query.OrderByDescending(p => p.OwnerId);
				}
				else if (sortColumn == "name")
				{
					query = sortOrder ? query.OrderBy(p => p.Name) : query.OrderByDescending(p => p.Name);
				}
				else if (sortColumn == "priceRange")
				{
					query = sortOrder ? query.OrderBy(p => p.Price) : query.OrderByDescending(p => p.Price);
				}
				else if (sortColumn == "category")
				{
					query = sortOrder ? query.OrderBy(p => p.CategoryId) : query.OrderByDescending(p => p.CategoryId);
				}
				else if (sortColumn == "onSale")
				{
					query = sortOrder ? query.OrderBy(p => p.OnSale) : query.OrderByDescending(p => p.OnSale);
				}
				else if (sortColumn == "averageRating")
				{
					query = sortOrder ? query.OrderBy(p => p.AverageRating) : query.OrderByDescending(p => p.AverageRating);
				}
			}

			// Apply pagination if pageSize and pageNumber are provided
			if (pageSize > 0 && pageNumber > 0)
			{
				query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
			}

			// Return the query results
			products = await query.ToListAsync();

			// Set the cache options
			var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(60));

			// Save the results in cache
			_cache.Set(cacheKey, products, cacheEntryOptions);

			return products;
		}

		public async Task<IEnumerable<Product>> GetProductsByNumLikesAsync(int numLikes, int pageSize = 0, int pageNumber = 1, string sortColumn = "", bool sortOrder = false, bool tracked = true)
		{
			// Build the cache key based on the pagination and sorting parameters
			string cacheKey = $"products_numlikes_{numLikes}_{pageSize}_{pageNumber}_{sortColumn}_{sortOrder}";

			// Check if the results are already cached
			if (_cache.TryGetValue(cacheKey, out IEnumerable<Product> products))
			{
				// Return the cached results
				return products;
			}

			// Build the query
			IQueryable<Product> query = _db.Products
				.Include(p => p.Category)
				.Include(p => p.Images)
				.Include(p => p.Videos)
				.Where(p => p.NumLikes >= numLikes)  // Filter by
				.AsQueryable();

			// Apply sorting if a valid sortColumn is provided
			if (_validSortColumns.Contains(sortColumn))
			{
				if (sortColumn == "owner")
				{
					query = sortOrder ? query.OrderBy(p => p.OwnerId) : query.OrderByDescending(p => p.OwnerId);
				}
				else if (sortColumn == "name")
				{
					query = sortOrder ? query.OrderBy(p => p.Name) : query.OrderByDescending(p => p.Name);
				}
				else if (sortColumn == "priceRange")
				{
					query = sortOrder ? query.OrderBy(p => p.Price) : query.OrderByDescending(p => p.Price);
				}
				else if (sortColumn == "category")
				{
					query = sortOrder ? query.OrderBy(p => p.CategoryId) : query.OrderByDescending(p => p.CategoryId);
				}
				else if (sortColumn == "onSale")
				{
					query = sortOrder ? query.OrderBy(p => p.OnSale) : query.OrderByDescending(p => p.OnSale);
				}
				else if (sortColumn == "averageRating")
				{
					query = sortOrder ? query.OrderBy(p => p.AverageRating) : query.OrderByDescending(p => p.AverageRating);
				}
			}

			// Apply pagination if pageSize and pageNumber are provided
			if (pageSize > 0 && pageNumber > 0)
			{
				query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
			}

			// Return the query results
			products = await query.ToListAsync();

			// Set the cache options
			var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(60));

			// Save the results in cache
			_cache.Set(cacheKey, products, cacheEntryOptions);

			return products;
		}

		public async Task<IEnumerable<Product>> GetProductsByNumPurchasesAsync(int numPurchases, int pageSize = 0, int pageNumber = 1, string sortColumn = "", bool sortOrder = false, bool tracked = true)
		{
			// Build the cache key based on the pagination and sorting parameters
			string cacheKey = $"products_numPurchases_{numPurchases}_{pageSize}_{pageNumber}_{sortColumn}_{sortOrder}";

			// Check if the results are already cached
			if (_cache.TryGetValue(cacheKey, out IEnumerable<Product> products))
			{
				// Return the cached results
				return products;
			}

			// Build the query
			IQueryable<Product> query = _db.Products
				.Include(p => p.Category)
				.Include(p => p.Images)
				.Include(p => p.Videos)
				.Where(p => p.NumPurchases >= numPurchases)  // Filter by
				.AsQueryable();

			// Apply sorting if a valid sortColumn is provided
			if (_validSortColumns.Contains(sortColumn))
			{
				if (sortColumn == "owner")
				{
					query = sortOrder ? query.OrderBy(p => p.OwnerId) : query.OrderByDescending(p => p.OwnerId);
				}
				else if (sortColumn == "name")
				{
					query = sortOrder ? query.OrderBy(p => p.Name) : query.OrderByDescending(p => p.Name);
				}
				else if (sortColumn == "priceRange")
				{
					query = sortOrder ? query.OrderBy(p => p.Price) : query.OrderByDescending(p => p.Price);
				}
				else if (sortColumn == "category")
				{
					query = sortOrder ? query.OrderBy(p => p.CategoryId) : query.OrderByDescending(p => p.CategoryId);
				}
				else if (sortColumn == "onSale")
				{
					query = sortOrder ? query.OrderBy(p => p.OnSale) : query.OrderByDescending(p => p.OnSale);
				}
				else if (sortColumn == "averageRating")
				{
					query = sortOrder ? query.OrderBy(p => p.AverageRating) : query.OrderByDescending(p => p.AverageRating);
				}
			}

			// Apply pagination if pageSize and pageNumber are provided
			if (pageSize > 0 && pageNumber > 0)
			{
				query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
			}

			// Return the query results
			products = await query.ToListAsync();

			// Set the cache options
			var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(60));

			// Save the results in cache
			_cache.Set(cacheKey, products, cacheEntryOptions);

			return products;
		}

		public async Task<IEnumerable<Product>> GetProductsByNumViewsAsync(int numViews, int pageSize = 0, int pageNumber = 1, string sortColumn = "", bool sortOrder = false, bool tracked = true)
		{
			// Build the cache key based on the pagination and sorting parameters
			string cacheKey = $"products_numViews_{numViews}_{pageSize}_{pageNumber}_{sortColumn}_{sortOrder}";

			// Check if the results are already cached
			if (_cache.TryGetValue(cacheKey, out IEnumerable<Product> products))
			{
				// Return the cached results
				return products;
			}

			// Build the query
			IQueryable<Product> query = _db.Products
				.Include(p => p.Category)
				.Include(p => p.Images)
				.Include(p => p.Videos)
				.Where(p => p.NumViews >= numViews)  // Filter by
				.AsQueryable();

			// Apply sorting if a valid sortColumn is provided
			if (_validSortColumns.Contains(sortColumn))
			{
				if (sortColumn == "owner")
				{
					query = sortOrder ? query.OrderBy(p => p.OwnerId) : query.OrderByDescending(p => p.OwnerId);
				}
				else if (sortColumn == "name")
				{
					query = sortOrder ? query.OrderBy(p => p.Name) : query.OrderByDescending(p => p.Name);
				}
				else if (sortColumn == "priceRange")
				{
					query = sortOrder ? query.OrderBy(p => p.Price) : query.OrderByDescending(p => p.Price);
				}
				else if (sortColumn == "category")
				{
					query = sortOrder ? query.OrderBy(p => p.CategoryId) : query.OrderByDescending(p => p.CategoryId);
				}
				else if (sortColumn == "onSale")
				{
					query = sortOrder ? query.OrderBy(p => p.OnSale) : query.OrderByDescending(p => p.OnSale);
				}
				else if (sortColumn == "averageRating")
				{
					query = sortOrder ? query.OrderBy(p => p.AverageRating) : query.OrderByDescending(p => p.AverageRating);
				}
			}

			// Apply pagination if pageSize and pageNumber are provided
			if (pageSize > 0 && pageNumber > 0)
			{
				query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
			}

			// Return the query results
			products = await query.ToListAsync();

			// Set the cache options
			var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(60));

			// Save the results in cache
			_cache.Set(cacheKey, products, cacheEntryOptions);

			return products;
		}

		public async Task<IEnumerable<Product>> GetProductsByOnSaleAsync(bool onSale, int pageSize = 0, int pageNumber = 1, string sortColumn = "", bool sortOrder = false, bool tracked = true)
		{
			// Build the cache key based on the pagination and sorting parameters
			string cacheKey = $"products_onSale_{onSale}_{pageSize}_{pageNumber}_{sortColumn}_{sortOrder}";

			// Check if the results are already cached
			if (_cache.TryGetValue(cacheKey, out IEnumerable<Product> products))
			{
				// Return the cached results
				return products;
			}

			// Build the query
			IQueryable<Product> query = _db.Products
				.Include(p => p.Category)
				.Include(p => p.Images)
				.Include(p => p.Videos)
				.Where(p => p.OnSale == onSale)  // Filter by
				.AsQueryable();

			// Apply sorting if a valid sortColumn is provided
			if (_validSortColumns.Contains(sortColumn))
			{
				if (sortColumn == "owner")
				{
					query = sortOrder ? query.OrderBy(p => p.OwnerId) : query.OrderByDescending(p => p.OwnerId);
				}
				else if (sortColumn == "name")
				{
					query = sortOrder ? query.OrderBy(p => p.Name) : query.OrderByDescending(p => p.Name);
				}
				else if (sortColumn == "priceRange")
				{
					query = sortOrder ? query.OrderBy(p => p.Price) : query.OrderByDescending(p => p.Price);
				}
				else if (sortColumn == "category")
				{
					query = sortOrder ? query.OrderBy(p => p.CategoryId) : query.OrderByDescending(p => p.CategoryId);
				}
				else if (sortColumn == "onSale")
				{
					query = sortOrder ? query.OrderBy(p => p.OnSale) : query.OrderByDescending(p => p.OnSale);
				}
				else if (sortColumn == "averageRating")
				{
					query = sortOrder ? query.OrderBy(p => p.AverageRating) : query.OrderByDescending(p => p.AverageRating);
				}
			}

			// Apply pagination if pageSize and pageNumber are provided
			if (pageSize > 0 && pageNumber > 0)
			{
				query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
			}

			// Return the query results
			products = await query.ToListAsync();

			// Set the cache options
			var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(60));

			// Save the results in cache
			_cache.Set(cacheKey, products, cacheEntryOptions);

			return products;
		}

		public async Task<IEnumerable<Product>> GetProductsByTotalRevenueAsync(double totalRevenue, int pageSize = 0, int pageNumber = 1, string sortColumn = "", bool sortOrder = false, bool tracked = true)
		{
			// Build the cache key based on the pagination and sorting parameters
			string cacheKey = $"products_totalRevenue_{totalRevenue}_{pageSize}_{pageNumber}_{sortColumn}_{sortOrder}";

			// Check if the results are already cached
			if (_cache.TryGetValue(cacheKey, out IEnumerable<Product> products))
			{
				// Return the cached results
				return products;
			}

			// Build the query
			IQueryable<Product> query = _db.Products
				.Include(p => p.Category)
				.Include(p => p.Images)
				.Include(p => p.Videos)
				.Where(p => p.TotalRevenue >= totalRevenue)  // Filter by
				.AsQueryable();

			// Apply sorting if a valid sortColumn is provided
			if (_validSortColumns.Contains(sortColumn))
			{
				if (sortColumn == "owner")
				{
					query = sortOrder ? query.OrderBy(p => p.OwnerId) : query.OrderByDescending(p => p.OwnerId);
				}
				else if (sortColumn == "name")
				{
					query = sortOrder ? query.OrderBy(p => p.Name) : query.OrderByDescending(p => p.Name);
				}
				else if (sortColumn == "priceRange")
				{
					query = sortOrder ? query.OrderBy(p => p.Price) : query.OrderByDescending(p => p.Price);
				}
				else if (sortColumn == "category")
				{
					query = sortOrder ? query.OrderBy(p => p.CategoryId) : query.OrderByDescending(p => p.CategoryId);
				}
				else if (sortColumn == "onSale")
				{
					query = sortOrder ? query.OrderBy(p => p.OnSale) : query.OrderByDescending(p => p.OnSale);
				}
				else if (sortColumn == "averageRating")
				{
					query = sortOrder ? query.OrderBy(p => p.AverageRating) : query.OrderByDescending(p => p.AverageRating);
				}
			}

			// Apply pagination if pageSize and pageNumber are provided
			if (pageSize > 0 && pageNumber > 0)
			{
				query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
			}

			// Return the query results
			products = await query.ToListAsync();

			// Set the cache options
			var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(60));

			// Save the results in cache
			_cache.Set(cacheKey, products, cacheEntryOptions);

			return products;
		}

		public async Task<IEnumerable<Product>> GetProductsForCategoryAsync(int categoryId, int pageSize, int pageNumber, string sortColumn, bool sortOrder, bool tracked = true)
		{
			// Validate the pagination parameters
			if (pageSize <= 0)
			{
				throw new ArgumentOutOfRangeException(nameof(pageSize), "The pageSize parameter must be greater than zero.");
			}

			if (pageNumber <= 0)
			{
				throw new ArgumentOutOfRangeException(nameof(pageNumber), "The pageNumber parameter must be greater than zero.");
			}

			// Validate the sort column parameter
			if (string.IsNullOrEmpty(sortColumn))
			{
				throw new ArgumentNullException(nameof(sortColumn), "The sortColumn parameter cannot be null or empty.");
			}
			if (!_validSortColumns.Contains(sortColumn))
			{
				throw new ArgumentException(nameof(sortColumn), "The sortColumn parameter is not a valid value.");
			}

			// Build the cache key based on the pagination and sorting parameters
			string cacheKey = $"products_for_category_{categoryId}_{pageSize}_{pageNumber}_{sortColumn}_{sortOrder}";

			// Check if the results are already cached
			if (_cache.TryGetValue(cacheKey, out IEnumerable<Product> products))
			{
				// Return the cached results
				return products;
			}

			// Build the query
			IQueryable<Product> query = _db.Products
				.Include(p => p.Category)  // Include the Category navigation property
				.Include(p => p.Images)  // Include the Images navigation property
				.Include(p => p.Videos)  // Include the Videos navigation property
				.Where(p => p.CategoryId == categoryId)
				.AsQueryable();

			// Implement sorting logic here using the sortColumn and sortOrder parameters
			if (sortColumn == "owner")
			{
				query = sortOrder ? query.OrderBy(p => p.OwnerId) : query.OrderByDescending(p => p.OwnerId);
			}
			else if (sortColumn == "name")
			{
				query = sortOrder ? query.OrderBy(p => p.Name) : query.OrderByDescending(p => p.Name);
			}
			else if (sortColumn == "priceRange")
			{
				query = sortOrder ? query.OrderBy(p => p.Price) : query.OrderByDescending(p => p.Price);
			}
			else if (sortColumn == "category")
			{
				query = sortOrder ? query.OrderBy(p => p.CategoryId) : query.OrderByDescending(p => p.CategoryId);
			}
			else if (sortColumn == "onSale")
			{
				query = sortOrder ? query.OrderBy(p => p.OnSale) : query.OrderByDescending(p => p.OnSale);
			}
			else if (sortColumn == "averageRating")
			{
				query = sortOrder ? query.OrderBy(p => p.AverageRating) : query.OrderByDescending(p => p.AverageRating);
			}

			// Implement pagination logic here using the pageSize and pageNumber parameters
			if (pageSize > 0 && pageNumber > 0)
			{
				query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
			}

			// Execute the query and cache the results
			products = await query.ToListAsync();
			_cache.Set(cacheKey, products, TimeSpan.FromMinutes(15));

			return products;

		}

		public async Task<IEnumerable<Product>> SearchProductsAsync(string searchQuery, Pagination pagination, bool tracked = true)
		{
			var query = _db.Products
				.Include(p => p.Category)  // Include the Category navigation property
				.Include(p => p.Images)  // Include the Images navigation property
				.Include(p => p.Videos)  // Include the Videos navigation property
				.Where(p => EF.Functions.ILike(p.Name, $"%{searchQuery}%") || EF.Functions.ILike(p.Description, $"%{searchQuery}%"))
				.AsQueryable();

			// Build the cache key based on the pagination and search query parameters
			string cacheKey = $"search_{searchQuery}_{pagination.PageSize}_{pagination.PageNumber}";

			// Check if the results are already cached
			if (_cache.TryGetValue(cacheKey, out IEnumerable<Product> products))
			{
				// Return the cached results
				return products;
			}

			// Implement pagination logic here using the PageSize and PageNumber properties of the pagination object
			if (pagination.PageSize > 0 && pagination.PageNumber > 0)
			{
				query = query.Skip((pagination.PageNumber - 1) * pagination.PageSize).Take(pagination.PageSize);
			}

			// Execute the query and cache the results
			products = await query.ToListAsync();
			_cache.Set(cacheKey, products, TimeSpan.FromMinutes(5));

			return products;
		}


		public async Task<string> GetOwnerIdAsync(int productId)
		{
			// Retrieve the product with the specified id
			var product = await dbSet.FindAsync(productId);
			if (product == null)
			{
				throw new InvalidOperationException($"Product with id {productId} not found");
			}

			// Check if the product has a property named "OwnerId"
			var property = product.GetType().GetProperty("OwnerId");
			if (property == null)
			{
				throw new InvalidOperationException("Product does not have a property named 'OwnerId'");
			}

			// Use the memory cache to store the "OwnerId" value for the product
			string cacheKey = $"{typeof(Product).Name}-{productId}-OwnerId";
			string ownerId = _cache.Get<string>(cacheKey);
			if (string.IsNullOrEmpty(ownerId))
			{
				ownerId = (string)property.GetValue(product);
				if (string.IsNullOrEmpty(ownerId))
				{
					throw new InvalidOperationException("OwnerId is null or empty");
				}

				// Store the "OwnerId" value in the cache for 1 hour
				_cache.Set(cacheKey, ownerId, TimeSpan.FromHours(1));
			}

			return ownerId;
		}

		public async Task CreateProductAsync(Product product)
		{
			try
			{
				// Set the video and image IDs
				int maxVideoId = _db.Videos.Max(v => v.VideoId);
				int maxImageId = _db.Images.Max(i => i.ImageId);

				if (product.Videos == null)
				{
					product.Videos = new List<Video>();
				}
				if (product.Images == null)
				{
					product.Images = new List<Image>();
				}
				if (product.Videos != null)
				{
					foreach (var video in product.Videos)
					{
						video.VideoId = maxVideoId == 0 ? 1 : ++maxVideoId; // Set the VideoId to 1 if the database is empty, otherwise increment the maxVideoId by 1
						video.ProductId = product.ProductId;
						video.OwnerId = product.OwnerId; // Add the ownerId to the video
					}
				}
				if (product.Images != null)
				{
					foreach (var image in product.Images)
					{
						image.ImageId = maxImageId == 0 ? 1 : ++maxImageId; // Set the ImageId to 1 if the database is empty, otherwise increment the maxImageId by 1
						image.ProductId = product.ProductId;
						image.OwnerId = product.OwnerId; // Add the ownerId to the image
					}
				}

				// Get the highest ProductId from the database
				int maxProductId = _db.Products.Max(p => p.ProductId);

				// Set the ProductId of the product based on the highest ProductId
				product.ProductId = maxProductId + 1;

				// Add the product, videos, and images to the database and save changes
				await _db.Products.AddAsync(product);
				if (product.Videos != null)
				{
					await _db.Videos.AddRangeAsync(product.Videos);
				}
				if (product.Images != null)
				{
					await _db.Images.AddRangeAsync(product.Images);
				}
				await _db.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				// Handle the exception
				Console.WriteLine(ex.Message);
			}
		}

		public async Task<Product> GetProductByIdAsync(int id, bool tracked = true)
		{
			// Get the product from the database
			var product = await _db.Products
				.Include(p => p.Category)  // Include the Category navigation property
				.Include(p => p.Images)  // Include the Images navigation property
				.Include(p => p.Videos)  // Include the Videos navigation property
				.Where(p => p.ProductId == id)
				.FirstOrDefaultAsync();

			if (!tracked)
			{
				_db.Entry(product).State = EntityState.Detached;
			}

			return product;
		}

		public async Task<bool> RemoveProductAsync(Product product)
		{
			try
			{
				// Check if the product exists in the database
				if (!_db.Products.Contains(product))
				{
					throw new Exception("Product does not exist in the database.");
				}

				// Check if the product has any associated videos
				if (product.Videos != null && product.Videos.Count > 0)
				{
					// Remove the associated videos from the database
					_db.Videos.RemoveRange(product.Videos);
					await _db.SaveChangesAsync();
				}

				// Check if the product has any associated images
				if (product.Images != null && product.Images.Count > 0)
				{
					// Remove the associated images from the database
					_db.Images.RemoveRange(product.Images);
					await _db.SaveChangesAsync();
				}

				// Remove the product and its associated images and videos from the database
				_db.Products.Remove(product);
				await _db.SaveChangesAsync();

				// Invalidate the cache for the GetAllAsync method
				_cache.Remove("products_*");  // remove all cached results for the GetAllAsync method

				return true;
			}
			catch (Exception ex)
			{
				// Handle the exception
				Console.WriteLine(ex.Message);
				return false;
			}
		}

		

	}
}
