using Microsoft.EntityFrameworkCore;
using ProductAPI.Models;
using ProductAPI.Models.DTO.ProductDtos;

namespace ProductAPI.Repository.IRepository
{
	public interface IProductRepository : IRepository<Product>
	{
		Task<IEnumerable<Product>> GetProductsForCategoryAsync(int categoryId, int pageSize = 0, int pageNumber = 1, string sortColumn = "", bool sortOrder = false, bool tracked = true);
		Task<int> CountAsync();
		Task<APIResponse> CreateProductAsync(ProductCreateDTO productDTO);
		Task<Product> UpdateAsync(Product entity, string ownerId);
		Task<Product> PatchAsync(Product entity, string ownerId);

		Task<IEnumerable<Product>> GetAllAsync(int pageSize = 0, int pageNumber = 1, string sortColumn = "", bool sortOrder = false);
		Task<Product> GetProductBySlugAsync(string slug, int cacheDuration, bool tracked = true);
		Task<Product> GetProductByIdAsync(int id, bool tracked = true);
		Task<IEnumerable<Product>> SearchProductsAsync(string searchQuery, Pagination pagination, bool tracked = true);  // Add the pagination parameter here

		Task<Product> GetByIdAsync(int id);
		Task<IEnumerable<Product>> GetProductsByOwnerAsync(string ownerId, int pageSize = 0, int pageNumber = 1, string sortColumn = "", bool sortOrder = false);
		Task<string> GetOwnerIdAsync(int id);
		Task<bool> RemoveProductAsync(Product product);

		// Query products by AverageRating
		Task<IEnumerable<Product>> GetProductsByAverageRatingAsync(double averageRating, int pageSize = 0, int pageNumber = 1, string sortColumn = "", bool sortOrder = false, bool tracked = true);

		// Query products by NumPurchases
		Task<IEnumerable<Product>> GetProductsByNumPurchasesAsync(int numPurchases, int pageSize = 0, int pageNumber = 1, string sortColumn = "", bool sortOrder = false, bool tracked = true);

		// Query products by NumViews
		Task<IEnumerable<Product>> GetProductsByNumViewsAsync(int numViews, int pageSize = 0, int pageNumber = 1, string sortColumn = "", bool sortOrder = false, bool tracked = true);

		// Query products by NumLikes
		Task<IEnumerable<Product>> GetProductsByNumLikesAsync(int numLikes, int pageSize = 0, int pageNumber = 1, string sortColumn = "", bool sortOrder = false, bool tracked = true);

		// Query products by AvgTimeSpent
		Task<IEnumerable<Product>> GetProductsByAvgTimeSpentAsync(int avgTimeSpent, int pageSize = 0, int pageNumber = 1, string sortColumn = "", bool sortOrder = false, bool tracked = true);

		// Query products by ClickThroughRate
		Task<IEnumerable<Product>> GetProductsByClickThroughRateAsync(double clickThroughRate, int pageSize = 0, int pageNumber = 1, string sortColumn = "", bool sortOrder = false, bool tracked = true);

		// Query products by ConversionRate
		Task<IEnumerable<Product>> GetProductsByConversionRateAsync(double conversionRate, int pageSize = 0, int pageNumber = 1, string sortColumn = "", bool sortOrder = false, bool tracked = true);

		// Query products by TotalRevenue
		Task<IEnumerable<Product>> GetProductsByTotalRevenueAsync(double totalRevenue, int pageSize = 0, int pageNumber = 1, string sortColumn = "", bool sortOrder = false, bool tracked = true);

		// Query products by Manufacturer
		Task<IEnumerable<Product>> GetProductsByManufacturerAsync(string manufacturer, int pageSize = 0, int pageNumber = 1, string sortColumn = "", bool sortOrder = false, bool tracked = true);
		// Query products by OnSale
		Task<IEnumerable<Product>> GetProductsByOnSaleAsync(bool onSale, int pageSize = 0, int pageNumber = 1, string sortColumn = "", bool sortOrder = false, bool tracked = true);
	}
}
