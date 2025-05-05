using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantProject.Models;
using RestaurantProject.Models.Enums;
using RestaurantProject.Services;

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

    public static int ReadGenre(string message)
    {
        while (true)
        {
            Console.Write(message);
            var userInput = Console.ReadLine();

            if (int.TryParse(userInput, out int input) && input > -1 && input < 2)
            {
                return input;
            }

            Console.WriteLine("Entrada inválida. Por favor, ingrese 0(Masculino) o 1(Femenino).");
        }
    }

    public static string ReadNonEmptyString(string message)
    {
        while (true)
        {
            Console.Write(message);
            var input = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(input.Trim()))
            {
                return input;
            }

            Console.WriteLine("La entrada no puede estar vacía. Intente nuevamente.");
        }
    }

    public static string ReadDni(string message)
    {
        while (true)
        {
            Console.Write(message);
            var input = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(input.Trim()) && input.Length == 8 && input.All(char.IsDigit))
            {
                return input;
            }

            Console.WriteLine("Entrada inválida. Ingrese un DNI válido de 8 dígitos numéricos.");
        }
    }

    public static DateTime? ReadDate(string message)
    {
        while (true)
        {
            Console.Write($"{message} (dd/mm/yyyy): ");
            var input = Console.ReadLine();

            if (DateTime.TryParseExact(
                    input,
                    "dd/MM/yyyy",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out var date))
            {
                return date;
            }

            Console.WriteLine("Fecha inválida. Intente nuevamente usando el formato dd/mm/yyyy.");
        }
    }

    public static DateTime? ReadDateWithHour(string message)
    {
        const string dateTimeFormat = "dd/MM/yyyy HH:mm";
        TimeSpan startTime = new TimeSpan(12, 0, 0);
        TimeSpan endTime = new TimeSpan(21, 0, 0); 

        while (true)
        {
            Console.Write($"{message} ({dateTimeFormat}): ");
            var input = Console.ReadLine();

            if (DateTime.TryParseExact(
                    input,
                    dateTimeFormat,
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out var dateTime))
            {
                TimeSpan timeOnly = dateTime.TimeOfDay;

                if (timeOnly >= startTime && timeOnly <= endTime)
                {
                    return dateTime;
                }

                Console.WriteLine("La hora debe estar entre las 12:00 y las 21:00.");
            }
            else
            {
                Console.WriteLine("Formato inválido. Por favor use el formato dd/MM/yyyy HH:mm (ejm: 04/05/2025 14:30).");
            }
        }
    }

    public static Client ReadClientData()
    {
        Console.WriteLine("\nDatos del cliente:");

        string dni = ReadNonEmptyString("Dni del cliente: ");

        var existingClient = ClientService.Instance.GetClientByDni(dni);
        if (existingClient != null)
        {
            Console.WriteLine("Cliente ya registrado con ese DNI.");
            return existingClient;
        }

        string name = ReadNonEmptyString("Nombre del cliente: ");

        Genre genre = (Genre)ReadGenre("Género del cliente (0:Masculino, 1:Femenino): ");

        string address = ReadNonEmptyString("Dirección del cliente: ");

        int age = ReadPositiveInt("Edad del cliente: ");

        return new Client
        {
            Name = name,
            Dni = dni,
            Genre = genre,
            Address = address,
            Age = age
        };
    }
}
