using ProyectoVentas.Entidades;
using ProyectoVentas.Negocio;
using ProyectoVentas.Negocio.ViewModels;
using ProyectoVentas.Presentacion.Helpers;
using System.Data;
using CoreBaseHelper = ProyectoVentas.Core.Helpers.BaseHelper;

namespace ProyectoVentas.Presentacion;
public partial class FrmVenta : Form
{
    private VentaServicio _servicio;
    private ProductoServicio _productoServicio;
    private ClienteServicio _clienteServicio;
    private List<ProductoDto> _lista = new List<ProductoDto>();

    private Cliente _clienteSeleccionado = null;
    private Producto _productoSeleccionado = null;

    public FrmVenta()
    {
        InitializeComponent();

        _servicio = new VentaServicio();
        _productoServicio = new ProductoServicio();
        _clienteServicio = new ClienteServicio();
    }

    private void btnBuscarProducto_Click(object sender, EventArgs e)
    {
        var form = new FrmConsultaProducto();
        var resultado = form.ShowDialog();

        if (resultado == DialogResult.OK)
        {
            _productoSeleccionado = _productoServicio.ObtenerPorId(form.idSeleccionado);
            txtCodigoProducto.Text = _productoSeleccionado.Codigo;
            txtProductoDescripcion.Text = _productoSeleccionado.Descripcion;
            txtPrecioVenta.Text = _productoSeleccionado.PrecioVenta.ToString();
            txtStock.Text = _productoSeleccionado.Stock.ToString();
            txtPrecioVenta.Text = _productoSeleccionado.PrecioCompra.ToString();
        }
    }

    private void btnBuscarCliente_Click(object sender, EventArgs e)
    {
        var form = new FrmConsultaCliente();
        var resultado = form.ShowDialog();

        if (resultado == DialogResult.OK)
        {
            _clienteSeleccionado = _clienteServicio.ObtenerPorId(form.idSeleccionado);
            txtNombreCliente.Text = _clienteSeleccionado.NombreCompleto;
            txtNumeroDocumento.Text = _clienteSeleccionado.Documento;
        }
    }

