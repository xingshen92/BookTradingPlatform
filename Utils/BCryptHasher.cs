using BCrypt.Net;

namespace BookTradingPlatform.Utilities
{
	public class BCryptHasher
	{
		// 密碼加密
		public static string HashPassword(string password)
		{
			// 使用 BCrypt 加密密碼，並返回加密後的結果
			return BCrypt.Net.BCrypt.HashPassword(password);
		}

		// 驗證密碼是否正確
		public static bool VerifyPassword(string password, string hashedPassword)
		{
			// 比對輸入的密碼和存儲的加密密碼
			return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
		}
	}
}
