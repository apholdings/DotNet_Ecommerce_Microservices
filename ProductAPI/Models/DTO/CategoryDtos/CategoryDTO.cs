using ProductAPI.Models.DTO.ProductDtos;

namespace ProductAPI.Models.DTO.CategoryDtos
{
	public class CategoryDTO
	{
		public int CategoryId { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }
		public int? ParentCategoryId { get; set; }
	}
}
