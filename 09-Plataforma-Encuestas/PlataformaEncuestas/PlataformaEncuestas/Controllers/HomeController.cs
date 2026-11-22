
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlataformaEncuestas.Data;
using PlataformaEncuestas.Models;

namespace PlataformaEncuestas.Controllers;

public class HomeController : Controller
{
    private readonly EncuestasContext _context;

    public HomeController(EncuestasContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var encuestas = await _context.Encuestas
            .Include(e => e.Opciones)
            .Include(e => e.Votos)
            .OrderByDescending(e => e.FechaCreacion)
            .ToListAsync();

        var identificador = Request.Cookies["EncuestasVotante"];

        ViewBag.EncuestasVotadas = identificador == null
            ? new List<int>()
            : await _context.Votos
                .Where(v => v.IdentificadorVotante == identificador)
                .Select(v => v.EncuestaId)
                .ToListAsync();

        return View(encuestas);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Crear(
        string titulo,
        string descripcion,
        string pregunta,
        List<string> opciones)
    {
        if (string.IsNullOrWhiteSpace(titulo) ||
            string.IsNullOrWhiteSpace(pregunta) ||
            opciones == null)
        {
            TempData["Error"] = "Completa los campos obligatorios.";
            return RedirectToAction(nameof(Index));
        }

        var opcionesLimpias = opciones
            .Where(o => !string.IsNullOrWhiteSpace(o))
            .Select(o => o.Trim())
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToList();

        if (opcionesLimpias.Count < 2 || opcionesLimpias.Count > 10)
        {
            TempData["Error"] = "La encuesta debe tener entre 2 y 10 opciones diferentes.";
            return RedirectToAction(nameof(Index));
        }

        var encuesta = new EncuestaModel
        {
            Titulo = titulo.Trim(),
            Descripcion = descripcion?.Trim() ?? string.Empty,
            Pregunta = pregunta.Trim(),
            Publicada = true,
            FechaCreacion = DateTime.Now,
            Opciones = opcionesLimpias.Select(texto => new OpcionModel
            {
                Texto = texto
            }).ToList()
        };

        _context.Encuestas.Add(encuesta);
        await _context.SaveChangesAsync();

        TempData["Mensaje"] = "Encuesta creada correctamente.";

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Votar(int encuestaId, int opcionId)
    {
        var encuesta = await _context.Encuestas
            .Include(e => e.Opciones)
            .FirstOrDefaultAsync(e => e.Id == encuestaId);

        if (encuesta == null || !encuesta.Publicada ||
            !encuesta.Opciones.Any(o => o.Id == opcionId))
        {
            TempData["Error"] = "La encuesta o la opción seleccionada no es válida.";
            return RedirectToAction(nameof(Index));
        }

        var identificador = Request.Cookies["EncuestasVotante"];

        if (string.IsNullOrWhiteSpace(identificador))
        {
            identificador = Guid.NewGuid().ToString();

            Response.Cookies.Append("EncuestasVotante", identificador, new CookieOptions
            {
                HttpOnly = true,
                Secure = Request.IsHttps,
                SameSite = SameSiteMode.Lax,
                Expires = DateTimeOffset.UtcNow.AddYears(1)
            });
        }

        var votoExistente = await _context.Votos.AnyAsync(v =>
            v.EncuestaId == encuestaId &&
            v.IdentificadorVotante == identificador);

        if (votoExistente)
        {
            TempData["Error"] = "Ya has votado en esta encuesta.";
            return RedirectToAction(nameof(Index));
        }

        _context.Votos.Add(new VotoModel
        {
            EncuestaId = encuestaId,
            OpcionId = opcionId,
            IdentificadorVotante = identificador,
            FechaVoto = DateTime.Now
        });

        try
        {
            await _context.SaveChangesAsync();
            TempData["Mensaje"] = "Tu voto se registró correctamente.";
        }
        catch (DbUpdateException)
        {
            _context.ChangeTracker.Clear();
            TempData["Error"] = "Ya has votado en esta encuesta.";
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CambiarEstado(int id)
    {
        var encuesta = await _context.Encuestas.FindAsync(id);

        if (encuesta == null)
        {
            return NotFound();
        }

        encuesta.Publicada = !encuesta.Publicada;

        await _context.SaveChangesAsync();

        TempData["Mensaje"] = encuesta.Publicada
            ? "Encuesta publicada."
            : "Encuesta cerrada.";

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Eliminar(int id)
    {
        var encuesta = await _context.Encuestas.FindAsync(id);

        if (encuesta == null)
        {
            return NotFound();
        }

        _context.Encuestas.Remove(encuesta);
        await _context.SaveChangesAsync();

        TempData["Mensaje"] = "Encuesta eliminada correctamente.";

        return RedirectToAction(nameof(Index));
    }
}
