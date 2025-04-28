using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantProject.Models.Interfaces;

public interface IReserveService
{
    void GetAllReserves();
    void CreateReserve();

    //Get reserves by client
    void GetReservesByClient(int clientId);

    //Get reserves by table
    void GetReservesByTable(int tableId);

    //Get reserves by date
    void GetReserveByDate(DateTime date);
}
