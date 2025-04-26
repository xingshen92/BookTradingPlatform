using BookTradingPlatform.Data;
using BookTradingPlatform.Dtos;
using BookTradingPlatform.Models;
using BookTradingPlatform.Services;
using Microsoft.EntityFrameworkCore;

namespace BookTradingPlatform.Services
{
    public class LoginService
    {
        private readonly WebDatabase _context;
        private readonly JwtTokenServices _jwt;

        public LoginService(WebDatabase context, JwtTokenServices jwt)
        {
            _context = context;
            _jwt = jwt;
        }

		public async Task<LoginResponseDto> LoginAsync(LoginRequestDto loginDto)
		{
			var input = loginDto.UsernameOrEmail.ToLower();

			var user = await _context.Users.FirstOrDefaultAsync(u =>
				u.Username.ToLower() == input || u.Email.ToLower() == input);

			if (user == null)
				return new LoginResponseDto
				{
					IsSuccess = false,
					Message = "帳號或信箱不存在"
				};

			if (!VerifyPassword(loginDto.Password, user.Password))
				return new LoginResponseDto
				{
					IsSuccess = false,
					Message = "密碼錯誤"
				};

			var token = _jwt.GenerateToken(user);

			var userDto = new UserDto
			{
				Id = user.Id,
				Username = user.Username,
				Email = user.Email,
				Role = user.Role
			};

			return new LoginResponseDto
			{
				IsSuccess = true,
				Message = "登入成功",
				Token = token,
				User = userDto
			};
		}
		public LoginResponseDto RefreshToken(int userId)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);

            if (user == null)
                return null;

            var token = _jwt.GenerateToken(user);

            return new LoginResponseDto
            {
                Message = "Token 重新產生成功",
                Token = token,
                User = new UserDto
                {
                    Id = user.Id,
                    Username = user.Username,
                    Email = user.Email,
                    Role = user.Role
                }
            };
        }
		private bool VerifyPassword(string inputPassword, string storedHashedPassword)
		{
			// 這裡是直接比對，如果有加Hash，可以用BCrypt/其他解碼方式
			return inputPassword == storedHashedPassword;
		}
	}
}
