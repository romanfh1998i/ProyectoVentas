using ProyectoVentas.Core.Helpers;
using ProyectoVentas.Datos;
using ProyectoVentas.Entidades;
using ProyectoVentas.Negocio.ViewModels;

namespace ProyectoVentas.Negocio;

public class ProveedorServicio
{
    private ProveedorData _dataServicio;

    public ProveedorServicio()
    {
        _dataServicio = new ProveedorData();
    }

    public Proveedor ObtenerPorId(int id)
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


    public bool Registrar(Proveedor obj)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(obj.Documento))
            {
                MessageBox.Show($"Debe Especificar el Documento", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(obj.RazonSocial))
            {
                MessageBox.Show($"Debe Especificar La Razón Social", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(obj.Telefono))
            {
                MessageBox.Show($"Debe Especificar El Teléfono", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (_dataServicio.ExisteDescripcion(obj.Documento, obj.IdProveedor))
            {
                MessageBox.Show($"El Documento especificado ya existe para otro registro", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

    public List<ProveedorVm> ObtenerListado()
    {
        try
        {
            var lista = _dataServicio.ObtenerListado();

            var listaVm = new List<ProveedorVm>();

            if (lista != null && lista.Count() > 0)
            {
                foreach (var item in lista)
                {
                    var vm = BaseHelper.MapObjects<ProveedorVm>(item, new ProveedorVm());
                    vm.EstadoDescripcion = item.Estado ? "Activo" : "Inactivo";
                    vm.Fecha = item.FechaCreacion.ToString("dd/MM/yyyy");

                    listaVm.Add(vm);
                }

                return listaVm;
            }

            return new List<ProveedorVm>();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error: {ex.Message}", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return new List<ProveedorVm>();
        }
    }
}
