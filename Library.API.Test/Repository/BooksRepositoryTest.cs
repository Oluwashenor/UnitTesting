using FakeItEasy;
using FluentAssertions;
using Library.API.Data;
using Library.API.Models;
using Library.API.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library.API.Test.Repository
{
    public class BooksRepositoryTest
    {
        private async  Task<LibraryAPIContext> GetDbContext()
        {
            var options = new DbContextOptionsBuilder<LibraryAPIContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var context =new LibraryAPIContext(options);
            await context.Database.EnsureCreatedAsync();
            if(!context.Books.Any())
            {
                context.Books.AddRange(new[]
                {
                    new Book()
                    {
                         Name = "Book A",
                         Author = "Author"
                    },
                    new Book()
                    {
                         Name = "Book B",
                         Author = "Author"
                    },
                });
                await context.SaveChangesAsync();
            }
            return context;
        }

        [Fact]
        public async Task BooksRepository_GetAll_ReturnsOk()
        {
            //Arrange 
            var context = await GetDbContext();
            var books = A.Fake<IEnumerable<Book>>();
            var repository = new BooksRepository(context);
            //Act
            var result = await repository.GetAll();
            result.Should().NotBeNull();
            result.Should().BeOfType<List<Book>>();
        }
    }
}
