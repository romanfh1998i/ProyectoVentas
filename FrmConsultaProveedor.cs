using ProyectoVentas.Negocio;
using ProyectoVentas.Negocio.ViewModels;
using System.Data;

namespace ProyectoVentas.Presentacion;

public partial class FrmConsultaProveedor : Form
{
    private ProveedorServicio _servicio;
    private List<ProveedorVm> lista = new List<ProveedorVm>();
    public int idSeleccionado = 0;

    public FrmConsultaProveedor()
    {
        InitializeComponent();

        _servicio = new ProveedorServicio();
    }

    private void FrmConsultaProveedor_Load(object sender, EventArgs e)
    {
        lista = _servicio.ObtenerListado();
        AsignarData(lista);
    }

    private void AsignarData(List<ProveedorVm> lista)
    {
        var detalle = lista.Select(o => new { Id = o.IdProveedor, Documento = o.Documento, Nombre = o.RazonSocial, Correo = o.Correo }).ToList();
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
                o.Documento.ToLower().Contains(filtro) ||
                o.RazonSocial.ToLower().Contains(filtro) ||
                o.Correo.ToLower().Contains(filtro)).ToList();

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
