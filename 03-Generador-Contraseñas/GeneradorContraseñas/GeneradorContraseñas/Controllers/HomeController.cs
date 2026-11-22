using GeneradorContraseñas.Models;
using Microsoft.AspNetCore.Mvc;

namespace GeneradorContraseñas.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View(new GeneradorModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(GeneradorModel modelo)
        {
            if (!modelo.Mayusculas && !modelo.Minusculas &&
                !modelo.Numeros && !modelo.Simbolos)
            {
                ModelState.AddModelError("", "Selecciona al menos un tipo de carácter.");
            }

            if (!ModelState.IsValid)
            {
                return View(modelo);
            }

            modelo.Resultado = modelo.Generar();

            return View(modelo);
        }
    }
}