using System;
using System.Collections.Generic;

namespace Gestion_Prestamos.Models;

public partial class Prestamo
{
    public int PrestamoId { get; set; }

    public int? ElementoId { get; set; }

    public int? UsuarioId { get; set; }

    public int UnidadesPrestadas { get; set; }

    public DateTime FechaPrestamo { get; set; }

    public DateOnly? FechaDevolucion { get; set; }

    public DateOnly? FechaEntrega { get; set; }

    public string? Observaciones { get; set; }

    public string? FechaPrestamoCreadoPor { get; set; }

    public DateTime? FechaPrestamoCreadoEn { get; set; }

    public string? FechaPrestamoModificadoPor { get; set; }

    public DateTime? FechaPrestamoModificadoEn { get; set; }

    public string? FechaDevolucionCreadoPor { get; set; }

    public DateTime? FechaDevolucionCreadoEn { get; set; }

    public string? FechaDevolucionModificadoPor { get; set; }

    public DateTime? FechaDevolucionModificadoEn { get; set; }

    public string? FechaEntregaCreadoPor { get; set; }

    public DateTime? FechaEntregaCreadoEn { get; set; }

    public string? FechaEntregaModificadoPor { get; set; }

    public DateTime? FechaEntregaModificadoEn { get; set; }

    public virtual Elemento? Elemento { get; set; }

    public virtual Usuario? Usuario { get; set; }
}
