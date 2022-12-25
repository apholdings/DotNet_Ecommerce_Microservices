using ProductAPI.Models.DTO.ProductDtos;
using System.ComponentModel.DataAnnotations;

namespace ProductAPI.Models.DTO.CategoryDtos
{
	public class CategoryCreateDTO
	{
		[Required]
		[StringLength(255)]
		public string Name { get; set; }

		[StringLength(2000)]
		public string Description { get; set; }

		public int? ParentCategoryId { get; set; }
	}
}
