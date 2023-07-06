using ProyectoVentas.Entidades;
using ProyectoVentas.Negocio;
using ProyectoVentas.Negocio.ViewModels;
using ProyectoVentas.Presentacion.Helpers;
using System.Data;
using CoreBaseHelper = ProyectoVentas.Core.Helpers.BaseHelper;

namespace ProyectoVentas.Presentacion;

public partial class FrmCompra : Form
{
    private CompraServicio _servicio;
    private ProductoServicio _productoServicio;
    private ProveedorServicio _proveedorServicio;

    private List<ProductoDto> _lista = new List<ProductoDto>();
    private Proveedor _proveedorSeleccionado = null;
    private Producto _productoSeleccionado = null;

    public FrmCompra()
    {
        InitializeComponent();

        _servicio = new CompraServicio();
        _productoServicio = new ProductoServicio();
        _proveedorServicio = new ProveedorServicio();
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
            txtPrecioCompra.Text = _productoSeleccionado.PrecioCompra.ToString();
        }
    }

    private void btnBuscarProveedor_Click(object sender, EventArgs e)
    {
        var form = new FrmConsultaProveedor();
        var resultado = form.ShowDialog();

        if (resultado == DialogResult.OK)
        {
            _proveedorSeleccionado = _proveedorServicio.ObtenerPorId(form.idSeleccionado);
            txtNumeroDocumento.Text = _proveedorSeleccionado.Documento;
            txtRazonSocial.Text = _proveedorSeleccionado.RazonSocial;
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

        var vm = new ProductoDto();

        CoreBaseHelper.MapObjects<ProductoDto>(producto, vm);
        vm.Producto = producto.Descripcion;
        vm.PrecioVenta = Convert.ToDecimal(txtPrecioVenta.Text);
        vm.PrecioCompra = Convert.ToDecimal(txtPrecioCompra.Text);
        vm.Cantidad = cantidad;
        vm.SubTotal = vm.PrecioCompra * cantidad;

        _lista.Add(vm);

        CrearDataSet();

        CalcularTotal();
    }

    private void CalcularTotal()
    {
        txtTotalPagar.Text = _lista.Sum(o => o.SubTotal).ToString("N2");
    }

    private void CrearDataSet()
    {
        DataTable dataTable = new DataTable();
        dataTable.Columns.Add("idProducto", typeof(string));
        dataTable.Columns.Add("Producto", typeof(string));
        dataTable.Columns.Add("Precio Compra", typeof(string));
        dataTable.Columns.Add("Precio Venta", typeof(string));
        dataTable.Columns.Add("Cantidad", typeof(string));
        dataTable.Columns.Add("SubTotal", typeof(string));

        foreach (var item in _lista)
        {
            DataRow fila = dataTable.NewRow();
            fila["idProducto"] = item.IdProducto;
            fila["Producto"] = item.Producto;
            fila["Precio Compra"] = item.PrecioCompra;
            fila["Precio Venta"] = item.PrecioVenta;
            fila["Cantidad"] = item.Cantidad;
            fila["SubTotal"] = item.PrecioCompra * item.Cantidad;
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

        if (string.IsNullOrWhiteSpace(txtPrecioCompra.Text) || Convert.ToDecimal(txtPrecioCompra.Text) <= 0)
        {
            MessageBox.Show($"Debe especificar el precio de compra", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        AgregarNuevoProducto(_productoSeleccionado);
        txtPrecioCompra.Select();
        LimpiarDetalle();
    }

    private void CargarCombos()
    {
        var listaEstado = new List<OpcionCombo>() {
            new OpcionCombo(1, "Factura"),
            new OpcionCombo(2, "Boleta")
        };

        cboTipoDocumento.DataSource = listaEstado;
        cboTipoDocumento.DisplayMember = "Descripcion";
        cboTipoDocumento.ValueMember = "Valor";
    }

    private void FrmCompra_Load(object sender, EventArgs e)
    {
        txtFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");

        CargarCombos();
    }

    private void btnLimpiar_Click(object sender, EventArgs e)
    {
        LimpiarForumulario();
    }

    private void LimpiarDetalle()
    {
        txtCodigoProducto.Text = "";
        txtProductoDescripcion.Text = "";
        txtPrecioCompra.Text = "";
        txtPrecioVenta.Text = "";
        numCantidad.Value = 0;
        _productoSeleccionado = null;
    }

    private void LimpiarForumulario()
    {
        cboTipoDocumento.SelectedIndex = 0;
        txtNumeroDocumento.Text = "";
        txtRazonSocial.Text = "";
        txtCodigoProducto.Text = "";
        txtProductoDescripcion.Text = "";
        txtPrecioCompra.Text = "";
        txtPrecioVenta.Text = "";
        numCantidad.Value = 0;
        dtgDatos.DataSource = null;
        _proveedorSeleccionado = null;
        _productoSeleccionado = null;
        _lista = new List<ProductoDto>();
    }

    private void btnRegistrar_Click(object sender, EventArgs e)
    {
        if (_proveedorSeleccionado == null)
        {
            MessageBox.Show($"Debe especificar El proveedor", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (_lista.Count() <= 0)
        {
            MessageBox.Show($"Debe Agregar un Producto", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var obj = new Compra
        {
            IdCompra = 0,
            IdUsuario = 1,
            IdProveedor = _proveedorSeleccionado.IdProveedor,
            TipoDocumento = ((OpcionCombo)cboTipoDocumento.SelectedItem)?.Descripcion ?? "",
            NumeroDocumento = "",
            MontoTotal = 0,
            FechaCreacion = DateTime.Now,
            ListaDetalle = _lista.Select(o => new DetalleCompra
            {
                IdDetalleCompra = 0,
                IdCompra = 0,
                IdProducto = o.IdProducto,
                PrecioCompra = o.PrecioCompra,
                PrecioVenta = o.PrecioVenta,
                Cantidad = o.Cantidad,
                MontoTotal = o.SubTotal,
                FechaCreacion = DateTime.Now,
            }).ToList()
        };

        _servicio.Registrar(obj);

        LimpiarForumulario();

        MessageBox.Show($"Compra Registrada correctamente", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
