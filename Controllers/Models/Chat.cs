using BookTradingPlatform.Models;

namespace BookTradingPlatform.Controllers.Models
{
	public class Chat
	{
		public int Id { get; set; } // 訊息 ID
		public int SenderId { get; set; } // 發送者 ID
		public int ReceiverId { get; set; } // 接收者 ID
		public int ProductId { get; set; } // 商品 ID
		public string Message { get; set; } // 訊息文字內容
		public byte[] Image { get; set; } // 圖片訊息（可選）
		public DateTime ModifiedAt { get; set; } // 資料更新時間

		public User Sender { get; set; }
		public User Receiver { get; set; }
		public Product Product { get; set; }
	}
}
