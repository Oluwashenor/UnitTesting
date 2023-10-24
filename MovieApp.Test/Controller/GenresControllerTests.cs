using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using MovieApp.Controllers;
using MovieApp.Models;
using MovieApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Tests.Controller;

public class GenresControllerTests
{
    private readonly IGenreRepository _genreRepository;
    private readonly GenresController _genresController;
    public GenresControllerTests()
    {
        //Dependencies
        _genreRepository = A.Fake<IGenreRepository>();

        //SUT - what we are actually going to execute
        _genresController = new GenresController(_genreRepository);

    }
     
    [Fact]
    public void GenresController_Index_ReturnsSuccess()
    {
        //Arrange  - What do i need to bring in 
        var genres = A.Fake<IEnumerable<Genre>>();
        A.CallTo(() => _genreRepository.GetAll()).Returns(genres);
        //Act 
        var result = _genresController.Index();
        //Assert
        result.Should().BeOfType<Task<IActionResult>>();
    }

    [Fact]  
    public void GenresController_Detail_ReturnsSuccess()
    {
        //Arrange 
        var videoId = 1;
        var genre = A.Fake<Genre>();
        A.CallTo(() => _genreRepository.Get(videoId)).Returns(genre);
        //Act
        var result = _genresController.Details(videoId);

        //Assert
        result.Should().BeOfType<Task<IActionResult>>();
    }
}
