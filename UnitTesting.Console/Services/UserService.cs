using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTesting.ConsoleApp.Services;

public class UserService
{
    public string GetUserById(int id)
    {
        if (id == 1)
            return "Adesina";
        return "User not Found";
    }
}
