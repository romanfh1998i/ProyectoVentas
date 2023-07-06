﻿namespace ProyectoVentas.Entidades;

public class Producto
{
    public int IdProducto { get; set; }
    public string Codigo { get; set; }
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public int Stock { get; set; }
    public int IdCategoria { get; set; }
    public decimal PrecioCompra { get; set; }
    public decimal PrecioVenta { get; set; }
    public bool Estado { get; set; }
    public DateTime FechaCreacion { get; set; }

    public virtual Categoria Categoria { get; set; }
}
