using Library.API.Data;
using Library.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.API.Repository
{
    public interface IBooksRepository
    {
        bool BookExists(int id);
        Task<bool> Create(Book book);
        Task<bool> Delete(int id);
        Task<Book> Get(int id);
        Task<IEnumerable<Book>> GetAll();
        Task<bool> Update(Book book);
    }

    public class BooksRepository : IBooksRepository
    {
        private readonly LibraryAPIContext _context;

        public BooksRepository(LibraryAPIContext context)
        {
            _context = context;
        }

        public async Task<bool> Create(Book book)
        {
            _context.Add(book);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Book> Get(int id)
        {
            return await _context.Books.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<bool> Delete(int id)
        {
            var book = await Get(id);
            if (book == null)
                return false;
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Book>> GetAll()
        {
            return await _context.Books.ToListAsync();
        }

        public async Task<bool> Update(Book book)
        {
            _context.Update(book);
            await _context.SaveChangesAsync();
            return true;
        }

        public bool BookExists(int id)
        {
            return (_context.Books?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
