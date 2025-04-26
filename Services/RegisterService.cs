using BookTradingPlatform.Data;
using BookTradingPlatform.Dtos;
using BookTradingPlatform.Models;
using System.Text.RegularExpressions;

namespace BookTradingPlatform.Services
{
	public class RegisterService
	{
		private readonly WebDatabase _context;

		public RegisterService(WebDatabase context)
		{
			_context = context;
		}
		//帳號限制
		private bool IsValidUsername(string Username)
		{
			if (Username.Length < 6)
				return false;

			bool hasLetter = Regex.IsMatch(Username, "[a-zA-Z]");

			return hasLetter;
		}
		//密碼限制
		private bool IsValidPassword(string password)
		{
			if (password.Length < 8)
				return false;

			bool hasLetter = Regex.IsMatch(password, "[a-zA-Z]");
			bool hasDigit = Regex.IsMatch(password, "[0-9]");

			return hasLetter && hasDigit;
		}
		//Email驗證
		private readonly string[] allowedDomains = new[]
		{
			"gmail.com",
			"chihlee.edu.tw"
		};
		private bool IsValidAllowedEmail(string email)
		{
			if (string.IsNullOrWhiteSpace(email))
				return false;

			var parts = email.Split('@');
			if (parts.Length != 2)
				return false;

			var domain = parts[1];
			return allowedDomains.Contains(domain);
		}
		//電話驗證
		private bool IsValidTaiwanPhoneNumber(string phoneNumber)
		{
			return Regex.IsMatch(phoneNumber, @"^09\d{2}\s?\d{3}\s?\d{3}$");
		}


		// 註冊
		public async Task<string> RegisterAsync(RegisterRequestDto registerRequest)
		{
			if (_context.Users.Any(u => u.Username == registerRequest.Username))
				return "用戶名稱已存在";
			if (_context.Users.Any(u => u.Email == registerRequest.Email))
				return "電子信箱已被註冊";
			if (_context.Users.Any(u => u.TelePhone == registerRequest.TelePhone))
				return "電話號碼已被註冊";

			if (!IsValidUsername(registerRequest.Username))
				return "帳號必須至少6個字元，且包含英文字母";

			if (!IsValidPassword(registerRequest.Password))
				return "密碼必須至少8個字元，且包含英文字母與數字";

			if (!IsValidAllowedEmail(registerRequest.Email))
				return "信箱必須是 @gmail.com 或 @chihlee.edu.tw 結尾的信箱";

			if (!IsValidTaiwanPhoneNumber(registerRequest.TelePhone))
				return "電話格式錯誤，必須是台灣手機格式 09XX XXX XXX";

			var user = new User
			{
				Username = registerRequest.Username,
				Password = registerRequest.Password,
				Email = registerRequest.Email,
				Student_id = registerRequest.Student_id,
				Department = registerRequest.Department,
				TelePhone = registerRequest.TelePhone,
				Role = "User",
				Modified_at = DateTime.UtcNow,
				Modified_name = DateTime.UtcNow // 先用現在時間，如果未來要用使用者名稱也可以
			};

			_context.Users.Add(user);
			await _context.SaveChangesAsync();

			return "註冊成功";
		}

		// 更新資料
		public async Task<string> UpdateUserAsync(int id, UpdateUserRequestDto updateRequest)
		{
			var user = await _context.Users.FindAsync(id);
			if (user == null)
				return "使用者不存在";

			user.Email = updateRequest.Email;
			user.TelePhone = updateRequest.TelePhone;
			user.Modified_at = DateTime.UtcNow;
			user.Modified_name = updateRequest.Modified_name;

			await _context.SaveChangesAsync();
			return "使用者資料已更新";
		}

		public List<User> GetAllUsers()
		{
			return _context.Users.ToList();
		}
	}
}
