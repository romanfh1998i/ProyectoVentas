using Microsoft.EntityFrameworkCore;
using ProyectoVentas.Entidades;

namespace ProyectoVentas.Datos.ModelConfig;

public static class VentaConfig
{
    public static ModelBuilder ConfigurarVenta(this ModelBuilder modelbuilder)
    {
        modelbuilder.Entity<Venta>(etb =>
        {
            etb.ToTable("Venta", "dbo");

            etb.HasKey(e => e.IdVenta);

            etb.Property(e => e.IdVenta);

            etb.Property(e => e.IdUsuario)
                .HasColumnType("int")
                .IsRequired();

            etb.Property(e => e.TipoDocumento)
                .HasColumnType("varchar(50)")
                .IsRequired();

            etb.Property(e => e.NumeroDocumento)
                .HasColumnType("varchar(50)")
                .IsRequired();

            etb.Property(e => e.DocumentoCliente)
                .HasColumnType("varchar(50)")
                .IsRequired();

            etb.Property(e => e.NombreCliente)
                .HasColumnType("varchar(100)")
                .IsRequired();

            etb.Property(e => e.MontoPago)
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            etb.Property(e => e.MontoCambio)
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            etb.Property(e => e.MontoTotal)
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            etb.Property(e => e.FechaCreacion)
                .HasColumnType("datetime")
                .IsRequired();

            etb.HasMany(e => e.ListaDetalle)
              .WithOne(x => x.Venta)
              .HasForeignKey(x => x.IdVenta)
              .HasConstraintName("ForeignKey_Venta_DetalleVenta")
              .OnDelete(DeleteBehavior.Cascade);

        });

        return modelbuilder;
    }
}
