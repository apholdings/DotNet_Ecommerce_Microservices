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

		public async Task<IEnumerable<Product>> GetAllAsync(int pageSize = 0, int pageNumber = 1, string sortColumn = "", bool sortOrder = false)
		{

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
