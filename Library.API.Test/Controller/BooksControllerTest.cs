using FakeItEasy;
using FluentAssertions;
using Library.API.Controllers;
using Library.API.Models;
using Library.API.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Library.API.Test.Controller
{
    public class BooksControllerTest
    {
        //Dependencies
        private readonly IBooksRepository _booksRepository;
        private readonly BooksController _booksController;

        //SUT

        public BooksControllerTest()
        {
            _booksRepository = A.Fake<IBooksRepository>();
            _booksController = new BooksController(_booksRepository);
        }

        [Fact]
        public async Task BooksController_GetAll_ReturnsOk()
        {
            //Arrange
            var books = A.Fake<IEnumerable<Book>>();
            A.CallTo(()=> _booksRepository.GetAll()).Returns(books);

            //Act
            var result = await _booksController.GetAll();

            var resultObject = result as OkObjectResult;
            //Assert
            result.Should().BeOfType<OkObjectResult>();
            resultObject?.StatusCode.Should().Be((int)HttpStatusCode.OK);
        }

        [Fact]
        public async Task BooksController_Create_ReturnsOk()
        {
            //Arrange
            var fbook = A.Fake<Book>();
            A.CallTo(() => _booksRepository.Create(fbook)).Returns(true);

            //Act
            var result = await _booksController.Create(fbook);
            var resultObject = result as OkObjectResult;
          
            //Assert
            result.Should().BeOfType<OkObjectResult>();
            resultObject?.StatusCode.Should().Be((int)HttpStatusCode.OK);
            resultObject?.Value.Should().Be(true);
        }
    }
}
