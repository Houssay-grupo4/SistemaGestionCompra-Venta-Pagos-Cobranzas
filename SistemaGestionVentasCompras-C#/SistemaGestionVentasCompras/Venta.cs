using System;
using System.Collections.Generic;
using System.Linq;

namespace SistemaGestionVentasCompras
{
    class Venta
    {
        public int Codigo_venta { get; set; }
        public DateTime Fecha_venta { get; set; }
        public double Total_venta { get; set; }
        public List<Producto> ListaProductos { get; set; }
        public Cliente Cliente { get; set; }

        public Venta()
        {
            this.ListaProductos = new List<Producto>();
        }

        public Venta(int codigo_venta, DateTime fecha_venta, double total_venta, List<Producto> listaProductos, Cliente cliente)
        {
            Codigo_venta = codigo_venta;
            Fecha_venta = fecha_venta;
            Total_venta = total_venta;
            ListaProductos = listaProductos;
            Cliente = cliente;
        }

        public void IngresarVentaNueva(Cliente cliente, List<Producto> productos)
        {
            Random random = new Random();
            int codigoVentaAleatorio = random.Next(1, 1000);
            DateTime fechaVentaActual = DateTime.Now;

            if (cliente == null)
            {
                Console.WriteLine("\nNo se ha especificado un cliente para la venta.");
                return;
            }

            if (productos == null || productos.Count == 0)
            {
                Console.WriteLine("\nLa lista de productos está vacía.");
                return;
            }

            Codigo_venta = codigoVentaAleatorio;
            Fecha_venta = fechaVentaActual;
            Cliente = cliente;
            ListaProductos.AddRange(productos);
            Total_venta = ListaProductos.Sum(producto => producto.Precio);

            Console.WriteLine("\n¡¡¡VENTA REGISTRADA CON ÉXITO!!!");
        }

        public List<Venta> ListarVentas(List<Venta> listaVentas, Venta ventaNueva)
        {
            listaVentas.Add(ventaNueva);
            return listaVentas;
        }

        public Venta BuscarVentaPorCodigo(List<Venta> listaVentas, int codigo)
        {
            foreach (Venta venta in listaVentas)
            {
                if (venta.Codigo_venta == codigo)
                {
                    return venta;
                }
            }
            return null;
        }

        public override string ToString()
        {
            return "\nVenta{" + "codigo_venta=" + Codigo_venta + ", fecha_venta=" + Fecha_venta
                + ", total_venta=" + Total_venta + ", listaProductos=" + string.Join(", ", ListaProductos)
                + ", cliente=" + Cliente + '}';
        }
    }
}
