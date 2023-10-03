using System;
using System.Data.SqlClient;

namespace SistemaGestionVentasCompras
{
    public class Cobros
    {
        private SqlConnection connection;

        public Cobros()
        {
            string connectionString = "Data Source=localhost;Initial Catalog=Negocio;Integrated Security=True";
            connection = new SqlConnection(connectionString);
        }

        public void MenuCobros()
        {
            int opcion;

            do
            {
                Console.WriteLine("\nMenú de opciones:");
                Console.WriteLine("1. Agregar Cobro");
                Console.WriteLine("2. Modificar Cobro");
                Console.WriteLine("3. Borrar Cobro");
                Console.WriteLine("4. Salir");
                Console.Write("Seleccione una opción: ");
                opcion = Convert.ToInt32(Console.ReadLine());

                switch (opcion)
                {
                    case 1:
                        AgregarCobro();
                        break;
                    case 2:
                        ModificarCobro();
                        break;
                    case 3:
                        BorrarCobro();
                        break;
                    case 4:
                        Console.WriteLine("Saliendo del menú de Cobros.");
                        return;
                    default:
                        Console.WriteLine("Opción no válida. Por favor, seleccione una opción válida.");
                        break;
                }

            } while (opcion != 4);
        }

        public void AgregarCobro()
        {
            Console.Write("Ingrese el ID del cliente: ");
            int idCliente = Convert.ToInt32(Console.ReadLine());

            // Abre la conexión antes de verificar la existencia del cliente
            connection.Open();

            if (!ClienteExiste(idCliente))
            {
                Console.WriteLine("No se encuentra el cliente con el ID especificado.");
                connection.Close(); // Cierra la conexión si el cliente no existe
                return;
            }

            try
            {
                // Verificar si las columnas están vacías antes de la actualización
                bool tieneDatos = VerificarDatos(idCliente);

                if (tieneDatos)
                {
                    Console.WriteLine("El Cliente ingresado no tiene cobros pendientes.");
                    connection.Close(); // Cierra la conexión si el cliente ya tiene cobros pendientes
                    return;
                }

                // Verificar si las columnas Fecha_Cobro e Importe tienen valores nulos
                bool columnasNulas = VerificarColumnasNulas(idCliente);

                if (!columnasNulas)
                {
                    Console.WriteLine("No se pueden agregar datos ya que las columnas Fecha_Cobro e Importe tienen valores reales.");
                    connection.Close(); // Cierra la conexión si no se permite la inserción
                    return;
                }

                Console.Write("Ingrese la nueva fecha del cobro (yyyy-MM-dd): ");
                DateTime nuevaFechaCobro = DateTime.Parse(Console.ReadLine());

                Console.Write("Ingrese el nuevo importe del cobro: ");
                decimal nuevoImporte = Convert.ToDecimal(Console.ReadLine());

                // Realizar la inserción con los valores actualizados
                string query = "UPDATE Clientes SET Fecha_Cobro = @NuevaFechaCobro, Importe = @NuevoImporte WHERE id_Cliente = @IdCliente";

                using (SqlCommand insertCommand = new SqlCommand(query, connection))
                {
                    insertCommand.Parameters.AddWithValue("@NuevaFechaCobro", nuevaFechaCobro);
                    insertCommand.Parameters.AddWithValue("@NuevoImporte", nuevoImporte);
                    insertCommand.Parameters.AddWithValue("@IdCliente", idCliente);
                    int rowsAffected = insertCommand.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Cobro al cliente agregado con éxito.");
                    }
                    else
                    {
                        Console.WriteLine("No se pudo agregar el cobro del cliente.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al agregar cobro: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        public void ModificarCobro()
        {
            Console.Write("Ingrese el ID del cliente cuyo cobro desea modificar: ");
            int idCliente = Convert.ToInt32(Console.ReadLine());

            // Abre la conexión antes de verificar la existencia del cliente
            connection.Open();

            if (!ClienteExiste(idCliente))
            {
                Console.WriteLine("No se encuentra el cliente con el ID especificado.");
                connection.Close(); // Cierra la conexión si el cliente no existe
                return;
            }

            try
            {
                // Verificar si las columnas están vacías antes de la actualización
                bool tieneDatos = VerificarDatos(idCliente);

                Console.Write("Ingrese la nueva fecha del cobro (yyyy-MM-dd): ");
                DateTime nuevaFechaCobro = DateTime.Parse(Console.ReadLine());

                Console.Write("Ingrese el nuevo importe del cobro: ");
                decimal nuevoImporte = Convert.ToDecimal(Console.ReadLine());

                // Realizar la actualización con los valores proporcionados
                string query = "UPDATE Clientes SET Fecha_Cobro = @NuevaFechaCobro, Importe = @NuevoImporte WHERE id_Cliente = @IdCliente";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@NuevaFechaCobro", nuevaFechaCobro);
                    command.Parameters.AddWithValue("@NuevoImporte", nuevoImporte);
                    command.Parameters.AddWithValue("@IdCliente", idCliente);
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Cobro del cliente modificado con éxito.");
                    }
                    else
                    {
                        Console.WriteLine("No se encontró el cobro del cliente con el ID especificado.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al modificar cobro: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        public void BorrarCobro()
        {
            Console.Write("Ingrese el ID del cliente cuyo cobro desea borrar: ");
            int idCliente = Convert.ToInt32(Console.ReadLine());

            // Abre la conexión antes de verificar la existencia del cliente
            connection.Open();

            if (!ClienteExiste(idCliente))
            {
                Console.WriteLine("No se encuentra el cliente con el ID especificado.");
                connection.Close(); // Cierra la conexión si el cliente no existe
                return;
            }

            try
            {
                // Verificar si las columnas están vacías antes de la eliminación
                bool tieneDatos = VerificarDatos(idCliente);

                if (!tieneDatos)
                {
                    Console.WriteLine("El Cliente ingresado no tiene cobros pendientes.");
                    connection.Close(); // Cierra la conexión si el cliente no tiene cobros pendientes
                    return;
                }

                // Realizar la eliminación de los datos de las columnas Fecha_Cobro e Importe
                string query = "UPDATE Clientes SET Fecha_Cobro = NULL, Importe = NULL WHERE id_Cliente = @IdCliente";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdCliente", idCliente);
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Datos de cobro del cliente borrados con éxito.");
                    }
                    else
                    {
                        Console.WriteLine("No se encontró el cobro del cliente con el ID especificado.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al borrar datos de cobro del cliente: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        public bool VerificarDatos(int idCliente)
        {
            try
            {
                string query = "SELECT Fecha_Cobro, Importe FROM Clientes WHERE id_Cliente = @IdCliente";
                using (SqlCommand selectCommand = new SqlCommand(query, connection))
                {
                    selectCommand.Parameters.AddWithValue("@IdCliente", idCliente);
                    SqlDataReader reader = selectCommand.ExecuteReader();

                    if (reader.Read())
                    {
                        DateTime? fechaCobroActual = reader.IsDBNull(0) ? (DateTime?)null : reader.GetDateTime(0);
                        decimal? montoActual = reader.IsDBNull(1) ? (decimal?)null : reader.GetDecimal(1);

                        reader.Close();

                        return fechaCobroActual.HasValue || montoActual.HasValue;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al verificar datos: " + ex.Message);
            }

            return false;
        }

        public bool ClienteExiste(int idCliente)
        {
            try
            {
                string query = "SELECT COUNT(*) FROM Clientes WHERE id_Cliente = @IdCliente";
                using (SqlCommand selectCommand = new SqlCommand(query, connection))
                {
                    selectCommand.Parameters.AddWithValue("@IdCliente", idCliente);
                    int count = (int)selectCommand.ExecuteScalar();

                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al verificar la existencia del cliente: " + ex.Message);
            }

            return false;
        }

        public bool VerificarColumnasNulas(int idCliente)
        {
            try
            {
                string query = "SELECT Fecha_Cobro, Importe FROM Clientes WHERE id_Cliente = @IdCliente";
                using (SqlCommand selectCommand = new SqlCommand(query, connection))
                {
                    selectCommand.Parameters.AddWithValue("@IdCliente", idCliente);
                    SqlDataReader reader = selectCommand.ExecuteReader();

                    if (reader.Read())
                    {
                        DateTime? fechaCobroActual = reader.IsDBNull(0) ? (DateTime?)null : reader.GetDateTime(0);
                        decimal? montoActual = reader.IsDBNull(1) ? (decimal?)null : reader.GetDecimal(1);

                        reader.Close();

                        return !fechaCobroActual.HasValue && !montoActual.HasValue;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al verificar columnas nulas: " + ex.Message);
            }

            return false;
        }
    }
}







