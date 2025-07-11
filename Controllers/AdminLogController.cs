using Microsoft.AspNetCore.Mvc;
using BookTradingPlatform.Services;

[ApiController]
[Route("api/[controller]")]
public class AdminLogController : ControllerBase
{
	private readonly AdminLogService _adminLogService;
	public AdminLogController(AdminLogService adminLogService)
	{
		_adminLogService = adminLogService;
	}

	// 取得管理員操作
	[HttpGet("log")]
	public async Task<IActionResult> GetAdminLogs()
	{
		var logs = await _adminLogService.GetAdminLogsAsync();

		if (logs == null || !logs.Any())
		{
			return NotFound("沒有管理員操作記錄");
		}

		return Ok(logs);
	}
}