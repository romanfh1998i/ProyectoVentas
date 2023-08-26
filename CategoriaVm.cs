

using System.ComponentModel;

namespace ProyectoVentas.Negocio.ViewModels;

public class CategoriaVm
{
    public int IdCategoria { get; set; }
    public string Descripcion { get; set; }

    [DisplayName("Estado")]
    public string EstadoDescripcion { get; set; }

    public string Fecha { get; set; }
}

