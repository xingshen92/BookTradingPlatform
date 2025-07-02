using BookTradingPlatform.Data;
using BookTradingPlatform.Dtos;
using BookTradingPlatform.Vos;
using Microsoft.EntityFrameworkCore;

namespace BookTradingPlatform.Services
{
	public class UserDataService
	{
		private readonly WebDatabase _context;
		public UserDataService(WebDatabase context)
		{
			_context = context;
		}

		public async Task<UserDataResponseDto> GetUserDataAsync(int id)
		{
			var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

			if (user == null)
				return null;

			return new UserDataResponseDto
			{
				IsSuccess = true,
				Message = "取得資料成功",
				User = new UserDataVO
				{
					Username = user.Username,
					Email = user.Email,
					Student_id = user.Student_id,
					PhoneNumber = user.TelePhone
				}
			};
		}
	}
}