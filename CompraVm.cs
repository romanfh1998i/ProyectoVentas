using System.ComponentModel;

namespace ProyectoVentas.Negocio.ViewModels;

public class CompraVm
{
    public int IdCompra { get; set; }
    [DisplayName("Tipo de Documento")]
    public string TipoDocumento { get; set; }

    [DisplayName("Número de Documento")]
    public string NumeroDocumento { get; set; }

    [DisplayName("Monto")]
    public decimal MontoTotal { get; set; }
    public string Fecha { get; set; }
}

