using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantProject.Models.Interfaces;

public interface IReserveService
{
    List<Reserve> GetAllReserves();
    void CreateReserve(int numSeats, DateTime dateReserve);

    //Get reserves by client
    List<Reserve> GetReservesByClient(int clientId);

    //Get reserves by table
    List<Reserve> GetReservesByTable(int tableId);

    //Get reserves by date
    List<Reserve> GetReserveByDate(DateTime date);
}
