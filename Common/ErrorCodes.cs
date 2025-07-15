namespace BookTradingPlatform.Common
{
	public static class ErrorCodes
	{
		public const string UserNotFound = "U0001";			// "U0001" = 找不到使用者

		public const string NameTimeRefuse = "U1001";		// "U1001" = 帳號名稱在30天內進行了修改
		public const string UserNameIsNull = "U1002";		// "U1002" = 帳號名為空
		public const string UserNameTooShort = "U1003";		// "U1003" = 帳號名小於6個字元
		public const string UserNameSpace = "U1004";		// "U1004" = 帳號名包含空格
		public const string UserNameFormatError = "U1005";	// "U1005" = 帳號名不包含英文字母

		public const string EmailIsNull = "U2001";          // "U2001" = 信箱為空
		public const string EmailRepeat = "U2002";          // "U2002" = 信箱已被使用
		public const string EmailSpace = "U2003";           // "U2003" = 信箱包含空格
		public const string EmailFormatError = "U2004";     // "U2004" = 信箱格式不正確
		public const string EmailFormError = "U2005";       // "U2005" = 信箱不是學校或gmail帳號

		public const string StudentIsNull = "U3001";        // "U3001" = 學號為空
		public const string StudentRepeat = "U3002";        // "U3002" = 學號已被使用
		public const string StudentSpace = "U3003";			// "U3003" = 學號包含空格
		public const string StudentFormatError = "U3004";	// "U3004" = 學號不是8位數字

		public const string PhoneIsNull = "U4001";          // "U4001" = 電話號碼為空
		public const string PhoneRepeat = "U4002";          // "U4002" = 電話號碼已被使用
		public const string PhoneFormatError = "U4003";		// "U4003" = 電話格式不是台灣手機格式

		public const string PasswordsInputError = "U5001";	// "U5001" = 舊密碼、新密碼及再次確認密碼不是同時填寫
		public const string PasswordsIncorrect = "U5002";	// "U5002" = 密碼錯誤
		public const string PasswordsTooShort = "U5003";	// "U5003" = 新密碼小於8個字元
		public const string PasswordsSpace = "U5004";		// "U5004" = 新密碼包含空格
		public const string PasswordsFormatError = "U5005";	// "U5005" = 新密碼不包含英文字母或一個數字
		public const string PasswordsDoNotMatch = "U5006";	// "U5006" = 新密碼與確認密碼不一致
	}
}