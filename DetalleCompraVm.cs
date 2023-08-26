using System.ComponentModel;

namespace ProyectoVentas.Negocio.ViewModels;

public class DetalleCompraVm
{
    public string Producto { get; set; }
    [DisplayName("Precio de Compra")]
    public decimal PrecioCompra { get; set; }

    [DisplayName("Precio de venta")]
    public decimal PrecioVenta { get; set; }
    public int Cantidad { get; set; }

    [DisplayName("Monto")]
    public decimal MontoTotal { get; set; }

}

