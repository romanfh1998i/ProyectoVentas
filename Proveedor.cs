namespace ProyectoVentas.Entidades;

public class Proveedor
{
    public int IdProveedor { get; set; }
    public string Documento { get; set; }
    public string RazonSocial { get; set; }
    public string Correo { get; set; }
    public string Telefono { get; set; }
    public bool Estado { get; set; }
    public DateTime FechaCreacion { get; set; }
}

