namespace ProyectoVentas.Core.Helpers;

public static class BaseHelper
{

    public static T MapObjects<T>(object source, object destination)
        where T : class
    {
        // Obtener la lista de propiedades con el mismo nombre en ambas clases
        var properties = source.GetType().GetProperties()
            .Where(s => destination.GetType().GetProperty(s.Name) != null);

        // Asignar los valores de las propiedades correspondientes de source a destination
        foreach (var prop in properties)
        {
            var value = prop.GetValue(source, null);
            destination.GetType().GetProperty(prop.Name).SetValue(destination, value, null);
        }

        return (T)destination;
    }
}
