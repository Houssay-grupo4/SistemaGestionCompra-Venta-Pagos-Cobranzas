using System;
using System.Data.SqlClient;

namespace SistemaGestionVentasCompras
{
    public class Ventas
    {
        private SqlConnection connection;

        public Ventas()
        {
            string connectionString = "Data Source=localhost;Initial Catalog=Negocio;Integrated Security=True";
            connection = new SqlConnection(connectionString);
        }

        public void GestionarVentas()
        {
            int opcion = 0;

            while (opcion != 4)
            {
                Console.WriteLine("\nMenú de Gestión de Ventas:");
                Console.WriteLine("1. Agregar Cliente");
                Console.WriteLine("2. Modificar Cliente");
                Console.WriteLine("3. Borrar Cliente");
                Console.WriteLine("4. Volver al Menú Principal");
                Console.Write("Seleccione una opción: ");

                opcion = Convert.ToInt32(Console.ReadLine());

                switch (opcion)
                {
                    case 1:
                        Console.Write("Ingrese el nombre completo del cliente: ");
                        string nombreCompleto = Console.ReadLine();

                        Console.Write("Ingrese el teléfono del cliente: ");
                        string telefono = Console.ReadLine();

                        Console.Write("Ingrese el email del cliente: ");
                        string email = Console.ReadLine();

                        AgregarCliente(nombreCompleto, telefono, email);
                        break;
                    case 2:
                        Console.Write("Ingrese el ID del cliente a modificar: ");
                        int idClienteModificar = Convert.ToInt32(Console.ReadLine());

                        Console.Write("Ingrese el nuevo nombre completo del cliente: ");
                        string nuevoNombreCompleto = Console.ReadLine();

                        Console.Write("Ingrese el nuevo teléfono del cliente: ");
                        string nuevoTelefono = Console.ReadLine();

                        Console.Write("Ingrese el nuevo email del cliente: ");
                        string nuevoEmail = Console.ReadLine();

                        ModificarCliente(idClienteModificar, nuevoNombreCompleto, nuevoTelefono, nuevoEmail);
                        break;
                    case 3:
                        Console.Write("Ingrese el ID del cliente a borrar: ");
                        int idClienteBorrar = Convert.ToInt32(Console.ReadLine());

                        BorrarCliente(idClienteBorrar);
                        break;
                    case 4:
                        Console.WriteLine("Volviendo al Menú Principal...");
                        break;
                    default:
                        Console.WriteLine("Opción inválida. Por favor, seleccione una opción válida.");
                        break;
                }
            }
        }

        public void AgregarCliente(string nombreCompleto, string telefono, string email)
        {
            try
            {
                connection.Open();

                string query = "INSERT INTO Clientes (Nombre_Completo, Telefono, Email) VALUES (@NombreCompleto, @Telefono, @Email)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@NombreCompleto", nombreCompleto);
                    command.Parameters.AddWithValue("@Telefono", telefono);
                    command.Parameters.AddWithValue("@Email", email);
                    command.ExecuteNonQuery();
                }

                Console.WriteLine("Cliente agregado con éxito.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al agregar cliente: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        public void ModificarCliente(int idCliente, string nuevoNombreCompleto, string nuevoTelefono, string nuevoEmail)
        {
            try
            {
                connection.Open();

                string query = "UPDATE Clientes SET Nombre_Completo = @NuevoNombreCompleto, Telefono = @NuevoTelefono, Email = @NuevoEmail WHERE id_Cliente = @ID";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", idCliente);
                    command.Parameters.AddWithValue("@NuevoNombreCompleto", nuevoNombreCompleto);
                    command.Parameters.AddWithValue("@NuevoTelefono", nuevoTelefono);
                    command.Parameters.AddWithValue("@NuevoEmail", nuevoEmail);
                    command.ExecuteNonQuery();
                }

                Console.WriteLine("Cliente modificado con éxito.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al modificar cliente: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        public void BorrarCliente(int idCliente)
        {
            try
            {
                connection.Open();

                string query = "DELETE FROM Clientes WHERE id_Cliente = @ID";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", idCliente);
                    command.ExecuteNonQuery();
                }

                Console.WriteLine("Cliente borrado con éxito.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al borrar cliente: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
