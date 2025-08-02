namespace BookTradingPlatform.Dtos
{
	public class UserDto
	{
		public int Id { get; set; }
		public string MemberNumber { get; set; } = string.Empty; //用戶ID(帳號) 
		public string Username { get; set; } = string.Empty;
		public string Email { get; set; } = string.Empty;
		public string Role { get; set; } = string.Empty;
		public string Student_id { get; internal set; } = string.Empty;
		public string Department { get; internal set; } = string.Empty;
		public string Telephone { get; internal set; } = string.Empty;

		public UserDto() { }
	}
}
