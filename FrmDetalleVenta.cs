using ProyectoVentas.Negocio.ViewModels;
using ProyectoVentas.Negocio;

namespace ProyectoVentas.Presentacion;
public partial class FrmDetalleVenta : FrmMantBase
{
    private VentaServicio _servicio;

    private string _nombreId = "IdVenta";

    public FrmDetalleVenta()
        : base("Venta")
    {
        InitializeComponent();

        dtgDatos.CellClick += new DataGridViewCellEventHandler(dtgDatos_CellDoubleClick);

        _servicio = new VentaServicio();
    }

    private void CargarListado()
    {
        var lista = _servicio.ObtenerListado();

        dtgDatos.DataSource = lista;
        dtgDatos.Columns[_nombreId].Visible = false;

        dtgDatos.Refresh();
    }

    private void dtgDatos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
        DataGridView dgv = sender as DataGridView;
        if (dgv != null)
        {
            int rowIndex = dgv.CurrentCell.RowIndex;
            int id = Convert.ToInt32(dgv.Rows[rowIndex].Cells[_nombreId].Value);

            var compra = _servicio.ObtenerPorId(id);

            var listaDetalle = new List<DetalleVentaVm>();

            foreach (var item in compra.ListaDetalle)
            {
                var item2 = new DetalleVentaVm
                {
                    Producto = item.Producto?.Descripcion,
                    PrecioVenta = item.PrecioVenta,
                    Cantidad = item.Cantidad,
                    SubTotal = item.SubTotal,
                };

                listaDetalle.Add(item2);
            }

            dtgDetalle.DataSource = listaDetalle;
        }
    }

    private void FrmDetalleVenta_Shown(object sender, EventArgs e)
    {
        CargarListado();

        CargarOpcionesBusqueda();
    }
}
