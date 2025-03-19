using Microsoft.AspNetCore.Mvc;

namespace BookTradingPlatform.Controllers
{
	public class LoginController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
