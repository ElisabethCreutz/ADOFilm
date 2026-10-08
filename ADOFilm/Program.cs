using Microsoft.Data.SqlClient;

namespace ADOFilm
{
    public class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Server=localhost;Database=ADOFilmDb;Trusted_Connection=True;TrustServerCertificate=True;";
            using var connection = new SqlConnection(connectionString);
            connection.Open();

            var menu = new MainMenu();
            menu.RunMenu(connection);
        }
    }
}
