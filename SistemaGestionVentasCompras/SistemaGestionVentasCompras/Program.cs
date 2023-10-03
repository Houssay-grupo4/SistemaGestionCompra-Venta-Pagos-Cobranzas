using System;
using System.Data;
using System.Data.SqlClient;

namespace SistemaGestionVentasCompras
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Data Source=localhost;Initial Catalog=Negocio;Integrated Security=True";

            // Crear una conexión a la base de datos
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Crear un comando SQL
                SqlCommand command = new SqlCommand();

                try
                {
                    // Asignar la conexión al objeto SqlCommand
                    command.Connection = connection;

                    // Asignar el comando SQL que deseas ejecutar (consulta de prueba)
                    command.CommandText = "SELECT 1";

                    // Abrir la conexión
                    connection.Open();

                    // Verificar si la conexión está abierta
                    if (connection.State == ConnectionState.Open)
                    {
                        Console.WriteLine("Conexión exitosa a la base de datos.");

                        // Ejecutar la consulta de prueba
                        var result = command.ExecuteScalar();

                        // Verificar si la consulta de prueba devuelve resultados
                        if (result != null && result.Equals(1))
                        {
                            Console.WriteLine("La conexión y la consulta funcionan correctamente.");
                        }
                        else
                        {
                            Console.WriteLine("La consulta de prueba no devolvió resultados esperados.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("La conexión a la base de datos no está abierta.");
                    }

                    // Crear una instancia de la clase Compras
                    Compras compras = new Compras();

                    // Llamar al método MenuCompras de la instancia de Compras
                    compras.MenuCompras();
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

            // Esperar hasta que se presione Enter para cerrar la consola
            Console.WriteLine("Presiona Enter para salir...");
            Console.ReadLine();
        }
    }
}
