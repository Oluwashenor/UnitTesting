using MealApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MealApp.Services;

public class MealService
{
    public string GetRandomMeal() 
    {
        List<string> meals = new List<string> { "Rice", "Dodo", "Beef Jerky", "Potato" };
        return meals[Random.Shared.Next(0, meals.Count)];
    }

    public string GetDrink()
    {
        return "Success: Coke";
    }

    public int CalculateInvoice(int plates, int amount)
    {
        return plates * amount;
    }

    public DateTime GetTime()
    {
        return DateTime.Now;
    }

    public Meal GetMeal()
    {
        return new Meal()
        {
             Amount = 5000,
             Name = "Rice"
        };
    }

    public IEnumerable<Meal> GetMeals()
    {
        IEnumerable<Meal> meals = new[]
        {
            new Meal()
            {
                Amount = 2000,
                Name = "Moi-Moi"
            },
            new Meal()
            {
                Amount = 1650,
                Name = "Pop Corn Small"
            },
            new Meal()
            {
                Amount = 2400,
                Name = "Shawarma"
            }
        };
        return meals;
    }
}
