namespace BookTradingPlatform.Common
{
	public static class ErrorMessages
	{
		private static readonly Dictionary<string, string> _messages = new()
		{
			{ ErrorCodes.UserNotFound, "找不到使用者" },
			{ ErrorCodes.NameTimeRefuse,"帳號名稱在30天內進行了修改"},
			{ ErrorCodes.UserNameIsNull, "帳號名為空" },
			{ ErrorCodes.UserNameTooShort, "帳號名小於6個字元" },
			{ ErrorCodes.UserNameSpace, "帳號名包含空格" },
			{ ErrorCodes.UserNameFormatError, "帳號名不包含英文字母" },
			{ ErrorCodes.EmailIsNull, "信箱為空" },
			{ ErrorCodes.EmailRepeat, "信箱已被使用" },
			{ ErrorCodes.EmailSpace, "信箱包含空格" },
			{ ErrorCodes.EmailFormatError, "信箱格式不正確" },
			{ ErrorCodes.EmailFormError, "信箱不是學校或gmail帳號" },
			{ ErrorCodes.StudentIsNull, "學號為空" },
			{ ErrorCodes.StudentRepeat, "學號已被使用" },
			{ ErrorCodes.StudentSpace, "學號包含空格" },
			{ ErrorCodes.StudentFormatError, "學號不是8位數字" },
			{ ErrorCodes.PhoneIsNull, "電話號碼為空" },
			{ ErrorCodes.PhoneRepeat, "電話號碼已被使用" },
			{ ErrorCodes.PhoneFormatError, "電話格式不是台灣手機格式" },
			{ ErrorCodes.PasswordsInputError, "舊密碼、新密碼及再次確認密碼不是同時填寫" },
			{ ErrorCodes.PasswordsIncorrect, "密碼錯誤" },
			{ ErrorCodes.PasswordsTooShort, "新密碼小於8個字元" },
			{ ErrorCodes.PasswordsSpace, "新密碼包含空格" },
			{ ErrorCodes.PasswordsFormatError, "新密碼不包含英文字母或一個數字" },
			{ ErrorCodes.PasswordsDoNotMatch, "新密碼與確認密碼不一致" }
		};

		public static string GetMessage(string errorCode)
		{
			return _messages.TryGetValue(errorCode, out var message)
				? message
				: "未知錯誤";
		}
	}
}