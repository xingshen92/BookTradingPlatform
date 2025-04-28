namespace BookTradingPlatform.Dtos
{
    // 登入回傳資料
    public class LoginResponseDto
    {
		public bool IsSuccess { get; set; }
		public string Message { get; set; }
        public string Token { get; set; }
        public UserDto User { get; set; }
    }
}
