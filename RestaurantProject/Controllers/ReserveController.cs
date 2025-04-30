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
            Console.WriteLine($"{reserve.Id}\t{reserve.DateAndHour}\t{reserve.Status.ToString()}\t{reserve.Client.Name}\t{reserve.Table.Id}");
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
            Console.WriteLine($"{reserve.Id}\t{reserve.DateAndHour}\t{reserve.Status.ToString()}\t{reserve.Table.Id}");
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
            Console.WriteLine($"{reserve.Id}\t{reserve.DateAndHour}\t{reserve.Status.ToString()}\t{reserve.Client.Name}");
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
            Console.WriteLine($"{reserve.Id}\t{reserve.DateAndHour}\t{reserve.Status.ToString()}\t{reserve.Client.Name}\t{reserve.Table.Id}");
        }
    }

    public void CreateReserve()
    {
        Console.WriteLine("\n=== Crear nueva reserva ===");
        // ingresar el n° de asientos
        Console.WriteLine("Ingrese el n° de aientos deseados");
        int numSeats = Convert.ToInt32(Console.ReadLine());
        // ingresar fecha de la reserva
        Console.WriteLine("Ingrese el día de la reserva");
        int day = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Ingrese el mes de la reserva");
        int month = Convert.ToInt32(Console.ReadLine());
        //año actual
        int year = DateTime.Now.Year;

        // buscar las mesas con ese n° de asientos
        List<Table> tables = tableService.GetTablesByNumSeats(numSeats);
        //buscar si existe una reserva en esa mesa con esa fecha y hora
        foreach (Table table in tables)
        {
            //obtener reserva por fecha y mesa
            Reserve reserve = reserveService.GetReserveByTableAndDate(table.Id, new DateTime(year, month, day));

            if (reserve != null)
            {
                //crear la reserva
                var newReserve = new Reserve 
                {
                    DateAndHour = new DateTime(year, month, day),
                    Status = ReserveStatus.ACTIVO,
                    Table = table,
                    
                };

                reserveService.CreateReserve(reserve);
                Console.WriteLine("Reserva creada");
                return;
            }
        }
    }
}
