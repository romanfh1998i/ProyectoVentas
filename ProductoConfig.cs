using Microsoft.EntityFrameworkCore;
using ProyectoVentas.Entidades;

namespace ProyectoVentas.Datos.ModelConfig;
public static class ProductoConfig
{
    public static ModelBuilder ConfigurarProducto(this ModelBuilder modelbuilder)
    {
        modelbuilder.Entity<Producto>(etb =>
        {
            etb.ToTable("Producto", "dbo");

            etb.HasKey(e => e.IdProducto);

            etb.Property(e => e.IdProducto);

            etb.Property(e => e.Codigo)
                .HasColumnType("varchar(50)")
                .IsRequired();

            etb.Property(e => e.Nombre)
                .HasColumnType("varchar(50)")
                .IsRequired();

            etb.Property(e => e.Descripcion)
                .HasColumnType("varchar(50)")
                .IsRequired();

            etb.Property(e => e.Stock)
                .HasColumnType("int")
                .IsRequired();

            etb.Property(e => e.IdCategoria)
                .HasColumnType("int")
                .IsRequired();

            etb.Property(e => e.PrecioCompra)
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            etb.Property(e => e.PrecioVenta)
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            etb.Property(e => e.Estado)
                .HasColumnType("bit")
                .IsRequired();

            etb.Property(e => e.FechaCreacion)
                .HasColumnType("datetime")
                .IsRequired();

            etb.HasOne(e => e.Categoria)
              .WithMany()
              .HasForeignKey(e => e.IdCategoria)
              .OnDelete(DeleteBehavior.Restrict);

        });

        return modelbuilder;
    }
}
