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
    private readonly TableController tableController = new TableController();
    private readonly ReserveController reserveController = new ReserveController();

    public void ClientManagement()
    {
        string option;
        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("========== GESTIÓN DE CLIENTES ==========");
            Console.WriteLine("1.Ver lista de clientes");
            Console.WriteLine("2.Buscar cliente");
            Console.WriteLine("3.Añadir nuevo cliente");
            Console.WriteLine("0.Atrás");

            option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    clientController.GetAllClients();
                    break;
                case "2":
                    clientController.GetClientByDni();
                    break;
                case "3":
                    clientController.CreateClient();
                    break;
                case "0":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Opción inválida. Elija una nueva opción");
                    break;
            }
        }
    }

    public void TableManagement()
    {
        string option;
        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("========== GESTIÓN DE MESAS ==========");
            Console.WriteLine("1.Ver lista de mesas");
            Console.WriteLine("2.Buscar mesa por id");
            Console.WriteLine("3.Añadir nueva mesa");
            Console.WriteLine("4.Buscar mesas por número de asientos");
            Console.WriteLine("0.Atrás");

            option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    tableController.GetAllTables();
                    break;
                case "2":
                    tableController.GetTableById();
                    break;
                case "3":
                    tableController.CreateTable();
                    break;
                case "4":
                    tableController.GetTablesByNumSeats();
                    break;
                case "0":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Opción inválida. Elija una nueva opción");
                    break;
            }
        }
    }

    public void ReserveManagement()
    {
        string option;
        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("========== GESTIÓN DE RESERVAS ==========");
            Console.WriteLine("1.Ver lista de reservas");
            Console.WriteLine("2.Obtener reservas por cliente");
            Console.WriteLine("3.Obtener reservas por mesa");
            Console.WriteLine("4.Obtener reservas por fecha");
            Console.WriteLine("5.Crear una nueva reserva");
            Console.WriteLine("0.Atrás");

            option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    reserveController.GetAllReserves();
                    break;
                case "2":
                    reserveController.GetReservesByClient();
                    break;
                case "3":
                    reserveController.GetReservesByTable();
                    break;
                case "4":
                    reserveController.GetReservesByDate();
                    break;
                case "5":
                    reserveController.CreateReserve();
                    break;
                case "0":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Opción inválida. Elija una nueva opción");
                    break;
            }
        }
    }

    public void FillData()
    {
        //agregar mesas
        tableController.FillInitialTables();

        //agregar clientes
        clientController.FillInitialClients();

    }
}


