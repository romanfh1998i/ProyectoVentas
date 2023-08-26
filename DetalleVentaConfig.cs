using Microsoft.EntityFrameworkCore;
using ProyectoVentas.Entidades;

namespace ProyectoVentas.Datos.ModelConfig;

public static class DetalleVentaConfig
{
    public static ModelBuilder ConfigurarDetalleVenta(this ModelBuilder modelbuilder)
    {
        modelbuilder.Entity<DetalleVenta>(etb =>
        {
            etb.ToTable("DetalleVenta", "dbo");

            etb.HasKey(e => e.IdDetalleVenta);

            etb.Property(e => e.IdDetalleVenta);

            etb.Property(e => e.IdVenta)
                .HasColumnType("int")
                .IsRequired();

            etb.Property(e => e.IdProducto)
                .HasColumnType("int")
                .IsRequired();

            etb.Property(e => e.PrecioVenta)
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            etb.Property(e => e.Cantidad)
                .HasColumnType("int")
                .IsRequired();

            etb.Property(e => e.SubTotal)
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
