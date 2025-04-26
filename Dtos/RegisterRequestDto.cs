namespace BookTradingPlatform.Dtos
{
	// 註冊請求
	public class RegisterRequestDto
	{
		public string Username { get; set; }
		public string Password { get; set; }
		public string Email { get; set; }
		public string Student_id { get; set; }
		public string Department { get; set; }
		public string TelePhone { get; set; }
	}

	// 更新用戶資料請求
	public class UpdateUserRequestDto
	{
		public string Email { get; set; }
		public string TelePhone { get; set; }
		public DateTime Modified_name { get; set; } //由前端帶上更新人資訊
	}
}
