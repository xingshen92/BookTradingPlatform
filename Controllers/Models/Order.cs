using BookTradingPlatform.Models;

namespace BookTradingPlatform.Controllers.Models
{
	public class Order
	{
		public int Id { get; set; } // 訂單 ID
		public int BuyerId { get; set; } // 買家 ID
		public int SellerId { get; set; } // 賣家 ID
		public int ProductId { get; set; } // 商品 ID
		public decimal TotalPrice { get; set; } // 總金額
		public DateTime CreatedAt { get; set; } // 訂單建立時間

		// 導覽屬性
		public User Buyer { get; set; }
		public User Seller { get; set; }
		public Product Product { get; set; }
	}
}
