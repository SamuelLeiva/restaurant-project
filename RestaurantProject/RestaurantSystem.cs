using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantProject.Models;
using RestaurantProject.Models.Interfaces;
using RestaurantProject.Services;

namespace RestaurantProject;

public class RestaurantSystem
{

    //listas de mesas, clientes y reservas iniciales
    private List<Table> tables = new List<Table>();
    private List<Client> clients = new List<Client>();
    private List<Reserve> reserves = new List<Reserve>();

    //servicios
    private IClientService clientService = new ClientService();

    void ClientManagement()
    {
        Console.WriteLine("========== GESTIÓN DE CLIENTES ==========");
        Console.WriteLine("1.Ver lista de clientes");
        Console.WriteLine("2.Buscar cliente");
        Console.WriteLine("3.Añadir nuevo cliente");
        Console.WriteLine("0.Atrás");

        string option;
        option = Console.ReadLine();

        switch (option)
        {
            case "1":
                var clientList = clientService.GetAllClients();
        }

        
    }

    void TableManagement()
    {
    }

    void ReserveManagement()
    {
    }

    void FillData()
    {
        //agregar mesas

        //agregar clientes

        //agregar reservas
    }
}


