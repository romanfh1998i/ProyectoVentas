
using System.ComponentModel;

namespace ProyectoVentas.Negocio.ViewModels;

public class ClienteVm
{
    public int IdCliente { get; set; }
    public string Documento { get; set; }

    [DisplayName("Nombre")]
    public string NombreCompleto { get; set; }

    public string Correo { get; set; }
    public string Telefono { get; set; }

    [DisplayName("Estado")]
    public string EstadoDescripcion { get; set; }

    public string Fecha { get; set; }

}

