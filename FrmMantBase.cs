
using ClosedXML.Excel;
using ProyectoVentas.Presentacion.Helpers;
using System.Data;

namespace ProyectoVentas.Presentacion;

public partial class FrmMantBase : Form
{
    private string _nombreFormulario = "";

    public List<OpcionComboString> ListaOpcionesBusqueda = new List<OpcionComboString>();

    public FrmMantBase(string nombreFormulario)
    {
        _nombreFormulario = nombreFormulario;

        InitializeComponent();
    }

    public FrmMantBase()
    {
        InitializeComponent();
    }

    public void CargarOpcionesBusqueda()
    {
        ListaOpcionesBusqueda.Clear();

        foreach (DataGridViewColumn columna in dtgDatos.Columns)
        {
            if (columna.Visible)
            {
                ListaOpcionesBusqueda.Add(new OpcionComboString(columna.Name, columna.HeaderText));
            }
        }

        if (ListaOpcionesBusqueda.Count() > 0)
        {
            cboBusqueda.DataSource = ListaOpcionesBusqueda;
            cboBusqueda.DisplayMember = "Descripcion";
            cboBusqueda.ValueMember = "Valor";
        }
    }

    private void FrmMantBase_Load(object sender, EventArgs e)
    {
        lblListado.Text = $"Listado de {_nombreFormulario}";
        lblDetalle.Text = $"Detalle de {_nombreFormulario}";
    }

    private void btnSalir_Click(object sender, EventArgs e)
    {
        var dialogResult = MessageBox.Show("Desea Salir del Formulario", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        if (dialogResult == DialogResult.Yes)
        {
            Close();
        }
    }

    private void btnSearch_Click(object sender, EventArgs e)
    {
        FiltrarDatos();
    }

    private void FiltrarDatos()
    {
        if (cboBusqueda.SelectedIndex < 0)
        {
            MessageBox.Show("Seleccione la propiedad a buscar", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        if (dtgDatos.Rows.Count <= 0)
        {
            return;
        }

        if (string.IsNullOrWhiteSpace(txtBusqueda.Text))
        {
            this.LimpiarFiltros();
        }

        var columnaFiltro = (OpcionComboString)cboBusqueda.SelectedItem;

        BindingContext[dtgDatos.DataSource].SuspendBinding();

        foreach (DataGridViewRow row in dtgDatos.Rows)
        {
            var valor = row.Cells[columnaFiltro.Valor]?.Value.ToString() ?? "";

            row.Visible = valor.Trim().ToLower().Contains(txtBusqueda.Text.Trim().ToLower());
        }

        BindingContext[dtgDatos.DataSource].ResumeBinding();
    }

    private void LimpiarFiltros()
    {
        txtBusqueda.Text = "";
        foreach (DataGridViewRow row in dtgDatos.Rows)
        {
            row.Visible = true;
        }
    }

    private void btnLimpiarBusqueda_Click(object sender, EventArgs e)
    {
        LimpiarFiltros();
    }


    private void btnExportar_Click(object sender, EventArgs e)
    {
        if (dtgDatos.Rows.Count <= 0)
        {
            MessageBox.Show("No existen detalles para exportar a excel", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        DataTable dt = new DataTable();

        foreach (DataGridViewColumn item in dtgDatos.Columns)
        {
            if (item.HeaderText != "" && item.Visible)
            {
                dt.Columns.Add(item.HeaderText, typeof(string));
            }
        }

        foreach (DataGridViewRow row in dtgDatos.Rows)
        {
            if (row.Visible)
            {
                var listaColumnas = new List<object>();
                
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.Visible)
                    {
                        listaColumnas.Add(cell.Value.ToString());
                    }
                }

                dt.Rows.Add(listaColumnas.ToArray());
            }
        }

        SaveFileDialog saveFile = new SaveFileDialog();
        saveFile.FileName = $"ReporteDatos_{_nombreFormulario}{DateTime.Now.ToString("ddMMyyyyHHmmss")}.xlsx";
        saveFile.Filter = "Excel Files | *.xlsx";

        if (saveFile.ShowDialog() == DialogResult.OK)
        {
            try
            {
                XLWorkbook wb = new XLWorkbook();
                var hoja = wb.Worksheets.Add(dt, "Informe");
                hoja.ColumnsUsed().AdjustToContents();
                wb.SaveAs(saveFile.FileName);

                MessageBox.Show("Reporte Generado Correctamente", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            catch (Exception)
            {
                MessageBox.Show("Error al tratar de guardar el reporte", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
    }
}
