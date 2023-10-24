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
