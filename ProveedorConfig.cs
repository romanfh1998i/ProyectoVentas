using Microsoft.EntityFrameworkCore;
using ProyectoVentas.Entidades;

namespace ProyectoVentas.Datos.ModelConfig;

public static class ProveedorConfig
{
    public static ModelBuilder ConfigurarProveedor(this ModelBuilder modelbuilder)
    {
        modelbuilder.Entity<Proveedor>(etb =>
        {
            etb.ToTable("Proveedor", "dbo");

            etb.HasKey(e => e.IdProveedor);

            etb.Property(e => e.IdProveedor);

            etb.Property(e => e.Documento)
                .HasColumnType("varchar(50)")
                .IsRequired();

            etb.Property(e => e.RazonSocial)
                .HasColumnType("varchar(50)")
                .IsRequired();

            etb.Property(e => e.Correo)
                .HasColumnType("varchar(50)")
                .IsRequired();

            etb.Property(e => e.Telefono)
                .HasColumnType("varchar(50)")
                .IsRequired();

            etb.Property(e => e.Estado)
                .HasColumnType("bit")
                .IsRequired();

            etb.Property(e => e.FechaCreacion)
                .HasColumnType("datetime")
                .IsRequired();

        });

        return modelbuilder;
    }
}
