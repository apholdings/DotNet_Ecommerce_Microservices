using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductAPI.Models
{
	public class Category
	{
		public int CategoryId { get; set; }

		[Required]
		[StringLength(255)]
		public string Name { get; set; }

		[StringLength(2000)]
		public string Description { get; set; }

		public ICollection<Product> Products { get; set; }
		[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
		public DateTime CreatedAt { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
		public DateTime UpdatedAt { get; set; }

		// Add a foreign key property to reference the parent category
		public int? ParentCategoryId { get; set; }

		// Add a navigation property to access the parent category
		public Category ParentCategory { get; set; }

		// Add a navigation property to access the child categories
		public ICollection<Category> ChildCategories { get; set; }
	}
}
