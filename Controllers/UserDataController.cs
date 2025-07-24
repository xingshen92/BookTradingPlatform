using BookTradingPlatform.Dtos;
using BookTradingPlatform.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class UserDataController : ControllerBase
{
	private readonly IUserDataService _userdataService;
	private readonly IAdminLogService _adminLogService;

	public UserDataController(IUserDataService userdataService, IAdminLogService adminLogService)
	{
		_userdataService = userdataService;
		_adminLogService = adminLogService;
	}

	//進入網頁，取得使用者資料
	[HttpGet("{id}")]
	public async Task<IActionResult> Getdata(int id)
	{
		var response = await _userdataService.GetUserDataAsync(id);

		if (!response.IsSuccess)
			return NotFound(new {IsSuccess = response.IsSuccess, ErrorCode = response.ErrorCode, ErrorMessage = response.ErrorMessage});

		return Ok(new {IsSuccess = response.IsSuccess, Data = response.Data});
	}

	//更新使用者資料
	[HttpPut("{id}")]
	public async Task<IActionResult> UpdateData(int id, [FromBody] UserDataRequestDto userdataDto)
	{
		var response = await _userdataService.UpdateDataAsync(id, userdataDto);
		
		if (!response.IsSuccess)
			return BadRequest(new {IsSuccess = response.IsSuccess, ErrorCode = response.ErrorCode, ErrorMessage = response.ErrorMessage, Data = response.Data });

		await _adminLogService.AddLogAdminAsync(id, "修改個人資料");

		return Ok(new {IsSuccess = response.IsSuccess, Data = response.Data});
	}
}