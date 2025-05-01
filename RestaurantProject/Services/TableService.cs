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
    private static TableService _instance;
    private static readonly object _lock = new object();

    private List<Table> tablesDB = new List<Table>();

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
        tablesDB.Add(table);
    }

    public List<Table> GetAllTables()
    {
        return tablesDB;
    }

    public Table GetTableById(int id)
    {
        return tablesDB.FirstOrDefault(t => t.Id == id);
    }

    public List<Table> GetTablesByNumSeats(int numSeats)
    {
        return tablesDB.FindAll(t => t.NumSeats >= numSeats);
    }

    public void FillTables()
    {
        Table table1 = new Table()
        {
            NumSeats = 2
        };
        Table table2 = new Table()
        {
            NumSeats = 2
        };
        Table table3 = new Table()
        {
            NumSeats = 4
        };
        Table table4 = new Table()
        {
            NumSeats = 4
        };
        Table table5 = new Table()
        {
            NumSeats = 6
        };
        Table table6 = new Table()
        {
            NumSeats = 6
        };

        tablesDB.Add(table1);
        tablesDB.Add(table2);
        tablesDB.Add(table3);
        tablesDB.Add(table4);
        tablesDB.Add(table5);
        tablesDB.Add(table6);
    }

    
}
