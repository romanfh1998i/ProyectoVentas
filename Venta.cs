namespace ProyectoVentas.Entidades;

public class Venta
{
    public int IdVenta { get; set; }
    public int IdUsuario { get; set; }
    public string TipoDocumento { get; set; }
    public string NumeroDocumento { get; set; }
    public string DocumentoCliente { get; set; }
    public string NombreCliente { get; set; }
    public decimal MontoPago { get; set; }
    public decimal MontoCambio { get; set; }
    public decimal MontoTotal { get; set; }
    public DateTime FechaCreacion { get; set; }

    public List<DetalleVenta> ListaDetalle { get; set; }
}
