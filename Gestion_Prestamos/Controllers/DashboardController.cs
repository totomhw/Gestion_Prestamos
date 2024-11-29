using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Gestion_Prestamos.Models;

namespace Gestion_Prestamos.Controllers
{
    public class DashboardController : Controller
    {
        private readonly GestionPrestamosContext _context;

        public DashboardController(GestionPrestamosContext context)
        {
            _context = context;
        }

        // Acción para cargar los datos iniciales del dashboard
        public async Task<IActionResult> Index()
        {
            // Obtener datos generales
            var prestamosPorMes = await _context.Prestamos
                .GroupBy(p => new { p.FechaPrestamo.Year, p.FechaPrestamo.Month })
                .Select(g => new
                {
                    Mes = new DateTime(g.Key.Year, g.Key.Month, 1).ToString("yyyy-MM"),
                    Cantidad = g.Count()
                })
                .ToListAsync();

            var elementosMasPrestados = await _context.Prestamos
                .Include(p => p.Elemento)
                .GroupBy(p => p.Elemento.Nombre)
                .Select(g => new
                {
                    Elemento = g.Key,
                    Prestamos = g.Count()
                })
                .OrderByDescending(g => g.Prestamos)
                .Take(5)
                .ToListAsync();

            var usuariosMasActivos = await _context.Prestamos
                .Include(p => p.Usuario)
                .GroupBy(p => p.Usuario.Nombre)
                .Select(g => new
                {
                    Usuario = g.Key,
                    Prestamos = g.Count()
                })
                .OrderByDescending(g => g.Prestamos)
                .Take(5)
                .ToListAsync();

            var unidadesTotalesDisponibles = await _context.Inventarios
                .Include(i => i.Elemento)
                .Select(i => new
                {
                    Elemento = i.Elemento.Nombre,
                    Totales = i.UnidadesTotales,
                    Disponibles = i.UnidadesDisponibles
                })
                .ToListAsync();

            // Pasar los datos a la vista a través de ViewBag
            ViewBag.PrestamosPorMesLabels = prestamosPorMes.Select(p => p.Mes).ToArray();
            ViewBag.PrestamosPorMesData = prestamosPorMes.Select(p => p.Cantidad).ToArray();
            ViewBag.ElementosMasPrestadosLabels = elementosMasPrestados.Select(e => e.Elemento).ToArray();
            ViewBag.ElementosMasPrestadosData = elementosMasPrestados.Select(e => e.Prestamos).ToArray();
            ViewBag.UsuariosMasActivosLabels = usuariosMasActivos.Select(u => u.Usuario).ToArray();
            ViewBag.UsuariosMasActivosData = usuariosMasActivos.Select(u => u.Prestamos).ToArray();
            ViewBag.InventarioLabels = unidadesTotalesDisponibles.Select(i => i.Elemento).ToArray();
            ViewBag.InventarioTotales = unidadesTotalesDisponibles.Select(i => i.Totales).ToArray();
            ViewBag.InventarioDisponibles = unidadesTotalesDisponibles.Select(i => i.Disponibles).ToArray();

            return View();
        }

        // Acción para filtrar datos
        [HttpPost]
        public async Task<IActionResult> FiltrarDatos(DateTime? fechaInicio, DateTime? fechaFin, string categoria)
        {
            var prestamosQuery = _context.Prestamos.AsQueryable();

            // Aplicar filtros
            if (fechaInicio.HasValue)
                prestamosQuery = prestamosQuery.Where(p => p.FechaPrestamo >= fechaInicio.Value);

            if (fechaFin.HasValue)
                prestamosQuery = prestamosQuery.Where(p => p.FechaPrestamo <= fechaFin.Value);

            if (!string.IsNullOrEmpty(categoria))
                prestamosQuery = prestamosQuery.Where(p => p.Elemento.Categoria.Nombre == categoria);

            // Obtener datos filtrados
            var prestamosPorMes = await prestamosQuery
                .GroupBy(p => new { p.FechaPrestamo.Year, p.FechaPrestamo.Month })
                .Select(g => new
                {
                    Mes = new DateTime(g.Key.Year, g.Key.Month, 1).ToString("yyyy-MM"),
                    Cantidad = g.Count()
                })
                .ToListAsync();

            var elementosMasPrestados = await prestamosQuery
                .Include(p => p.Elemento)
                .GroupBy(p => p.Elemento.Nombre)
                .Select(g => new
                {
                    Elemento = g.Key,
                    Prestamos = g.Count()
                })
                .OrderByDescending(g => g.Prestamos)
                .Take(5)
                .ToListAsync();

            var usuariosMasActivos = await prestamosQuery
                .Include(p => p.Usuario)
                .GroupBy(p => p.Usuario.Nombre)
                .Select(g => new
                {
                    Usuario = g.Key,
                    Prestamos = g.Count()
                })
                .OrderByDescending(g => g.Prestamos)
                .Take(5)
                .ToListAsync();

            var unidadesTotalesDisponibles = await _context.Inventarios
                .Include(i => i.Elemento)
                .Select(i => new
                {
                    Elemento = i.Elemento.Nombre,
                    Totales = i.UnidadesTotales,
                    Disponibles = i.UnidadesDisponibles
                })
                .ToListAsync();

            // Retornar los datos en formato JSON para los gráficos
            return Json(new
            {
                prestamosPorMes,
                elementosMasPrestados,
                usuariosMasActivos,
                unidadesTotalesDisponibles
            });
        }
    }
}
