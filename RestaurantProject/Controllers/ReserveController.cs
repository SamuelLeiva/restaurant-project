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
    private readonly IReserveService reserveService = new ReserveService();
    private readonly IClientService clientService = new ClientService();
    private readonly ITableService tableService = new TableService();

    public void GetAllReserves()
    {
        List<Reserve> reserveList = reserveService.GetAllReserves();

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

        Client client = clientService.GetClientByDni(dni);

        if (client == null)
        {
            Console.WriteLine("El cliente con ese DNI no existe.");
            return;
        }

        List<Reserve> reservesClient = reserveService.GetReservesByClient(client.Id);

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

        Table table = tableService.GetTableById(idTable);

        if (table == null)
        {
            Console.WriteLine($"La mesa n° {idTable} no existe.");
            return;
        }

        List<Reserve> reservesTable = reserveService.GetReservesByTable(idTable);

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

        List<Reserve> reserveList = reserveService.GetReservesByDate(fullDate);

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
        // ingresar el n° de asientos
        //Console.WriteLine("Ingrese el n° de asientos deseados:");
        //if (!int.TryParse(Console.ReadLine(), out int numSeats) || numSeats <= 0)
        //{
        //    Console.WriteLine("Número de asientos inválido.");
        //    return;
        //}

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

        DateTime date;
        try
        {
            date = new DateTime(year, month, day);
        }
        catch (Exception)
        {
            Console.WriteLine("Fecha inválida.");
            return;
        }

        // Buscar cliente
        Client foundClient = clientService.GetClientByDni("65559948");
        if (foundClient == null)
        {
            Console.WriteLine("Cliente no encontrado.");
            return;
        }

        // Buscar mesa
        Table foundTable = tableService.GetTableById(1);
        if (foundTable == null)
        {
            Console.WriteLine("Mesa no encontrada.");
            return;
        }

        // Crear la reserva
        var newReserve = new Reserve
        {
            DateAndHour = date,
            Status = ReserveStatus.ACTIVO,
            ReserveTable = foundTable,
            ReserveClient = foundClient
        };

        reserveService.CreateReserve(newReserve);
        Console.WriteLine("Reserva creada exitosamente.");
    }
}
