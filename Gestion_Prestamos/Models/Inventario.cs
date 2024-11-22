using System;
using System.Collections.Generic;

namespace Gestion_Prestamos.Models;

public partial class Inventario
{
    public int InventarioId { get; set; }

    public int? ElementoId { get; set; }

    public int UnidadesTotales { get; set; }

    public int UnidadesDisponibles { get; set; }

    public virtual Elemento? Elemento { get; set; }
}
