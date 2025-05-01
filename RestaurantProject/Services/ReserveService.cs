using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantProject.Models;
using RestaurantProject.Models.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RestaurantProject.Services;

public class ReserveService : IReserveService
{

    List<Reserve> reservesDB = new List<Reserve>();

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
        return reservesDB.FindAll(r => r.DateAndHour == date);
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
        return reservesDB.FirstOrDefault(r => r.ReserveTable.Id == tableId && r.DateAndHour == date);
    }
}
