using BookTradingPlatform.Controllers.Models;
using System.ComponentModel.DataAnnotations;

namespace BookTradingPlatform.Models
{
	public class User
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string MemberNumber { get; set; } = string.Empty;//用戶ID(帳號)
		[Required]
		public string Username { get; set; } = string.Empty;//用戶名稱(帳號)
		[Required]
		public string Password { get; set; } = string.Empty;//密碼
		[Required, EmailAddress]
		public string Email { get; set; } = string.Empty; //信箱
		[Required]
		public string Student_id { get; set; } = string.Empty; //學號
		[Required]
		public string Department { get; set; } = string.Empty; //科系名稱
		[Required, Phone]
		public string TelePhone { get; set; } = string.Empty;  //用戶電話
		public DateTime Modified_at { get; set; } = DateTime.Now;   //更新時間
		public DateTime Modified_name { get; set; }    //名稱更新時間
		[Required]
		public string Role { get; set; } = "user";   //用戶角色(管理員/一般用戶)

		// 導覽屬性 
		public ICollection<Product> Products { get; set; } = new List<Product>();// 賣家的商品
		//public ICollection<Favorite> Favorites { get; set; }
		//public ICollection<Chat> SentChats { get; set; }
		//public ICollection<Chat> ReceivedChats { get; set; }
	}
}