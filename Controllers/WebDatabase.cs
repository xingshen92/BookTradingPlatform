using BookTradingPlatform.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookTradingPlatform.Data
{
	public class WebDatabase : DbContext
	{
		public WebDatabase(DbContextOptions<WebDatabase> options) : base(options) { }
		public DbSet<User> Users { get; set; }
	}
}

