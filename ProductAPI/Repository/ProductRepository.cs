using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using ProductAPI.Data;
using ProductAPI.Models;
using ProductAPI.Repository.IRepository;

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

		public async Task<Product> UpdateAsync(Product entity)
		{
			entity.UpdatedAt = DateTime.Now;
			_db.Products.Update(entity);
			await _db.SaveChangesAsync();
			return entity;
		}
	}
}
