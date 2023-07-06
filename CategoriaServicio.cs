using ProyectoVentas.Core.Helpers;
using ProyectoVentas.Datos;
using ProyectoVentas.Entidades;
using ProyectoVentas.Negocio.ViewModels;

namespace ProyectoVentas.Negocio;

public class CategoriaServicio
{
    private CategoriaData _dataServicio;

    public CategoriaServicio()
    {
        _dataServicio = new CategoriaData();
    }

    public Categoria ObtenerPorId(int id)
    {
        try
        {
            return _dataServicio.ObtenerPorId(id);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error: {ex.Message}", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return null;
        }
    }


    public bool Registrar(Categoria obj)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(obj.Descripcion))
            {
                MessageBox.Show($"Debe Especificar el Documento", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (_dataServicio.ExisteDescripcion(obj.Descripcion, obj.IdCategoria))
            {
                MessageBox.Show($"La Descripcion especificada ya existe para otro registro", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return _dataServicio.Registrar(obj);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error: {ex.Message}", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }
    }

    public bool Eliminar(int id)
    {
        try
        {
            var result = _dataServicio.Eliminar(id);

            if (result == false)
            {
                MessageBox.Show($"No Se pudo eliminar el registro", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error: {ex.Message}", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }
    }

    public List<CategoriaVm> ObtenerListado()
    {
        try
        {
            var lista = _dataServicio.ObtenerListado();

            var listaVm = new List<CategoriaVm>();

            if (lista != null && lista.Count() > 0)
            {
                foreach (var item in lista)
                {
                    var vm = BaseHelper.MapObjects<CategoriaVm>(item, new CategoriaVm());
                    vm.EstadoDescripcion = item.Estado ? "Activo" : "Inactivo";
                    vm.Fecha = item.FechaCreacion.ToString("dd/MM/yyyy");

                    listaVm.Add(vm);
                }

                return listaVm;
            }

            return new List<CategoriaVm>();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error: {ex.Message}", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return new List<CategoriaVm>();
        }
    }

    public List<Categoria> ObtenerParaCombo()
    {
        var lista = _dataServicio.ObtenerParaCombo();
        if (lista == null)
        {
            return new List<Categoria>();
        }

        return lista;
    }
}
