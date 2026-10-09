using ADOFilm.Models;
using Spectre.Console;

namespace ADOFilm
{
    public class CRUDfilms
    {
        private readonly IMovieRepository _repo;
        public CRUDfilms(IMovieRepository repo)
        {
            _repo = repo;
        }
        public void ReadAllFilms()
        {
            foreach (var movie in _repo.GetAll())
            {
                Console.WriteLine($"{movie.Title}, {movie.Year}, {movie.GenreName}");
            }
            Console.ReadKey();
        }
        public void AddFilm()
        {
            Console.Write("Namn: ");
            var name = Console.ReadLine()!;
            Console.Write("År: ");
            var year = int.Parse(Console.ReadLine()!);
            var genrelist = _repo.GetGenres();

            var prompt = new SelectionPrompt<Genre>()
                            .Title("Välj genre:")
                            .UseConverter(genre => $"{genre.GenreName}")
                            .AddChoices(genrelist);
            var selectedGenre = AnsiConsole.Prompt(prompt);
            var movie = new Movie
            { Title = name, Year = year, GenreId = selectedGenre.Id };
            _repo.Add(movie);
        }
        public void DeleteFilm()
        {
            var filmList = _repo.GetAll();
            var prompt = new SelectionPrompt<Movie>()
                 .Title("Välj film att ta bort:")
                 .UseConverter(movie => $"{movie.Title}, {movie.Year}, {movie.GenreName}")
                 .AddChoices(filmList);
            var selectedFilm = AnsiConsole.Prompt(prompt);
            _repo.Delete(selectedFilm.Id);
        }
        public void ReadFilmByGenre()
        {
            var genrelist = _repo.GetGenres();
            var prompt = new SelectionPrompt<Genre>()
                .Title("Välj genre:")
                .UseConverter(genre => $"{genre.GenreName}")
                .AddChoices(genrelist);
            var selectedGenre = AnsiConsole.Prompt(prompt);
            Console.WriteLine("Vald genre:" + selectedGenre.GenreName);
            var filmList = _repo.GetFilmByGenre(selectedGenre);
            if (filmList.Count() == 0)
            {
                Console.WriteLine("Det finns inga filmer i den här kategorin");
            }
            foreach (var movie in filmList)
            {
                Console.WriteLine($"{movie.Title}, {movie.Year}, {movie.GenreName}");
            }
            Console.ReadKey();
        }
    }
}