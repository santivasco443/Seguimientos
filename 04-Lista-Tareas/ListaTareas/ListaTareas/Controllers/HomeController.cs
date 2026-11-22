using ListaTareas.Models;
using Microsoft.AspNetCore.Mvc;

namespace ListaTareas.Controllers
{
    public class HomeController : Controller
    {
        private static readonly List<TareaModel> tareas = new();
        private static int siguienteId = 1;

        [HttpGet]
        public IActionResult Index(string filtro = "todas")
        {
            ViewBag.Filtro = filtro;

            IEnumerable<TareaModel> resultado = tareas;

            if (filtro == "pendientes")
            {
                resultado = resultado.Where(t => !t.Completada);
            }
            else if (filtro == "completadas")
            {
                resultado = resultado.Where(t => t.Completada);
            }

            return View(resultado.OrderByDescending(t => t.FechaCreacion).ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Agregar(string descripcion)
        {
            if (!string.IsNullOrWhiteSpace(descripcion))
            {
                tareas.Add(new TareaModel
                {
                    Id = siguienteId++,
                    Descripcion = descripcion.Trim(),
                    FechaCreacion = DateTime.Now
                });
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Completar(int id)
        {
            TareaModel? tarea = tareas.FirstOrDefault(t => t.Id == id);

            if (tarea != null)
            {
                tarea.Completada = !tarea.Completada;
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(int id, string descripcion)
        {
            TareaModel? tarea = tareas.FirstOrDefault(t => t.Id == id);

            if (tarea != null && !string.IsNullOrWhiteSpace(descripcion))
            {
                tarea.Descripcion = descripcion.Trim();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Eliminar(int id)
        {
            TareaModel? tarea = tareas.FirstOrDefault(t => t.Id == id);

            if (tarea != null)
            {
                tareas.Remove(tarea);
            }

            return RedirectToAction("Index");
        }
    }
}