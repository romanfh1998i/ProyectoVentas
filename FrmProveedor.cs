using ProyectoVentas.Entidades;
using ProyectoVentas.Negocio;
using ProyectoVentas.Presentacion.Helpers;
using System;
using System.Collections.Generic;

using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoVentas.Presentacion;
public partial class FrmProveedor : FrmMantBase
{
    private ProveedorServicio _servicio;
    private string _nombreId = "IdProveedor";
    private Proveedor _registro = null;

    public FrmProveedor()
        : base("Proveedor")
    {
        InitializeComponent();

        dtgDatos.CellDoubleClick += new DataGridViewCellEventHandler(dtgDatos_CellDoubleClick);

        _servicio = new ProveedorServicio();
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
            txtDocumento.Text = registro.Documento;
            txtRazonSocial.Text = registro.RazonSocial;
            txtCorreo.Text = registro.Correo;
            txtTelefono.Text = registro.Telefono;
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
        var obj = new Proveedor
        {
            IdProveedor = 0,
            Documento = txtDocumento.Text,
            RazonSocial = txtRazonSocial.Text,
            Correo = txtCorreo.Text,
            Telefono = txtTelefono.Text,
            Estado = ((OpcionCombo)cboEstado.SelectedItem)?.Valor == 1 ? true : false,
            FechaCreacion = DateTime.Now,
        };

        if (_registro != null)
        {
            obj.IdProveedor = _registro.IdProveedor;
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
        txtDocumento.Text = "";
        txtRazonSocial.Text = "";
        txtCorreo.Text = "";
        txtTelefono.Text = "";
        cboEstado.SelectedIndex = 0;

        lblEditando.Visible = false;
        txtDocumento.Select();
        _registro = null;
    }

    private void btnLimpiar_Click(object sender, EventArgs e)
    {
        LimpiarFormulario();
    }

    private void FrmProveedor_Shown(object sender, EventArgs e)
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
            _servicio.Eliminar(_registro.IdProveedor);

            CargarListado();

            LimpiarFormulario();
        }
    }
}
