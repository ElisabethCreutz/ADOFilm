using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace ADOFilm.Data
{
    public class DataInitializer
    {
        private readonly string _connectionString;
        public DataInitializer(string connectionstring)
        {
            _connectionString = connectionstring;
        }
        public void Initialize()
        {
            var builder = new SqlConnectionStringBuilder(_connectionString);
            var dbName = builder.InitialCatalog;
            if (string.IsNullOrEmpty(dbName))
                throw new InvalidOperationException("Connection string must include Initial Catalog (Database).");

            // Connect to master to create the database if it doesn't exist
            var masterBuilder = new SqlConnectionStringBuilder(_connectionString) { InitialCatalog = "master" };
            using (var masterConn = new SqlConnection(masterBuilder.ConnectionString))
            {
                masterConn.Open();
                var createDbCmd = $@"IF DB_ID(N'{dbName}') IS NULL CREATE DATABASE [{dbName}];";
                using var cmd = new SqlCommand(createDbCmd, masterConn);
                cmd.ExecuteNonQuery();
            }

            // Connect to the app database to create tables and seed data
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var createTables = @"
IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'Genre')
BEGIN
    CREATE TABLE Genre (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        GenreName NVARCHAR(100) NOT NULL
    );
END

IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'Movie')
BEGIN
    CREATE TABLE Movie (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Title NVARCHAR(200) NOT NULL,
        Year INT NOT NULL,
        GenreId INT NOT NULL REFERENCES Genre(Id)
    );
END
";
                using (var command = new SqlCommand(createTables, connection))
                {
                    command.ExecuteNonQuery();
                }

                var seedGenres = @"
IF NOT EXISTS (SELECT 1 FROM Genre)
BEGIN
    INSERT INTO Genre (GenreName) VALUES (N'Action'), (N'Komedi'), (N'Drama'), (N'Thriller'), (N'Dokumentär'), (N'Skräck');
END
";
                using (var command = new SqlCommand(seedGenres, connection))
                {
                    command.ExecuteNonQuery();
                }

                var seedFilms = @"
IF NOT EXISTS (SELECT 1 FROM Movie)
BEGIN
    INSERT INTO Movie (Title, Year, GenreId) VALUES ('GodFather', 1972,3), ('The Old Guard',2020,1);
END
";
                using (var command = new SqlCommand(seedFilms, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
