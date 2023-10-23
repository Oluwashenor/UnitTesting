using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTesting.ConsoleApp.Services;

namespace UnitTesting.ConsoleApp.Tests;

public static class UserServiceTests
{
    //ClassName_methodName_expectedResult
    public static void UserService_GetUserById_ReturnString()
    {
        try
        {
            //Arrange -- bring in everything you need, classes, variables and the likes
            int userId = 1;
            UserService userService = new UserService();

            //Act - use those things you arranged above, Execute those functions
            var result = userService.GetUserById(userId);
            if (result == "Adesina")
                Console.WriteLine("Passed: Test passes Successfully");
            else
                Console.WriteLine("Failed: Test Failed");
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
}
