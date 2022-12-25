using ProductAPI.Models.DTO.ProductDtos;

namespace ProductAPI.Models.DTO.CategoryDtos
{
	public class CategoryUpdateDTO
	{
		public int CategoryId { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public int? ParentCategoryId { get; set; }
	}
}
