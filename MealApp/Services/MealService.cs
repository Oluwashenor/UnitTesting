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
}
