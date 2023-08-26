using System.ComponentModel;

namespace ProyectoVentas.Negocio.ViewModels;

public class UsuarioVm
{
    public int IdUsuario { get; set; }

    public string Documento { get; set; }

    [DisplayName("Nombre Completo")]
    public string NombreCompleto { get; set; }

    public string Correo { get; set; }

    [DisplayName("Rol")]
    public string RolDescripcion { get; set; }

    [DisplayName("Estado")]
    public string EstadoDescripcion { get; set; }

    [DisplayName("Fecha Creación")]
    public string Fecha { get; set; }
}
