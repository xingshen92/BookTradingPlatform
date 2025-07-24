namespace BookTradingPlatform.Common
{
	public static class ErrorCodes
	{
		// 使用者相關錯誤碼
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
		public const string PasswordsDoNotMatch = "U5006";  // "U5006" = 新密碼與確認密碼不一致

		// 商品相關錯誤碼
		public const string ProductNotFound = "P0001";      // "P0001" = 找不到商品

		public const string ProductNameIsNull = "P1001";    // "P1001" = 商品名稱為空
		public const string ProductNameTooShort = "P1002";  // "P1002" = 商品名稱小於3個字元
		public const string ProductNameTooLong = "P1003";   // "P1003" = 商品名稱超過50個字元

		public const string ProductPublishingHouseIsNull = "P2001"; // "P2001" = 出版社為空
		public const string ProductPublishingHouseSpace = "P2002"; // "P2002" = 出版社包含空格
		public const string ProductPublishingHouseFormatError = "P2003"; // "P2003" = 出版社格式不正確

		public const string ProductPublishingAtIsNull = "P3001"; // "P3001" = 出版時間為空
		public const string ProductPublishingAtSpace = "P3002"; // "P3002" = 出版時間包含空格
		public const string ProductPublishingAtTimeError = "P3003"; // "P3003" = 出版時間大於當前時間
		public const string ProductPublishingAtFormatError = "P3004"; // "P3004" = 出版時間格式不正確d

		public const string ProductPriceIsNull = "P4001";     // "P4001" = 商品價格為空
		public const string ProductPriceIsNegative = "P4002";     // "P4002" = 商品價格為負數
		public const string ProductPriceTooHigh = "P4003";    // "P4003" = 商品價格高於100000元

		public const string ProductDescIsNull = "P5001";    // "P5001" = 商品描述為空
		public const string ProductDescTooLong = "P5002";   // "P5002" = 商品描述超過1000個字元

		public const string ProductImageIsNull = "P6001";   // "P6001" = 商品圖片為空
	}
}