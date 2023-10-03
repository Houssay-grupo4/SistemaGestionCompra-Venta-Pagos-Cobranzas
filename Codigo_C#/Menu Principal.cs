using System;

namespace SistemaGestionVentasCompras
{
    class Menu_Principal
    {
        static void Main(string[] args)
        {
            Console.WriteLine("************** BIENVENIDO A LA GESTIÓN DE LA EMPRESA **************");

            int opcion = 0;
            Ventas venta = new Ventas(); // Crear una instancia de la clase Ventas
            Compras compra = new Compras();
            Cobros cobro = new Cobros();
            Pagos pago = new Pagos();

            while (opcion != 5)
            {
                Console.WriteLine("¿Qué es lo que desea hacer?:");
                Console.WriteLine("1) Ingresar a gestión de ventas.");
                Console.WriteLine("2) Ingresar a gestión de compras.");
                Console.WriteLine("3) Ingresar a gestión de Cobranzas.");
                Console.WriteLine("4) Ingresar a gestión de Pagos.");
                Console.WriteLine("5) Salir.");
                Console.Write("\nIngrese la opción seleccionada: ");
                opcion = Convert.ToInt32(Console.ReadLine());

                switch (opcion)
                {
                    case 1:
                        venta.GestionarVentas();
                        break;
                    case 2:
                        compra.MenuCompras();
                        break;
                    case 3:
                        cobro.MenuCobros();
                        break;
                    case 4:
                        // Llamar al menú de Pagos
                        pago.MenuPagos();
                        break;
                    case 5:
                        Console.WriteLine("Saliendo del Sistema...\nHasta la próxima!!!!");
                        break;
                    default:
                        Console.WriteLine("Opción inválida. Por favor, ingrese una de las opciones que aparecen en el menú.");
                        break;
                }
            }
        }
    }
}
