using BookTradingPlatform.Controllers.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BookTradingPlatform.Models
{
	
	public class User
	{
		//public User(string username, int password, string email, int studentId,
		//			int department, string telephone, DateTime modifiedAt,
		//			DateTime modifiedName, string role)
		//{
		//	Username = "username";
		//	Password = "1233456";
		//	Email = "11110334@gm.chihlee.edu.tw";
		//	Student_id = "11110334";
		//	Department = "資訊管理系";
		//	TelePhone = "0912000001";
		//	Modified_at = DateTime.Now;
		//	Modified_name = DateTime.Now;
		//	Role = "admin";
		//}
		[Key]
		public int Id { get; set; }
		[Required]
		public string Username { get; set; } //用戶名稱(帳號)
		[Required]
		public string Password { get; set; } //密碼
		[Required, EmailAddress]
		public string Email { get; set; }   //信箱
		[Required]
		public string Student_id { get; set; }  //學號
		[Required]
		public string Department { get; set; }  //科系名稱
		[Required, Phone]
		public string TelePhone { get; set; }   //用戶電話
		public DateTime Modified_at { get; set; } = DateTime.Now;	//更新時間
		public DateTime  Modified_name { get; set; }    //名稱更新時間
		[Required]
		public string Role { get; set; } = "user";   //用戶角色(管理員/一般用戶)
													 // 導覽屬性
		public ICollection<Product> Products { get; set; }
		public ICollection<Favorite> Favorites { get; set; }
		public ICollection<Chat> SentChats { get; set; }
		public ICollection<Chat> ReceivedChats { get; set; }

		public User() { }
		public User(string username, string password, string email, int studentId, int department, string telephone, DateTime modifiedAt, string modifiedName, string role)
		{
			Username = "username";
			Password = "1233456";
			Email = "11110334@gm.chihlee.edu.tw";
			Student_id = "11110334";
			Department = "資訊管理系";
			TelePhone = "0912000001";
			Modified_at = DateTime.Now;
			Modified_name = DateTime.Now;
			Role = "admin";
		}
	}
}
