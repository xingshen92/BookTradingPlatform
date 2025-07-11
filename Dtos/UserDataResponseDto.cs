using BookTradingPlatform.Vos;

namespace BookTradingPlatform.Dtos
{
	public class UserDataResponseDto
	{
		// 修改資料是否成功
		public bool IsSuccess { get; set; }
		// 修改後的結果訊息
		public string Message { get; set; }
		public UserDataVO User { get; set; }
	}
}