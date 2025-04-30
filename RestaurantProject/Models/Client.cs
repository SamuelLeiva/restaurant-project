using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantProject.Models.Abstracts;

namespace RestaurantProject.Models;

public class Client : Person
{
    private static int _nextId = 1;

    public int Id { get; private set; }
    //Lista de reservas
    public List<Reserve> Reserves { get; set; }

    public Client()
    {
        Reserves = new List<Reserve>();
        Id = _nextId++;
    }

    public void AddReserve(Reserve reserve)
    {
        Reserves.Add(reserve);
    }
}
