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
    private List<Table> _tables = new List<Table>();
    public void CreateTable(Table table)
    {
        _tables.Add(table);
    }

    public List<Table> GetAllTables()
    {
        return _tables.ToList();
    }

    public Table GetTableById(int id)
    {
        return _tables.FirstOrDefault(t => t.Id == id);
    }

    public List<Table> GetTablesByNumSeats(int numSeats)
    {
        throw new NotImplementedException();
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

        _tables.Add(table1);
        _tables.Add(table2);
        _tables.Add(table3);
        _tables.Add(table4);
        _tables.Add(table5);
        _tables.Add(table6);
    }

    
}
