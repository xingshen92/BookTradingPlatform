using System.Text.Json.Serialization;

namespace BookTradingPlatform.Common
{
	public class Result<T>
	{
		public bool IsSuccess { get; set; }
		public string? ErrorCode { get; set; }
		[JsonInclude]
		public string? ErrorMessage => ErrorCode != null ? ErrorMessages.GetMessage(ErrorCode) : null;
		public T? Data { get; set; }

		public static Result<T> Success(T data) => new()
		{
			IsSuccess = true,
			Data = data
		};

		public static Result<T> Failure(string code) => new()
		{
			IsSuccess = false,
			ErrorCode = code
		};
		// Overload，to include data in failure result
		public static Result<T> Failure(string code, T data) => new() 
		{ 
			IsSuccess = false, 
			ErrorCode = code,
			Data = data
		};
	}
}