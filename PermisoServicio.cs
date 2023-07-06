using ProyectoVentas.Datos;
using ProyectoVentas.Entidades;

namespace ProyectoVentas.Negocio;

public class PermisoServicio
{
    private PermisoData _dataServicio;

    public PermisoServicio()
    {
        _dataServicio = new PermisoData();
    }

    public List<Permiso> ObtenerListadoPorRolId(int idRol)
    {
        var lista = _dataServicio.ObtenerListadoPorRolId(idRol);
        if (lista == null)
        {
            return new List<Permiso>();
        }

        return lista;
    }
}
