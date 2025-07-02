namespace BookTradingPlatform.Vos
{
	public class UserDataVO
	{
		public string Username { get; set; } //用戶名稱(帳號)
		public string Email { get; set; } //信箱
		public string Student_id { get; internal set; } //學號
		public string PhoneNumber { get; set; } //用戶電話
	}
}
