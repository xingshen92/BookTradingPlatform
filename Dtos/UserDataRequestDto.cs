namespace BookTradingPlatform.Dtos
{
	public class UserDataRequestDto
	{
		public string Username { get; set; } //用戶名稱(帳號)
		public string Email { get; set; } //信箱
		public string Student_id { get; set; } //學號
		public string PhoneNumber { get; set; } //用戶電話
		public string? OldPassword { get; set; } //舊密碼
		public string? NewPassword { get; set; } //新密碼
		public string? ConfirmPassword { get; set; } //確認新密碼
	}
}