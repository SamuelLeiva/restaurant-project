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
    public void CreateReserve(int numSeats, DateTime dateReserve)
    {
        throw new NotImplementedException();
    }

    public List<Reserve> GetAllReserves()
    {
        throw new NotImplementedException();
    }

    public List<Reserve> GetReserveByDate(DateTime date)
    {
        throw new NotImplementedException();
    }

    public List<Reserve> GetReservesByClient(int clientId)
    {
        throw new NotImplementedException();
    }

    public List<Reserve> GetReservesByTable(int tableId)
    {
        throw new NotImplementedException();
    }
}
