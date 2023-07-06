

namespace ProyectoVentas.Entidades;

public class Usuario
{
    public int IdUsuario { get; set; }
    public string Documento { get; set; }
    public string NombreCompleto { get; set; }
    public string Correo { get; set; }
    public string Clave { get; set; }
    public int IdRol { get; set; }
    public bool Estado { get; set; }
    public DateTime FechaCreacion { get; set; }
    public bool EsPrimerUsuarioAdmin { get; set; } = false;

    public virtual Rol Rol { get; set; }
}
