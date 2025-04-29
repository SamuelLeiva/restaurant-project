using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantProject.Controllers;
using RestaurantProject.Models;
using RestaurantProject.Models.Interfaces;
using RestaurantProject.Services;

namespace RestaurantProject;

public class RestaurantSystem
{

    //servicios
    private readonly ClientController clientController = new ClientController();

    public void ClientManagement()
    {
        Console.WriteLine("========== GESTIÓN DE CLIENTES ==========");
        Console.WriteLine("1.Ver lista de clientes");
        Console.WriteLine("2.Buscar cliente");
        Console.WriteLine("3.Añadir nuevo cliente");
        Console.WriteLine("0.Atrás");

        string option;
        bool exit = false;
        option = Console.ReadLine();

        switch (option)
        {
            case "1":
                clientController.GetAllClients();
                break;
            case "2":
                //clientService.GetClientByDni();
                break;
            case "3":
                //clientService.CreateClient();
                break;
            case "0":
                exit = true;
                break;
            default:
                Console.WriteLine("Opción inválida. Elija una nueva opción");
                break;
        }

        
    }

    void TableManagement()
    {
    }

    void ReserveManagement()
    {
    }

    public void FillData()
    {
        //agregar mesas

        //agregar clientes
        clientController.FillInitialClients();

        //agregar reservas
    }
}


