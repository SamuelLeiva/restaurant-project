using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantProject.Models;
using RestaurantProject.Models.Interfaces;
using RestaurantProject.Services;

namespace RestaurantProject.Controllers;

public class ClientController
{
    //servicios
    private readonly IClientService clientService = new ClientService();
    public void CreateClient()
    {

    }

    public void GetAllClients()
    {
        var listClients = clientService.GetAllClients();
        if (listClients.Count == 0)
        {
            Console.WriteLine("No hay ningún cliente");
            return;
        }

        Console.WriteLine("Lista de Clientes");
        Console.WriteLine("ID\tNombre\tDNI\tDirección\tEdad");
        Console.WriteLine("----------------------------------");
        foreach (Client client in listClients)
        {
            Console.WriteLine($"{client.Id}\t{client.Name}\t{client.Dni}\t{client.Address}\t\t{client.Age}");
        }
    }

    public void GetClientByDni()
    {

    }

    public void FillInitialClients()
    {
        clientService.FillClients();
    }
}
