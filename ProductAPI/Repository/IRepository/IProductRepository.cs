using ProductAPI.Models;

namespace ProductAPI.Repository.IRepository
{
	public interface IProductRepository : IRepository<Product>
	{
		Task<IEnumerable<Product>> GetProductsForCategoryAsync(int categoryId, int pageSize = 0, int pageNumber = 1, string sortColumn = "", bool sortOrder = false, bool tracked = true);
		Task<int> CountAsync();
		Task<IEnumerable<Product>> GetAllAsync(int pageSize = 0, int pageNumber = 1, string sortColumn = "", bool sortOrder = false);
		Task<Product> GetProductByNameAsync(string productName,int cacheDuration, bool tracked = true);
		Task<IEnumerable<Product>> SearchProductsAsync(string searchQuery, Pagination pagination, bool tracked = true);  // Add the pagination parameter here
		Task<Product> UpdateAsync(Product entity);
	}
}
