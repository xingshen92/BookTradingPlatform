using BookTradingPlatform.Data;
using BookTradingPlatform.Models;

public class IAdminLogService
{
	private readonly WebDatabase _context;
	public IAdminLogService(WebDatabase context)
	{
		_context = context;
	}
	public async Task LogAdminActionAsync(User user, string ip, string workDescription){
	if (user.Role != "Admin") return;
	var log = new Adminlog
	{
		IP = ip,
		Login_at = DateTime.UtcNow,
		Modified_at = DateTime.UtcNow,
		Work = workDescription
	};
	_context.Adminlogs.Add(log);
	await _context.SaveChangesAsync();
	}
}