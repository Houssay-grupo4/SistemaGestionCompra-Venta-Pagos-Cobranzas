using System;

namespace SistemaGestionVentasCompras
{
    class Producto
    {
        public int Codigo_producto { get; set; }
        public string Nombre { get; set; }
        public int Cantidad { get; set; }
        public double Precio { get; set; }

        public Producto()
        {
        }

        public Producto(int codigo_producto, string nombre, int cantidad, double precio)
        {
            Codigo_producto = codigo_producto;
            Nombre = nombre;
            Cantidad = cantidad;
            Precio = precio;
        }

        private static void ValidarDatosProducto(string nombre, int cantidad, double precio)
        {
            if (string.IsNullOrEmpty(nombre))
            {
                throw new ArgumentException("Debe indicar el nombre del producto.");
            }
            if (cantidad <= 0)
            {
                throw new ArgumentException("La cantidad debe ser un número positivo.");
            }
            if (precio <= 0)
            {
                throw new ArgumentException("El precio debe ser un número positivo.");
            }
        }

        public static Producto IngresarProductoNuevo()
        {
            try
            {
                Random random = new Random();
                int codigo_producto = random.Next(1, 1000);
                Console.WriteLine("\nIngrese los siguientes datos del producto:");
                Console.Write("Nombre del producto: ");
                string nombre = Console.ReadLine();
                Console.Write("Precio del producto: ");
                double precio = Convert.ToDouble(Console.ReadLine());
                Console.Write("Cantidad de unidades del producto: ");
                int cantidad = Convert.ToInt32(Console.ReadLine());

                ValidarDatosProducto(nombre, cantidad, precio);
                Producto producto = new Producto(codigo_producto, nombre, cantidad, precio);

                Console.WriteLine("\n¡¡¡PRODUCTO GUARDADO CON ÉXITO!!!");

                return producto;
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            catch (FormatException)
            {
                Console.WriteLine("Error de formato al ingresar los datos del producto.");
                return null;
            }
        }

        public override string ToString()
        {
            return "\nProducto{" + "codigo_producto=" + Codigo_producto + ", nombre=" + Nombre
                + ", cantidad=" + Cantidad + ", precio=" + Precio + '}';
        }
    }
}
