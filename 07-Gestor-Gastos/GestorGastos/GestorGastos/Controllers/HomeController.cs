
using GestorGastos.Models;
using Microsoft.AspNetCore.Mvc;

namespace GestorGastos.Controllers
{
    public class HomeController : Controller
    {
        private static readonly List<GastoModel> gastos = new();
        private static int siguienteId = 1;

        private static readonly string[] categorias =
        {
            "Alimentación",
            "Transporte",
            "Vivienda",
            "Entretenimiento",
            "Salud",
            "Educación",
            "Servicios",
            "Otros"
        };

        [HttpGet]
        public IActionResult Index(string categoria = "Todas")
        {
            ViewBag.Categorias = categorias;
            ViewBag.CategoriaSeleccionada = categoria;
            ViewBag.TotalGeneral = gastos.Sum(g => g.Monto);

            IEnumerable<GastoModel> resultado = gastos;

            if (categoria != "Todas" && categorias.Contains(categoria))
            {
                resultado = resultado.Where(g => g.Categoria == categoria);
            }

            return View(resultado.OrderByDescending(g => g.Fecha)
                .ThenByDescending(g => g.Id).ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Agregar(string descripcion, string categoria, decimal monto, DateTime fecha)
        {
            if (string.IsNullOrWhiteSpace(descripcion) ||
                descripcion.Trim().Length > 100 ||
                !categorias.Contains(categoria) ||
                monto <= 0 ||
                monto > 999999999 ||
                fecha == DateTime.MinValue)
            {
                TempData["Error"] = "Verifica los datos del gasto e intenta nuevamente.";
                return RedirectToAction("Index");
            }

            gastos.Add(new GastoModel
            {
                Id = siguienteId++,
                Descripcion = descripcion.Trim(),
                Categoria = categoria,
                Monto = monto,
                Fecha = fecha.Date
            });

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Eliminar(int id)
        {
            GastoModel? gasto = gastos.FirstOrDefault(g => g.Id == id);

            if (gasto != null)
            {
                gastos.Remove(gasto);
            }

            return RedirectToAction("Index");
        }
    }
}
