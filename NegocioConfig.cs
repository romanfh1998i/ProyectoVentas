using Microsoft.EntityFrameworkCore;
using ProyectoVentas.Entidades;

namespace ProyectoVentas.Datos.ModelConfig;

public static class NegocioConfig
{
    public static ModelBuilder ConfigurarNegocio(this ModelBuilder modelbuilder)
    {
        modelbuilder.Entity<Negocio>(etb =>
        {
            etb.ToTable("Negocio", "dbo");

            etb.HasKey(e => e.IdNegocio);

            etb.Property(e => e.Nombre)
               .HasColumnType("varchar(60)")
               .IsRequired();

            etb.Property(e => e.Rnc)
               .HasColumnType("varchar(60)")
               .IsRequired();

            etb.Property(e => e.Direccion)
               .HasColumnType("varchar(100)")
               .IsRequired();

            etb.Property(e => e.Logo)
               .HasColumnType("varbinary(MAX)")
               .IsRequired();

            etb.Property(e => e.FechaCreacion)
               .HasColumnType("datetime")
               .IsRequired();
        });

        return modelbuilder;
    }
}
