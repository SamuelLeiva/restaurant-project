using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantProject.Models.Interfaces;

public interface IClientService
{
    List<Client> GetAllClients();
    void GetClientByDni();
    void CreateClient();

    void FillClients();
}
