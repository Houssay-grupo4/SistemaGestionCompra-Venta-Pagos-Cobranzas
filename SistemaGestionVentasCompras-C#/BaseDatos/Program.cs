using System;
using System.Data.SqlClient;

public class ClienteRepository
{
    private string connectionString;

    public ClienteRepository(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public bool InsertarCliente(string nombre, string correo)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string query = "INSERT INTO Clientes (Nombre, Correo) VALUES (@Nombre, @Correo)";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Nombre", nombre);
                command.Parameters.AddWithValue("@Correo", correo);

                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
    }
}
