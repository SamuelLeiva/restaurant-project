﻿using RestaurantProject.Models;
using RestaurantProject.Services;
using RestaurantProject.Validators;

namespace RestaurantProject.Controllers;

public class TableController
{ 
    public void GetAllTables()
    {
        var tables = TableService.Instance.GetAllTables();

        if (!tables.Any())
        {
            Console.WriteLine("No hay ninguna mesa");
            return;
        }

        PrintTableHeader("Lista de Mesas");

        PrintTables(tables);
    }

    public void GetTableById()
    {
        int id = DataValidator.ReadPositiveInt("Ingrese el N° de mesa");

        var table = TableService.Instance.GetTableById(id);

        if (table is null)
        {
            Console.WriteLine("Mesa con ese Id no existe.");
            return;
        }

        PrintTableHeader("Mesa encontrada");
        PrintTable(table);
    }

    public void GetTablesByNumSeats()
    {
        int minSeats = DataValidator.ReadPositiveInt("Ingrese el número mínimo de asientos: ");

        var tables = TableService.Instance.GetTablesByNumSeats(minSeats);

        if (!tables.Any())
        {
            Console.WriteLine($"No se encontraron mesas con al menos {minSeats} asientos.");
            return;
        }

        PrintTableHeader("Mesas encontradas");
        PrintTables(tables);
    }

    public void CreateTable()
    {
        Console.WriteLine("\nAgregar una nueva mesa:");

        int seats = DataValidator.ReadPositiveInt("Número de asientos de la mesa: ");

        var table = new Table { NumSeats = seats };
        TableService.Instance.CreateTable(table);

        Console.WriteLine($"Mesa agregada: ID {table.Id}, Asientos {table.NumSeats}");
    }

    public void FillInitialTables()
    {
        TableService.Instance.FillTables();
    }

    //--------------- métodos auxiliares ---------------------
    private void PrintTableHeader(string title)
    {
        Console.WriteLine($"\n{title}");
        Console.WriteLine("ID\tN° de asientos");
        Console.WriteLine("----------------------------------");
    }

    private void PrintTable(Table table)
    {
        Console.WriteLine($"{table.Id}\t{table.NumSeats}");
    }

    private void PrintTables(IEnumerable<Table> tables)
    {
        foreach (var table in tables)
        {
            PrintTable(table);
        }
    }
}
