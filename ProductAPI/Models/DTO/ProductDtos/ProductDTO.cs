using ProductAPI.Models.DTO.CategoryDtos;
using ProductAPI.Models.DTO.ImageDtos;
using ProductAPI.Models.DTO.VideoDtos;

namespace ProductAPI.Models.DTO.ProductDtos
{
	public class ProductDTO
	{
		public int ProductId { get; set; }
		public string OwnerId { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public decimal Price { get; set; }
		public CategoryDTO Category { get; set; }  // Add this property
		public int Quantity { get; set; }
		// Add properties for the list of Images and Videos
		public IEnumerable<ImageDTO> Images { get; set; }
		public IEnumerable<VideoDTO> Videos { get; set; }
		public double AverageRating { get; set; }
		public int NumPurchases { get; set; }
		public int NumViews { get; set; }
		public int NumLikes { get; set; }
		public int AvgTimeSpent { get; set; }
		public double ClickThroughRate { get; set; }
		public double ConversionRate { get; set; }
		public double TotalRevenue { get; set; }
		public int NumReturns { get; set; }
		public int NumRefunds { get; set; }
		public string Manufacturer { get; set; }
		public bool OnSale { get; set; }
	}
}
