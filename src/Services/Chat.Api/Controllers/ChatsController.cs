using Microsoft.AspNetCore.Mvc;

namespace Chat.Api.Controllers
{
    public class ChatsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
