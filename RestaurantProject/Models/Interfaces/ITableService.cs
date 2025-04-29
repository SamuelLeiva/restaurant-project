using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantProject.Models.Interfaces;

public interface ITableService
{
    List<Table> GetAllTables();
    List<Table> GetTablesByNumSeats(int numSeats);
    Table GetTableById(int id);
    void CreateTable();
    void FillTables();
}
