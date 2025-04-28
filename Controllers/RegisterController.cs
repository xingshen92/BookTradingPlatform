using BookTradingPlatform.Dtos;
using BookTradingPlatform.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookTradingPlatform.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegisterController : ControllerBase
    {
        private readonly RegisterService _registerService;

        public RegisterController(RegisterService registerService)
        {
            _registerService = registerService;
        }

        // 註冊API
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerDto)
        {
            // 呼叫 RegisterService 來處理註冊邏輯
            var response = await _registerService.RegisterAsync(registerDto);

            if (!response.IsSuccess)
            {
                // 註冊失敗，返回錯誤訊息
                return BadRequest(response.Message);
            }

            // 註冊成功，返回成功訊息
            return Ok(response);
        }
    }
}
