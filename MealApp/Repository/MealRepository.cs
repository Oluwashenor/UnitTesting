using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MealApp.Repository;

public class MealRepository : IMealRepository
{
    public bool OffersOnlineDelivery()
    {
        return true;
    }
}

public interface IMealRepository
{
    bool OffersOnlineDelivery();
}