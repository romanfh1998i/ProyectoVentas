using ProyectoVentas.Entidades;
using ProyectoVentas.Negocio;
using ProyectoVentas.Presentacion.Helpers;

namespace ProyectoVentas.Presentacion;
public partial class FrmCategoria : FrmMantBase
{
    private CategoriaServicio _servicio;
    private string _nombreId = "IdCategoria";
    private int _registroId = 0;

    public FrmCategoria()
        : base("Categoría")
    {
        InitializeComponent();

        dtgDatos.CellDoubleClick += new DataGridViewCellEventHandler(dtgDatos_CellDoubleClick);

        _servicio = new CategoriaServicio();
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
            Editar(id);
        }
    }

    private void Editar(int id)
    {
        var registro = _servicio.ObtenerPorId(id);

        _registroId = 0;
        lblEditando.Visible = true;

        if (registro != null)
        {
            _registroId = registro.IdCategoria;

            txtDescripcion.Text = registro.Descripcion;
            cboEstado.SelectedIndex = BaseHelper.ObtenerIndexCombo(cboEstado, registro.Estado ? 1 : 0);
        }
    }

    private void CargarCombos()
    {
        var listaEstado = new List<OpcionCombo>() {
            new OpcionCombo(1, "Activo"),
            new OpcionCombo(0, "Inactivo")
        };

        cboEstado.DataSource = listaEstado;
        cboEstado.DisplayMember = "Descripcion";
        cboEstado.ValueMember = "Valor";
    }

    private void btnGuardar_Click(object sender, EventArgs e)
    {
        var obj = new Categoria
        {
            IdCategoria = _registroId,
            Descripcion = txtDescripcion.Text,
            Estado = ((OpcionCombo)cboEstado.SelectedItem)?.Valor == 1 ? true : false,
            FechaCreacion = DateTime.Now,
        };

        var result = _servicio.Registrar(obj);

        if (result)
        {
            LimpiarFormulario();

            CargarListado();

            MessageBox.Show("Datos Registrados Correctamente", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

    private void LimpiarFormulario()
    {
        txtDescripcion.Text = "";
        cboEstado.SelectedIndex = 0;
        _registroId = 0;
        lblEditando.Visible = false;
        txtDescripcion.Select();
    }

    private void btnLimpiar_Click(object sender, EventArgs e)
    {
        LimpiarFormulario();
    }

    private void FrmCategoria_Shown(object sender, EventArgs e)
    {
        CargarListado();

        CargarCombos();

        CargarOpcionesBusqueda();
    }

    private void btnEliminar_Click(object sender, EventArgs e)
    {
        if (_registroId <= 0)
        {
            MessageBox.Show("Primero debe seleccionar con doble click el registro que desea eliminar", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        var dialogResult = MessageBox.Show("Desea Eliminar el Registro Seleccionado", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        if (dialogResult == DialogResult.Yes)
        {
            _servicio.Eliminar(_registroId);

            CargarListado();

            LimpiarFormulario();
        }
    }
}
