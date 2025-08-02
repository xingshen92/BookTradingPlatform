namespace BookTradingPlatform.Dtos
{
	// 使用者登入請求
	public class LoginRequestDto
	{
		public string UsernameOrEmail { get; set; } = string.Empty;
		public string Password { get; set; } = string.Empty;

		public LoginRequestDto() { }
	}
}