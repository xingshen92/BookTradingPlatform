namespace BookTradingPlatform.Dtos
{
    // 註冊請求的資料傳輸物件
    public class RegisterRequestDto
    {
        
        public string Username { get; set; }  // 帳號名稱
        public string Email { get; set; }  // 電子郵件
        public string Password { get; set; } // 密碼
		public string StudentId { get; set; }  // 學生編號
		public string Department { get; set; } // 所屬部門
		public string Telephone { get; set; }  // 電話
	}
}
