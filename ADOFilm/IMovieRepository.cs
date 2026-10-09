using ADOFilm.Models;

namespace ADOFilm
{
    public interface IMovieRepository
    {
        IEnumerable<Movie> GetAll();
        void Add(Movie movie);
        void Delete(int id);
        IEnumerable<Genre> GetGenres();
        IEnumerable<Movie> GetFilmByGenre(Genre genre);
    }
}
