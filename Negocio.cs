namespace ProyectoVentas.Entidades;

public class Negocio
{
    public int IdNegocio { get; set; }
    public string Nombre { get; set; }
    public string Rnc { get; set; }
    public string Direccion { get; set; }
    public byte[] Logo { get; set; }
    public DateTime FechaCreacion { get; set; }
}
