using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantProject.Models.Interfaces;

public interface ITableService
{
    void GetAllTables();
    Table GetTableById(int id);
    void CreateTable();
}
