using ProyectoVentas.Core.Helpers;
using ProyectoVentas.Negocio;
using ProyectoVentas.Negocio.ViewModels;
using System.ComponentModel;

namespace ProyectoVentas.Presentacion;
public partial class FrmDetalleCompra : FrmMantBase
{
    private CompraServicio _servicio;

    private string _nombreId = "IdCompra";

    public FrmDetalleCompra()
        : base("Compra")
    {
        InitializeComponent();

        dtgDatos.CellClick += new DataGridViewCellEventHandler(dtgDatos_CellDoubleClick);

        _servicio = new CompraServicio();
    }

    private void CargarListado()
    {
        var lista = _servicio.ObtenerListado();

        dtgDatos.DataSource = lista;
        dtgDatos.Columns[_nombreId].Visible = false;

        dtgDatos.Refresh();
    }

    private void FrmDetalleCompra_Shown(object sender, EventArgs e)
    {
        CargarListado();

        CargarOpcionesBusqueda();
    }

    private void dtgDatos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
        DataGridView dgv = sender as DataGridView;
        if (dgv != null)
        {
            int rowIndex = dgv.CurrentCell.RowIndex;
            int id = Convert.ToInt32(dgv.Rows[rowIndex].Cells[_nombreId].Value);

            var compra = _servicio.ObtenerPorId(id);

            var listaDetalle = new List<DetalleCompraVm>();

            foreach (var item in compra.ListaDetalle)
            {
                var item2 = new DetalleCompraVm
                {
                    Producto = item.Producto?.Descripcion,
                    PrecioCompra = item.PrecioCompra,
                    PrecioVenta = item.PrecioVenta,
                    Cantidad = item.Cantidad,
                    MontoTotal = item.MontoTotal,
                };

                listaDetalle.Add(item2);
            }

            dtgDetalle.DataSource = listaDetalle;
        }
    }
}
