using System.ComponentModel;

namespace ProyectoVentas.Negocio.ViewModels;

public class VentaVm
{
    public int IdVenta { get; set; }
    public int IdUsuario { get; set; }

    [DisplayName("Tipo de Documento")]
    public string TipDocumento { get; set; }

    [DisplayName("Número de Documento")]
    public string NumeroDocumento { get; set; }

    [DisplayName("Documento del Cliente")]
    public string DocumentoCliente { get; set; }

    [DisplayName("Nombre del Cliente")]
    public string NombreCliente { get; set; }

    [DisplayName("Monto Pagado")]
    public decimal MontoPago { get; set; }

    [DisplayName("Monto de Cambio")]
    public decimal MontoCambio { get; set; }

    [DisplayName("Monto Total")]
    public decimal MontoTotal { get; set; }
    public string Fecha { get; set; }

}

