using Microsoft.EntityFrameworkCore;
using ProyectoVentas.Entidades;

namespace ProyectoVentas.Datos.ModelConfig;

public static class CompraConfig
{
    public static ModelBuilder ConfigurarCompra(this ModelBuilder modelbuilder)
    {
        modelbuilder.Entity<Compra>(etb =>
        {
            etb.ToTable("Compra", "dbo");

            etb.HasKey(e => e.IdCompra);

            etb.Property(e => e.IdCompra);

            etb.Property(e => e.IdUsuario)
                .HasColumnType("int")
                .IsRequired();

            etb.Property(e => e.IdProveedor)
                .HasColumnType("int")
                .IsRequired();

            etb.Property(e => e.TipoDocumento)
                .HasColumnType("varchar(50)")
                .IsRequired();

            etb.Property(e => e.NumeroDocumento)
                .HasColumnType("varchar(50)")
                .IsRequired();

            etb.Property(e => e.MontoTotal)
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            etb.Property(e => e.FechaCreacion)
                .HasColumnType("datetime")
                .IsRequired();

            etb.HasMany(e => e.ListaDetalle)
              .WithOne(x => x.Compra)
              .HasForeignKey(x => x.IdCompra)
              .HasConstraintName("ForeignKey_Compra_DetalleCompra")
              .OnDelete(DeleteBehavior.Cascade);

        });

        return modelbuilder;
    }
}
