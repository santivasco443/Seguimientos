
using GestorNotas.Models;
using Microsoft.AspNetCore.Mvc;

namespace GestorNotas.Controllers
{
    public class HomeController : Controller
    {
        private static readonly List<NotaModel> notas = new();
        private static int siguienteId = 1;

        [HttpGet]
        public IActionResult Index()
        {
            return View(notas.OrderByDescending(n => n.FechaModificacion).ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Agregar(string titulo, string contenido)
        {
            if (string.IsNullOrWhiteSpace(titulo) ||
                string.IsNullOrWhiteSpace(contenido))
            {
                TempData["Error"] = "El título y el contenido son obligatorios.";
                return RedirectToAction("Index");
            }

            if (titulo.Trim().Length > 80 || contenido.Trim().Length > 2000)
            {
                TempData["Error"] = "El título o el contenido superan la longitud permitida.";
                return RedirectToAction("Index");
            }

            notas.Add(new NotaModel
            {
                Id = siguienteId++,
                Titulo = titulo.Trim(),
                Contenido = contenido.Trim(),
                FechaCreacion = DateTime.Now,
                FechaModificacion = DateTime.Now
            });

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(int id, string titulo, string contenido)
        {
            NotaModel? nota = notas.FirstOrDefault(n => n.Id == id);

            if (nota == null)
            {
                return RedirectToAction("Index");
            }

            if (string.IsNullOrWhiteSpace(titulo) ||
                string.IsNullOrWhiteSpace(contenido) ||
                titulo.Trim().Length > 80 ||
                contenido.Trim().Length > 2000)
            {
                TempData["Error"] = "Revisa el título y el contenido de la nota.";
                return RedirectToAction("Index");
            }

            nota.Titulo = titulo.Trim();
            nota.Contenido = contenido.Trim();
            nota.FechaModificacion = DateTime.Now;

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Eliminar(int id)
        {
            NotaModel? nota = notas.FirstOrDefault(n => n.Id == id);

            if (nota != null)
            {
                notas.Remove(nota);
            }

            return RedirectToAction("Index");
        }
    }
}
