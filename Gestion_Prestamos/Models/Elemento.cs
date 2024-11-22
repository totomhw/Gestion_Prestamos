using System;
using System.Collections.Generic;

namespace Gestion_Prestamos.Models;

public partial class Elemento
{
    public int ElementoId { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public DateOnly? FechaAdquisicion { get; set; }

    public decimal? Valor { get; set; }

    public bool? EsActivo { get; set; }

    public int? CategoriaId { get; set; }

    public virtual Categoria? Categoria { get; set; }

    public virtual ICollection<Inventario> Inventarios { get; set; } = new List<Inventario>();

    public virtual ICollection<Prestamo> Prestamos { get; set; } = new List<Prestamo>();
}
