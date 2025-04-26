namespace BookTradingPlatform.Dtos
{
    // 使用者登入請求
    public class LoginRequestDto
    {
        public string UsernameOrEmail { get; set; }
        public string Password { get; set; }
    }

    // 登入回傳資料
    public class LoginResponseDto
    {
		public bool IsSuccess { get; set; }
		public string Message { get; set; }
        public string Token { get; set; }
        public UserDto User { get; set; }
    }

    // 簡化版的使用者資料
    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
}
