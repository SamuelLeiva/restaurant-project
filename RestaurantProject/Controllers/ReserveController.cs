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
        var reserves = ReserveService.Instance.GetAllReserves();

        if (!reserves.Any())
        {
            Console.WriteLine("No hay ninguna reserva hecha");
            return;
        }

        PrintReserveHeader();
        foreach (var reserve in reserves)
        {
            PrintReserve(reserve);
        }
    }

    public void GetReservesByClient()
    {
        string dni = ReadString("Ingrese el DNI del cliente: ");

        var client = ClientService.Instance.GetClientByDni(dni);

        if (client == null)
        {
            Console.WriteLine("El cliente con ese DNI no existe.");
            return;
        }

        var clientReserves = ReserveService.Instance.GetReservesByClient(client.Id);

        if (!clientReserves.Any())
        {
            Console.WriteLine("Este cliente no ha hecho ninguna reserva");
            return;
        }

        Console.WriteLine($"Reservas hechas por {client.Name}");
        Console.WriteLine("Id\tFecha y Hora\tStatus\tMesa");

        foreach (var reserve in clientReserves)
        {
            Console.WriteLine($"{reserve.Id}\t{reserve.DateAndHour}\t{reserve.Status}\t{reserve.ReserveTable.Id}");
        }
    }

    public void GetReservesByTable()
    {
        int idTable = ReadInt("Ingrese el N° de mesa: ");

        var table = TableService.Instance.GetTableById(idTable);

        if (table == null)
        {
            Console.WriteLine($"La mesa n° {idTable} no existe.");
            return;
        }

        var tableReserves = ReserveService.Instance.GetReservesByTable(idTable);

        if (!tableReserves.Any())
        {
            Console.WriteLine("Esta mesa no contiene ninguana reserva.");
            return;
        }

        Console.WriteLine($"Reservas hechas en la mesa n° {idTable}");
        Console.WriteLine("Id\tFecha y Hora\tStatus\tCliente");

        foreach (var reserve in tableReserves)
        {
            Console.WriteLine($"{reserve.Id}\t{reserve.DateAndHour}\t{reserve.Status}\t{reserve.ReserveClient.Name}");
        }
    }

    public void GetReservesByDate()
    {
        var date = ReadDate("Ingrese la fecha de la reserva: ");

        if (date == null) return;

        var reserves = ReserveService.Instance.GetReservesByDate(date.Value);
        if (!reserves.Any())
        {
            Console.WriteLine("No hay ninguna reserva hecha para ese día.");
            return;
        }

        Console.WriteLine($"Reservas del {date.Value:dd/MM/yyyy}");
        PrintReserveHeader();
        foreach (var reserve in reserves)
        {
            PrintReserve(reserve);
        }
    }

    public void CreateReserve()
    {
        Console.WriteLine("\n=== Crear nueva reserva ===");
        if (!ReadPositiveInt("Ingrese el n° de asientos deseados:", out var numSeats))
            return;

        //obtenemos mesas con ese n° de asientos
        var availableTables = TableService.Instance.GetTablesByNumSeats(numSeats);
        if (!availableTables.Any())
        {
            Console.WriteLine("No hay mesas disponibles con esa cantidad de asientos.");
            return;
        }

        PrintTables(availableTables);

        if (!ReadPositiveInt("Elija una de las mesas:", out var idTable))
            return;

        var selectedTable = TableService.Instance.GetTableById(idTable);
        if (selectedTable == null)
        {
            Console.WriteLine("Mesa no encontrada.");
            return;
        }

        var client = ReadClientData();

        if (ClientService.Instance.GetClientByDni(client.Dni) == null)
        {
            ClientService.Instance.CreateClient(client);
            Console.WriteLine("Cliente agregado a la base de datos.");
        }

        var date = ReadDate();
        if (date == null) return;

        var existingReserve = ReserveService.Instance.GetReserveByTableAndDate(selectedTable.Id, date.Value);
        if (existingReserve != null)
        {
            Console.WriteLine("Ya hay una reserva hecha a esa hora.");
            return;
        }

        // Crear la reserva
        var newReserve = new Reserve
        {
            DateAndHour = date.Value,
            Status = ReserveStatus.ACTIVO,
            ReserveTable = selectedTable,
            ReserveClient = ClientService.Instance.GetClientByDni(dni)
        };

        ReserveService.Instance.CreateReserve(newReserve);
        Console.WriteLine("Reserva creada exitosamente.");
    }

    public void FillInitialReserves()
    {
        Client client1 = ClientService.Instance.GetClientByDni("78874956");
        Client client2 = ClientService.Instance.GetClientByDni("65559948");

        Table table1 = TableService.Instance.GetTableById(1);
        Table table2 = TableService.Instance.GetTableById(2);

        var newReserve1 = new Reserve()
        {
            ReserveClient = client1,
            ReserveTable = table1,
            Status = ReserveStatus.ACTIVO,
            DateAndHour = new DateTime(2025, 5, 20, 18, 0, 0)
        };

        var newReserve2 = new Reserve()
        {
            ReserveClient = client2,
            ReserveTable = table2,
            Status = ReserveStatus.ACTIVO,
            DateAndHour = new DateTime(2025, 5, 21, 15, 0, 0)
        };

        ReserveService.Instance.CreateReserve(newReserve1);
        ReserveService.Instance.CreateReserve(newReserve2);
    }

    // métodos auxiliares
    private int ReadInt(string message)
    {
        Console.Write(message);
        int result;
        while (!int.TryParse(Console.ReadLine(), out result))
        {
            Console.Write("Entrada inválida. Intente nuevamente: ");
        }
        return result;
    }

    private bool ReadPositiveInt(string message, out int result)
    {
        Console.WriteLine(message);
        return int.TryParse(Console.ReadLine(), out result) && result > 0;
    }

    private string ReadString(string message)
    {
        Console.Write(message + " ");
        return Console.ReadLine() ?? string.Empty;
    }

    private DateTime? ReadDate(string prompt)
    {
        Console.WriteLine($"{prompt} (dd/mm/yyyy): ");
        var input = Console.ReadLine();
        if (DateTime.TryParse(input, out var date))
        {
            return date;
        }

        Console.WriteLine("Fecha inválida.");
        return null;
    }

    private Client ReadClientData()
    {
        Console.WriteLine("\nDatos del cliente:");
        Console.Write("Nombre: ");
        var name = Console.ReadLine();

        Console.Write("DNI: ");
        var dni = Console.ReadLine();

        Console.Write("Género (0: Masculino, 1: Femenino): ");
        var genre = (Genre)Convert.ToInt32(Console.ReadLine());

        Console.Write("Dirección: ");
        var address = Console.ReadLine();

        Console.Write("Edad: ");
        var age = Convert.ToInt32(Console.ReadLine());

        return new Client
        {
            Name = name,
            Dni = dni,
            Genre = genre,
            Address = address,
            Age = age
        };
    }

    private void PrintReserveHeader()
    {
        Console.WriteLine("ID\tFecha y Hora\tStatus\tCliente\tMesa");
        Console.WriteLine("---------------------------------------------");
    }

    private void PrintReserve(Reserve reserve)
    {
        Console.WriteLine($"{reserve.Id}\t{reserve.DateAndHour}\t{reserve.Status}\t{reserve.ReserveClient.Name}\t{reserve.ReserveTable.Id}");
    }

    private void PrintTables(List<Table> tables)
    {
        Console.WriteLine("Mesas disponibles:");
        Console.WriteLine("ID\tN° de asientos");
        foreach (var table in tables)
        {
            Console.WriteLine($"{table.Id}\t{table.NumSeats}");
        }
    }
}
