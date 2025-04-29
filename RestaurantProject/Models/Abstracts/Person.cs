using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RestaurantProject.Models.Enums;

namespace RestaurantProject.Models.Abstracts;

public abstract class Person
{
    public string Name { get; set; }
    public Genre Genre { get; set; }
    public string Dni { get; set; }
    public string Address { get; set; }
    public int Age { get; set; }
}
