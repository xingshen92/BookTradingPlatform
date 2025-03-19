//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
////using BookTradingPlatform.Controllers
//using BookTradingPlatform.Models;
//using System.Security.Cryptography;
//using System.Text;
//
//namespace BookTradingPlatform.Controllers
//{
//	[Route("api/[controller]")]
//	[ApiController]
//	public class AuthController : ControllerBase
//	{
//		private readonly WebDatabase _context;
//
//		public AuthController(WebDatabase context)
//		{
//			_context = context;
//		}
//
//		[HttpPost("register")]
//		public async Task<IActionResult> Register([FromBody] User user)
//		{
//			if (await _context.Users.AnyAsync(u => u.Username == user.Username))
//			{
//				return BadRequest("User already exists.");
//			}
//
//			user.PasswordHash = HashPassword(user.PasswordHash);
//			_context.Users.Add(user);
//			await _context.SaveChangesAsync();
//
//			return Ok("User registered successfully.");
//		}
//
//		private string HashPassword(string password)
//		{
//			using (SHA256 sha256 = SHA256.Create())
//			{
//				byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
//				return Convert.ToBase64String(bytes);
//			}
//		}
//	}
//}
//