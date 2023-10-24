using FakeItEasy;
using FluentAssertions;
using Library.API.Data;
using Library.API.Models;
using Library.API.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.API.Test.Repository
{
    public class BooksRepository
    {
        private readonly IBooksRepository _booksRepository;
        public BooksRepository()
        {
            _booksRepository = A.Fake<IBooksRepository>();
        }

        private async  Task<LibraryAPIContext> GetDbContext()
        {
            var options = new DbContextOptionsBuilder<LibraryAPIContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var context =new LibraryAPIContext(options);
            await context.Database.EnsureCreatedAsync();
            if(context.Books != null)
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
            }
            return context;
        }

        [Fact]
        public async Task BooksRepository_GetAll_ReturnsOk()
        {
            //Arrange 
            var context = await GetDbContext();
            var books = A.Fake<IEnumerable<Book>>();

            //Act
            var result = await _booksRepository.GetAll();
            result.Should().NotBeNull();
            var resultObject = result as OkObjectResult;
            resultObject?.Value.Should().BeOfType<IEnumerable<Book>>();
        }
    }
}
