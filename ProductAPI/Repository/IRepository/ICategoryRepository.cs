using ProductAPI.Models;

namespace ProductAPI.Repository.IRepository
{
    public interface ICategoryRepository : IRepository<Category>
	{
		Task<Category> GetCategoryByNameAsync(string categoryName, bool tracked = true);
		Task<IEnumerable<Category>> GetChildCategoriesAsync(int parentCategoryId, bool tracked = true);
		Task<IEnumerable<Category>> GetRootCategoriesAsync(bool tracked = true);
		Task<IEnumerable<Category>> GetCategoriesForProductAsync(int productId, bool tracked = true);
		Task<Category> UpdateAsync(Category entity);
	}
}
