namespace BookTradingPlatform.Bos
{
	public class UserBO
	{
		public string Username { get; set; }
		public string Email { get; set; }
		public string PasswordHash { get; set; }
		public string StudentId { get; set; }
		public string Department { get; set; }
		public string Telephone { get; set; }
		public string Role { get; set; }
		public DateTime ModifiedAt { get; set; }

		// 轉換成PO
		public Models.User ToPersistenceObject()
		{
			return new Models.User
			{
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
	}
}
