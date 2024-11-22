using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Gestion_Prestamos.Models;

namespace Gestion_Prestamos.Controllers
{
    public class PrestamosController : Controller
    {
        private readonly GestionPrestamosContext _context;

        public PrestamosController(GestionPrestamosContext context)
        {
            _context = context;
        }

        // GET: Prestamos
        public async Task<IActionResult> Index()
        {
            var gestionPrestamosContext = _context.Prestamos
                .Include(p => p.Elemento)
                .Include(p => p.Usuario);
            return View(await gestionPrestamosContext.ToListAsync());
        }

        // GET: Prestamos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prestamo = await _context.Prestamos
                .Include(p => p.Elemento)
                .Include(p => p.Usuario)
                .FirstOrDefaultAsync(m => m.PrestamoId == id);
            if (prestamo == null)
            {
                return NotFound();
            }

            return View(prestamo);
        }

        // GET: Prestamos/Create
        public IActionResult Create()
        {
            ViewData["ElementoId"] = new SelectList(_context.Elementos, "ElementoId", "Nombre");
            return View();
        }

        // POST: Prestamos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PrestamoId,ElementoId,UnidadesPrestadas,FechaPrestamo,FechaDevolucion,FechaEntrega,Observaciones")] Prestamo prestamo)
        {
            if (ModelState.IsValid)
            {
                // Asigna el ID del usuario autenticado al préstamo
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                prestamo.UsuarioId = int.Parse(userId);

                _context.Add(prestamo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["ElementoId"] = new SelectList(_context.Elementos, "ElementoId", "Nombre", prestamo.ElementoId);
            return View(prestamo);
        }

        // GET: Prestamos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prestamo = await _context.Prestamos.FindAsync(id);
            if (prestamo == null)
            {
                return NotFound();
            }

            // Verifica si el usuario autenticado es el mismo que creó el préstamo
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (prestamo.UsuarioId != int.Parse(userId))
            {
                return Forbid();
            }

            ViewData["ElementoId"] = new SelectList(_context.Elementos, "ElementoId", "Nombre", prestamo.ElementoId);
            return View(prestamo);
        }

        // POST: Prestamos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PrestamoId,ElementoId,UnidadesPrestadas,FechaPrestamo,FechaDevolucion,FechaEntrega,Observaciones")] Prestamo prestamo)
        {
            if (id != prestamo.PrestamoId)
            {
                return NotFound();
            }

            // Verificar que el usuario autenticado sea el creador del préstamo
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var prestamoExistente = await _context.Prestamos.AsNoTracking().FirstOrDefaultAsync(p => p.PrestamoId == id);
            if (prestamoExistente == null || prestamoExistente.UsuarioId != int.Parse(userId))
            {
                return Forbid();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    prestamo.UsuarioId = int.Parse(userId); // Mantener el UsuarioId sin cambios
                    _context.Update(prestamo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrestamoExists(prestamo.PrestamoId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ElementoId"] = new SelectList(_context.Elementos, "ElementoId", "Nombre", prestamo.ElementoId);
            return View(prestamo);
        }

        // GET: Prestamos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prestamo = await _context.Prestamos
                .Include(p => p.Elemento)
                .Include(p => p.Usuario)
                .FirstOrDefaultAsync(m => m.PrestamoId == id);
            if (prestamo == null)
            {
                return NotFound();
            }

            return View(prestamo);
        }

        // POST: Prestamos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var prestamo = await _context.Prestamos.FindAsync(id);
            if (prestamo != null)
            {
                _context.Prestamos.Remove(prestamo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PrestamoExists(int id)
        {
            return _context.Prestamos.Any(e => e.PrestamoId == id);
        }
    }
}
