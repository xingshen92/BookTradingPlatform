using BookTradingPlatform.Models;

namespace BookTradingPlatform.Controllers.Models
{
	public class Report
	{
		public int Id { get; set; } // 檢舉 ID
		public int ReporterId { get; set; } // 檢舉者 ID
		public int ProductId { get; set; } // 被檢舉商品 ID
		public string Reason { get; set; } // 檢舉原因
		public DateTime CreatedAt { get; set; } // 檢舉時間

		public User Reporter { get; set; }
		public Product Product { get; set; }
	}
}
