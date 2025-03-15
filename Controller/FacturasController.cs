using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

public class HomeController : Controller
{
    private readonly string _connectionString = "Server=JULS\\SQLEXPRESS;Database=GestionFacturasDB;Trusted_Connection=True;";

    public IActionResult TestConexion()
    {
        string mensaje;
        try
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                mensaje = " Conexión exitosa a SQL Server";
            }
        }
        catch (Exception ex)
        {
            mensaje = $" Error de conexión: {ex.Message}";
        }

        return Content(mensaje); // Esto mostrará el mensaje en la web
    }
}
