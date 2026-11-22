
using PlataformaRecetas.Models;
using Microsoft.AspNetCore.Mvc;

namespace PlataformaRecetas.Controllers
{
    public class HomeController : Controller
    {
        private static readonly List<RecetaModel> recetas = new();
        private static int siguienteId = 1;

        private static readonly string[] categorias =
        {
            "Desayunos",
            "Almuerzos",
            "Cenas",
            "Postres",
            "Bebidas",
            "Ensaladas",
            "Otros"
        };

        [HttpGet]
        public IActionResult Index(string categoria = "Todas")
        {
            ViewBag.Categorias = categorias;
            ViewBag.CategoriaSeleccionada = categoria;

            IEnumerable<RecetaModel> resultado = recetas;

            if (categoria != "Todas" && categorias.Contains(categoria))
            {
                resultado = resultado.Where(r => r.Categoria == categoria);
            }

            return View(resultado.OrderByDescending(r => r.FechaCreacion).ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Agregar(
            string nombre,
            string descripcion,
            string categoria,
            string ingredientes,
            string instrucciones,
            int tiempoPreparacion)
        {
            if (string.IsNullOrWhiteSpace(nombre) ||
                nombre.Trim().Length > 100 ||
                string.IsNullOrWhiteSpace(descripcion) ||
                descripcion.Trim().Length > 300 ||
                !categorias.Contains(categoria) ||
                string.IsNullOrWhiteSpace(ingredientes) ||
                ingredientes.Trim().Length > 2000 ||
                string.IsNullOrWhiteSpace(instrucciones) ||
                instrucciones.Trim().Length > 3000 ||
                tiempoPreparacion < 1 ||
                tiempoPreparacion > 1440)
            {
                TempData["Error"] = "Verifica los datos de la receta e intenta nuevamente.";
                return RedirectToAction("Index");
            }

            recetas.Add(new RecetaModel
            {
                Id = siguienteId++,
                Nombre = nombre.Trim(),
                Descripcion = descripcion.Trim(),
                Categoria = categoria,
                Ingredientes = ingredientes.Trim(),
                Instrucciones = instrucciones.Trim(),
                TiempoPreparacion = tiempoPreparacion,
                FechaCreacion = DateTime.Now
            });

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(
            int id,
            string nombre,
            string descripcion,
            string categoria,
            string ingredientes,
            string instrucciones,
            int tiempoPreparacion)
        {
            RecetaModel? receta = recetas.FirstOrDefault(r => r.Id == id);

            if (receta == null)
            {
                return RedirectToAction("Index");
            }

            if (string.IsNullOrWhiteSpace(nombre) ||
                nombre.Trim().Length > 100 ||
                string.IsNullOrWhiteSpace(descripcion) ||
                descripcion.Trim().Length > 300 ||
                !categorias.Contains(categoria) ||
                string.IsNullOrWhiteSpace(ingredientes) ||
                ingredientes.Trim().Length > 2000 ||
                string.IsNullOrWhiteSpace(instrucciones) ||
                instrucciones.Trim().Length > 3000 ||
                tiempoPreparacion < 1 ||
                tiempoPreparacion > 1440)
            {
                TempData["Error"] = "Verifica los datos de la receta.";
                return RedirectToAction("Index");
            }

            receta.Nombre = nombre.Trim();
            receta.Descripcion = descripcion.Trim();
            receta.Categoria = categoria;
            receta.Ingredientes = ingredientes.Trim();
            receta.Instrucciones = instrucciones.Trim();
            receta.TiempoPreparacion = tiempoPreparacion;

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Eliminar(int id)
        {
            RecetaModel? receta = recetas.FirstOrDefault(r => r.Id == id);

            if (receta != null)
            {
                recetas.Remove(receta);
            }

            return RedirectToAction("Index");
        }
    }
}
