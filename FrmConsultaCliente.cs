using ProyectoVentas.Datos;
using ProyectoVentas.Entidades;
using ProyectoVentas.Negocio;
using ProyectoVentas.Negocio.ViewModels;
using System.Data;

namespace ProyectoVentas.Presentacion;

public partial class FrmConsultaCliente : Form
{
    private ClienteServicio _servicio;
    private List<ClienteVm> lista = new List<ClienteVm>();
    public int idSeleccionado = 0;

    public FrmConsultaCliente()
    {
        InitializeComponent();

        _servicio = new ClienteServicio();
    }

    private void FrmConsultaCliente_Load(object sender, EventArgs e)
    {
        lista = _servicio.ObtenerListado();
        AsignarData(lista);
    }

    private void AsignarData(List<ClienteVm> lista)
    {
        var detalle = lista.Select(o => new { Id = o.IdCliente, Documento = o.Documento, Nombre = o.NombreCompleto, Correo = o.Correo }).ToList();
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
                o.NombreCompleto.ToLower().Contains(filtro) ||
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
