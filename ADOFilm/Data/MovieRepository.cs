using ADOFilm.Models;
using Microsoft.Data.SqlClient;

namespace ADOFilm.Data
{
    public class MovieRepository : IMovieRepository
    {
        private readonly IDbConnectionFactory _factory;
        public MovieRepository(IDbConnectionFactory factory)
        {
            _factory = factory;
        }
        public void Add(Movie movie)
        {
            using var connection = _factory.CreateConnection();
            connection.Open();

            string sqlAdd = "INSERT INTO Movie (Title, Year, GenreId) VALUES (@Title, @Year, @GenreId)";
            using var command = new SqlCommand(sqlAdd, (SqlConnection)connection);
            command.Parameters.AddWithValue("@Title", movie.Title);
            command.Parameters.AddWithValue("@Year", movie.Year);
            command.Parameters.AddWithValue("@GenreId", movie.GenreId);
            int rowsAffected = command.ExecuteNonQuery();
            Console.WriteLine(rowsAffected);
        }

        public void Delete(int id)
        {
            using var connection = _factory.CreateConnection();
            connection.Open();
            string query = "DELETE FROM Movie WHERE Id=@Id";
            using var command = new SqlCommand(query, (SqlConnection)connection);
            command.Parameters.AddWithValue("@Id", id);

            int rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected == 0)
            {
                Console.WriteLine("Filmen finns inte");
            }
        }
        public IEnumerable<Movie> GetAll()
        {
            var list = new List<Movie>();
            using var connection = _factory.CreateConnection();
            connection.Open();
            string sql = "SELECT Movie.Id, Movie.Title, Movie.Year, Genre.GenreName FROM Movie INNER JOIN Genre ON Movie.GenreId=Genre.Id";
            using var command = new SqlCommand(sql, (SqlConnection)connection);
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new Movie
                {
                    Id = reader.GetInt32(0),
                    Title = reader.GetString(1),
                    Year = reader.GetInt32(2),
                    GenreName = reader.GetString(3),
                });
            }
            return list;
        }
        public IEnumerable<Genre> GetGenres()
        {
            var list = new List<Genre>();
            using var connection = _factory.CreateConnection();
            connection.Open();
            string sqlGenre = "SELECT Id, GenreName FROM Genre";
            using var command = new SqlCommand(sqlGenre, (SqlConnection)connection);
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new Genre
                {
                    Id = reader.GetInt32(0),
                    GenreName = reader.GetString(1)
                });
            }
            return list;
        }
        public IEnumerable<Movie> GetFilmByGenre(Genre genre)
        {
            var list = new List<Movie>();
            using var connection = _factory.CreateConnection();
            connection.Open();
            string query = "SELECT Movie.Id, Movie.Title, Movie.Year, Genre.GenreName FROM Movie INNER JOIN Genre ON Movie.GenreId=Genre.Id WHERE Movie.GenreId=@Id";
            using var command = new SqlCommand(query, (SqlConnection)connection);
            command.Parameters.AddWithValue("@Id", genre.Id);
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new Movie
                {
                    Id = reader.GetInt32(0),
                    Title = reader.GetString(1),
                    Year = reader.GetInt32(2),
                    GenreName = reader.GetString(3),
                });
            }
            return list;
        }
    }
}