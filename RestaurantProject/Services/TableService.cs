using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using RestaurantProject.Models;
using RestaurantProject.Models.Interfaces;

namespace RestaurantProject.Services;

public class TableService : ITableService
{
    private static TableService? _instance;
    private static readonly object _lock = new object();

    private readonly List<Table> _tables = new List<Table>();

    private TableService() { }

    public static TableService Instance
    {
        get
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = new TableService();
                }
                return _instance;
            }
        }
    }

    
    public void CreateTable(Table table)
    {
        _tables.Add(table);
    }

    public List<Table> GetAllTables()
    {
        return _tables;
    }

    public Table GetTableById(int id)
    {
        return _tables.FirstOrDefault(t => t.Id == id);
    }

    public List<Table> GetTablesByNumSeats(int minSeats)
    {
        return _tables.Where(t => t.NumSeats >= minSeats).ToList();
    }

    public void FillTables()
    {
        var seatCounts = new[] { 2, 2, 4, 4, 6, 6 };

        foreach (var seats in seatCounts)
        {
            _tables.Add(new Table { NumSeats = seats });
        }
    }
}
