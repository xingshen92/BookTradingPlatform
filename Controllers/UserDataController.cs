using BookTradingPlatform.Dtos;
using BookTradingPlatform.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class UserDataController : ControllerBase
{
	private readonly UserDataService _userdataService;
	private readonly AdminLogService _adminLogService;

	public UserDataController(UserDataService userdataService, AdminLogService adminLogService)
	{
		_userdataService = userdataService;
		_adminLogService = adminLogService;
	}

	//進入網頁，取得使用者資料
	[HttpGet("{id}")]
	public async Task<IActionResult> Getdata(int id)
	{
		var response = await _userdataService.GetUserDataAsync(id);

		if (response == null)
			return NotFound("使用者資料取得失敗");

		return Ok(response);
	}

	//更新使用者資料
	[HttpPut("{id}")]
	public async Task<IActionResult> UpdateData(int id, [FromBody] UserDataRequestDto userdataDto)
	{
		var response = await _userdataService.UpdateDataAsync(id, userdataDto);
		
		if (response == null)
			return NotFound("更新資料失敗：未回傳資料");
		
		if (!response.IsSuccess)
			return BadRequest(response);

		await _adminLogService.AddLogAdminAsync(id, "修改個人資料");

		return Ok(response);
	}
}