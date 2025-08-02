namespace BookTradingPlatform.Dtos
{
    // 登入回傳資料
    public class LoginResponseDto
    {
		public bool IsSuccess { get; set; } = false;
		// 登入結果訊息
		public string Message { get; set; } = string.Empty;
		public string Token { get; set; } = string.Empty;
		public UserDto User { get; set; }

        public LoginResponseDto()
        {
            User = new UserDto();
		}
	}
}