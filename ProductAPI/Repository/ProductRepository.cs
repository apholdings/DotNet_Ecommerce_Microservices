using Microsoft.EntityFrameworkCore;
using ProductAPI.Data;
using ProductAPI.Models;
using ProductAPI.Repository.IRepository;

namespace ProductAPI.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
	{

		private readonly ApplicationDbContext _db;
		public ProductRepository(ApplicationDbContext db) : base(db)
		{
			_db = db;
		}

		public async Task<int> CountAsync()
		{
			return await _db.Products.CountAsync();
		}


		private readonly IList<string> _validSortColumns = new List<string> { "Owner", "Name", "Price", "Category", "OnSale", "AverageRating" };
		public async Task<IEnumerable<Product>> GetAllAsync(int pageSize, int pageNumber, string sortColumn, bool sortOrder)
		{
			if (pageNumber <= 0)
			{
				throw new ArgumentOutOfRangeException(nameof(pageNumber), "The pageNumber parameter must be greater than zero.");
			}

			// Validate the sortColumn parameter
			if (string.IsNullOrEmpty(sortColumn))
			{
				throw new ArgumentException("The sortColumn parameter is required.");
			}

			// Validate that the sortColumn value is valid
			if (!_validSortColumns.Contains(sortColumn))
			{
				throw new ArgumentException($"The sortColumn value '{sortColumn}' is not valid. Valid values are: {string.Join(", ", _validSortColumns)}.");
			}

			// Build the query
			IQueryable<Product> query = _db.Products
				.Include(p => p.Category)  // Include the Category navigation property
				.Include(p => p.Images)  // Include the Images navigation property
				.Include(p => p.Videos)  // Include the Videos navigation property
				.AsQueryable();

			// Validate that the sortColumn value is a valid property of the Product class
			var prop = typeof(Product).GetProperty(sortColumn);
			if (prop == null)
			{
				throw new ArgumentException($"The sortColumn value '{sortColumn}' is not a valid property of the Product class.");
			}

			// Implement sorting logic here using the sortColumn and sortOrder parameters
			query = sortOrder ? query.OrderBy(p => prop.GetValue(p)) : query.OrderByDescending(p => prop.GetValue(p));

			// Implement pagination logic here using the pageSize and pageNumber parameters
			if (pageSize > 0 && pageNumber > 0)
			{
				query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
			}

			return await query.ToListAsync();
		}

		public async Task<Product> GetProductByNameAsync(string productName, bool tracked = true)
		{
			var product = await _db.Products
				.Where(p => p.Name == productName)
				.FirstOrDefaultAsync();

			if (!tracked)
			{
				_db.Entry(product).State = EntityState.Detached;
			}

			return product;
		}

		public async Task<IEnumerable<Product>> GetProductsForCategoryAsync(int categoryId, bool tracked = true)
		{
			var query = _db.Products.Where(p => p.CategoryId == categoryId);
			if (tracked)
			{
				query = query.AsTracking();
			}
			return await query.ToListAsync();
		}

		public async Task<IEnumerable<Product>> SearchProductsAsync(string searchQuery, bool tracked = true)
		{
			var query = _db.Products.Where(p => p.Name.Contains(searchQuery) || p.Description.Contains(searchQuery));
			if (tracked)
			{
				query = query.AsTracking();
			}
			return await query.ToListAsync();
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
