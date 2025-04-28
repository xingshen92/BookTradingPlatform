namespace BookTradingPlatform.Vos
{
	public class UserTokenVO
	{
		public string Token { get; set; }
		public DateTime ExpireTime { get; set; }
		public string Username { get; set; }
		public string Role { get; set; }
	}
}
