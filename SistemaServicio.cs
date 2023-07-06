using ProyectoVentas.Datos;

namespace ProyectoVentas.Negocio;

public class SistemaServicio
{
    private SistemaData _dataServicio;

    public SistemaServicio()
	{
		_dataServicio = new SistemaData();
	}

	public void InicializarSistema()
	{
		_dataServicio.InicializarSistema();
	}
}
