using System;
using System.Data.SqlClient;

namespace SistemaGestionVentasCompras
{
    public class Pagos
    {
        private SqlConnection connection;

        public Pagos()
        {
            string connectionString = "Data Source=localhost;Initial Catalog=Negocio;Integrated Security=True";
            connection = new SqlConnection(connectionString);
        }

        public void MenuPagos()
        {
            int opcion;

            do
            {
                Console.WriteLine("\nMenú de opciones de Pagos:");
                Console.WriteLine("1. Agregar Pago");
                Console.WriteLine("2. Modificar Pago");
                Console.WriteLine("3. Borrar Pago");
                Console.WriteLine("4. Salir");
                Console.Write("Seleccione una opción: ");
                opcion = Convert.ToInt32(Console.ReadLine());

                switch (opcion)
                {
                    case 1:
                        AgregarPago();
                        break;
                    case 2:
                        ModificarPago();
                        break;
                    case 3:
                        BorrarPago();
                        break;
                    case 4:
                        Console.WriteLine("Saliendo del menú de Pagos.");
                        break;
                    default:
                        Console.WriteLine("Opción no válida. Por favor, seleccione una opción válida.");
                        break;
                }

            } while (opcion != 4);
        }

        public void AgregarPago()
        {
            Console.Write("Ingrese el ID del proveedor: ");
            int idProveedor = Convert.ToInt32(Console.ReadLine());

            // Abre la conexión antes de verificar la existencia del proveedor
            connection.Open();

            if (!ProveedorExiste(idProveedor))
            {
                Console.WriteLine("No se encuentra el proveedor con el ID especificado.");
                connection.Close(); // Cierra la conexión si el proveedor no existe
                return;
            }

            try
            {
                // Verificar si las columnas están vacías antes de la actualización
                bool tieneDatos = VerificarDatos(idProveedor);

                if (tieneDatos)
                {
                    Console.WriteLine("El proveedor ingresado ya tiene pagos registrados.");
                    connection.Close(); // Cierra la conexión si el proveedor ya tiene pagos registrados
                    return;
                }

                // Verificar si las columnas Fecha_Pago e Importe tienen valores nulos
                bool columnasNulas = VerificarColumnasNulas(idProveedor);

                if (!columnasNulas)
                {
                    Console.WriteLine("No se pueden agregar datos ya que las columnas Fecha_Pago e Importe tienen valores reales.");
                    connection.Close(); // Cierra la conexión si no se permite la inserción
                    return;
                }

                Console.Write("Ingrese la nueva fecha del pago (yyyy-MM-dd): ");
                DateTime nuevaFechaPago = DateTime.Parse(Console.ReadLine());

                Console.Write("Ingrese el nuevo importe del pago: ");
                decimal nuevoImporte = Convert.ToDecimal(Console.ReadLine());

                // Realizar la inserción con los valores actualizados
                string query = "UPDATE Proveedores SET Fecha_Pago = @NuevaFechaPago, Importe = @NuevoImporte WHERE Id_Proveedor = @IdProveedor";

                using (SqlCommand insertCommand = new SqlCommand(query, connection))
                {
                    insertCommand.Parameters.AddWithValue("@NuevaFechaPago", nuevaFechaPago);
                    insertCommand.Parameters.AddWithValue("@NuevoImporte", nuevoImporte);
                    insertCommand.Parameters.AddWithValue("@IdProveedor", idProveedor);
                    int rowsAffected = insertCommand.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Pago al proveedor agregado con éxito.");
                    }
                    else
                    {
                        Console.WriteLine("No se pudo agregar el pago al proveedor.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al agregar pago al proveedor: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        public void ModificarPago()
        {
            Console.Write("Ingrese el ID del proveedor cuyo pago desea modificar: ");
            int idProveedor = Convert.ToInt32(Console.ReadLine());

            // Abre la conexión antes de verificar la existencia del proveedor
            connection.Open();

            if (!ProveedorExiste(idProveedor))
            {
                Console.WriteLine("No se encuentra el proveedor con el ID especificado.");
                connection.Close(); // Cierra la conexión si el proveedor no existe
                return;
            }

            try
            {
                // Verificar si las columnas están vacías antes de la actualización
                bool tieneDatos = VerificarDatos(idProveedor);

                Console.Write("Ingrese la nueva fecha del pago (yyyy-MM-dd): ");
                DateTime nuevaFechaPago = DateTime.Parse(Console.ReadLine());

                Console.Write("Ingrese el nuevo importe del pago: ");
                decimal nuevoImporte = Convert.ToDecimal(Console.ReadLine());

                // Realizar la actualización con los valores proporcionados
                string query = "UPDATE Proveedores SET Fecha_Pago = @NuevaFechaPago, Importe = @NuevoImporte WHERE Id_Proveedor = @IdProveedor";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@NuevaFechaPago", nuevaFechaPago);
                    command.Parameters.AddWithValue("@NuevoImporte", nuevoImporte);
                    command.Parameters.AddWithValue("@IdProveedor", idProveedor);
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Pago al proveedor modificado con éxito.");
                    }
                    else
                    {
                        Console.WriteLine("No se encontró el pago al proveedor con el ID especificado.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al modificar pago al proveedor: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        public void BorrarPago()
        {
            Console.Write("Ingrese el ID del proveedor cuyo pago desea borrar: ");
            int idProveedor = Convert.ToInt32(Console.ReadLine());

            // Abre la conexión antes de verificar la existencia del proveedor
            connection.Open();

            if (!ProveedorExiste(idProveedor))
            {
                Console.WriteLine("No se encuentra el proveedor con el ID especificado.");
                connection.Close(); // Cierra la conexión si el proveedor no existe
                return;
            }

            try
            {
                // Verificar si las columnas están vacías antes de la eliminación
                bool tieneDatos = VerificarDatos(idProveedor);

                if (!tieneDatos)
                {
                    Console.WriteLine("El proveedor ingresado no tiene pagos registrados.");
                    connection.Close(); // Cierra la conexión si el proveedor no tiene pagos registrados
                    return;
                }

                // Realizar la eliminación de los datos de las columnas Fecha_Pago e Importe
                string query = "UPDATE Proveedores SET Fecha_Pago = NULL, Importe = NULL WHERE Id_Proveedor = @IdProveedor";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdProveedor", idProveedor);
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Datos de pago al proveedor borrados con éxito.");
                    }
                    else
                    {
                        Console.WriteLine("No se encontró el pago al proveedor con el ID especificado.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al borrar datos de pago al proveedor: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        public bool VerificarDatos(int idProveedor)
        {
            try
            {
                string query = "SELECT Fecha_Pago, Importe FROM Proveedores WHERE Id_Proveedor = @IdProveedor";
                using (SqlCommand selectCommand = new SqlCommand(query, connection))
                {
                    selectCommand.Parameters.AddWithValue("@IdProveedor", idProveedor);
                    SqlDataReader reader = selectCommand.ExecuteReader();

                    if (reader.Read())
                    {
                        DateTime? fechaPagoActual = reader.IsDBNull(0) ? (DateTime?)null : reader.GetDateTime(0);
                        decimal? importeActual = reader.IsDBNull(1) ? (decimal?)null : reader.GetDecimal(1);

                        reader.Close();

                        return fechaPagoActual.HasValue || importeActual.HasValue;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al verificar datos: " + ex.Message);
            }

            return false;
        }

        public bool ProveedorExiste(int idProveedor)
        {
            try
            {
                string query = "SELECT COUNT(*) FROM Proveedores WHERE Id_Proveedor = @IdProveedor";
                using (SqlCommand selectCommand = new SqlCommand(query, connection))
                {
                    selectCommand.Parameters.AddWithValue("@IdProveedor", idProveedor);
                    int count = (int)selectCommand.ExecuteScalar();

                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al verificar la existencia del proveedor: " + ex.Message);
            }

            return false;
        }

        public bool VerificarColumnasNulas(int idProveedor)
        {
            try
            {
                string query = "SELECT Fecha_Pago, Importe FROM Proveedores WHERE Id_Proveedor = @IdProveedor";
                using (SqlCommand selectCommand = new SqlCommand(query, connection))
                {
                    selectCommand.Parameters.AddWithValue("@IdProveedor", idProveedor);
                    SqlDataReader reader = selectCommand.ExecuteReader();

                    if (reader.Read())
                    {
                        DateTime? fechaPagoActual = reader.IsDBNull(0) ? (DateTime?)null : reader.GetDateTime(0);
                        decimal? importeActual = reader.IsDBNull(1) ? (decimal?)null : reader.GetDecimal(1);

                        reader.Close();

                        return !fechaPagoActual.HasValue && !importeActual.HasValue;
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

