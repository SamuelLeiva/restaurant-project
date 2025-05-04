using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using RestaurantProject.Models;
using RestaurantProject.Models.Enums;
using RestaurantProject.Models.Interfaces;

namespace RestaurantProject.Services;

public class ClientService : IClientService
{
    private static ClientService? _instance;
    private static readonly object _lock = new object();

    private readonly List<Client> _clients = new List<Client>();

    private ClientService() { }

    public static ClientService Instance
    {
        get
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = new ClientService();
                }
                return _instance;
            }
        }
    }

    public void CreateClient(Client client)
    {
        _clients.Add(client);
    }

    public void AddReserveToClient(Client client, Reserve reserve)
    {
        client.AddReserve(reserve);
    }

    public Client? GetClientById(int id)
    {
        return _clients.FirstOrDefault(c => c.Id == id);
    }
    public Client? GetClientByDni(string dni)
    {
        return _clients.FirstOrDefault(c => c.Dni == dni);
    }


    public List<Client> GetAllClients()
    {
        return _clients;
    }

    public void FillClients()
    {
        var client1 = new Client()
        {
            Name = "Samuel Leiva",
            Genre = Genre.MASCULINO,
            Dni = "78874956",
            Address = "Av La Marina 567",
            Age = 35
        };

        var client2 = new Client()
        {
            Name = "Juana Perez",
            Genre = Genre.FEMENINO,
            Dni = "65559948",
            Address = "Av Izaguirre 456",
            Age = 27
        };

        _clients.Add(client1);
        _clients.Add(client2);
    }
}
