using AutoMapper;
using BookTradingPlatform.Common;
using BookTradingPlatform.Data;
using BookTradingPlatform.Dtos;
using BookTradingPlatform.Models;
using BookTradingPlatform.Vos;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace BookTradingPlatform.Services
{
	public interface IUserDataService
	{
		Task<Result<UserDataResponseDto>> GetUserDataAsync(int id);
		Task<Result<UserDataResponseDto>> UpdateDataAsync(int id, UserDataRequestDto userDataDto);
	}

	class UserDataService : IUserDataService
	{
		private readonly WebDatabase _context;
		private readonly IMapper _mapper;
		public UserDataService(WebDatabase context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		//gmail及學校帳號後綴
		private readonly string[] allowedDomains = new[]
		{
			"gmail.com",
			"chihlee.edu.tw"
		};

		private bool CheckModifiedName(DateTime Modifiedname, out string result) //帳號名稱修改時間限制
		{
			DateTime Thirtydayago = DateTime.UtcNow.AddDays(-30); //30天前的時間

			if (Thirtydayago <= Modifiedname) //如果修改時間在30天內
			{
				result = ErrorCodes.NameTimeRefuse;
				return false;
			}

			result = "";
			return true;
		}

		private bool CheckUsername(string Username, out string result) //帳號名稱限制
		{
			if (string.IsNullOrEmpty(Username)) //檢查帳號名稱是否為空
			{
				result = ErrorCodes.UserNameIsNull;
				return false;
			}

			if (Username.Length < 6) //檢查帳號名稱長度是否至少6個字元
			{
				result = ErrorCodes.UserNameTooShort;
				return false;
			}

			if(Username.Any(char.IsWhiteSpace)) //檢查帳號名稱是否包含空格
			{
				result = ErrorCodes.UserNameSpace;
				return false;
			}


			if (!Regex.IsMatch(Username, "[a-zA-Z]")) //檢查帳號名稱是否包含至少一個英文字母
			{
				result = ErrorCodes.UserNameFormatError;
				return false;
			}

			result = "";
			return true;
		}

		private bool CheckEmail(string Email, out string result) //信箱格式限制
		{
			string[] parts = null!;
			string domain = string.Empty;

			if (string.IsNullOrEmpty(Email)) //檢查信箱是否為空
			{
				result = ErrorCodes.EmailIsNull;
				return false;
			}

			try //檢查信箱格式是否至少有一個 '@' 符號
			{
				parts = Email.Split('@');
				domain = parts[1];
			}
			catch
			{
				result = ErrorCodes.EmailFormatError;
				return false;
			}

			if (_context.Users.Any(u => u.Email == Email)) //檢查信箱是否已被使用
			{
				result = ErrorCodes.EmailRepeat;
				return false;
			}

			if (Email.Any(char.IsWhiteSpace)) //檢查信箱是否包含空格
			{
				result = ErrorCodes.EmailSpace;
				return false;
			}

			if (parts.Length != 2) //檢查信箱格式是否正確
			{
				result = ErrorCodes.EmailFormatError;
				return false;
			}

			if (!allowedDomains.Contains(domain)) //檢查信箱後綴是否為允許的域名
			{
				result = ErrorCodes.EmailFormError;
				return false;
			}

			result = "";
			return true;
		}

		private bool CheckStudentId(string Student_id, out string result) //學號格式限制
		{
			if (string.IsNullOrEmpty(Student_id)) //檢查學號是否為空
			{
				result = ErrorCodes.StudentIsNull;
				return false;
			}

			if (_context.Users.Any(u => u.Student_id == Student_id)) //檢查學號是否已被使用
			{
				result = ErrorCodes.StudentRepeat;
				return false;
			}

			if (Student_id.Any(char.IsWhiteSpace)) //檢查學號是否包含空格
			{
				result = ErrorCodes.StudentSpace;
				return false;
			}

			if (!Regex.IsMatch(Student_id, @"^\d{8}$")) //檢查學號格式是否符合8位數字
			{
				result = ErrorCodes.StudentFormatError;
				return false;
			}

			result = "";
			return true;
		}

		private bool CheckPhoneNumber(string Phonenumber, out string result) //電話格式限制
		{
			if (string.IsNullOrEmpty(Phonenumber)) //檢查電話是否為空
			{
				result = ErrorCodes.PhoneIsNull;
				return false;
			}

			if (_context.Users.Any(u => u.TelePhone == Phonenumber)) //檢查電話是否已被使用
			{
				result = ErrorCodes.PhoneRepeat;
				return false;
			}

			if (!Regex.IsMatch(Phonenumber, @"^09\d{2}\s?\d{3}\s?\d{3}$")) //檢查電話格式是否符合台灣手機號碼格式
			{
				result = ErrorCodes.PhoneFormatError;
				return false;
				//"電話格式錯誤，必須是台灣手機格式 09XX XXX XXX";
			}

			result = "";
			return true;
		}

		private bool CheckOldPassword(string Oldpassword, string Password, out string result) //檢查舊密碼
		{
			if (!BCrypt.Net.BCrypt.Verify(Oldpassword, Password)) //檢查舊密碼是否正確
			{
				result = ErrorCodes.PasswordsIncorrect;
				return false;
			}

			result = "";
			return true;
		}

		private bool CheckNewPassword(string Newpassword, string Confirmpassword, out string result) //新密碼格式限制
		{
			if (Newpassword.Length < 8) //檢查新密碼長度是否至少8個字元
			{
				result = ErrorCodes.PasswordsTooShort;
				return false;
			}

			if (Newpassword.Any(char.IsWhiteSpace)) //檢查新密碼是否包含空格
			{
				result = ErrorCodes.PasswordsSpace;
				return false;
			}

			if (!Regex.IsMatch(Newpassword, "[a-zA-Z]") || !Regex.IsMatch(Newpassword, "[0-9]")) //檢查新密碼是否符合規定
			{
				result = ErrorCodes.PasswordsFormatError;
				return false;
			}

			if (Newpassword != Confirmpassword) //檢查再次確認密碼是否與新密碼相同
			{
				result = ErrorCodes.PasswordsDoNotMatch;
				return false;
			}

			result = "";
			return true;
		}

		public async Task<Result<UserDataResponseDto>> GetUserDataAsync(int id)
		{
			var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

			if (user == null)
				return Result<UserDataResponseDto>.Failure(ErrorCodes.UserNotFound);

			return Result<UserDataResponseDto>.Success(new UserDataResponseDto
			{
				Message = "取得資料成功",
				User = _mapper.Map<UserDataVO>(user)
			});
		}

		public async Task<Result<UserDataResponseDto>> UpdateDataAsync(int id, UserDataRequestDto userDataDto)
		{
			var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
			User changeuser; //用來儲存需要更新的用戶資料
			var passwordfilled = 0; //用來計算密碼欄位是否有填寫

			if (user == null)
				return Result<UserDataResponseDto>.Failure(ErrorCodes.UserNotFound);

			changeuser = user;

			if (user.Username == userDataDto.Username && //如果沒有任何資料需要更新，直接返回成功訊息
				user.Email == userDataDto.Email &&
				user.Student_id == userDataDto.Student_id &&
				user.TelePhone == userDataDto.PhoneNumber &&
				string.IsNullOrEmpty(userDataDto.OldPassword) &&
				string.IsNullOrEmpty(userDataDto.NewPassword) &&
				string.IsNullOrEmpty(userDataDto.ConfirmPassword)
				)
			{
				return Result<UserDataResponseDto>.Success(new UserDataResponseDto
				{
					Message = "沒有需要更新的資料",
					User = _mapper.Map<UserDataVO>(user)
				});
			}

			if (user.Username != userDataDto.Username) //是否有修改帳號名稱
			{
				//檢查帳號名稱上次修改時間是否已經過30天
				if (!CheckModifiedName(user.Modified_name, out string modifiedNameResult))
				{
					return Result<UserDataResponseDto>.Failure(modifiedNameResult, new UserDataResponseDto
					{
						Message = "更新資料失敗："+ErrorMessages.GetMessage(modifiedNameResult),
						User = _mapper.Map<UserDataVO>(user)
					});
				}

				//檢查帳號名稱是否符合規定
				if (!CheckUsername(userDataDto.Username, out string usernameResult))
				{
					return Result<UserDataResponseDto>.Failure(usernameResult, new UserDataResponseDto
					{
						Message = "更新資料失敗："+ErrorMessages.GetMessage(usernameResult),
						User = _mapper.Map<UserDataVO>(user)
					});
				}

				changeuser.Username = userDataDto.Username;
				changeuser.Modified_name = DateTime.UtcNow; //更新帳號修改時間
			}

			if (user.Email != userDataDto.Email) //是否有修改信箱
			{
				//檢查信箱格式是否正確
				if (!CheckEmail(userDataDto.Email, out string emailResult))
				{
					return Result<UserDataResponseDto>.Failure(emailResult, new UserDataResponseDto
					{
						Message = "更新資料失敗：" + ErrorMessages.GetMessage(emailResult),
						User = _mapper.Map<UserDataVO>(user)
					});
				}

				changeuser.Email = userDataDto.Email;
			}

			if (user.Student_id != userDataDto.Student_id) //是否有修改學號
			{
				//檢查學號格式是否正確
				if (!CheckStudentId(userDataDto.Student_id, out string studentIdResult))
				{
					return Result<UserDataResponseDto>.Failure(studentIdResult, new UserDataResponseDto
					{
						Message = "更新資料失敗：" + ErrorMessages.GetMessage(studentIdResult),
						User = _mapper.Map<UserDataVO>(user)
					});
				}

				changeuser.Student_id = userDataDto.Student_id;
			}

			if (user.TelePhone != userDataDto.PhoneNumber) //是否有修改電話
			{
				//檢查電話格式是否正確
				if (!CheckPhoneNumber(userDataDto.PhoneNumber, out string phoneResult))
				{
					return Result<UserDataResponseDto>.Failure(phoneResult, new UserDataResponseDto
					{
						Message = "更新資料失敗：" + ErrorMessages.GetMessage(phoneResult),
						User = _mapper.Map<UserDataVO>(user)
					});
				}

				changeuser.TelePhone = userDataDto.PhoneNumber;
			}

			if (!string.IsNullOrEmpty(userDataDto.OldPassword)) passwordfilled++; //檢查舊密碼是否有填寫

			if (!string.IsNullOrEmpty(userDataDto.NewPassword)) passwordfilled++; //檢查新密碼是否有填寫

			if (!string.IsNullOrEmpty(userDataDto.ConfirmPassword)) passwordfilled++; //檢查再次確認密碼是否有填寫

			if (passwordfilled != 0 && passwordfilled != 3) //檢查舊密碼、新密碼或再次確認密碼是否同時填寫或同時為空
			{
				return Result<UserDataResponseDto>.Failure(ErrorCodes.PasswordsInputError, new UserDataResponseDto
				{
					Message = "更新資料失敗：" + ErrorMessages.GetMessage(ErrorCodes.PasswordsInputError),
					User = _mapper.Map<UserDataVO>(user)
				});
			}
			else if (passwordfilled == 3)
			{
				//檢查舊密碼是否正確
				//if (!CheckOldPassword(userDataDto.OldPassword, user.Password, out string oldPasswordResult))
				//{
				//	return Result<UserDataResponseDto>.Failure(oldPasswordResult, new UserDataResponseDto
				//	{
				//		Message = "更新資料失敗：" + ErrorMessages.GetMessage(oldPasswordResult),
				//		User = _mapper.Map<UserDataVO>(user)
				//	});
				//}

				//檢查新密碼是否符合規定
				if (!CheckNewPassword(userDataDto.NewPassword, userDataDto.ConfirmPassword, out string newPasswordResult))
				{
					return Result<UserDataResponseDto>.Failure(newPasswordResult, new UserDataResponseDto
					{
						Message = "更新資料失敗：" + ErrorMessages.GetMessage(newPasswordResult),
						User = _mapper.Map<UserDataVO>(user)
					});
				}

				//寄信二次確認
				//這裡可以加入寄信二次確認的邏輯

				//密碼加密處理
				changeuser.Password = BCrypt.Net.BCrypt.HashPassword(userDataDto.NewPassword);
			}

			changeuser.Modified_at = DateTime.UtcNow;

			_context.Users.Update(changeuser);
			await _context.SaveChangesAsync();

			return Result<UserDataResponseDto>.Success(new UserDataResponseDto
			{
				Message = "更新資料成功",
				User = _mapper.Map<UserDataVO>(changeuser)
			});
		}
	}
}