using ADOFilm.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ADOFilm
{
    public interface IMovieRepository
    {
        //IMovieRepository with async CRUD signatures:
        //Task<Movie?> GetByIdAsync(int id, CancellationToken ct = default);
        //Task<IEnumerable<Movie>> GetAllAsync(CancellationToken ct = default);
        //Task AddAsync(Movie movie, CancellationToken ct = default);
        //Task UpdateAsync(Movie movie, CancellationToken ct = default);
        //Task DeleteAsync(int id, CancellationToken ct = default);

        IEnumerable<Movie> GetAll(); 
        void Add(Movie movie); 
        void Delete(int id); 
        IEnumerable<Genre>GetGenres();
    }
}
