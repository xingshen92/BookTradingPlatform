using BookTradingPlatform.Data;
using BookTradingPlatform.Models;
using Microsoft.EntityFrameworkCore;

namespace BookTradingPlatform.Services
{
	public interface IAdminLogService
	{
		Task AddLogAdminAsync(int id, string workDescription);
		Task<List<Adminlog>> GetAdminLogsAsync();
	}

	class AdminLogService: IAdminLogService
	{
		private readonly WebDatabase _context;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public AdminLogService(WebDatabase context, IHttpContextAccessor httpContextAccessor)
		{
			_context = context;
			_httpContextAccessor = httpContextAccessor;
		}

		//取得ip
		private string GetIp()
		{
			string ip = _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? "Unknown IP";

			return ip;
		}

		public async Task AddLogAdminAsync(int id, string workDescription)
		{
			var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

			if (user == null)
			{
				var log = new Adminlog
				{
					MemberNumber = "Unknown Member",
					IP = GetIp(),
					Login_at = DateTime.UtcNow,
					Modified_at = DateTime.UtcNow,
					Work = "未能找到使用者資訊"
				};

				_context.Adminlogs.Add(log);
			}
			else
			{
				var log = new Adminlog
				{
					MemberNumber = user.MemberNumber,
					IP = GetIp(),
					Login_at = DateTime.UtcNow,
					Modified_at = DateTime.UtcNow,
					Work = workDescription
				};

				_context.Adminlogs.Add(log);
			}

			await _context.SaveChangesAsync();
		}

		public async Task<List<Adminlog>> GetAdminLogsAsync()
		{
			// 取得所有管理員操作記錄
			var log = await _context.Adminlogs.ToListAsync();

			if (log == null || !log.Any())
			{
				return new List<Adminlog>(); // 如果沒有記錄，返回空列表
			}

			return log;
		}
	}
}