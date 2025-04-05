using BookTradingPlatform.Data;
using BookTradingPlatform.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    // 假設在某個 Controller 裡
private readonly WebDatabase _context;

public UserController(WebDatabase context)
{
    _context = context;
}

[HttpGet("test-db")]
public IActionResult TestDatabase()
{
    // 1) 測試讀取
    var user = _context.Users.FirstOrDefault(); 
    // 若資料表本來就有資料，看看是否能正常抓到

    // 2) 測試寫入
    var newUser = new User
    {
        Username = "testuser",
        Password = "123456",
        Email = "test@xxx.com",
        Student_id = 123,
        Department = "測試科系",
        TelePhone = "0912345678",
        Modified_at = DateTime.Now,
        Modified_name = DateTime.Now,
        Role = "User"
    };
    _context.Users.Add(newUser);
    _context.SaveChanges(); // 寫入 DB

    return Ok("資料庫測試成功");
}

//    private readonly WebDatabase _context;
//
//    public UserController(WebDatabase context)
//    {
//        _context = context;
//    }
//
//    // 取得所有使用者資料
//    [HttpGet]
//    public async Task<ActionResult<IEnumerable<User>>> GetUsers()
//    {
//        return await _context.Users.ToListAsync();
//    }
}
