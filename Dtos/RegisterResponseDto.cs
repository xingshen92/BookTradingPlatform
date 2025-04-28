namespace BookTradingPlatform.Dtos
{
    // 註冊回應的資料傳輸物件
    public class RegisterResponseDto 
    {
        // 註冊是否成功
        public bool IsSuccess { get; set; }

        // 註冊的結果訊息
        public string Message { get; set; }
        public string Token { get; set; }
        public UserDto User { get; set; }
    }
}
