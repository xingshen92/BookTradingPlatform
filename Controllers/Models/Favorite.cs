using BookTradingPlatform.Models;

namespace BookTradingPlatform.Controllers.Models
{
	public class Favorite
	{
		public int Id { get; set; } // 我的最愛 ID
		public int UserId { get; set; } // 使用者 ID
		public int ProductId { get; set; } // 商品 ID
		public DateTime CreatedAt { get; set; } // 加入最愛時間

		public User User { get; set; }
		public Product Product { get; set; }
	}
}
