using Microsoft.Data.SqlClient;
using System.Reflection;

namespace ADOFilm
{
    public class CRUDfilms
    {

        public void ReadAllFilms(SqlConnection connection)
        {
                        string sql = "SELECT Movie.Id, Movie.Title, Movie.Year, Genre.GenreName FROM Movie INNER JOIN Genre ON Movie.GenreId=Genre.Id";
            using var command = new SqlCommand(sql, connection);

            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string title = reader.GetString(1);
                int year = reader.GetInt32(2);
                string genre = reader.GetString(3);
                Console.WriteLine($"{id} : {title}, {year}, {genre}");
            }

        }
        public void AddFilm(SqlConnection connection)
        {
            Console.Write("Namn: ");
            string name = Console.ReadLine()!;
            Console.Write("År: ");
            int year = Convert.ToInt32(Console.ReadLine());
            //TODO:felhantering osv.
            var genreId = SelectGenre(connection);
            string sqlAdd = "INSERT INTO Movie (Title, Year, GenreId) VALUES (@Title, @Year, @GenreId)";
            using var command = new SqlCommand(sqlAdd, connection);
            command.Parameters.AddWithValue("@Title", name);
            command.Parameters.AddWithValue("@Year", year);
            command.Parameters.AddWithValue("@GenreId", genreId);
            int rowsAffected = command.ExecuteNonQuery();
            Console.WriteLine(rowsAffected);
            Console.ReadKey();

        }
        public int SelectGenre(SqlConnection connection)
        {
            Console.WriteLine("Välj genre: ");
            string sqlGenre = "SELECT Id, GenreName FROM Genre";
            using var command = new SqlCommand(sqlGenre, connection);
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string genreName = reader.GetString(1);
                Console.WriteLine($"{id} - {genreName}");
            }
            Console.Write("Skriv rätt siffra: ");
            var genreId = Convert.ToInt32(Console.ReadLine());
            //TODO: felhantering osv.
            return genreId;
        }
        public void DeleteFilm(SqlConnection connection)
        {
            var films = new CRUDfilms();
            films.ReadAllFilms(connection);
            Console.WriteLine("Select film to delete. No:");
            var selection = Convert.ToInt32(Console.ReadLine());
            //TODO:felhantering
            string query = "DELETE FROM Movie WHERE Id=@Id";
            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", selection);

            int rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected == 0)
            {
                Console.WriteLine("Filmen finns inte");
            }
        }
    }
}
