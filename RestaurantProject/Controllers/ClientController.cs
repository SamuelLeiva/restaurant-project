using RestaurantProject.Models;
using RestaurantProject.Models.Enums;
using RestaurantProject.Services;
using RestaurantProject.Validators;

namespace RestaurantProject.Controllers;

public class ClientController
{
    public void CreateClient()
    {
        Console.WriteLine("\nAgregar un nuevo cliente:");
        string dni = DataValidator.ReadDni("DNI del cliente:");
        var existingClient = ClientService.Instance.GetClientByDni(dni);
        if (existingClient != null)
        {
            Console.WriteLine("Cliente ya registrado con ese DNI.");
            return;
        }

        string name = DataValidator.ReadNonEmptyString("Nombre del cliente: ");
        Genre genre = (Genre)DataValidator.ReadGenre("Género del cliente (0:Masculino, 1:Femenino): ");
        string address = DataValidator.ReadNonEmptyString("Dirección del cliente:");
        int age = DataValidator.ReadPositiveInt("Edad del cliente:");

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

    public void GetClientById()
    {
        int idClient = DataValidator.ReadPositiveInt("Ingrese el Id del cliente: ");

        var client = ClientService.Instance.GetClientById(idClient);

        if (client == null)
        {
            Console.WriteLine("Cliente no encontrado.");
            return;
        }

        Console.WriteLine("\n=== Cliente encontrado ===");
        PrintClientHeader();
        PrintClient(client);
    }

    public void GetClientByDni()
    {
        string dni = DataValidator.ReadDni("Ingrese el DNI del cliente:");

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
