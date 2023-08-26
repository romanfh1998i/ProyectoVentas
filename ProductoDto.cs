using System.ComponentModel;

namespace ProyectoVentas.Negocio.ViewModels;

public class ProductoDto
{
    public int IdProducto { get; set; }

    public string Producto { get; set; }
    
    [DisplayName("Precio")]
    public decimal PrecioCompra { get; set; }

    public decimal PrecioVenta { get; set; }
    public int Cantidad { get; set; }

    [DisplayName("Sub Total")]
    public decimal SubTotal { get; set; }
}
