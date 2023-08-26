using Microsoft.EntityFrameworkCore;
using ProyectoVentas.Entidades;

namespace ProyectoVentas.Datos.ModelConfig;

public static class DetalleCompraConfig
{
    public static ModelBuilder ConfigurarDetalleCompra(this ModelBuilder modelbuilder)
    {
        modelbuilder.Entity<DetalleCompra>(etb =>
        {
            etb.ToTable("DetalleCompra", "dbo");

            etb.HasKey(e => e.IdDetalleCompra);

            etb.Property(e => e.IdDetalleCompra);

            etb.Property(e => e.IdCompra)
                .HasColumnType("int")
                .IsRequired();

            etb.Property(e => e.IdProducto)
                .HasColumnType("int")
                .IsRequired();

            etb.Property(e => e.PrecioCompra)
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            etb.Property(e => e.PrecioVenta)
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            etb.Property(e => e.Cantidad)
                .HasColumnType("int")
                .IsRequired();

            etb.Property(e => e.MontoTotal)
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            etb.Property(e => e.FechaCreacion)
                .HasColumnType("datetime")
                .IsRequired();

            etb.HasOne(e => e.Producto)
               .WithMany()
               .HasForeignKey(e => e.IdProducto)
               .OnDelete(DeleteBehavior.Restrict);

        });

        return modelbuilder;
    }
}
