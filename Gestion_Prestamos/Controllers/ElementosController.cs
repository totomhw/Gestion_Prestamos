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
    public class ElementosController : Controller
    {
        private readonly GestionPrestamosContext _context;

        public ElementosController(GestionPrestamosContext context)
        {
            _context = context;
        }

        // GET: Elementos
        public async Task<IActionResult> Index()
        {
            var gestionPrestamosContext = _context.Elementos.Include(e => e.Categoria);
            return View(await gestionPrestamosContext.ToListAsync());
        }

        // GET: Elementos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var elemento = await _context.Elementos
                .Include(e => e.Categoria)
                .FirstOrDefaultAsync(m => m.ElementoId == id);
            if (elemento == null)
            {
                return NotFound();
            }

            return View(elemento);
        }

        // GET: Elementos/Create
        public IActionResult Create()
        {
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "CategoriaId", "Nombre");
            return View();
        }

        // POST: Elementos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ElementoId,Nombre,Descripcion,FechaAdquisicion,Valor,EsActivo,CategoriaId")] Elemento elemento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(elemento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "CategoriaId", "Nombre", elemento.CategoriaId);
            return View(elemento);
        }

        // GET: Elementos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var elemento = await _context.Elementos.FindAsync(id);
            if (elemento == null)
            {
                return NotFound();
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "CategoriaId", "Nombre", elemento.CategoriaId);
            return View(elemento);
        }

        // POST: Elementos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ElementoId,Nombre,Descripcion,FechaAdquisicion,Valor,EsActivo,CategoriaId")] Elemento elemento)
        {
            if (id != elemento.ElementoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(elemento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ElementoExists(elemento.ElementoId))
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
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "CategoriaId", "Nombre", elemento.CategoriaId);
            return View(elemento);
        }

        // GET: Elementos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var elemento = await _context.Elementos
                .Include(e => e.Categoria)
                .FirstOrDefaultAsync(m => m.ElementoId == id);
            if (elemento == null)
            {
                return NotFound();
            }

            return View(elemento);
        }

        // POST: Elementos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var elemento = await _context.Elementos.FindAsync(id);
            if (elemento != null)
            {
                _context.Elementos.Remove(elemento);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Acción para la vista principal (Main)
        public async Task<IActionResult> Main()
        {
            // Obtener el inventario de elementos
            var inventario = await _context.Inventarios
                .Include(i => i.Elemento)
                .Select(i => new InventarioViewModel
                {
                    NombreElemento = i.Elemento.Nombre,
                    UnidadesTotales = i.UnidadesTotales,
                    UnidadesDisponibles = i.UnidadesDisponibles
                }).ToListAsync();

            // Obtener los últimos 5 préstamos
            var prestamosRecientes = await _context.Prestamos
                .Include(p => p.Elemento)
                .Include(p => p.Usuario)
                .OrderByDescending(p => p.FechaPrestamo)
                .Take(5)
                .Select(p => new PrestamoViewModel
                {
                    NombreElemento = p.Elemento.Nombre,
                    NombreUsuario = p.Usuario.Nombre,
                    UnidadesPrestadas = p.UnidadesPrestadas,
                    FechaPrestamo = p.FechaPrestamo
                }).ToListAsync();

            // Preparar el modelo de la vista
            var mainViewModel = new MainViewModel
            {
                Inventario = inventario,
                PrestamosRecientes = prestamosRecientes
            };

            // Devolver la vista con el modelo
            return View(mainViewModel);
        }

        private bool ElementoExists(int id)
        {
            return _context.Elementos.Any(e => e.ElementoId == id);
        }
    }
}