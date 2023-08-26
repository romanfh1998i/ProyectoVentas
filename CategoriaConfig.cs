using Microsoft.EntityFrameworkCore;
using ProyectoVentas.Entidades;

namespace ProyectoVentas.Datos.ModelConfig;

public static class CategoriaConfig
{
    public static ModelBuilder ConfigurarCategoria(this ModelBuilder modelbuilder)
    {
        modelbuilder.Entity<Categoria>(etb =>
        {
            etb.ToTable("Categoria", "dbo");

            etb.HasKey(e => e.IdCategoria);

            etb.Property(e => e.IdCategoria);

            etb.Property(e => e.Descripcion)
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
