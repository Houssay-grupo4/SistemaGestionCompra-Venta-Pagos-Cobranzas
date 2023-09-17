using System;

namespace SistemaGestionVentasCompras
{
   public  class Cliente
    {
        public int Dni_cliente { get; set; }
        public string NombreCompleto { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }

        public Cliente()
        {
        }

        public Cliente(int dni_cliente, string nombreCompleto, string telefono, string email)
        {
            Dni_cliente = dni_cliente;
            NombreCompleto = nombreCompleto;
            Telefono = telefono;
            Email = email;
        }

        private static void ValidarDatosCliente(int dni, string nombreCompleto, string telefono, string email)
        {
            if (dni <= 0)
            {
                throw new ArgumentException("El DNI debe ser un número positivo.");
            }
            if (string.IsNullOrEmpty(nombreCompleto))
            {
                throw new ArgumentException("Debe indicar el nombre completo del cliente.");
            }
            if (string.IsNullOrEmpty(telefono))
            {
                throw new ArgumentException("Debe indicar el teléfono del cliente.");
            }
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentException("Debe indicar el email del cliente.");
            }
        }

        public static Cliente CrearClienteNuevo()
        {
            try
            {
                Console.WriteLine("\nIngrese los siguientes datos del cliente:");
                Console.Write("DNI: ");
                int dni = int.Parse(Console.ReadLine());
                Console.Write("Nombre y Apellido: ");
                string nombre = Console.ReadLine();
                Console.Write("Teléfono: ");
                string tel = Console.ReadLine();
                Console.Write("Email: ");
                string correo = Console.ReadLine();

                ValidarDatosCliente(dni, nombre, tel, correo);
                Cliente cliente = new Cliente(dni, nombre, tel, correo);

                Console.WriteLine("\n¡¡¡CLIENTE GUARDADO CON ÉXITO!!!");
                return cliente;
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public override string ToString()
        {
            return "\nCliente{" + "dni_cliente = " + Dni_cliente + ", nombreCompleto = " + NombreCompleto
                + ", telefono = " + Telefono + ", email = " + Email + '}';
        }
    }
}

