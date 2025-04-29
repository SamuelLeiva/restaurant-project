using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantProject.Models;
using RestaurantProject.Models.Interfaces;

namespace RestaurantProject.Services;

public class ClientService : IClientService
{
    private List<Client> clientsDB = new List<Client>();

    public void CreateClient()
    {
        throw new NotImplementedException();
    }

    public void GetAllClients()
    {
        if (clientsDB.Count == 0)
        {
            Console.WriteLine("No hay ningún cliente");
            return;
        }

        Console.WriteLine("Lista de Clientes");
        Console.WriteLine("ID\tNombre\tDNI\tDirección\tEdad");
        Console.WriteLine("----------------------------------");
        foreach (Client client in clientsDB)
        {
            Console.WriteLine($"{client.Id}\t{client.Name}\t{client.Dni}\t{client.Adress}\t\t{client.Age}");
        }
    }

    public void GetClientById(int id)
    {
        
    }
}
