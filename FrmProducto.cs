using ProyectoVentas.Entidades;
using ProyectoVentas.Negocio;
using ProyectoVentas.Presentacion.Helpers;

namespace ProyectoVentas.Presentacion;
public partial class FrmProducto : FrmMantBase
{
    private ProductoServicio _servicio;
    private CategoriaServicio _categoriaSevice;
    private string _nombreId = "IdProducto";
    private Producto _registro = null;

    public FrmProducto()
        : base("Producto")
    {
        InitializeComponent();

        dtgDatos.CellDoubleClick += new DataGridViewCellEventHandler(dtgDatos_CellDoubleClick);

        _servicio = new ProductoServicio();
        _categoriaSevice = new CategoriaServicio();
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

        _registro = registro;

        lblEditando.Visible = true;

        if (registro != null)
        {
            txtCodigo.Text = registro.Codigo;
            txtNombre.Text = registro.Nombre;
            txtDescripcion.Text = registro.Descripcion;
            cboEstado.SelectedIndex = BaseHelper.ObtenerIndexCombo(cboEstado, registro.Estado ? 1 : 0);
            cboCategoria.SelectedIndex = BaseHelper.ObtenerIndexCombo(cboCategoria, registro.IdCategoria);
        }
    }

    private void CargarCombos()
    {
        var listaCategoria = _categoriaSevice.ObtenerParaCombo();

        if (listaCategoria.Count() > 0)
        {
            cboCategoria.DataSource = listaCategoria.Select(o => new OpcionCombo(o.IdCategoria, o.Descripcion)).ToList();
            cboCategoria.DisplayMember = "Descripcion";
            cboCategoria.ValueMember = "Valor";
        }

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
        var obj = new Producto
        {
            IdProducto = 0,
            Codigo = txtCodigo.Text,
            Nombre = txtNombre.Text,
            Descripcion = txtDescripcion.Text,
            Stock = 0,
            IdCategoria = ((OpcionCombo)cboCategoria.SelectedItem)?.Valor ?? 0,
            Estado = ((OpcionCombo)cboEstado.SelectedItem)?.Valor == 1 ? true : false,
            FechaCreacion = DateTime.Now,
        };

        if (_registro != null)
        {
            obj.IdProducto = _registro.IdProducto;
            obj.Stock = _registro.Stock;
            obj.PrecioVenta = _registro.PrecioVenta;
            obj.PrecioCompra = _registro.PrecioCompra;
        }

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
        txtCodigo.Text = "";
        txtNombre.Text = "";
        txtDescripcion.Text = "";
        cboEstado.SelectedIndex = 0;
        cboCategoria.SelectedIndex = 0;
        lblEditando.Visible = false;
        txtCodigo.Select();
        _registro = null;
    }

    private void btnLimpiar_Click(object sender, EventArgs e)
    {
        LimpiarFormulario();
    }

    private void FrmProducto_Shown(object sender, EventArgs e)
    {
        CargarListado();

        CargarCombos();

        CargarOpcionesBusqueda();
    }

    private void btnEliminar_Click(object sender, EventArgs e)
    {
        if (_registro == null)
        {
            MessageBox.Show("Primero debe seleccionar con doble click el registro que desea eliminar", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        var dialogResult = MessageBox.Show("Desea Eliminar el Registro Seleccionado", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        if (dialogResult == DialogResult.Yes)
        {
            _servicio.Eliminar(_registro.IdProducto);

            CargarListado();

            LimpiarFormulario();
        }
    }
}
