using Microsoft.Data.SqlClient;
using Spectre.Console;

namespace ADOFilm
{
    public class MainMenu
    {
        private readonly CRUDfilms _crud; 
        public MainMenu(CRUDfilms crud) { _crud = crud; }
        public void RunMenu()
        {
            bool isRunning = true;
            while (isRunning)
            {
                Console.Clear();
                var option = AnsiConsole.Prompt(new SelectionPrompt<string>()
                    .Title("Huvudmeny")
                    .WrapAround()
                    .AddChoices("Visa alla Filmer", "Sök film utifrån genre", "Lägg till ny film", "Ta bort film", "Avsluta"));
                AnsiConsole.MarkupLine($"[green]{option.ToUpper()}[/]\n");

                switch (option)
                {
                    case "Visa alla Filmer":
                        _crud.ReadAllFilms();
                        Console.ReadKey();
                        break;
                    case "Sök film utifrån genre":
                        //osvosv
                        break;
                    case "Lägg till ny film":
                        _crud.AddFilm();
                        break;
                    case "Ta bort film":
                        _crud.DeleteFilm();
                        break;
                    case "Avsluta":
                        isRunning = false;
                        Console.WriteLine("Tack för besöket!");
                        break;

                }
            }
        }
    }
}