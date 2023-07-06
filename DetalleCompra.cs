namespace ProyectoVentas.Entidades;


public class DetalleCompra
{
    public int IdDetalleCompra { get; set; }
    public int IdCompra { get; set; }
    public int IdProducto { get; set; }
    public decimal PrecioCompra { get; set; }
    public decimal PrecioVenta { get; set; }
    public int Cantidad { get; set; }
    public decimal MontoTotal { get; set; }
    public DateTime FechaCreacion { get; set; }

    public virtual Compra Compra { get; set; }
    public virtual Producto Producto { get; set; }
}

