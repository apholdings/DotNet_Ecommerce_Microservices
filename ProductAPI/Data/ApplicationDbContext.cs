using Microsoft.EntityFrameworkCore;
using ProductAPI.Models;
using ProductAPI.Repository.IRepository;

namespace ProductAPI.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
			AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
		}

		public DbSet<Product> Products { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Video> Videos { get; set; }
		public DbSet<Image> Images { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// Set up relationships between entities...

			// Set up the relationship between Product and Category
			modelBuilder.Entity<Product>()
				.HasOne(p => p.Category)
				.WithMany(c => c.Products)
				.HasForeignKey(p => p.CategoryId);

			// Set up the relationship between Category and Category
			modelBuilder.Entity<Category>()
				.HasOne(c => c.ParentCategory)
				.WithMany(c => c.ChildCategories)
				.HasForeignKey(c => c.ParentCategoryId);

			// Set up the relationship between Product and Video
			modelBuilder.Entity<Product>()
				.HasMany(p => p.Videos)
				.WithOne(v => v.Product)
				.HasForeignKey(v => v.ProductId);

			// Set up the relationship between Product and Image
			modelBuilder.Entity<Product>()
				.HasMany(p => p.Images)
				.WithOne(i => i.Product)
				.HasForeignKey(i => i.ProductId);

			// End Set up relationships between entities section ...


			modelBuilder.Entity<Product>()
				.HasIndex(p => p.Name)
				.HasDatabaseName("IX_Product_Name");
			modelBuilder.Entity<Product>()
				.HasIndex(p => p.Description)
				.HasDatabaseName("IX_Product_Description");


			modelBuilder.Entity<Product>()
				.HasIndex(p => p.Name)
				.HasDatabaseName("IX_Product_Name_FTS");


			// tell Entity Framework to use the SQL function NOW() as the default value for the
			// CreatedAt property when a new Product entity is added to the database. 
			modelBuilder.Entity<Product>()
				.Property(p => p.CreatedAt)
				.HasDefaultValueSql("NOW()")
				.ValueGeneratedOnAdd();
			// We do the same for UpdatedAt
			modelBuilder.Entity<Product>()
				.Property(p => p.UpdatedAt)
				.HasDefaultValueSql("NOW()")
				.ValueGeneratedOnAddOrUpdate();
			
			modelBuilder.Entity<Image>()
				.Property(p => p.CreatedAt)
				.HasDefaultValueSql("NOW()")
				.ValueGeneratedOnAdd();
			// We do the same for UpdatedAt
			modelBuilder.Entity<Image>()
				.Property(p => p.UpdatedAt)
				.HasDefaultValueSql("NOW()")
				.ValueGeneratedOnAddOrUpdate();

			modelBuilder.Entity<Video>()
				.Property(p => p.CreatedAt)
				.HasDefaultValueSql("NOW()")
				.ValueGeneratedOnAdd();
			// We do the same for UpdatedAt
			modelBuilder.Entity<Video>()
				.Property(p => p.UpdatedAt)
				.HasDefaultValueSql("NOW()")
				.ValueGeneratedOnAddOrUpdate();

			modelBuilder.Entity<Category>()
				.Property(p => p.CreatedAt)
				.HasDefaultValueSql("NOW()")
				.ValueGeneratedOnAdd();
			// We do the same for UpdatedAt
			modelBuilder.Entity<Category>()
				.Property(p => p.UpdatedAt)
				.HasDefaultValueSql("NOW()")
				.ValueGeneratedOnAddOrUpdate();


			// Add Seed Data
			// Seed the database with dummy data for the categories
			modelBuilder.Entity<Category>().HasData(
				new Category
				{
					CategoryId = 1,
					Name = "Smart Home",
					Description = "Smart home devices and systems",
					CreatedAt = DateTime.UtcNow,
					UpdatedAt = DateTime.UtcNow
				},
				new Category
				{
					CategoryId = 2,
					Name = "Electronics",
					Description = "Electronic devices and gadgets",
					CreatedAt = DateTime.UtcNow,
					UpdatedAt = DateTime.UtcNow
				},
				new Category
				{
					CategoryId = 3,
					Name = "Arduino",
					Description = "Arduino microcontroller boards and kits",
					CreatedAt = DateTime.UtcNow,
					UpdatedAt = DateTime.UtcNow,
					ParentCategoryId = 1
				},
				new Category
				{
					CategoryId = 4,
					Name = "Accessories",
					Description = "Electronic accessories and peripherals",
					CreatedAt = DateTime.UtcNow,
					UpdatedAt = DateTime.UtcNow,
					ParentCategoryId = 2
				}
			);


			// Seed the database with dummy data for Products
			// Product 1
			modelBuilder.Entity<Product>().HasData(
				new Product
				{
					ProductId = 1,
					OwnerId = "username",
					Name = "Arduino Uno",
					Description = "A microcontroller board based on the ATmega328 microcontroller.",
					Price = 29.99m,
					Slug = "arduino-uno",
					CategoryId = 3,
					Quantity = 10,
					AverageRating = 4.5,
					NumPurchases = 100,
					NumViews = 1000,
					NumLikes = 500,
					AvgTimeSpent = 60,
					ClickThroughRate = 0.2,
					ConversionRate = 0.1,
					TotalRevenue = 499.9,
					NumReturns = 10,
					NumRefunds = 5,
					Manufacturer = "Arduino LLC",
					OnSale = false,
					CreatedAt = DateTime.UtcNow,
					UpdatedAt = DateTime.UtcNow
				}
			);
			modelBuilder.Entity<Image>().HasData(
				new Image
				{
					ImageId = 1,
					OwnerId = "username",
					ProductId = 1,
					Url = "https://boomslag.s3.us-east-2.amazonaws.com/lightbulb.jpg",
					CreatedAt = DateTime.UtcNow,
					UpdatedAt = DateTime.UtcNow
				}
			);
			modelBuilder.Entity<Video>().HasData(
				new Video
				{
					VideoId = 1,
					OwnerId = "username",
					ProductId = 1,
					Url = "https://www.youtube.com/watch?v=jDigbTQ7xAM",
					CreatedAt = DateTime.UtcNow,
					UpdatedAt = DateTime.UtcNow
				}
			);


			// Product 2
			modelBuilder.Entity<Product>().HasData(
				new Product
				{
					ProductId = 2,
					OwnerId = "username",
					Name = "Raspberry Pi",
					Slug = "raspberry-pi",
					Description = "Its more than just a microcontroller!",
					Price = 19.99m,
					CategoryId = 1,
					Quantity = 10,
					AverageRating = 4,
					NumPurchases = 182,
					NumViews = 764,
					NumLikes = 450,
					AvgTimeSpent = 45,
					ClickThroughRate = 0.2,
					ConversionRate = 0.1,
					TotalRevenue = 899.9,
					NumReturns = 0,
					NumRefunds = 3,
					Manufacturer = "Raspberry Pi Foundation",
					OnSale = false,
					CreatedAt = DateTime.UtcNow,
					UpdatedAt = DateTime.UtcNow
				}
			);
			modelBuilder.Entity<Image>().HasData(
				new Image
				{
					ImageId = 2,
					OwnerId = "username",
					ProductId = 2,
					Url = "https://boomslag.s3.us-east-2.amazonaws.com/lightbulb.jpg",
					CreatedAt = DateTime.UtcNow,
					UpdatedAt = DateTime.UtcNow
				}
			);
			modelBuilder.Entity<Video>().HasData(
				new Video
				{
					VideoId = 2,
					OwnerId = "username",
					ProductId = 2,
					Url = "https://www.youtube.com/watch?v=jDigbTQ7xAM",
					CreatedAt = DateTime.UtcNow,
					UpdatedAt = DateTime.UtcNow
				}
			);


			// Product 3
			modelBuilder.Entity<Product>().HasData(
				new Product
				{
					ProductId = 3,
					OwnerId = "username",
					Name = "PlayStation 5",
					Slug = "playstation-5",
					Description = "Its better than the xbox",
					Price = 599.99m,
					CategoryId = 2,
					Quantity = 10,
					AverageRating = 4,
					NumPurchases = 182,
					NumViews = 764,
					NumLikes = 450,
					AvgTimeSpent = 45,
					ClickThroughRate = 0.2,
					ConversionRate = 0.1,
					TotalRevenue = 899.9,
					NumReturns = 0,
					NumRefunds = 3,
					Manufacturer = "Sony Electornics",
					OnSale = false,
					CreatedAt = DateTime.UtcNow,
					UpdatedAt = DateTime.UtcNow
				}
			);
			modelBuilder.Entity<Image>().HasData(
				new Image
				{
					ImageId = 3,
					OwnerId = "username",
					ProductId = 3,
					Url = "https://boomslag.s3.us-east-2.amazonaws.com/lightbulb.jpg",
					CreatedAt = DateTime.UtcNow,
					UpdatedAt = DateTime.UtcNow
				}
			);
			modelBuilder.Entity<Video>().HasData(
				new Video
				{
					VideoId = 3,
					OwnerId = "username",
					ProductId = 3,
					Url = "https://www.youtube.com/watch?v=jDigbTQ7xAM",
					CreatedAt = DateTime.UtcNow,
					UpdatedAt = DateTime.UtcNow
				}
			);


			// Product 4
			modelBuilder.Entity<Product>().HasData(
				new Product
				{
					ProductId = 4,
					OwnerId = "username",
					Name = "Atheros 9271L",
					Slug = "atheros-9271l",
					Description = "Anthenna for ethical hacking with kali linux",
					Price = 19.99m,
					CategoryId = 2,
					Quantity = 10,
					AverageRating = 4,
					NumPurchases = 182,
					NumViews = 764,
					NumLikes = 450,
					AvgTimeSpent = 45,
					ClickThroughRate = 0.2,
					ConversionRate = 0.1,
					TotalRevenue = 899.9,
					NumReturns = 0,
					NumRefunds = 3,
					Manufacturer = "Atheros",
					OnSale = false,
					CreatedAt = DateTime.UtcNow,
					UpdatedAt = DateTime.UtcNow
				}
			);
			modelBuilder.Entity<Image>().HasData(
				new Image
				{
					ImageId = 4,
					OwnerId = "username",
					ProductId = 4,
					Url = "https://boomslag.s3.us-east-2.amazonaws.com/lightbulb.jpg",
					CreatedAt = DateTime.UtcNow,
					UpdatedAt = DateTime.UtcNow
				}
			);
			modelBuilder.Entity<Video>().HasData(
				new Video
				{
					VideoId = 4,
					OwnerId = "username",
					ProductId = 4,
					Url = "https://www.youtube.com/watch?v=jDigbTQ7xAM",
					CreatedAt = DateTime.UtcNow,
					UpdatedAt = DateTime.UtcNow
				}
			);



		}
	}
}