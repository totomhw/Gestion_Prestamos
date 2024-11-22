namespace Gestion_Prestamos.Models  // Asegúrate de que este namespace coincida con el que tienes en tu proyecto
{
    public class MainViewModel
    {
        public List<InventarioViewModel> Inventario { get; set; }
        public List<PrestamoViewModel> PrestamosRecientes { get; set; }
    }

    public class InventarioViewModel
    {
        public string NombreElemento { get; set; }
        public int UnidadesTotales { get; set; }
        public int UnidadesDisponibles { get; set; }
    }

    public class PrestamoViewModel
    {
        public string NombreElemento { get; set; }
        public string NombreUsuario { get; set; }
        public int UnidadesPrestadas { get; set; }
        public DateTime FechaPrestamo { get; set; }
    }
}
