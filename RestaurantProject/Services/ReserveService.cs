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

    public List<Reserve> GetReserveByDate(DateTime date)
    {
        return reservesDB.FindAll(r => r.DateAndHour == date);
    }

    public List<Reserve> GetReservesByClient(int clientId)
    {
        return reservesDB.FindAll(r => r.Client.Id == clientId);
    }

    public List<Reserve> GetReservesByTable(int tableId)
    {
        return reservesDB.FindAll(r => r.Table.Id == tableId);
    }

}
