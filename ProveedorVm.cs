
using System.ComponentModel;

namespace ProyectoVentas.Negocio.ViewModels;

public class ProveedorVm
{
    public int IdProveedor { get; set; }
    public string Documento { get; set; }

    [DisplayName("Razón Social")]
    public string RazonSocial { get; set; }
    public string Correo { get; set; }
    public string Telefono { get; set; }

    [DisplayName("Estado")]
    public string EstadoDescripcion { get; set; }

    public string Fecha { get; set; }

}

