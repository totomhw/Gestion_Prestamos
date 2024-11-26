using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Gestion_Prestamos.Models;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Layout.Borders;
using iText.IO.Image;
using iText.Kernel.Pdf.Canvas;

namespace Gestion_Prestamos.Controllers
{
    public class ReportesController : Controller
    {
        private readonly GestionPrestamosContext _context;

        public ReportesController(GestionPrestamosContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> PrestamosReporte(string usuario, string categoria, DateTime? fechaInicio, DateTime? fechaFin)
        {
            ViewBag.Categorias = new SelectList(await _context.Categorias.Select(c => c.Nombre).Distinct().ToListAsync());
            ViewBag.Usuarios = new SelectList(await _context.Usuarios.Select(u => u.Nombre).Distinct().ToListAsync());

            var prestamos = _context.Prestamos.Include(p => p.Elemento).Include(p => p.Usuario).AsQueryable();

            if (!string.IsNullOrEmpty(usuario))
            {
                prestamos = prestamos.Where(p => p.Usuario.Nombre == usuario);
            }

            if (!string.IsNullOrEmpty(categoria))
            {
                prestamos = prestamos.Where(p => p.Elemento.Categoria.Nombre == categoria);
            }

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

        [HttpGet]
        public async Task<IActionResult> ExportarPDF(string usuario, string categoria, DateTime? fechaInicio, DateTime? fechaFin)
        {
            try
            {
                var prestamos = _context.Prestamos
                    .Include(p => p.Elemento)
                    .Include(p => p.Usuario)
                    .AsQueryable();

                if (!string.IsNullOrEmpty(usuario))
                {
                    prestamos = prestamos.Where(p => p.Usuario.Nombre == usuario);
                }

                if (!string.IsNullOrEmpty(categoria))
                {
                    prestamos = prestamos.Where(p => p.Elemento.Categoria.Nombre == categoria);
                }

                if (fechaInicio.HasValue)
                {
                    prestamos = prestamos.Where(p => p.FechaPrestamo >= fechaInicio);
                }

                if (fechaFin.HasValue)
                {
                    prestamos = prestamos.Where(p => p.FechaPrestamo <= fechaFin);
                }

                var prestamosFiltrados = await prestamos.ToListAsync();

                using (var stream = new MemoryStream())
                {
                    var writerProperties = new WriterProperties();
                    var writer = new PdfWriter(stream, writerProperties);
                    var pdf = new PdfDocument(writer);
                    var document = new Document(pdf);

                    // Agregar marca de agua
                    AddWatermarkToPdf(pdf, "CONFIDENCIAL");

                    // Logo y fecha
                    var logoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/logo uni.png");
                    var logoImage = ImageDataFactory.Create(logoPath);
                    var logo = new Image(logoImage).ScaleAbsolute(50, 50);

                    var headerTable = new Table(2).UseAllAvailableWidth();
                    headerTable.SetBorder(Border.NO_BORDER);
                    headerTable.AddCell(new Cell().Add(logo).SetBorder(Border.NO_BORDER));
                    headerTable.AddCell(new Cell().Add(new Paragraph($"Fecha: {DateTime.Now:yyyy-MM-dd}")
                        .SetTextAlignment(TextAlignment.RIGHT))
                        .SetBorder(Border.NO_BORDER));

                    document.Add(headerTable);

                    // Espaciado
                    document.Add(new Paragraph("\n"));

                    // Título del reporte
                    document.Add(new Paragraph("Reporte de Préstamos")
                        .SetTextAlignment(TextAlignment.CENTER)
                        .SetFontSize(18)
                        .SetBold());

                    // Espaciado
                    document.Add(new Paragraph("\n"));

                    // Tabla
                    var table = new Table(UnitValue.CreatePercentArray(4)).UseAllAvailableWidth();
                    table.AddHeaderCell(new Cell().Add(new Paragraph("Elemento").SetBold()));
                    table.AddHeaderCell(new Cell().Add(new Paragraph("Usuario").SetBold()));
                    table.AddHeaderCell(new Cell().Add(new Paragraph("Unidades Prestadas").SetBold()));
                    table.AddHeaderCell(new Cell().Add(new Paragraph("Fecha de Préstamo").SetBold()));

                    foreach (var prestamo in prestamosFiltrados)
                    {
                        table.AddCell(new Cell().Add(new Paragraph(prestamo.Elemento.Nombre)));
                        table.AddCell(new Cell().Add(new Paragraph(prestamo.Usuario.Nombre)));
                        table.AddCell(new Cell().Add(new Paragraph(prestamo.UnidadesPrestadas.ToString())));
                        table.AddCell(new Cell().Add(new Paragraph(prestamo.FechaPrestamo.ToString("yyyy-MM-dd"))));
                    }

                    document.Add(table);
                    document.Close();

                    return File(stream.ToArray(), "application/pdf", "ReportePrestamos.pdf");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al generar el PDF: {ex.Message}");
            }
        }

        private void AddWatermarkToPdf(PdfDocument pdf, string watermarkText)
        {
            int numberOfPages = pdf.GetNumberOfPages();
            var pdfFont = iText.Kernel.Font.PdfFontFactory.CreateFont(iText.IO.Font.Constants.StandardFonts.HELVETICA);

            for (int i = 1; i <= numberOfPages; i++)
            {
                var page = pdf.GetPage(i);
                var canvas = new PdfCanvas(page.NewContentStreamBefore(), page.GetResources(), pdf);

                var width = page.GetPageSize().GetWidth();
                var height = page.GetPageSize().GetHeight();

                // Configurar la marca de agua
                canvas.SaveState();
                canvas.SetFontAndSize(pdfFont, 60); // Establecer fuente y tamaño
                canvas.SetColor(new iText.Kernel.Colors.DeviceRgb(200, 200, 200), true); // Color gris claro
                canvas.BeginText();
                canvas.SetTextMatrix(1, 0, 0, 1, width / 2 - 150, height / 2); // Ajustar posición al centro
                canvas.ShowText(watermarkText); // Agregar el texto
                canvas.EndText();
                canvas.RestoreState();
            }
        }







    }
}
