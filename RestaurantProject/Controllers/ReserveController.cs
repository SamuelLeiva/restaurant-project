using RestaurantProject.Models;
using RestaurantProject.Models.Enums;
using RestaurantProject.Services;
using RestaurantProject.Validators;

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
        string dni = DataValidator.ReadNonEmptyString("Ingrese el DNI del cliente: ");

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
        int idTable = DataValidator.ReadPositiveInt("Ingrese el N° de mesa: ");

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
        var date = DataValidator.ReadDate("Ingrese la fecha de la reserva: ");

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
        int numSeats = DataValidator.ReadPositiveInt("Ingrese el n° de asientos deseados: ");

        //obtenemos mesas con ese n° de asientos
        var availableTables = TableService.Instance.GetTablesByNumSeats(numSeats);
        if (!availableTables.Any())
        {
            Console.WriteLine("No hay mesas disponibles con esa cantidad de asientos.");
            return;
        }

        PrintTables(availableTables);

        int idTable = DataValidator.ReadPositiveInt("Elija una de las mesas: ");

        var selectedTable = TableService.Instance.GetTableById(idTable);
        if (selectedTable == null)
        {
            Console.WriteLine("Mesa no encontrada.");
            return;
        }

        var client = DataValidator.ReadClientData(); //acá vamos

        if (ClientService.Instance.GetClientByDni(client.Dni) == null)
        {
            ClientService.Instance.CreateClient(client); //si es nuevo, añadirlo a la lista de datos
            Console.WriteLine("Cliente agregado a la base de datos.");
        }

        var date = DataValidator.ReadDateWithHour("Ingrese la fecha de la reserva: ");

        var existingReserve = ReserveService.Instance.GetReserveByTableAndDate(selectedTable.Id, date.Value);
        if (existingReserve != null)
        {
            Console.WriteLine("Ya hay una reserva hecha a esa hora.");
            return;
        }

        var reserveClient = ClientService.Instance.GetClientByDni(client.Dni);

        // Crear la reserva
        var newReserve = new Reserve
        {
            DateAndHour = date.Value,
            Status = ReserveStatus.ACTIVO,
            ReserveTable = selectedTable,
            ReserveClient = reserveClient!
        };

        ReserveService.Instance.CreateReserve(newReserve);

        //añadir reserva a cliente
        ClientService.Instance.AddReserveToClient(reserveClient, newReserve);

        Console.WriteLine("Reserva creada exitosamente.");
    }

    public void CompleteReserve()
    {
        Console.WriteLine("\n=== Completar reserva ===");
        int idReserve = DataValidator.ReadPositiveInt("Ingrese el id de la reserva a completar: ");

        var foundReserve = ReserveService.Instance.GetReserveById(idReserve);

        if (foundReserve != null)
        {
            ReserveService.Instance.CompleteReserve(foundReserve);
            Console.WriteLine("Reserva completada.");
            return;
        }

        if (foundReserve.Status == ReserveStatus.CANCELADO)
        {
            Console.WriteLine("Esta reserva fue cancelada.");
            return;
        }

        Console.WriteLine($"No existe reserva con id {idReserve}");
    }

    public void CancelReserve()
    {
        Console.WriteLine("\n=== Cancelar reserva ===");
        int idReserve = DataValidator.ReadPositiveInt("Ingrese el id de la reserva a cancelar: ");

        var foundReserve = ReserveService.Instance.GetReserveById(idReserve);

        if (foundReserve != null)
        {
            ReserveService.Instance.CancelReserve(foundReserve);
            Console.WriteLine("Reserva cancelada.");
            return;
        }

        Console.WriteLine($"No existe reserva con id {idReserve}");
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

    //-------------------- métodos auxiliares ------------------------------------
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
