using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProductAPI.Models
{
	public class Image
	{
		public int ImageId { get; set; }
		// Add an OwnerId property to the Image model
		public string OwnerId { get; set; }

		// Use the [ForeignKey] attribute to explicitly specify the foreign key property
		[ForeignKey("Product")]
		public int ProductId { get; set; }

		public Product Product { get; set; }

		// Use the [Required] attribute to specify that the URL is required
		[Required]
		public string Url { get; set; }
		[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
		public DateTime CreatedAt { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
		public DateTime UpdatedAt { get; set; }
	}
}