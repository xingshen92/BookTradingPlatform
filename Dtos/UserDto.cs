namespace BookTradingPlatform.Dtos
{
	public class UserDto
	{
		public int Id { get; set; }
		public string Username { get; set; }
		public string Email { get; set; }
		public string Role { get; set; }
		public string Student_id { get; internal set; }
		public string Department { get; internal set; }
		public string Telephone { get; internal set; }
	}
}
