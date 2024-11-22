using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Gestion_Prestamos.Models;

namespace Gestion_Prestamos.Controllers
{
    public class InventariosController : Controller
    {
        private readonly GestionPrestamosContext _context;

        public InventariosController(GestionPrestamosContext context)
        {
            _context = context;
        }

        // GET: Inventarios
        public async Task<IActionResult> Index()
        {
            // Incluimos el Elemento para mostrar su nombre en la vista
            var gestionPrestamosContext = _context.Inventarios.Include(i => i.Elemento);
            return View(await gestionPrestamosContext.ToListAsync());
        }

        // GET: Inventarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventario = await _context.Inventarios
                .Include(i => i.Elemento)
                .FirstOrDefaultAsync(m => m.InventarioId == id);
            if (inventario == null)
            {
                return NotFound();
            }

            return View(inventario);
        }

        // GET: Inventarios/Create
        public IActionResult Create()
        {
            // Mostrar los nombres de los elementos en lugar del ID
            ViewData["ElementoId"] = new SelectList(_context.Elementos, "ElementoId", "Nombre");
            return View();
        }

        // POST: Inventarios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InventarioId,ElementoId,UnidadesTotales,UnidadesDisponibles")] Inventario inventario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(inventario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ElementoId"] = new SelectList(_context.Elementos, "ElementoId", "Nombre", inventario.ElementoId);
            return View(inventario);
        }

        // GET: Inventarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventario = await _context.Inventarios.FindAsync(id);
            if (inventario == null)
            {
                return NotFound();
            }
            // Mostrar los nombres de los elementos en lugar del ID
            ViewData["ElementoId"] = new SelectList(_context.Elementos, "ElementoId", "Nombre", inventario.ElementoId);
            return View(inventario);
        }

        // POST: Inventarios/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("InventarioId,ElementoId,UnidadesTotales,UnidadesDisponibles")] Inventario inventario)
        {
            if (id != inventario.InventarioId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(inventario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InventarioExists(inventario.InventarioId))
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
            ViewData["ElementoId"] = new SelectList(_context.Elementos, "ElementoId", "Nombre", inventario.ElementoId);
            return View(inventario);
        }

        // GET: Inventarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventario = await _context.Inventarios
                .Include(i => i.Elemento)
                .FirstOrDefaultAsync(m => m.InventarioId == id);
            if (inventario == null)
            {
                return NotFound();
            }

            return View(inventario);
        }

        // POST: Inventarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var inventario = await _context.Inventarios.FindAsync(id);
            if (inventario != null)
            {
                _context.Inventarios.Remove(inventario);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InventarioExists(int id)
        {
            return _context.Inventarios.Any(e => e.InventarioId == id);
        }
    }
}
