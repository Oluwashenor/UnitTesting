using FakeItEasy;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MovieApp.Data;
using MovieApp.Models;
using MovieApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Tests.Repository
{
    public class GenresRepositoryTests
    {
        private async Task<MovieDbContext> GetDbContext()
        {
            var options = new DbContextOptionsBuilder<MovieDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var context = new MovieDbContext(options);
            context.Database.EnsureCreated();
            if (!await context.Genres.AnyAsync())
            {
                await context.Genres.AddRangeAsync(Genres());
                await context.SaveChangesAsync();
            }
            return context;
        }

        [Fact]
        public async Task GenreRepository_Create_ReturnBool()
        {
            //Arrange 
            var genre = new Genre()
            {
                Name = "Sci-Fi"
            };
            var context = await GetDbContext();
            //context.Genres.AsNoTracking(); use if there is tracking issue
            var genreRepository = new GenreRepository(context);
            //Act
            var result = await genreRepository.Create(genre);

            //Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task GenreRepository_Get_ReturnBool()
        {
            //Arrange 
            var id = 1;
            var context = await GetDbContext();
            var genreRepository = new GenreRepository(context);
            //Act
            var result = genreRepository.Get(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Task<Genre>>();
        }

        [Fact]
        public async Task GenreRepository_Delete_ReturnsBool()
        {
            //Arrange
            var context = await GetDbContext();
            var count = await context.Genres.CountAsync();
            var repository = new GenreRepository(context);
            //Act
            var genre = await context.Genres.LastAsync();
            var result = await repository.Delete(genre.Id);
            //Assert
            result.Should().Be(true);
            var newCount = await context.Genres.CountAsync();
            newCount.Should().BeLessThan(count);
        }

        [Fact]
        public async Task GenreRepository_GetAll_ReturnsOk()
        {
            //Arrange 
            var context = await GetDbContext();
            var repository = new GenreRepository(context);
            //Act
            var result = await repository.GetAll();
            result.Should().NotBeNull();
            result.Should().BeOfType<List<Genre>>();
            result.Should().HaveCountGreaterThan(0);
        }


        private List<Genre> Genres()
        {
            return new List<Genre>
        {
             new Genre()
                {
                     Name = "Dance Musical"
                },
                new Genre()
                {
                    Name = "Horror"
                },
                new Genre()
                {
                    Name = "Comedy"
                },
                new Genre()
                {
                    Name = "Action"
                }
        };
        }
    }
}
