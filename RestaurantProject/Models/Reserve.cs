using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantProject.Models.Enums;

namespace RestaurantProject.Models;

public class Reserve
{
    private static int _nextId = 1;

    public int Id { get; private set; }
    public DateTime DateAndHour { get; set; }
    public ReserveStatus Status { get; set; }
    public Client Client { get; set; }
    public Table Table { get; set; }

    public Reserve() 
    {
        Id = _nextId++;
    }
}
