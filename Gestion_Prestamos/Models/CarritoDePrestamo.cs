using System;
using System.Collections.Generic;

namespace Gestion_Prestamos.Models
{
    public class CarritoDePrestamo
    {
        public int CarritoId { get; set; }
        public int UsuarioId { get; set; }
        public int ElementoId { get; set; }
        public int Cantidad { get; set; }
        public DateTime FechaAgregado { get; set; }

        public virtual Usuario Usuario { get; set; } = null!;
        public virtual Elemento Elemento { get; set; } = null!;
    }
}
