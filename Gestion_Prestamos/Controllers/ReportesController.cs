using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Gestion_Prestamos.Models;

namespace Gestion_Prestamos.Controllers
{
    public class ReportesController : Controller
    {
        private readonly GestionPrestamosContext _context;

        public ReportesController(GestionPrestamosContext context)
        {
            _context = context;
        }

        // GET: Reporte de Préstamos con filtros
        public async Task<IActionResult> PrestamosReporte(string usuario, string categoria, DateTime? fechaInicio, DateTime? fechaFin)
        {
            // Obtener lista de categorías y usuarios para los filtros
            ViewData["Categorias"] = new SelectList(_context.Categorias, "CategoriaId", "Nombre");
            ViewData["Usuarios"] = new SelectList(_context.Usuarios, "UsuarioId", "Nombre");

            // Obtener préstamos con los filtros aplicados
            var prestamos = _context.Prestamos.Include(p => p.Elemento).Include(p => p.Usuario).AsQueryable();

            // Filtro por usuario
            if (!string.IsNullOrEmpty(usuario))
            {
                prestamos = prestamos.Where(p => p.Usuario.Nombre == usuario);
            }

            // Filtro por categoría
            if (!string.IsNullOrEmpty(categoria))
            {
                prestamos = prestamos.Where(p => p.Elemento.Categoria.Nombre == categoria);
            }

            // Filtro por fechas
            if (fechaInicio.HasValue)
            {
                prestamos = prestamos.Where(p => p.FechaPrestamo >= fechaInicio);
            }
            if (fechaFin.HasValue)
            {
                prestamos = prestamos.Where(p => p.FechaPrestamo <= fechaFin);
            }

            return View(await prestamos.ToListAsync());
        }
    }
}
