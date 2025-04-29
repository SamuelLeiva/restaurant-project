using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantProject.Models;
using RestaurantProject.Models.Enums;
using RestaurantProject.Models.Interfaces;

namespace RestaurantProject.Services;

public class ClientService : IClientService
{
    private List<Client> clientsDB = new List<Client>();

    public void CreateClient()
    {
        Console.WriteLine("\nAgregar un nuevo cliente:");

        Console.WriteLine("Nombre del cliente: ");
        string name = Console.ReadLine();

        Console.WriteLine("Género del cliente (0:Masculino, 1:Femenino): ");
        Genre genre = (Genre)Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("DNI del cliente");
        string dni = Console.ReadLine();

        Console.WriteLine("Dirección del cliente");
        string adress = Console.ReadLine();

        Console.WriteLine("Edad del cliente");
        int age = Convert.ToInt32(Console.ReadLine());

        var newClient = new Client()
        {
            Name = name,
            Genre = genre,
            Dni = dni,
            Address = adress,
            Age = age
        };

        clientsDB.Add(newClient);
        Console.WriteLine($"Cliente agregado: {newClient.Name}");
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
            Console.WriteLine($"{client.Id}\t{client.Name}\t{client.Dni}\t{client.Address}\t\t{client.Age}");
        }
    }

    public void GetClientByDni()
    {
        Console.WriteLine("Ingrese el DNI del cliente");
        string dni = Console.ReadLine();

        var client = clientsDB.FirstOrDefault(c => c.Dni == dni);

        if(client == null)
        {
            Console.WriteLine("Cliente no existe.");
            return;
        }

        Console.WriteLine("Cliente encontrado");
        Console.WriteLine("ID\tNombre\tDNI\tDirección\tEdad");
        Console.WriteLine("----------------------------------");
        Console.WriteLine($"{client.Id}\t{client.Name}\t{client.Dni}\t{client.Address}\t{client.Age}\n");
    }

    public void FillData()
    {

    }
}
