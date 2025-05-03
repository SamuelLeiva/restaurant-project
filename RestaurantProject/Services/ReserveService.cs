using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantProject.Models;
using RestaurantProject.Models.Interfaces;

namespace RestaurantProject.Services;

public class ReserveService : IReserveService
{
    private static ReserveService _instance;
    private static readonly object _lock = new object();

    List<Reserve> reservesDB = new List<Reserve>();

    private ReserveService() { }

    public static ReserveService Instance
    {
        get
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = new ReserveService();
                }
                return _instance;
            }
        }
    }

    public void CreateReserve(Reserve reserve)
    {
        reservesDB.Add(reserve);
    }

    public List<Reserve> GetAllReserves()
    {
        return reservesDB;
    }

    public List<Reserve> GetReservesByDate(DateTime date)
    {
        return reservesDB.FindAll(r => r.DateAndHour.Year == date.Year
        && r.DateAndHour.Month == date.Month
        && r.DateAndHour.Day == date.Day);
    }

    public List<Reserve> GetReservesByClient(int clientId)
    {
        return reservesDB.FindAll(r => r.ReserveClient.Id == clientId);
    }

    public List<Reserve> GetReservesByTable(int tableId)
    {
        return reservesDB.FindAll(r => r.ReserveTable.Id == tableId);
    }

    public Reserve GetReserveByTableAndDate(int tableId, DateTime date)
    {
        return reservesDB.FirstOrDefault(r => r.ReserveTable.Id == tableId
        && r.DateAndHour.Year == date.Year
        && r.DateAndHour.Month == date.Month
        && r.DateAndHour.Day == date.Day
        && r.DateAndHour.Hour == date.Hour);
    }
}
