using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantProject.Validators;

public static class DataValidator
{
    public static int ReadPositiveInt(string message)
    {
        while (true)
        {
            Console.Write(message);
            var userInput = Console.ReadLine();

            if (int.TryParse(userInput, out int input) && input > 0)
            {
                return input;
            }

            Console.WriteLine("Entrada inválida. Por favor, ingrese un número entero mayor a 0.");
        }
    }
}
