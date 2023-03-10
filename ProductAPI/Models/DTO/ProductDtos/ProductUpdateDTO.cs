using ProductAPI.Models.DTO.ImageDtos;
using ProductAPI.Models.DTO.VideoDtos;

namespace ProductAPI.Models.DTO.ProductDtos
{
	public class ProductUpdateDTO
	{
		public int ProductId { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public decimal Price { get; set; }
		public decimal DiscountPrice { get; set; }
		public int CategoryId { get; set; }
		public int Quantity { get; set; }
		public string Manufacturer { get; set; }
		public bool OnSale { get; set; }
	}
}
