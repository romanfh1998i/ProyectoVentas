using FontAwesome.Sharp;
using ProyectoVentas.Entidades;
using ProyectoVentas.Negocio;
using ProyectoVentas.Presentacion;

namespace ProyectoVentas;

public partial class FrmPrincipal : Form
{
    private IconMenuItem _menuActivo;
    private Form _formularioActivo;
    private Usuario _usuarioActual;
    private PermisoServicio _permisoServicio;

    public FrmPrincipal(Usuario usuario)
    {
        _usuarioActual = usuario != null && usuario.IdUsuario > 0
            ? usuario
            : new Usuario { NombreCompleto ="Admin Predefinido", IdUsuario = 1 };

        _permisoServicio = new PermisoServicio();

        InitializeComponent();

        StatusBar.Items[0].Text = $"Fecha: {DateTime.Now.ToString("dd/MM/yyyy")}";
        StatusBar.Items[1].Text = $"| Usuario Actual: {_usuarioActual.NombreCompleto}";
    }

    private void FrmPrincipal_Load(object sender, EventArgs e)
    {
        GenerarIconosMostrarMenu();
    }

    private void GenerarIconosMostrarMenu()
    {
        if (_usuarioActual.IdRol <= 0 ) { return; }

        var listaPermiso = _permisoServicio.ObtenerListadoPorRolId(_usuarioActual.IdRol);

        foreach (IconMenuItem iconMenu in Menu.Items)
        {
            if (iconMenu.Name != "MenuSalir")
            {
                iconMenu.Visible = listaPermiso.Any(o => o.Nombre == iconMenu.Name);
            }
        }
    }

    private void AbrirFormulario(IconMenuItem menu, Form formulario)
    {
        if (_menuActivo != null)
        {
            _menuActivo.BackColor = Color.White;
        }

        menu.BackColor = Color.Silver;

        _menuActivo = menu;

        if (_formularioActivo != null)
        {
            _formularioActivo.Close();
        }

        _formularioActivo = formulario;
        formulario.TopLevel = false;
        formulario.FormBorderStyle = FormBorderStyle.None;
        formulario.Dock = DockStyle.Fill;
        formulario.BackColor = Color.SteelBlue;
        PanelContendor.Controls.Add(formulario);
        formulario.Show();

    }



    private void FrmPrincipal_FormClosed(object sender, FormClosedEventArgs e)
    {
        Application.Exit();
    }

    private void menuSalir_Click(object sender, EventArgs e)
    {
        var dialogResult = MessageBox.Show("Desea Salir del Sistema", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        if (dialogResult == DialogResult.Yes)
        {
            Application.Exit();
        }
    }

    private void MenuUsuario_Click(object sender, EventArgs e)
    {
        AbrirFormulario((IconMenuItem)sender, new FrmUsuario());
    }

    private void SubMenuCategoria_Click(object sender, EventArgs e)
    {
        AbrirFormulario((IconMenuItem)sender, new FrmCategoria());
    }

    private void SubMenuProducto_Click(object sender, EventArgs e)
    {
        AbrirFormulario((IconMenuItem)sender, new FrmProducto());
    }

    private void SubMenuRegistrarVenta_Click(object sender, EventArgs e)
    {
        AbrirFormulario((IconMenuItem)sender, new FrmVenta());
    }

    private void SubMenuVerVenta_Click(object sender, EventArgs e)
    {
        AbrirFormulario((IconMenuItem)sender, new FrmDetalleVenta());
    }

    private void SubMenuRegistrarCompra_Click(object sender, EventArgs e)
    {
        AbrirFormulario((IconMenuItem)sender, new FrmCompra());
    }

    private void SubMenuVerCompra_Click(object sender, EventArgs e)
    {
        AbrirFormulario((IconMenuItem)sender, new FrmDetalleCompra());
    }

    private void MenuClientes_Click(object sender, EventArgs e)
    {
        AbrirFormulario((IconMenuItem)sender, new FrmCliente());
    }

    private void MenuProveedores_Click(object sender, EventArgs e)
    {
        AbrirFormulario((IconMenuItem)sender, new FrmProveedor());
    }

    private void MenuReportes_Click(object sender, EventArgs e)
    {
        AbrirFormulario((IconMenuItem)sender, new FrmReporte());
    }

    private void iconMenuItem1_Click(object sender, EventArgs e)
    {
        AbrirFormulario((IconMenuItem)sender, new FrmNegocio());
    }
}
