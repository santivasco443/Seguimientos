using Microsoft.AspNetCore.Mvc;
using CalculadoraPropinas.Models;
using System.Diagnostics;

namespace CalculadoraPropinas.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            var modelo = new CalculadoraModel();

            return View(modelo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(CalculadoraModel modelo)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.MostrarResultado = false;

                return View(modelo);
            }

            ViewBag.MostrarResultado = true;

            return View(modelo);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(
            Duration = 0,
            Location = ResponseCacheLocation.None,
            NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id
                    ?? HttpContext.TraceIdentifier
            });
        }
    }
}