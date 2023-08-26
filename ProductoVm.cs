
using System.ComponentModel;

namespace ProyectoVentas.Negocio.ViewModels;

public class ProductoVm
{
    public int IdProducto { get; set; }
    public string Codigo { get; set; }
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public int Stock { get; set; }

    [DisplayName("Categoría")]
    public string CategoriaDescripcion { get; set; }

    [DisplayName("Precio de Compra")]
    public decimal PrecioCompra { get; set; }

    [DisplayName("Precio de Venta")]
    public decimal PrecioVenta { get; set; }

    [DisplayName("Estado")]
    public string EstadoDescripcion { get; set; }

    public string Fecha { get; set; }

}

