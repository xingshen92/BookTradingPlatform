namespace BookTradingPlatform.Vos
{
	public class UserTokenVO
	{
		public string Token { get; set; } = string.Empty;
		public DateTime ExpireTime { get; set; }
		public string Username { get; set; } = string.Empty;
		public string Role { get; set; } = string.Empty;

		public UserTokenVO() { }
	}
}