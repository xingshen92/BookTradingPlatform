namespace BookTradingPlatform.Bos
{
	public class UserBO
	{
		public string MemberNumber { get; set; } = string.Empty;
		public string Username { get; set; } = string.Empty;
		public string Email { get; set; } = string.Empty;
		public string PasswordHash { get; set; } = string.Empty;
		public string StudentId { get; set; } = string.Empty;
		public string Department { get; set; } = string.Empty;
		public string Telephone { get; set; } = string.Empty;
		public string Role { get; set; } = string.Empty;
		public DateTime ModifiedAt { get; set; }

		// 轉換成PO
		public Models.User ToPersistenceObject()
		{
			return new Models.User
			{
				MemberNumber = this.MemberNumber, 
				Username = this.Username,
				Email = this.Email,
				Password = this.PasswordHash,
				Student_id = this.StudentId,
				Department = this.Department,
				TelePhone = this.Telephone,
				Role = this.Role,
				Modified_at = this.ModifiedAt
			};
		}

		public UserBO() { }
	}
}
