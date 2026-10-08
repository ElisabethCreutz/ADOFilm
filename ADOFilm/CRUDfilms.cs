using Microsoft.Data.SqlClient;

namespace ADOFilm
{
    public class CRUDfilms
    {
        public string connectionString = "Server=localhost;Database=ADOFilmDb;Trusted_Connection=True;TrustServerCertificate=True;";

        public void ReadAllFilms()
        {
            using var connection = new SqlConnection(connectionString);
            connection.Open();
            //Console.WriteLine("Connected!");

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
            Console.ReadKey();
        }
        public void AddFilm()
        {

        }
        public void DeleteFilm()
        {

        }
    }
}
