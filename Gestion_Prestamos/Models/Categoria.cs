using System;
using System.Collections.Generic;

namespace Gestion_Prestamos.Models;

public partial class Categoria
{
    public int CategoriaId { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public virtual ICollection<Elemento> Elementos { get; set; } = new List<Elemento>();
}
