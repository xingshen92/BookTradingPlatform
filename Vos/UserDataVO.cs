namespace BookTradingPlatform.Vos
{
	public class UserDataVO
	{
		public string Username { get; set; } = string.Empty; //用戶名稱(帳號)
		public string Email { get; set; } = string.Empty; //信箱
		public string Student_id { get; internal set; } = string.Empty; //學號
		public string PhoneNumber { get; set; } = string.Empty; //用戶電話

		public UserDataVO() { }
	}
}
