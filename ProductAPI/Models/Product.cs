using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace ProductAPI.Models
{
	public class Product
	{
		public int ProductId { get; set; }
		public string OwnerId { get; set; }
		[Required]
		[StringLength(255)]
		public string Name { get; set; }
		[RegularExpression(@"^[a-z0-9-]+$", ErrorMessage = "Slug must only contain lowercase alphanumeric characters and hyphens.")]
		[StringLength(255, ErrorMessage = "Slug must be at most 255 characters long.")]
		[Required(ErrorMessage = "Slug is required.")]
		public string Slug { get; set; }
		public void GenerateSlug()
		{
			// Replace spaces with hyphens and convert to lowercase
			Slug = Name.Replace(" ", "-").ToLowerInvariant();

			// Remove any invalid characters
			Slug = Regex.Replace(Slug, @"[^a-z0-9-]", "");
		}

		[StringLength(2000)]
		public string Description { get; set; }
		[Required]
		[Range(0, double.MaxValue)]
		public decimal Price { get; set; }
		[Range(0, double.MaxValue)]
		public decimal DiscountPrice { get; set; }
		[Required]
		[ForeignKey("Category")]
		public int CategoryId { get; set; }
		public Category Category { get; set; }
		public ICollection<Category> Categories { get; set; }
		[Required]
		[Range(0, int.MaxValue)]
		public int Quantity { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
		public DateTime CreatedAt { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
		public DateTime UpdatedAt { get; set; }


		// Add navigation properties for the Video and Image classes
		public ICollection<Video> Videos { get; set; }
		public ICollection<Image> Images { get; set; }

		// AI Metrics for Recommender System
		[Range(0, 5)]
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

		//public List<Comment> Comments { get; set; }
		public string Manufacturer { get; set; }
		public bool OnSale { get; set; }
	}
}