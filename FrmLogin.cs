using ProyectoVentas.Negocio;

namespace ProyectoVentas;

public partial class FrmLogin : Form
{
    private UsuarioServicio _servicio;

    public FrmLogin()
    {
        InitializeComponent();

        _servicio = new UsuarioServicio();
    }

    private void btnAceptar_Click(object sender, EventArgs e)
    {
        var usuario = _servicio.IniciarSesion(txtDocumento.Text, txtClave.Text);

        if (usuario != null)
        {
            FrmPrincipal Form = new FrmPrincipal(usuario);
            Form.Show();
            Hide();
            LimpiarDatos();
        }
    }

    private void LimpiarDatos()
    {
        txtClave.Text = "";
        txtDocumento.Text = "";
    }

    private void btnCancelar_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void FrmLogin_Load(object sender, EventArgs e)
    {
        var dataServicio = new SistemaServicio();

        dataServicio.InicializarSistema();
    }
}
