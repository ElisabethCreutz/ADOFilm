using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;


namespace ADOFilm
{
    public class Program
    {
        static void Main(string[] args)
        {
            using IHost host = Host.CreateDefaultBuilder(args).ConfigureServices((context, services) =>
            {
                var connection = context.Configuration.GetConnectionString("DefaultConnection");
                services.AddSingleton<IDbConnectionFactory>(new SqlConnectionFactory(connection));
                services.AddScoped<IMovieRepository, MovieRepository>();
                services.AddScoped<CRUDfilms>();
                services.AddScoped<MainMenu>();
            })
            .Build();

            using var scope = host.Services.CreateScope();
            var menu = scope.ServiceProvider.GetRequiredService<MainMenu>();
            menu.RunMenu();
        }
    }
}
