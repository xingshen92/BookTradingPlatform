using System.ComponentModel.DataAnnotations;

namespace BookTradingPlatform.Models
{
	public class User
	{
		[Key]
		public int Id { get; set; }
		public string Userame { get; set; } //用戶名稱(帳號)
		public string Password { get; set; } //密碼
		public string Email { get; set; }	//信箱
		public int Student_id { get; set; }	//學號
		public string Department { get; set; }	//科系名稱
		public string TelePhone { get; set; }	//用戶電話
		public int Modified_at { get; set; }	//更新時間
		public int Modified_name { get; set; }	//名稱更新時間
		
		public string Role { get; set; }	//用戶角色(管理員/一般用戶)
	}
}
