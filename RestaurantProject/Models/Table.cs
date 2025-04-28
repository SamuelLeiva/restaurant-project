using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantProject.Models;

public class Table
{
    private static int _nextId = 1;

    public int Id { get; private set; }
    public int NumSeats { get; set; }

    public Table()
    {
        Id = _nextId++;
    }
}
