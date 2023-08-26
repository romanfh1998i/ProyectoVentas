using Microsoft.EntityFrameworkCore;
using ProyectoVentas.Entidades;

namespace ProyectoVentas.Datos.ModelConfig;

public static class UsuarioConfig
{
    public static ModelBuilder ConfigurarUsuario(this ModelBuilder modelbuilder)
    {
        modelbuilder.Entity<Usuario>(etb =>
        {
            etb.ToTable("Usuario", "dbo");

            etb.HasKey(e => e.IdUsuario);

            etb.Property(e => e.Documento)
               .HasColumnType("varchar(50)")
               .IsRequired(); ;

            etb.Property(e => e.NombreCompleto)
               .HasColumnType("varchar(50)")
               .IsRequired();

            etb.Property(e => e.Correo)
               .HasColumnType("varchar(50)")
               .IsRequired();

            etb.Property(e => e.Clave)
               .HasColumnType("varchar(50)")
               .IsRequired();

            etb.Property(e => e.IdRol)
               .HasColumnType("int")
               .IsRequired();

            etb.Property(e => e.Estado)
               .HasColumnType("bit")
               .IsRequired();

            etb.Property(e => e.FechaCreacion)
               .HasColumnType("datetime")
               .IsRequired();

            etb.Property(e => e.EsPrimerUsuarioAdmin)
               .HasColumnType("bit")
               .IsRequired();

            etb.HasOne(e => e.Rol)
               .WithMany()
               .HasForeignKey(e => e.IdRol)
               .OnDelete(DeleteBehavior.Restrict);
        });

        return modelbuilder;
    }
}
