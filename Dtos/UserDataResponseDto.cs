using BookTradingPlatform.Vos;

namespace BookTradingPlatform.Dtos
{
	public class UserDataResponseDto
	{
		// 修改後的結果訊息
		public string Message { get; set; }
		public UserDataVO User { get; set; }
	}
}