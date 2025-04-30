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

public class TableController
{
    //servicio
    private readonly ITableService tableService = new TableService();

    public void FillInitialTables()
    {
        tableService.FillTables();
    }

    public void GetAllTables()
    {
        List<Table> tableList = tableService.GetAllTables();

        if (tableList.Count == 0)
        {
            Console.WriteLine("No hay ninguna mesa");
            return;
        }

        Console.WriteLine("Lista de Mesas");
        Console.WriteLine("ID\tN° de asientos");
        Console.WriteLine("----------------------------------");

        foreach (Table table in tableList)
        {
            Console.WriteLine($"{table.Id}\t{table.NumSeats}");
        }
    }

    public void GetTableById()
    {
        Console.WriteLine("Ingrese el DNI del cliente");
        int id = Convert.ToInt32(Console.ReadLine());

        Table table = tableService.GetTableById(id);

        if (table == null)
        {
            Console.WriteLine("Mesa con ese id no existe.");
            return;
        }

        Console.WriteLine("Mesa encontrada");
        Console.WriteLine("ID\tN° de asientos");
        Console.WriteLine("----------------------------------");
        Console.WriteLine($"{table.Id}\t{table.NumSeats}\n");
    }

    public void GetTablesByNumSeats()
    {
        Console.WriteLine("Ingrese el n° de asientos que necesita");
        int numSeats = Convert.ToInt32(Console.ReadLine());

        List<Table> tableList = tableService.GetTablesByNumSeats(numSeats);

        if (tableList.Count == 0)
        {
            Console.WriteLine($"No se encontraron mesas con mínimo {numSeats} asientos.");
            return;
        }

        foreach (Table table in tableList)
        {
            Console.WriteLine("Mesas encontradas");
            Console.WriteLine("Id\tN° de asientos");
            Console.WriteLine($"{table.Id}\t{table.NumSeats}");
        }
    }

    public void CreateTable()
    {
        Console.WriteLine("\nAgregar una nueva mesa:");

        Console.WriteLine("Número de asientos de la mesa: ");
        int numSeats = Convert.ToInt32(Console.ReadLine());

        var newTable = new Table()
        {
            NumSeats = numSeats,
        };

        tableService.CreateTable(newTable);
        Console.WriteLine($"Mesa agregada: N° {newTable.Id}");
    }
}
