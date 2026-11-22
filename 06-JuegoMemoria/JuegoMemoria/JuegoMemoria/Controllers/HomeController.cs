
using JuegoMemoria.Models;
using Microsoft.AspNetCore.Mvc;

namespace JuegoMemoria.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View(new JuegoMemoriaModel());
        }
    }
}
