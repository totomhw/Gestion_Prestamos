using System;
using System.Collections.Generic;

namespace Gestion_Prestamos.Models;

public partial class Departamento
{
    public int DepartamentoId { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
