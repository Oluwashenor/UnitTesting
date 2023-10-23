using FluentAssertions;
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
        [Fact]
        public void MealService_GetRandomMeal_ReturnString()
        {
            //Arrange - variables, classes, mocks 
            MealService mealService = new MealService();

            //Act
            var result = mealService.GetRandomMeal();

            //Assert
            result.Should().NotBeNullOrWhiteSpace();
        } 
        
        [Fact]
        public void MealService_GetDrink_ReturnString()
        {
            //Arrange - variables, classes, mocks 
            MealService mealService = new MealService();

            //Act
            var result = mealService.GetDrink();

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
            var mealService = new MealService();

            //Act
            var result = mealService.CalculateInvoice(plates, amount);

            //Assert
            result.Should().Be(expected);
            result.Should().BeGreaterThan(0);
            result.Should().NotBeInRange(-1000000, 0);
        }
    }
}
