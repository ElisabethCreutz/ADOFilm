using Microsoft.Data.SqlClient;

namespace ADOFilm
{
    public class Program
    {
        static void Main(string[] args)
        {
            var menu = new MainMenu();
            menu.RunMenu();
        }
    }
}
