using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantProject.Models;
using RestaurantProject.Models.Enums;
using RestaurantProject.Models.Interfaces;
using RestaurantProject.Services;

namespace RestaurantProject.Controllers;

public class ClientController
{
    public void CreateClient()
    {
        Console.WriteLine("\nAgregar un nuevo cliente:");

        string name = ReadString("Nombre del cliente: ");
        Genre genre = (Genre)ReadInt("Género del cliente (0:Masculino, 1:Femenino): ");
        string dni = ReadString("DNI del cliente:");
        string address = ReadString("Dirección del cliente:");
        int age = ReadInt("Edad del cliente:");

        var newClient = new Client()
        {
            Name = name,
            Genre = genre,
            Dni = dni,
            Address = address,
            Age = age
        };

        ClientService.Instance.CreateClient(newClient);
        Console.WriteLine($"Cliente agregado: {newClient.Name}");
    }

    public void GetAllClients()
    {
        var clients = ClientService.Instance.GetAllClients();

        if (!clients.Any())
        {
            Console.WriteLine("No hay ningún cliente registrado.");
            return;
        }

        Console.WriteLine("\n=== Lista de Clientes ===");
        PrintClientHeader();

        foreach (var client in clients)
        {
            PrintClient(client);
        }
    }

    public void GetClientByDni()
    {
        string dni = ReadString("Ingrese el DNI del cliente:");

        var client = ClientService.Instance.GetClientByDni(dni);

        if (client == null)
        {
            Console.WriteLine("Cliente no encontrado.");
            return;
        }

        Console.WriteLine("\n=== Cliente encontrado ===");
        PrintClientHeader();
        PrintClient(client);
    }

    public void FillInitialClients()
    {
        ClientService.Instance.FillClients();
    }

    //Métodos auxiliares
    private string ReadString(string message)
    {
        Console.Write(message + " ");
        return Console.ReadLine() ?? string.Empty;
    }

    private int ReadInt(string message)
    {
        Console.Write(message);
        int result;
        while (!int.TryParse(Console.ReadLine(), out result))
        {
            Console.Write("Entrada inválida. Intente nuevamente: ");
        }
        return result;
    }

    private void PrintClientHeader()
    {
        Console.WriteLine("ID\tNombre\t\tDNI\t\tDirección\t\tEdad");
        Console.WriteLine("--------------------------------------------------------------");
    }

    private void PrintClient(Client client)
    {
        Console.WriteLine($"{client.Id}\t{client.Name}\t{client.Dni}\t{client.Address}\t{client.Age}");
    }
}
