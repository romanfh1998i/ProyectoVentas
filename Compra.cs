namespace ProyectoVentas.Entidades;

public class Compra
{
    public int IdCompra { get; set; }
    public int IdUsuario { get; set; }
    public int IdProveedor { get; set; }
    public string TipoDocumento { get; set; }
    public string NumeroDocumento { get; set; }
    public decimal MontoTotal { get; set; }
    public DateTime FechaCreacion { get; set; }
    public List<DetalleCompra> ListaDetalle { get; set; }
}

