using System.ComponentModel.DataAnnotations;

namespace BookTradingPlatform.Models
{
	public class Adminlog
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string MemberNumber { get; set; } = string.Empty;
		[Required]
		public string IP { get; set; } = string.Empty; // 登入 IP
		public DateTime Login_at { get; set; } // 登入時間
		public DateTime Modified_at { get; set; } // 更新時間
		[Required]
		[MaxLength(50)]
		public string Work { get; set; } = string.Empty; // 執行操作內容
	}
}