using Spectre.Console;

namespace ADOFilm
{
    public class MainMenu
    {
        public void RunMenu()
        {
            bool isRunning = true;
            var CRUDFilm = new CRUDfilms();
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
                        CRUDFilm.ReadAllFilms();
                        break;
                    case "Sök film utifrån genre":
                        //osvosv
                        break;
                    case "Lägg till ny film":
                        CRUDFilm.AddFilm();
                        break;
                    case "Ta bort film":
                        CRUDFilm.DeleteFilm();
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