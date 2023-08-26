using Microsoft.EntityFrameworkCore;
using ProyectoVentas.Entidades;

namespace ProyectoVentas.Datos.ModelConfig;

public static class RolConfig
{
    public static ModelBuilder ConfigurarRol(this ModelBuilder modelbuilder)
    {
        modelbuilder.Entity<Rol>(etb =>
        {
            etb.ToTable("Rol", "dbo");

            etb.HasKey(e => e.IdRol);

            etb.Property(e => e.Descripcion)
               .HasColumnType("varchar(50)")
               .IsRequired(); ;

            etb.Property(e => e.FechaDeRegistro)
               .HasColumnType("datetime")
               .IsRequired();
        });

        return modelbuilder;
    }
}
