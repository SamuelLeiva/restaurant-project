using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantProject.Models.Interfaces;

public interface IReserveService
{
    List<Reserve> GetAllReserves();
    Reserve GetReserveById(int id);
    void CreateReserve(Reserve reserve);
    void CompleteReserve(Reserve reserve);
    void CancelReserve(Reserve reserve);
    List<Reserve> GetReservesByClient(int clientId);
    List<Reserve> GetReservesByTable(int tableId);
    List<Reserve> GetReservesByDate(DateTime date);
    Reserve GetReserveByTableAndDate(int tableId, DateTime date);

}
