using ProyectoVentas.Datos;
using ProyectoVentas.Entidades;

namespace ProyectoVentas.Negocio;

public class RolServicio
{
    private RolData _dataServicio;

    public RolServicio()
    {
        _dataServicio = new RolData();
    }

    public List<Rol> ObtenerParaCombo()
    {
        var lista = _dataServicio.ObtenerParaCombo();
        if (lista == null)
        {
            return new List<Rol>();
        }

        return lista;
    }
}
