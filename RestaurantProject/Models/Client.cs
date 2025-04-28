using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantProject.Models.Abstracts;

namespace RestaurantProject.Models;

public class Client : Person
{
    //Lista de reservas
    List<Reserve> Reserves { get; set; }

    public Client()
    {
        Reserves = new List<Reserves>();
    }
}
