using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantProject.Models.Interfaces;

public interface IClientService
{
    void GetAllClients();
    Client GetClientById(int id);
    void CreateClient();
}
