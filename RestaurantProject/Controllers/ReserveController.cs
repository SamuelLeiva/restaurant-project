using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantProject.Models;
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


}
