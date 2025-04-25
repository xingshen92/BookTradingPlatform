using BookTradingPlatform.Controllers.Models;
using BookTradingPlatform.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookTradingPlatform.Data
{
	public class WebDatabase : DbContext
	{
		public WebDatabase(DbContextOptions<WebDatabase> options) : base(options) { }
		public DbSet<Admin> Admins { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<Product> Products { get; set; }
		protected override void OnModelCreating(ModelBuilder ModelBuilder)
		{
			ModelBuilder.Entity<Admin>(entity =>
			{
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
				entity.Property(e => e.SKUName).HasColumnName("SKUName").HasMaxLength(50);
				entity.Property(e => e.publishing_house).HasColumnName("publishing_house");
				entity.Property(e => e.publishing_at).HasColumnType("datetime").HasColumnName("publishing_at");
				entity.Property(e => e.price).HasColumnName("price");
				entity.Property(e => e.desc).HasColumnName("desc");
				entity.Property(e => e.image).HasColumnType("image").HasMaxLength(20);
				entity.Property(e => e.Modified_at).HasColumnType("datetime").HasColumnName("modified_at");
				entity.Property(e => e.transaction).HasColumnName("transaction").HasMaxLength(10);
			}
			);
		}
	}
}

