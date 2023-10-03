
using System;
using System.Data;
using System.Data.SqlClient;

namespace SistemaGestionVentasCompras
{
    public class Compras
    {
        private SqlConnection connection;

        public Compras()
        {
            string connectionString = "Data Source=localhost;Initial Catalog=Negocio;Integrated Security=True";
            connection = new SqlConnection(connectionString);
        }

        public void CargarProveedor()
        {
            Console.WriteLine("\nIngresar los datos del nuevo proveedor:");

            Console.Write("Nombre completo del proveedor: ");
            string nombreProveedor = Console.ReadLine();

            Console.Write("Teléfono del proveedor: ");
            string telefonoProveedor = Console.ReadLine();

            Console.Write("Email del proveedor: ");
            string emailProveedor = Console.ReadLine();

            try
            {
                connection.Open();

                // Verificar si la conexión está abierta
                if (connection.State == ConnectionState.Open)
                {
                    // Definir la consulta SQL para insertar un nuevo proveedor sin especificar el ID
                    string insertQuery = "INSERT INTO Proveedores (Nombre_Completo, Telefono, Email) " +
                                         "VALUES (@Nombre, @Telefono, @Email)";

                    // Crear un objeto SqlCommand y asignar la consulta y la conexión
                    SqlCommand insertCommand = new SqlCommand(insertQuery, connection);

                    // Agregar los parámetros a la consulta
                    insertCommand.Parameters.AddWithValue("@Nombre", nombreProveedor);
                    insertCommand.Parameters.AddWithValue("@Telefono", telefonoProveedor);
                    insertCommand.Parameters.AddWithValue("@Email", emailProveedor);

                    // Ejecutar la consulta de inserción
                    int rowsAffected = insertCommand.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Proveedor agregado con éxito.");
                    }
                    else
                    {
                        Console.WriteLine("Error al agregar el proveedor.");
                    }
                }
                else
                {
                    Console.WriteLine("La conexión a la base de datos no está abierta.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al conectar a la base de datos: " + ex.Message);
            }
            finally
            {
                // Cerrar la conexión en el bloque finally para asegurarse de que se cierre siempre
                connection.Close();
            }
        }

        public void ModificarProveedor()
        {
            Console.Write("Ingrese el ID del proveedor que desea modificar: ");
            int idProveedor = Convert.ToInt32(Console.ReadLine());

            // Aquí deberías agregar la lógica para verificar si el proveedor con idProveedor existe en la base de datos
            // Si no existe, muestra un mensaje de error y sale de la función

            Console.Write("Ingrese el nuevo nombre del proveedor: ");
            string nuevoNombre = Console.ReadLine();

            Console.Write("Ingrese el nuevo teléfono del proveedor: ");
            string nuevoTelefono = Console.ReadLine();

            Console.Write("Ingrese el nuevo email del proveedor: ");
            string nuevoEmail = Console.ReadLine();

            try
            {
                connection.Open();

                // Verificar si la conexión está abierta
                if (connection.State == ConnectionState.Open)
                {
                    // Definir la consulta SQL para actualizar el proveedor
                    string updateQuery = "UPDATE Proveedores SET Nombre_Completo = @NuevoNombre, " +
                                         "Telefono = @NuevoTelefono, Email = @NuevoEmail " +
                                         "WHERE id_Proveedor = @IdProveedor";

                    // Crear un objeto SqlCommand y asignar la consulta y la conexión
                    SqlCommand updateCommand = new SqlCommand(updateQuery, connection);

                    // Agregar los parámetros a la consulta
                    updateCommand.Parameters.AddWithValue("@NuevoNombre", nuevoNombre);
                    updateCommand.Parameters.AddWithValue("@NuevoTelefono", nuevoTelefono);
                    updateCommand.Parameters.AddWithValue("@NuevoEmail", nuevoEmail);
                    updateCommand.Parameters.AddWithValue("@IdProveedor", idProveedor);

                    // Ejecutar la consulta de actualización
                    int rowsAffected = updateCommand.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Proveedor modificado con éxito.");
                    }
                    else
                    {
                        Console.WriteLine("No se encontró el proveedor con el ID especificado.");
                    }
                }
                else
                {
                    Console.WriteLine("La conexión a la base de datos no está abierta.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al conectar a la base de datos: " + ex.Message);
            }
            finally
            {
                // Cerrar la conexión en el bloque finally para asegurarse de que se cierre siempre
                connection.Close();
            }
        }

        public void BajaProveedor()
        {
            Console.Write("Ingrese el ID del proveedor que desea dar de baja: ");
            int idProveedor = Convert.ToInt32(Console.ReadLine());

            try
            {
                connection.Open();

                if (connection.State == ConnectionState.Open)
                {
                    // Definir la consulta SQL para eliminar el proveedor por ID
                    string deleteQuery = "DELETE FROM Proveedores WHERE id_Proveedor = @Id";

                    // Crear un objeto SqlCommand y asignar la consulta y la conexión
                    SqlCommand deleteCommand = new SqlCommand(deleteQuery, connection);

                    // Agregar el parámetro del ID
                    deleteCommand.Parameters.AddWithValue("@Id", idProveedor);

                    // Ejecutar la consulta de eliminación
                    int rowsAffected = deleteCommand.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Proveedor dado de baja con éxito.");
                    }
                    else
                    {
                        Console.WriteLine("No se encontró un proveedor con el ID proporcionado.");
                    }
                }
                else
                {
                    Console.WriteLine("La conexión a la base de datos no está abierta.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al conectar a la base de datos: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        public void MenuCompras()
        {
            int opcion;

            do
            {
                Console.WriteLine("\nMenú de opciones:");
                Console.WriteLine("1. Cargar Proveedor ");
                Console.WriteLine("2. Modificar Proveedor");
                Console.WriteLine("3. Dar de Baja Proveedor ");
                Console.WriteLine("4. Salir");
                Console.Write("Seleccione una opción: ");
                opcion = Convert.ToInt32(Console.ReadLine());

                switch (opcion)
                {
                    case 1:
                        CargarProveedor();
                        break;
                    case 2:
                        ModificarProveedor();
                        break;
                    case 3:
                        BajaProveedor();
                        break;
                    case 4:
                        Console.WriteLine("Saliendo del programa.");
                        break;
                    default:
                        Console.WriteLine("Opción no válida. Por favor, seleccione una opción válida.");
                        break;
                }

            } while (opcion != 4);
        }
    }
}


