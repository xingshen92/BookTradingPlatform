using BookTradingPlatform.Data;
using BookTradingPlatform.Dtos;
using BookTradingPlatform.Vos;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace BookTradingPlatform.Services
{
	public class UserDataService
	{
		private readonly WebDatabase _context;
		public UserDataService(WebDatabase context)
		{
			_context = context;
		}

		//gmail及學校帳號後綴
		private readonly string[] allowedDomains = new[]
		{
			"gmail.com",
			"chihlee.edu.tw"
		};

		private bool CheckModifiedName(DateTime Modifiedname, out string result) //帳號名稱修改時間限制
		{
			DateTime Thirtydayago= DateTime.UtcNow.AddDays(-30); //30天前的時間

			if (Thirtydayago <= Modifiedname) //如果修改時間在30天內
			{
				result = "帳號名稱得30天後才能修改";
				return false;
			}

			result = "";
			return true;
		}

		private bool CheckUsername(string Username, out string result) //帳號名稱限制
		{
			if (string.IsNullOrEmpty(Username)) //檢查帳號名稱是否為空
			{
				result = "帳號名不得為空";
				return false;
			}

			if (Username.Length < 6) //檢查帳號名稱長度是否至少6個字元
			{
				result = "帳號名必須至少6個字元";
				return false;
			}

			if (!Regex.IsMatch(Username, "[a-zA-Z]")) //檢查帳號名稱是否包含至少一個英文字母
			{
				result = "帳號名必須包含英文名";
				return false;
			}

			result = "";
			return true;
		}

		private bool CheckEmail(string Email, out string result) //信箱格式限制
		{
			var parts = Email.Split('@');
			var domain = parts[1];

			if (string.IsNullOrEmpty(Email)) //檢查信箱是否為空
			{
				result = "信箱不得為空";
				return false;
			}

			if (parts.Length != 2) //檢查信箱格式是否正確
			{
				result = "信箱格式不正確";
				return false;
			}

			if (!allowedDomains.Contains(domain)) //檢查信箱後綴是否為允許的域名
			{
				result = "信箱必須為學校或gmail帳號";
				return false;
			}
			
			result = "";
			return true;
		}

		private bool CheckPhoneNumber(string Phonenumber, out string result) //電話格式限制
		{
			if (string.IsNullOrEmpty(Phonenumber)) //檢查電話是否為空
			{
				result = "電話號碼不得為空";
				return false;
			}

			if (!Regex.IsMatch(Phonenumber, @"^09\d{2}\s?\d{3}\s?\d{3}$")) //檢查電話格式是否符合台灣手機號碼格式
			{
				result = "電話格式錯誤，必須是台灣手機格式 09XX XXX XXX";
				return false;
			}

			result = "";
			return true;
		}

		private bool CheckNewPassword(string Newpassword, string Confirmpassword, out string result)
		{
			if (string.IsNullOrEmpty(Confirmpassword)) //檢查再次確認密碼是否為空
			{
				result = "再次確認密碼不得為空";
				return false;
			}
			
			if (Newpassword.Length < 8) //檢查新密碼長度是否至少8個字元
			{
				result = "新密碼必須至少8個字元";
				return false;
			}

			if (Newpassword != Confirmpassword) //檢查再次確認密碼是否與新密碼相同
			{
				result = "新密碼必須與再次確認密碼相同";
				return false;
			}
			
			if (!Regex.IsMatch(Newpassword, "[a-zA-Z]") || !Regex.IsMatch(Newpassword, "[0-9]")) //檢查新密碼是否符合規定
			{
				result = "新密碼必須包含至少一個英文字母和一個數字";
				return false;
			}

			result = "";
			return true;
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

		public async Task<UserDataResponseDto> UpdateDataAsync(int id, UserDataRequestDto userDataDto)
		{
			var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

			if (user == null)
				return null;

			if (user.Username != userDataDto.Username) // 是否有修改帳號名稱
			{
				// 檢查帳號名稱上次修改時間是否已經過30天
				if (!CheckModifiedName(user.Modified_name, out string modifiedNameResult))
				{
					return new UserDataResponseDto
					{
						IsSuccess = false,
						Message = "更新資料失敗: " + modifiedNameResult,
						User =
						{
							Username = user.Username,
							Email = user.Email,
							Student_id = user.Student_id,
							PhoneNumber = user.TelePhone
						}
					};
				}
				// 檢查帳號名稱是否符合規定
				if (!CheckUsername(userDataDto.Username, out string usernameResult))
				{
					return new UserDataResponseDto {
						IsSuccess = false,
						Message = "更新資料失敗: "+usernameResult,
						User =
						{
							Username = user.Username,
							Email = user.Email,
							Student_id = user.Student_id,
							PhoneNumber = user.TelePhone
						}
					};
				}

				user.Username = userDataDto.Username;
				user.Modified_name = DateTime.UtcNow; // 更新帳號修改時間
			}

			if (user.Email != userDataDto.Email) // 是否有修改信箱
			{
				// 檢查信箱格式是否正確
				if (!CheckEmail(userDataDto.Email, out string emailResult))
				{
					return new UserDataResponseDto
					{
						IsSuccess = false,
						Message = "更新資料失敗: " + emailResult,
						User =
						{
							Username = user.Username,
							Email = user.Email,
							Student_id = user.Student_id,
							PhoneNumber = user.TelePhone
						}
					};
				}

				user.Email = userDataDto.Email;
			}

			if (user.TelePhone != userDataDto.PhoneNumber) // 是否有修改電話
			{
				// 檢查電話格式是否正確
				if (!CheckPhoneNumber(userDataDto.PhoneNumber, out string phoneResult))
				{
					return new UserDataResponseDto
					{
						IsSuccess = false,
						Message = "更新資料失敗: " + phoneResult,
						User =
						{
							Username = user.Username,
							Email = user.Email,
							Student_id = user.Student_id,
							PhoneNumber = user.TelePhone
						}
					};
				}

				user.TelePhone = userDataDto.PhoneNumber;
			}

			if (userDataDto.NewPassword != null)
			{
				// 檢查新密碼是否符合規定
				if (!CheckNewPassword(userDataDto.NewPassword, userDataDto.ConfirmPassword, out string newPasswordResult))
				{
					return new UserDataResponseDto
					{
						IsSuccess = false,
						Message = "更新資料失敗: " + newPasswordResult,
						User =
						{
							Username = user.Username,
							Email = user.Email,
							Student_id = user.Student_id,
							PhoneNumber = user.TelePhone
						}
					};
				}
				// 寄信二次確認
				// 這裡可以加入寄信二次確認的邏輯

				// 密碼加密處理
				user.Password = BCrypt.Net.BCrypt.HashPassword(userDataDto.NewPassword); // 密碼加密處理
			}

			user.Modified_at = DateTime.UtcNow;

			_context.Users.Update(user);
			await _context.SaveChangesAsync();
			return new UserDataResponseDto { 
				IsSuccess = true, 
				Message = "更新資料成功",
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