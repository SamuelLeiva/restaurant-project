using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantProject.Models;
using RestaurantProject.Models.Enums;
using RestaurantProject.Models.Interfaces;
using RestaurantProject.Services;

namespace RestaurantProject.Controllers;

public class ReserveController
{
    public void GetAllReserves()
    {
        List<Reserve> reserveList = ReserveService.Instance.GetAllReserves();

        if (reserveList.Count == 0)
        {
            Console.WriteLine("No hay ninguna reserva hecha");
            return;
        }

        Console.WriteLine("Lista de reservas");
        Console.WriteLine("ID\tFecha y Hora\tStatus\tCliente\tMesa");
        Console.WriteLine("---------------------------------------------");

        foreach (Reserve reserve in reserveList)
        {
            Console.WriteLine($"{reserve.Id}\t{reserve.DateAndHour}\t{reserve.Status}\t{reserve.ReserveClient.Name}\t{reserve.ReserveTable.Id}");
        }
    }

    public void GetReservesByClient()
    {
        Console.WriteLine("Ingrese el DNI del cliente");
        string dni = Console.ReadLine();

        Client client = ClientService.Instance.GetClientByDni(dni);

        if (client == null)
        {
            Console.WriteLine("El cliente con ese DNI no existe.");
            return;
        }

        List<Reserve> reservesClient = ReserveService.Instance.GetReservesByClient(client.Id);

        if (reservesClient.Count == 0)
        {
            Console.WriteLine("Este cliente no ha hecho ninguna reserva");
            return;
        }

        Console.WriteLine($"Reservas hechas por {client.Name}");
        Console.WriteLine("Id\tFecha y Hora\tStatus\tMesa");

        foreach (Reserve reserve in reservesClient)
        {
            Console.WriteLine($"{reserve.Id}\t{reserve.DateAndHour}\t{reserve.Status.ToString()}\t{reserve.ReserveTable.Id}");
        }
    }

    public void GetReservesByTable()
    {
        Console.WriteLine("Ingrese el N° de mesa: ");
        int idTable = Convert.ToInt32(Console.ReadLine());

        Table table = TableService.Instance.GetTableById(idTable);

        if (table == null)
        {
            Console.WriteLine($"La mesa n° {idTable} no existe.");
            return;
        }

        List<Reserve> reservesTable = ReserveService.Instance.GetReservesByTable(idTable);

        if (reservesTable.Count == 0)
        {
            Console.WriteLine("Esta mesa no contiene ninguana reserva.");
            return;
        }

        Console.WriteLine($"Reservas hechas en la mesa n° {idTable}");
        Console.WriteLine("Id\tFecha y Hora\tStatus\tCliente");

        foreach (Reserve reserve in reservesTable)
        {
            Console.WriteLine($"{reserve.Id}\t{reserve.DateAndHour}\t{reserve.Status}\t{reserve.ReserveClient.Name}");
        }
    }

    public void GetReservesByDate()
    {
        Console.WriteLine("\nIngrese el día de la reserva");
        int day = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("Ingrese el mes de la reserva");
        int month = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("Ingrese el año de la reserva");
        int year = Convert.ToInt32(Console.ReadLine());

        DateTime fullDate = new DateTime(year, month, day);

        List<Reserve> reserveList = ReserveService.Instance.GetReservesByDate(fullDate);

        if (reserveList.Count == 0)
        {
            Console.WriteLine("No hay ninguna reserva hecha para ese día");
            return;
        }

        Console.WriteLine($"Lista de reservas del {day}/{month}/{year}");
        Console.WriteLine("ID\tFecha y Hora\tStatus\tCliente\tMesa");
        Console.WriteLine("---------------------------------------------");

        foreach (Reserve reserve in reserveList)
        {
            Console.WriteLine($"{reserve.Id}\t{reserve.DateAndHour}\t{reserve.Status.ToString()}\t{reserve.ReserveClient.Name}\t{reserve.ReserveTable.Id}");
        }
    }

    public void CreateReserve()
    {
        Console.WriteLine("\n=== Crear nueva reserva ===");
        Console.WriteLine("Ingrese el n° de asientos deseados:");
        if (!int.TryParse(Console.ReadLine(), out int numSeats) || numSeats <= 0)
        {
            Console.WriteLine("Número de asientos inválido.");
            return;
        }

        //obtenemos mesas con ese n° de asientos
        List<Table> tableList = TableService.Instance.GetTablesByNumSeats(numSeats);

        Console.WriteLine("\nMesas disponibles: ");
        Console.WriteLine("ID\tN° de asientos");

        foreach (Table table in tableList)
        {
            Console.WriteLine($"{table.Id}\t{table.NumSeats}");
        }

        Console.WriteLine("Elija una de las mesas");
        int numTable = Convert.ToInt32(Console.ReadLine());

        Table foundTable = TableService.Instance.GetTableById(numTable);

        //Datos del cliente
        Console.WriteLine("\nDatos del cliente");
        Console.WriteLine("Nombre del cliente: ");
        string name = Console.ReadLine();

        Console.WriteLine("DNI del cliente");
        string dni = Console.ReadLine();

        Console.WriteLine("Género del cliente (0:Masculino, 1:Femenino): ");
        Genre genre = (Genre)Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("Dirección del cliente");
        string adress = Console.ReadLine();

        Console.WriteLine("Edad del cliente");
        int age = Convert.ToInt32(Console.ReadLine());

        if (ClientService.Instance.GetClientByDni(dni) == null)
        {
            var newClient = new Client()
            {
                Name = name,
                Genre = genre,
                Dni = dni,
                Address = adress,
                Age = age
            };

            ClientService.Instance.CreateClient(newClient);
            Console.WriteLine("\nCliente nuevo agregado a la base de datos.");
        }

        // Ingresar la fecha
        Console.WriteLine("Ingrese el día de la reserva:");
        if (!int.TryParse(Console.ReadLine(), out int day) || day <= 0 || day > 31)
        {
            Console.WriteLine("Día inválido.");
            return;
        }

        Console.WriteLine("Ingrese el mes de la reserva:");
        if (!int.TryParse(Console.ReadLine(), out int month) || month <= 0 || month > 12)
        {
            Console.WriteLine("Mes inválido.");
            return;
        }

        int year = DateTime.Now.Year;

        Console.WriteLine("\nIngrese la hora de la reserva:");
        int hour = Convert.ToInt32(Console.ReadLine());

        DateTime date;
        try
        {
            date = new DateTime(year, month, day, hour, 0, 0);
        }
        catch (Exception)
        {
            Console.WriteLine("Fecha inválida.");
            return;
        }

        // Crear la reserva
        var newReserve = new Reserve
        {
            DateAndHour = date,
            Status = ReserveStatus.ACTIVO,
            ReserveTable = foundTable,
            ReserveClient = ClientService.Instance.GetClientByDni(dni)
        };

        ReserveService.Instance.CreateReserve(newReserve);
        Console.WriteLine("Reserva creada exitosamente.");
    }
}
