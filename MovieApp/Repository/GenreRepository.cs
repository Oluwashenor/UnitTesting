using Humanizer.Localisation;
using Microsoft.EntityFrameworkCore;
using MovieApp.Data;
using MovieApp.Models;

namespace MovieApp.Repository
{
    public class GenreRepository : IGenreRepository
    {

        private readonly MovieDbContext _context;

        public GenreRepository(MovieDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Create(Genre genre)
        {
            _context.Add(genre);
            return await _context.SaveChangesAsync() > 0; 
        }

        public async Task<Genre> Get(int id)
        {
            return await _context.Genres.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<bool> Delete(int id)
        {
            var genre = await Get(id);
            if(genre == null)
                return false;
            _context.Genres.Remove(genre);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Genre>> GetAll()
        {
            return await _context.Genres.ToListAsync();
        }

        public async Task<bool> Update(Genre genre)
        {
            _context.Update(genre);
            await _context.SaveChangesAsync();
            return true;
        }

        public bool GenreExists(int id)
        {
            return (_context.Genres?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }

    public interface IGenreRepository
    {
        Task<bool> Create(Genre genre);
        Task<bool> Delete(int id);
        Task<Genre> Get(int id);
        Task<IEnumerable<Genre>> GetAll();
        Task<bool> Update(Genre genre);
    }
}
