using ProductAPI.Models.DTO.ImageDtos;
using ProductAPI.Models.DTO.VideoDtos;

namespace ProductAPI.Models.DTO.ProductDtos
{
	public class ProductCreateDTO
	{
		public string OwnerId { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public decimal Price { get; set; }
		public int CategoryId { get; set; }
		public int Quantity { get; set; }
		public ICollection<VideoCreateDTO> Videos { get; set; }
		public ICollection<ImageCreateDTO> Images { get; set; }
	}
}