    private void AgregarNuevoProducto(Producto producto)
    {
        var cantidad = Convert.ToInt32(numCantidad.Value);

        var detalle = _lista.Where(o => o.IdProducto == producto.IdProducto).FirstOrDefault();

        if (detalle != null)
        {
            MessageBox.Show($"El producto ya existe en el Detalle Editelo", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (cantidad > _productoSeleccionado.Stock)
        {
            MessageBox.Show($"La cantidad especificada del producto sobrepasa el Stock actual del mismo", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var vm = new ProductoDto();

        CoreBaseHelper.MapObjects<ProductoDto>(producto, vm);
        vm.Cantidad = cantidad;
        vm.SubTotal = vm.PrecioVenta * cantidad;
        vm.Producto = producto.Descripcion;

        _lista.Add(vm);

        dtgDatos.DataSource = _lista;
        dtgDatos.Columns[0].Visible = false;

        CrearDataSet();

        CalcularTotal();
    }

    private void CrearDataSet()
    {
        DataTable dataTable = new DataTable();
        dataTable.Columns.Add("idProducto", typeof(string));
        dataTable.Columns.Add("Producto", typeof(string));
        dataTable.Columns.Add("Precio", typeof(string));
        dataTable.Columns.Add("Cantidad", typeof(string));
        dataTable.Columns.Add("SubTotal", typeof(string));

        foreach (var item in _lista)
        {
            DataRow fila = dataTable.NewRow();
            fila["idProducto"] = item.IdProducto;
            fila["Producto"] = item.Producto;
            fila["Precio"] = item.PrecioVenta;
            fila["Cantidad"] = item.Cantidad;
            fila["SubTotal"] = item.PrecioVenta * item.Cantidad;
            dataTable.Rows.Add(fila);
        }

        dtgDatos.DataSource = dataTable;

        dtgDatos.Columns[0].Visible = false;

        dtgDatos.Refresh();
    }


    private void btnAgregar_Click(object sender, EventArgs e)
    {
        if (_productoSeleccionado == null)
        {
            MessageBox.Show($"Debe especificar el producto a comprar", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (string.IsNullOrWhiteSpace(txtPrecioVenta.Text) || Convert.ToDecimal(txtPrecioVenta.Text) <= 0)
        {
            MessageBox.Show($"Debe especificar el precio de venta", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (numCantidad.Value <= 0)
        {
            MessageBox.Show($"Debe especificar la cantidad a comprar", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (numCantidad.Value > _productoSeleccionado.Stock)
        {
            MessageBox.Show($"La cantidad especificada no existe en Stock", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        AgregarNuevoProducto(_productoSeleccionado);
        numCantidad.Select();
        LimpiarDetalle();
    }

    private void LimpiarDetalle()
    {
        txtCodigoProducto.Text = "";
        txtProductoDescripcion.Text = "";
        txtPrecioVenta.Text = "";
        txtStock.Text = "";
        numCantidad.Value = 0;
        _productoSeleccionado = null;
    }

    private void LimpiarForumulario()
    {
        cboTipoDocumento.SelectedIndex = 0;
        txtNumeroDocumento.Text = "";
        txtNombreCliente.Text = "";
        txtNumeroDocumento.Text = "";
        txtCodigoProducto.Text = "";
        txtProductoDescripcion.Text = "";
        txtPrecioVenta.Text = "";
        numCantidad.Value = 0;
        dtgDatos.DataSource = null;
        _clienteSeleccionado = null;
        _productoSeleccionado = null;
        _lista = new List<ProductoDto>();
    }

    private void btnLimpiar_Click(object sender, EventArgs e)
    {
        LimpiarForumulario();
    }

    private void FrmVenta_Load(object sender, EventArgs e)
    {
        txtFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");

        CargarCombos();
    }

    private void CargarCombos()
    {
        var listaEstado = new List<OpcionCombo>() {
            new OpcionCombo(1, "Credito"),
            new OpcionCombo(2, "Contado")
        };

        cboTipoDocumento.DataSource = listaEstado;
        cboTipoDocumento.DisplayMember = "Descripcion";
        cboTipoDocumento.ValueMember = "Valor";
    }

    private void CalcularTotal()
    {
        txtTotalPagar.Text = _lista.Sum(o => o.SubTotal).ToString("N2");
    }

    private void btnRegistrar_Click(object sender, EventArgs e)
    {
        if (_clienteSeleccionado == null)
        {
            MessageBox.Show($"Debe especificar el Cliente", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (_lista.Count() <= 0)
        {
            MessageBox.Show($"Debe Agregar un Producto", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (string.IsNullOrEmpty(txtMontoPago.Text))
        {
            MessageBox.Show($"Debe Especificar el monto pagado", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (Convert.ToDecimal(txtMontoPago.Text) <= 0)
        {
            MessageBox.Show($"Debe Especificar el monto pagado", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (Convert.ToDecimal(txtMontoPago.Text) != Convert.ToDecimal(txtTotalPagar.Text))
        {
            MessageBox.Show($"El monto a Pagar debe ser igual al total a pagar", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var obj = new Venta
        {
            IdVenta = 0,
            IdUsuario = 0,
            TipoDocumento = ((OpcionCombo)cboTipoDocumento.SelectedItem)?.Descripcion ?? "",
            NumeroDocumento = "",
            DocumentoCliente = txtNumeroDocumento.Text,
            NombreCliente = txtNombreCliente.Text,
            MontoPago = Convert.ToDecimal(txtMontoPago.Text),
            MontoCambio = Convert.ToDecimal(txtCambio.Text),
            MontoTotal = Convert.ToDecimal(txtTotalPagar.Text),
            FechaCreacion = DateTime.Now,
            ListaDetalle = _lista.Select(o => new DetalleVenta
            {
                IdDetalleVenta = 0,
                IdVenta = 0,
                IdProducto = o.IdProducto,
                PrecioVenta = o.PrecioVenta,
                Cantidad = o.Cantidad,
                SubTotal = o.SubTotal,
                FechaCreacion = DateTime.Now,
            }).ToList()
        };

        _servicio.Registrar(obj);

        LimpiarForumulario();

        MessageBox.Show($"Compra Registrada correctamente", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void txtCambio_TextChanged(object sender, EventArgs e)
    {

    }

    private void txtMontoPago_TextChanged(object sender, EventArgs e)
    {
        decimal.TryParse(txtMontoPago.Text, out decimal montoPagado);
        decimal.TryParse(txtTotalPagar.Text, out decimal totalPagar);

        txtCambio.Text = (totalPagar - montoPagado).ToString();
    }

    private void dtgDatos_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyValue == 46)
        {
            if (dtgDatos.Rows.Count > 0)
            {
                int rowIndex = dtgDatos.CurrentCell.RowIndex;
                int id = Convert.ToInt32(dtgDatos.Rows[rowIndex].Cells["idProducto"].Value);

                var item = _lista.FirstOrDefault(o => o.IdProducto == id);
                _lista.Remove(item);
                CrearDataSet();
                CalcularTotal();
            }
        }
    }
}
