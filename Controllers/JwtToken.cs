using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using BookTradingPlatform.Models;


namespace BookTradingPlatform.Services
{
	public class JwtToken
	{
		private readonly IConfiguration _config;

		public JwtToken(IConfiguration config)
		{
			_config = config;
		}

		public string GenerateToken(User user)
		{
			var claims = new[]
			{
				new Claim(ClaimTypes.Name, user.Username),
				new Claim(ClaimTypes.Role, user.Role)
			};

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			var token = new JwtSecurityToken(
				issuer: _config["Jwt:Issuer"],
				audience: _config["Jwt:Audience"],
				claims: claims,
				expires: DateTime.Now.AddHours(1),
				signingCredentials: creds
			);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}
		public ClaimsPrincipal? ValidateToken(string token)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);

			try
			{
				var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
				{
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidateLifetime = true,
					ValidateIssuerSigningKey = true,
					ValidIssuer = _config["Jwt:Issuer"],
					ValidAudience = _config["Jwt:Audience"],
					IssuerSigningKey = new SymmetricSecurityKey(key),
					ClockSkew = TimeSpan.Zero
				}, out SecurityToken validatedToken);

				return principal;
			}
			catch
			{
				return null;
			}
		}
	}
}
