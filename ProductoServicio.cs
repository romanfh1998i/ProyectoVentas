using ProyectoVentas.Core.Helpers;
using ProyectoVentas.Datos;
using ProyectoVentas.Entidades;
using ProyectoVentas.Negocio.ViewModels;

namespace ProyectoVentas.Negocio;

public class ProductoServicio
{
    private ProductoData _dataServicio;

    public ProductoServicio()
    {
        _dataServicio = new ProductoData();
    }

    public Producto ObtenerPorId(int id)
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


    public bool Registrar(Producto obj)
    {
        try
        {

            if (string.IsNullOrWhiteSpace(obj.Codigo))
            {
                MessageBox.Show($"Debe Especificar el codigo", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(obj.Nombre))
            {
                MessageBox.Show($"Debe Especificar el Nombre", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(obj.Descripcion))
            {
                MessageBox.Show($"Debe Especificar la Descripción", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (obj.IdCategoria <= 0)
            {
                MessageBox.Show($"Debe Especificar la Categoría", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (_dataServicio.ExisteDescripcion(obj.Codigo, obj.IdProducto))
            {
                MessageBox.Show($"El codigo especificado ya existe para otro registro", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

    public List<ProductoVm> ObtenerListado()
    {
        try
        {
            var lista = _dataServicio.ObtenerListado();

            var listaVm = new List<ProductoVm>();

            if (lista != null && lista.Count() > 0)
            {
                foreach (var item in lista)
                {
                    var vm = BaseHelper.MapObjects<ProductoVm>(item, new ProductoVm());
                    vm.EstadoDescripcion = item.Estado ? "Activo" : "Inactivo";
                    vm.CategoriaDescripcion = item.Categoria?.Descripcion;
                    vm.Fecha = item.FechaCreacion.ToString("dd/MM/yyyy");

                    listaVm.Add(vm);
                }

                return listaVm;
            }

            return new List<ProductoVm>();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error: {ex.Message}", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return new List<ProductoVm>();
        }
    }
}
