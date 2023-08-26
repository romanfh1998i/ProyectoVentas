using Microsoft.EntityFrameworkCore;
using ProyectoVentas.Entidades;

namespace ProyectoVentas.Datos.ModelConfig;

public static class PermisoConfig
{
    public static ModelBuilder ConfigurarPermiso(this ModelBuilder modelbuilder)
    {
        modelbuilder.Entity<Permiso>(etb =>
        {
            etb.ToTable("Permiso", "dbo");

            etb.HasKey(e => e.IdPermiso);

            etb.Property(e => e.IdPermiso);

            etb.Property(e => e.IdRol)
                .HasColumnType("int")
                .IsRequired();

            etb.Property(e => e.Nombre)
                .HasColumnType("varchar(50)")
                .IsRequired();

            etb.Property(e => e.FechaCreacion)
                .HasColumnType("datetime")
                .IsRequired();

        });

        return modelbuilder;
    }
}
