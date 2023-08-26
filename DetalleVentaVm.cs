using System.ComponentModel;

namespace ProyectoVentas.Negocio.ViewModels;

public class DetalleVentaVm
{
    public string Producto { get; set; }

    [DisplayName("Precio de Venta")]
    public decimal PrecioVenta { get; set; }
    public int Cantidad { get; set; }
    [DisplayName("Sub Total")]
    public decimal SubTotal { get; set; }

}

