using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantProject.Models.Enums;

namespace RestaurantProject.Models.Interfaces;

public interface IClientService
{
    List<Client> GetAllClients();
    Client GetClientByDni(string dni);
    void CreateClient(Client client);
    void FillClients();
    void AddReserveToClient(Client client, Reserve reserve);
}
