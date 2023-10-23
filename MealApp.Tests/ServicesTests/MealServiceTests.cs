using FluentAssertions;
using FluentAssertions.Extensions;
using MealApp.Domain;
using MealApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MealApp.Tests.ServicesTests
{
    public class MealServiceTests
    {
        private readonly MealService _mealService;

        public MealServiceTests()
        {
            _mealService = new MealService();
        }
        [Fact]
        public void MealService_GetRandomMeal_ReturnString()
        {
            //Arrange - variables, classes, mocks 

            //Act
            var result = _mealService.GetRandomMeal();

            //Assert
            result.Should().NotBeNullOrWhiteSpace();
        } 
        
        [Fact]
        public void MealService_GetDrink_ReturnString()
        {
            //Arrange - variables, classes, mocks

            //Act
            var result = _mealService.GetDrink();

            //Assert
            result.Should().NotBeNullOrWhiteSpace();
            result.Should().Be("Success: Coke");
            result.Should().Contain("Success", Exactly.Once());
        }

        [Theory] // Just like facts but used when you want to pass in variables 
        [InlineData(5, 2000, 10000)] // Variable data, last one is the expected
        [InlineData(2, 1500, 3000)]
        public void MailService_CalculateInvoice_ReturnInt(int plates, int amount, int expected)
        {
            //Arrange 
            
            //Act
            var result = _mealService.CalculateInvoice(plates, amount);

            //Assert
            result.Should().Be(expected);
            result.Should().BeGreaterThan(0);
            result.Should().NotBeInRange(-1000000, 0);
        }

        [Fact]
        public void MealService_GetTime_ReturnString()
        {
            //Arrange - variables, classes, mocks

            //Act
            var result = _mealService.GetTime();

            //Assert
            result.Should().BeAfter(1.January(2023));
        }

        [Fact]
        public void MealService_GetMeal_ReturnObject()
        {
            //Arrange
            var expected = new Meal()
            {
                Amount = 5000,
                Name = "Rice"
            }; 

            //Act
            var result = _mealService.GetMeal();

            //Assert
            result.Should().BeOfType<Meal>();
            result.Should().BeEquivalentTo(expected);
            result.Amount.Should().Be(expected.Amount);
        }

        [Fact]
        public void MealService_GetMeals_ReturnObject()
        {
            //Arrange
            var expected = new Meal()
            {
                Amount = 5000,
                Name = "Rice"
            };

            //Act
            var result = _mealService.GetMeals();

            //Assert
            //result.Should().BeOfType<IEnumerable<Meal>>();
            //result.Should().ContainEquivalentOf(expected);
            result.Should().Contain(x => x.Name == "Shawarma");
        }
    }
}
