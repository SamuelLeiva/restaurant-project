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
    private static ReserveService? _instance;
    private static readonly object _lock = new object();

    private readonly List<Reserve> _reserves = new List<Reserve>();

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
        _reserves.Add(reserve);
    }

    public List<Reserve> GetAllReserves()
    {
        return _reserves;
    }

    public List<Reserve> GetReservesByDate(DateTime date)
    {
        return _reserves.Where(
            r => r.DateAndHour.Year == date.Year
            && r.DateAndHour.Month == date.Month
            && r.DateAndHour.Day == date.Day)
            .ToList();
    }

    public List<Reserve> GetReservesByClient(int clientId)
    {
        return _reserves.Where(
            r => r.ReserveClient.Id == clientId)
            .ToList();
    }

    public List<Reserve> GetReservesByTable(int tableId)
    {
        return _reserves
            .Where(r => r.ReserveTable.Id == tableId)
            .ToList();
    }

    public Reserve? GetReserveByTableAndDate(int tableId, DateTime date)
    {
        return _reserves.FirstOrDefault(r => 
        r.ReserveTable.Id == tableId
        && r.DateAndHour.Year == date.Year
        && r.DateAndHour.Month == date.Month
        && r.DateAndHour.Day == date.Day
        && r.DateAndHour.Hour == date.Hour);
    }
}
