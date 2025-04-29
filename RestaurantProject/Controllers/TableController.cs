using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantProject.Models;
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
}
