using BookTradingPlatform.Controllers.Models;
using BookTradingPlatform.Models;
using BookTradingPlatform.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookTradingPlatform.Data
{
	public class WebDatabase : DbContext
	{
		public WebDatabase(DbContextOptions<WebDatabase> options) : base(options) { }
		public DbSet<Adminlog> Adminlogs { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<Product> Products { get; set; }
		protected override void OnModelCreating(ModelBuilder ModelBuilder)
		{
			ModelBuilder.Entity<Adminlog>(entity =>
			{
				entity.Property(e => e.Id).HasColumnName("id");
				entity.Property(e => e.IP).HasColumnName("ip").HasMaxLength(50);
				entity.Property(e => e.Login_at).HasColumnType("datetime").HasColumnName("login_at");
				entity.Property(e => e.Modified_at).HasColumnType("datetime").HasColumnName("modified_at");
				entity.Property(e => e.Work).HasColumnName("work").HasMaxLength(50);
			}
			);
			ModelBuilder.Entity<User>(entity =>
			{
				entity.Property(e => e.Id).HasColumnName("id");
				entity.Property(e => e.Username).HasColumnName("username").HasMaxLength(50);
				entity.Property(e => e.Password).HasColumnName("password");
				entity.Property(e => e.Email).HasColumnName("email").HasMaxLength(100);
				entity.Property(e => e.Department).HasColumnName("department").HasMaxLength(50);
				entity.Property(e => e.Student_id).HasColumnName("student_id").HasMaxLength(20);
				entity.Property(e => e.TelePhone).HasColumnName("telephone");
				entity.Property(e => e.Modified_at).HasColumnType("datetime").HasColumnName("modified_at");
				entity.Property(e => e.Modified_name).HasColumnType("datetime").HasColumnName("modified_name");
				entity.Property(e => e.Role).HasColumnName("role").HasMaxLength(10);
			}
			);
			ModelBuilder.Entity<Product>(entity =>
			{
				entity.Property(e => e.Id).HasColumnName("id");
				entity.Property(e => e.SKU).HasColumnName("SKU").HasMaxLength(50);
				entity.Property(e => e.Name).HasColumnName("Name").HasMaxLength(200);
				entity.Property(e => e.PublishingHouse).HasColumnName("publishing_house");
				entity.Property(e => e.PublishingAt).HasColumnName("publishing_at").HasMaxLength(50);;
				entity.Property(e => e.Price).HasColumnName("price");
				entity.Property(e => e.Desc).HasColumnName("desc");
				entity.Property(e => e.Image).HasColumnType("image").HasMaxLength(20);
				entity.Property(e => e.ModifiedAt).HasColumnType("datetime").HasColumnName("modified_at");
				entity.Property(e => e.Transaction).HasColumnName("transaction").HasMaxLength(20);
			}
			);
			//ModelBuilder.Entity<Favorite>(entity =>
			//{
			//	entity.Property(e => e.Id).HasColumnName("id");
			//	entity.Property(e => e.UserId).HasColumnName("user_id");
			//	entity.Property(e => e.ProductId).HasColumnName("product_id");
			//	entity.Property(e => e.Created_at).HasColumnType("datetime").HasColumnName("created_at");
			//}
			//);
			//ModelBuilder.Entity<Report>(entity =>
			//{
			//	entity.Property(e => e.Id).HasColumnName("id");
			//	entity.Property(e => e.ReporterId).HasColumnName("reporter_id");
			//	entity.Property(e => e.ProductId).HasColumnName("product_id");
			//	entity.Property(e => e.Reason).HasColumnName("reason").HasMaxLength(200);
			//	entity.Property(e => e.Created_at).HasColumnType("datetime").HasColumnName("created_at");
			//}
			//);
			//ModelBuilder.Entity<Order>(entity =>
			//{
			//	entity.Property(e => e.Id).HasColumnName("id");
			//	entity.Property(e => e.BuyerId).HasColumnName("BuyerId");
			//	entity.Property(e => e.SellerId).HasColumnName("SellerId");
			//	entity.Property(e => e.ProductId).HasColumnName("ProductId");
			//	entity.Property(e => e.TotalPrice).HasColumnName("TotalPrice");
			//	entity.Property(e => e.Created_at).HasColumnType("datetime").HasColumnName("created_at");
			//}
			//);
			//ModelBuilder.Entity<Chat>(entity =>
			//{
			//	entity.Property(e => e.Id).HasColumnName("id");
			//	entity.Property(e => e.SenderId).HasColumnName("sender_id");
			//	entity.Property(e => e.ReceiverId).HasColumnName("receiver_id");
			//	entity.Property(e => e.Message).HasColumnName("message").HasMaxLength(500);
			//	entity.Property(e => e.Image).HasColumnName("image").HasMaxLength(500);
			//	entity.Property(e => e.Modified_at).HasColumnType("datetime").HasColumnName("modified_at");
			//}
			//);
		}
	}
}

