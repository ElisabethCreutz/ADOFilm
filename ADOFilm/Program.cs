using Microsoft.Data.SqlClient;

namespace ADOFilm
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Server=localhost;Database=ADOFilmDb;Trusted_Connection=True;TrustServerCertificate=True;";

            using var connection = new SqlConnection(connectionString);
            connection.Open();
            Console.WriteLine("Connected!");

            string sql = "SELECT Movie.Title, Movie.Year, Genre.GenreName FROM Movie INNER JOIN Genre ON Movie.GenreId=Genre.Id";
            using var command = new SqlCommand(sql, connection);

            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                string title = reader.GetString(0);
                int year = reader.GetInt32(1);
                string genre = reader.GetString(2);
                Console.WriteLine($"{title}, {year}, {genre}");
            }
        }
    }
}
