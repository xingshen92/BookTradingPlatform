using BookTradingPlatform.Data;
using BookTradingPlatform.Dtos;
using BookTradingPlatform.Models;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using BookTradingPlatform.Utilities;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Identity.Data;
using BookTradingPlatform.Bos;

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
		public async Task<RegisterResponseDto> RegisterAsync(RegisterRequestDto registerDto)
        {
            // 檢查帳號或信箱是否已經存在
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Username.ToLower() == registerDto.Username.ToLower() || u.Email.ToLower() == registerDto.Email.ToLower());

			if (existingUser != null) { return new RegisterResponseDto { IsSuccess = false, Message = "帳號或信箱已經存在" }; }
 
			if (!IsValidUsername(registerDto.Username))
				return new RegisterResponseDto { IsSuccess = false, Message = "帳號必須至少6個字元，且包含英文字母" };
			if (!IsValidPassword(registerDto.Password))
				return new RegisterResponseDto{IsSuccess = false, Message = "密碼必須至少8個字元，且包含英文字母與數字"}; 
			if (!IsValidAllowedEmail(registerDto.Email))
				return new RegisterResponseDto { IsSuccess = false, Message = "信箱必須是 @gmail.com 或 @chihlee.edu.tw 結尾的信箱" };

			if (!IsValidTaiwanPhoneNumber(registerDto.Telephone))
				return new RegisterResponseDto { IsSuccess = false, Message = "電話格式錯誤，必須是台灣手機格式 09XX XXX XXX" };

			// 生成密碼的哈希值
			var hashedPassword = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);

            // 創建新的使用者
            var userBo = new UserBO
			{
                Username = registerDto.Username,
                Email = registerDto.Email,
				PasswordHash = hashedPassword,
				StudentId = registerDto.StudentId,      // 學生編號
				Department = registerDto.Department,    // 所屬部門
				Telephone = registerDto.Telephone,      // 電話
				Role = "User", // 預設為普通使用者
				ModifiedAt = DateTime.UtcNow
            };

			var userPo = userBo.ToPersistenceObject();
			try
			{
				// 儲存到資料庫
				_context.Users.Add(userPo);
				await _context.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				return new RegisterResponseDto
				{
					IsSuccess = false,
					Message = "註冊失敗: " + ex.Message
				};
			}

			return new RegisterResponseDto
            {
                IsSuccess = true,
                Message = "註冊成功",
                User = new UserDto
                {
                    Id = userPo.Id,
                    Username = userPo.Username,
                    Email = userPo.Email,
                    Role = userPo.Role,
                    Student_id = userPo.Student_id,  // 返回學生編號
					Department = userPo.Department, // 返回部門
					Telephone = userPo.TelePhone  // 返回電話
                }
            };
        }
	}
}
