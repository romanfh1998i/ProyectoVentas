using ProyectoVentas.Core.Helpers;
using ProyectoVentas.Datos;
using ProyectoVentas.Entidades;
using ProyectoVentas.Negocio.ViewModels;

namespace ProyectoVentas.Negocio;

public class UsuarioServicio
{
    private UsuarioData _dataServicio;

    public UsuarioServicio()
    {
        _dataServicio = new UsuarioData();
    }

    public Usuario IniciarSesion(string documento, string clave)
    {
        if (string.IsNullOrWhiteSpace(documento))
        {
            MessageBox.Show($"Debe Especificar el Documento", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return null;
        }

        if (string.IsNullOrWhiteSpace(clave))
        {
            MessageBox.Show($"Debe especificar la clave", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return null;
        }

        try
        {
            var usuario = _dataServicio.ObtenerUsuario(documento, clave);

            if (usuario == null)
            {
                MessageBox.Show($"El usuario o la clave son incorrectos", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return usuario;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error: {ex.Message}", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return null;
        }
    }

    public Usuario ObtenerPorId(int id)
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


    public bool Registrar(Usuario obj)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(obj.Documento))
            {
                MessageBox.Show($"Debe Especificar el Documento", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(obj.Clave))
            {
                MessageBox.Show($"Debe especificar la clave", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(obj.NombreCompleto))
            {
                MessageBox.Show($"Debe especificar El Nombre", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (obj.IdRol <= 0)
            {
                MessageBox.Show($"Debe especificar Rol", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (_dataServicio.ExisteDescripcion(obj.Documento, obj.IdUsuario))
            {
                MessageBox.Show($"El Documento especificado ya existe para otro usuario", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            var usuario = _dataServicio.ObtenerPorId(id);

            if (usuario.EsPrimerUsuarioAdmin)
            {
                MessageBox.Show($"No Puede Eliminar el Usuario Administrador del Sistema", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

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

    public List<UsuarioVm> ObtenerListado()
    {
        try
        {
            var lista = _dataServicio.ObtenerListado();

            var listaVm = new List<UsuarioVm>();

            if (lista != null && lista.Count() > 0)
            {
                //var listaVm = lista
                //    .Select(o => BaseHelper.MapObjects<UsuarioVm>(o, new UsuarioVm()))
                //    .ToList();

                foreach (var item in lista)
                {
                    var vm = BaseHelper.MapObjects<UsuarioVm>(item, new UsuarioVm());
                    vm.RolDescripcion = item.Rol?.Descripcion;
                    vm.EstadoDescripcion = item.Estado ? "Activo" : "Inactivo";
                    vm.Fecha = item.FechaCreacion.ToString("dd/MM/yyyy");

                    listaVm.Add(vm);
                }

                return listaVm;
            }

            return new List<UsuarioVm>();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error: {ex.Message}", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return new List<UsuarioVm>();
        }
    }
}
