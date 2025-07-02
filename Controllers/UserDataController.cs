using BookTradingPlatform.Dtos;
using BookTradingPlatform.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class UserDataController : ControllerBase
{
	private readonly UserDataService _userdataService;

	public UserDataController(UserDataService userdataService)
	{
		_userdataService = userdataService;
	}

	//進入網頁，取得使用者資料
	[HttpGet("{id}")]
	public async Task<IActionResult> Getdata(int id)
	{
		var response = await _userdataService.GetUserDataAsync(id);

		if (response == null)
			return NotFound("使用者資料不存在");

		return Ok(response);
	}

	//更新使用者資料
	//[HttpPut("{id}")]
	//public async Task<IActionResult> UpdateData(int id, [FromBody] UserDataRequestDto userdataDto)
	//{
	//	var response = await _userdataService.UpdateDataAsync(id, userdataDto);
	//	//如果回傳的response為null，則表示更新失敗
	//	if (response == null)
	//		return NotFound("更新資料失敗：未回傳資料");
	//	//如果回傳的response.IsSuccess為false，則表示更新失敗
	//	if (!response.IsSuccess)
	//		return BadRequest("更新資料失敗"+response.Message);
	//	return Ok(response);
	//}	
}


