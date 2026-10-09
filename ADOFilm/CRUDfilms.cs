using ADOFilm.Models;
using Microsoft.Data.SqlClient;
using Spectre.Console;
using System.Reflection;
using System.Xml.Linq;

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
                Console.WriteLine($"{movie.Id} : {movie.Title}, {movie.Year}, {movie.GenreName}");
            }


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
                            .AddChoices(filmList);
            var selectedFilm = AnsiConsole.Prompt(prompt);
            _repo.Delete(selectedFilm.Id);
        }
    }
}
