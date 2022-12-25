using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductAPI.Models
{
	public class Product
	{
		public int ProductId { get; set; }
		public string OwnerId { get; set; }
		[Required]
		[StringLength(255)]
		public string Name { get; set; }

		[StringLength(2000)]
		public string Description { get; set; }

		[Range(0, double.MaxValue)]
		public decimal Price { get; set; }

		[ForeignKey("Category")]
		public int CategoryId { get; set; }
		public Category Category { get; set; }
		public ICollection<Category> Categories { get; set; }

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