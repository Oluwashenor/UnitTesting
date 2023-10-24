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
            var repository = new BooksRepository(context);
            //Act
            var result = await repository.GetAll();
            result.Should().NotBeNull();
            result.Should().BeOfType<List<Book>>();
            result.Should().HaveCountGreaterThan(0);
        }

        [Fact]
        public async Task BooksRepository_Create_ReturnsBool()
        {
            //Arrange
            var context = await GetDbContext();
            var book = new Book()
            {
                Author = "Author",
                Name = "Name",
            };
            var count = await context.Books.CountAsync();   
            var repository = new BooksRepository(context);

            //Act
            var result = await repository.Create(book);
            //Assert
            result.Should().Be(true);
            var newCount = await context.Books.CountAsync();
            newCount.Should().BeGreaterThan(count);
        }

        [Fact]
        public async Task BookRepository_Delete_ReturnsBool()
        {
            //Arrange
            var context = await GetDbContext();
            var count = await context.Books.CountAsync();
            var repository = new BooksRepository(context);
            //Act
            var book = await context.Books.LastAsync();
            var result = await repository.Delete(book.Id);
            //Assert
            result.Should().Be(true);
            var newCount = await context.Books.CountAsync();
            newCount.Should().BeLessThan(count);
        }

        [Fact]
        public async Task BookRepository_Get_ReturnsBook()
        {
            //Arrange
            var context = await GetDbContext();
            var repository = new BooksRepository(context);
            //Act
            var result = await repository.Get(1);
            //Assert
            result.Should().BeOfType<Book>();
        }
    }
}
