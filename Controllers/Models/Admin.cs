namespace BookTradingPlatform.Controllers.Models
{
	public class Admin
	{
		public int Id { get; set; }
		public string IP { get; set; }
		public DateTime login_at { get; set; }
		public DateTime Modified_at { get; set; } = DateTime.Now;
		public string work { get; set; }
	}
}
