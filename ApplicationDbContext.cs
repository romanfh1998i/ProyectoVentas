using Microsoft.EntityFrameworkCore;
using ProyectoVentas.Datos.ModelConfig;
using ProyectoVentas.Entidades;

namespace ProyectoVentas.Datos;

public class ApplicationDbContext : DbContext
{
    public DbSet<Rol> Rol { get; set; }
    public DbSet<Usuario> Usuario { get; set; }
    public DbSet<Categoria> Categoria { get; set; }
    public DbSet<Cliente> Cliente { get; set; }
    public DbSet<Compra> Compra { get; set; }
    public DbSet<DetalleCompra> DetalleCompra { get; set; }
    public DbSet<Permiso> Permiso { get; set; }
    public DbSet<Producto> Producto { get; set; }
    public DbSet<Proveedor> Proveedor { get; set; }
    public DbSet<Venta> Venta { get; set; }
    public DbSet<Negocio> Negocio { get; set; }

    public ApplicationDbContext() 
    {

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=MSI;Initial Catalog=Ventas;Integrated Security=True");
        //optionsBuilder.UseSqlServer("Data Source=DESA02-02\\SQL2014;Initial Catalog=Ventas;Integrated Security=True");
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigurarRol();
        builder.ConfigurarUsuario();
        builder.ConfigurarCategoria();
        builder.ConfigurarCliente();
        builder.ConfigurarCompra();
        builder.ConfigurarDetalleCompra();
        builder.ConfigurarDetalleVenta();
        builder.ConfigurarPermiso();
        builder.ConfigurarProducto();
        builder.ConfigurarProveedor();
        builder.ConfigurarVenta();
        builder.ConfigurarNegocio();


    }
}



