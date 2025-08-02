namespace BookTradingPlatform.Dtos
{
	public class UserDataRequestDto
	{
		public string Username { get; set; } = string.Empty;		//用戶名稱(帳號)
		public string Email { get; set; } = string.Empty;			//信箱
		public string Student_id { get; set; } = string.Empty;		//學號
		public string PhoneNumber { get; set; } = string.Empty;		//用戶電話
		public string OldPassword { get; set; } = string.Empty;		//舊密碼
		public string NewPassword { get; set; } = string.Empty;		//新密碼
		public string ConfirmPassword { get; set; } = string.Empty;	//確認新密碼

		public UserDataRequestDto() { }
	}
}