namespace BookTradingPlatform.Dtos
{
    // 註冊請求的資料傳輸物件
    public class RegisterRequestDto
    {
        public string MemberNumber { get; set; } = string.Empty; // 使用者ID
        public string Username { get; set; } = string.Empty;     // 帳號名稱
        public string Email { get; set; } = string.Empty;        // 電子郵件
        public string Password { get; set; } = string.Empty;     // 密碼
		public string StudentId { get; set; } = string.Empty;    // 學生編號
		public string Department { get; set; } = string.Empty;   // 所屬部門
		public string Telephone { get; set; } = string.Empty;    // 電話

        public RegisterRequestDto() { }
	}
}