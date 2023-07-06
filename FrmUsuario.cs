using ProyectoVentas.Entidades;
using ProyectoVentas.Negocio;
using ProyectoVentas.Presentacion;
using ProyectoVentas.Presentacion.Helpers;

namespace ProyectoVentas;

public partial class FrmUsuario : FrmMantBase
{
    private UsuarioServicio _servicio;
    private RolServicio _rolServicio;
    private string _nombreId = "IdUsuario";
    private int _registroId = 0;

    public FrmUsuario()
        : base("Usuario")
    {
        InitializeComponent();

        dtgDatos.CellDoubleClick += new DataGridViewCellEventHandler(dtgDatos_CellDoubleClick);

        _servicio = new UsuarioServicio();
        _rolServicio = new RolServicio();
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
        var usuario = _servicio.ObtenerPorId(id);

        _registroId = 0;
        lblEditando.Visible = true;
        txtNoDocumento.Enabled = false;

        if (usuario.EsPrimerUsuarioAdmin)
        {
            cboRol.Enabled = false;
        }

        if (usuario != null)
        {
            _registroId = usuario.IdUsuario;

            txtNoDocumento.Text = usuario.Documento;
            txtNombreCompleto.Text = usuario.NombreCompleto;
            txtCorreo.Text = usuario.Correo;
            txtClave.Text = usuario.Clave;
            txtRepetirClave.Text = usuario.Clave;
            cboRol.SelectedIndex = BaseHelper.ObtenerIndexCombo(cboRol, usuario.IdRol);
            cboEstado.SelectedIndex = BaseHelper.ObtenerIndexCombo(cboEstado, usuario.Estado ? 1 : 0);
        }
    }

    private void CargarCombos()
    {
        var listaRol = _rolServicio.ObtenerParaCombo();

        if (listaRol.Count() > 0)
        {
            cboRol.DataSource = listaRol.Select(o => new OpcionCombo(o.IdRol, o.Descripcion)).ToList();
            cboRol.DisplayMember = "Descripcion";
            cboRol.ValueMember = "Valor";
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
        if (txtClave.Text != txtRepetirClave.Text)
        {
            MessageBox.Show($"La Clave y la Confirmación no Coinciden", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var obj = new Usuario
        {
            IdUsuario = _registroId,
            Documento = txtNoDocumento.Text,
            NombreCompleto = txtNombreCompleto.Text,
            Correo = txtCorreo.Text,
            Clave = txtClave.Text,
            IdRol = ((OpcionCombo)cboRol.SelectedItem)?.Valor ?? 0,
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
        txtNoDocumento.Text = "";
        txtNombreCompleto.Text = "";
        txtCorreo.Text = "";
        txtClave.Text = "";
        txtRepetirClave.Text = "";
        cboRol.SelectedIndex = 0;
        cboEstado.SelectedIndex = 0;
        _registroId = 0;
        lblEditando.Visible = false;
        txtNoDocumento.Enabled = true;
        cboRol.Enabled = true;
        txtNoDocumento.Select();
    }

    private void btnLimpiar_Click(object sender, EventArgs e)
    {
        LimpiarFormulario();
    }

    private void FrmUsuario_Shown(object sender, EventArgs e)
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

        if (dtgDatos.Rows.Count == 1)
        {
            MessageBox.Show("No Puede eliminar todos los usuarios del sistema", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

    private void FrmUsuario_Load(object sender, EventArgs e)
    {

    }
}
