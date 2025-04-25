namespace BookTradingPlatform.Controllers.Models
{
	public class Admin
	{
		public int Id { get; set; } // 管理 ID
		public string IP { get; set; } // 登入 IP
		public DateTime Login_at { get; set; } // 登入時間
		public DateTime Modified_at { get; set; } // 更新時間
		public string Work { get; set; } // 執行操作內容
	}
}
