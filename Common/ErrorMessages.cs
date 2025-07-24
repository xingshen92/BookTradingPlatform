namespace BookTradingPlatform.Common
{
	public static class ErrorMessages
	{
		private static readonly Dictionary<string, string> _messages = new()
		{
			{ ErrorCodes.UserNotFound, "找不到使用者" }, // 使用者相關錯誤碼
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
			{ ErrorCodes.PasswordsDoNotMatch, "新密碼與確認密碼不一致" },
			{ ErrorCodes.ProductNotFound, "找不到商品" }, // 商品相關錯誤碼
			{ ErrorCodes.ProductNameIsNull, "商品名稱為空" },
			{ ErrorCodes.ProductNameTooShort, "商品名稱小於3個字元" },
			{ ErrorCodes.ProductNameTooLong, "商品名稱超過50個字元" },
			{ ErrorCodes.ProductPublishingHouseIsNull, "出版社為空" },
			{ ErrorCodes.ProductPublishingHouseSpace, "出版社包含空格" },
			{ ErrorCodes.ProductPublishingHouseFormatError, "出版社格式不正確" },
			{ ErrorCodes.ProductPublishingAtIsNull, "出版時間為空" },
			{ ErrorCodes.ProductPublishingAtSpace, "出版時間包含空格" },
			{ ErrorCodes.ProductPublishingAtTimeError, "出版時間大於當前時間" },
			{ ErrorCodes.ProductPublishingAtFormatError, "出版時間格式不正確" },
			{ ErrorCodes.ProductPriceIsNull, "商品價格為空" },
			{ ErrorCodes.ProductPriceIsNegative, "商品價格為負數" },
			{ ErrorCodes.ProductPriceTooHigh, "商品價格高於100000元" },
			{ ErrorCodes.ProductDescIsNull, "商品描述為空" },
			{ ErrorCodes.ProductDescTooLong, "商品描述超過1000個字元" },
			{ ErrorCodes.ProductImageIsNull, "商品圖片為空" }
		};

		public static string GetMessage(string errorCode)
		{
			return _messages.TryGetValue(errorCode, out var message)
				? message
				: "未知錯誤";
		}
	}
}