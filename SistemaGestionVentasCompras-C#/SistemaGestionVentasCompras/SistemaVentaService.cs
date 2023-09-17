using System;
using System.Collections.Generic;
using System.Linq;

namespace SistemaGestionVentasCompras
{
    class SistemaVentaService
    {
        Venta venta;
        List<Venta> listaVentas;

        public SistemaVentaService()
        {
            this.venta = new Venta();
            this.listaVentas = new List<Venta>();
        }

        public void MenuGestionVentas()
        {
            string salir = "N";
            int opcion;
            do
            {
                Console.WriteLine("\n************** BIENVENIDO A LA GESTIÓN DE VENTAS **************");
                Console.WriteLine("¿Qué es lo que desea hacer?:");
                Console.WriteLine("1) Ingresar una venta nueva.");
                Console.WriteLine("2) Ver listado de todas las ventas.");
                Console.WriteLine("3) Buscar una venta por su código.");
                Console.WriteLine("4) Volver al menú principal.");
                Console.Write("Ingrese la opción seleccionada: ");
                opcion = Convert.ToInt32(Console.ReadLine());

                switch (opcion)
                {
                    case 1:
                        Venta ventaNueva = IngresoVenta();
                        listaVentas.Add(ventaNueva);
                        break;
                    case 2:
                        ListarTodasLasVentas(listaVentas);
                        break;
                    case 3:
                        Console.Write("\nIngrese el código de la venta a buscar: ");
                        int codigoBusqueda = Convert.ToInt32(Console.ReadLine());
                        Venta ventaEncontrada = BuscarVentaPorCodigo(listaVentas, codigoBusqueda);
                        if (ventaEncontrada != null)
                        {
                            Console.WriteLine("\nVenta encontrada: " + ventaEncontrada);
                        }
                        else
                        {
                            Console.WriteLine("\nNo se encontró ninguna venta con ese código.");
                        }
                        break;
                    case 4:
                        Console.WriteLine("\n¿Desea volver al menú principal? (S/N): ");
                        salir = Console.ReadLine();
                        break;
                    default:
                        Console.WriteLine("\nOpción no válida.");
                        break;
                }

            } while (salir.Equals("N", StringComparison.OrdinalIgnoreCase));
            if (salir.Equals("S", StringComparison.OrdinalIgnoreCase))
            {
                //SistemaGestionVentasCompras.Main(new string[] { });
            }
        }

        public Venta IngresoVenta()
        {
            Cliente cliente = new Cliente();
            Producto producto = new Producto();
            Cliente clienteNuevo = Cliente.CrearClienteNuevo();
            string continuarIngreso;

            Venta ventaNueva = new Venta();
            List<Producto> carritoProductos = new List<Producto>();

            do
            {
                Producto productoNuevo = Producto.IngresarProductoNuevo();
                carritoProductos.Add(productoNuevo);

                Console.Write("\nDesea agregar otro producto al carrito? (S/N): ");
                continuarIngreso = Console.ReadLine();

            } while (continuarIngreso.Equals("S", StringComparison.OrdinalIgnoreCase));

            ventaNueva.IngresarVentaNueva(clienteNuevo, carritoProductos);

            return ventaNueva;
        }

        public void ListarTodasLasVentas(List<Venta> listaVentas)
        {
            if (listaVentas.Count == 0)
            {
                Console.WriteLine("\nNo hay ventas registradas.");
                return;
            }

            Console.WriteLine("\nLISTADO DE VENTAS:");
            foreach (Venta venta in listaVentas)
            {
                Console.WriteLine(venta);
            }
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
    }
}
