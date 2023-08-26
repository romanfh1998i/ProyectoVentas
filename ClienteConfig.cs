using Microsoft.EntityFrameworkCore;
using ProyectoVentas.Entidades;

namespace ProyectoVentas.Datos.ModelConfig;

public static class ClienteConfig
{
    public static ModelBuilder ConfigurarCliente(this ModelBuilder modelbuilder)
    {
        modelbuilder.Entity<Cliente>(etb =>
        {
            etb.ToTable("Cliente", "dbo");

            etb.HasKey(e => e.IdCliente);

            etb.Property(e => e.IdCliente);

            etb.Property(e => e.Documento)
                .HasColumnType("varchar(50)")
                .IsRequired();

            etb.Property(e => e.NombreCompleto)
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
