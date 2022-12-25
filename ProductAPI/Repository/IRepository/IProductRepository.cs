using ProductAPI.Models;

namespace ProductAPI.Repository.IRepository
{
	public interface IProductRepository : IRepository<Product>
	{
		Task<IEnumerable<Product>> GetProductsForCategoryAsync(int categoryId, bool tracked = true);
		Task<int> CountAsync();
		Task<IEnumerable<Product>> GetAllAsync(int pageSize = 0, int pageNumber = 1, string sortColumn = "", bool sortOrder = false);
		Task<Product> GetProductByNameAsync(string productName, bool tracked = true);
		Task<IEnumerable<Product>> SearchProductsAsync(string searchQuery, bool tracked = true);
		Task<Product> UpdateAsync(Product entity);
	}
}
