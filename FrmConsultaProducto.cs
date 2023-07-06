using ProyectoVentas.Negocio;
using ProyectoVentas.Negocio.ViewModels;
using System.Data;

namespace ProyectoVentas.Presentacion;

public partial class FrmConsultaProducto : Form
{
    private ProductoServicio _servicio;
    private List<ProductoVm> lista = new List<ProductoVm>();
    public int idSeleccionado = 0;

    public FrmConsultaProducto()
    {
        InitializeComponent();

        _servicio = new ProductoServicio();
    }

    private void FrmConsultaProducto_Load(object sender, EventArgs e)
    {
        lista = _servicio.ObtenerListado();
        AsignarData(lista);
    }

    private void AsignarData(List<ProductoVm> lista)
    {
        var detalle = lista.Select(o => new { Id = o.IdProducto, o.Codigo, o.Nombre, o.Descripcion }).ToList();
        dtgDatos.DataSource = detalle;
        dtgDatos.Columns[0].Visible = false;
    }

    private void txtBusqueda_TextChanged(object sender, EventArgs e)
    {
        Filtrar(txtBusqueda.Text);
    }

    private void Filtrar(string filtro)
    {
        filtro = filtro.ToLower();

        if (lista.Count > 0)
        {
            var detalle = lista.Where(o =>
                o.Codigo.ToLower().Contains(filtro) ||
                o.Nombre.ToLower().Contains(filtro) ||
                o.Descripcion.ToLower().Contains(filtro)).ToList();

            AsignarData(detalle);
        }
    }

    private void dtgDatos_DoubleClick(object sender, EventArgs e)
    {
        DataGridView dgv = sender as DataGridView;
        if (dgv != null)
        {
            int rowIndex = dgv.CurrentCell.RowIndex;
            idSeleccionado = Convert.ToInt32(dgv.Rows[rowIndex].Cells[0].Value);
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
