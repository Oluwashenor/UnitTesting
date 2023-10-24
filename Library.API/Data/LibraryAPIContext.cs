using Microsoft.EntityFrameworkCore;
using Library.API.Models;

namespace Library.API.Data
{
    public class LibraryAPIContext : DbContext
    {
        public LibraryAPIContext (DbContextOptions<LibraryAPIContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Books { get; set; } = default!;
    }
}
