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
    private List<Client> clientsDB = new List<Client>();

    public void CreateClient(Client newClient)
    {
        clientsDB.Add(newClient);
    }
    public Client GetClientByDni(string dni)
    {
        return clientsDB.FirstOrDefault(c => c.Dni == dni);
    }

    
    //public void CreateClient()
    //{
    //    Console.WriteLine("\nAgregar un nuevo cliente:");

    //    Console.WriteLine("Nombre del cliente: ");
    //    string name = Console.ReadLine();

    //    Console.WriteLine("Género del cliente (0:Masculino, 1:Femenino): ");
    //    Genre genre = (Genre)Convert.ToInt32(Console.ReadLine());

    //    Console.WriteLine("DNI del cliente");
    //    string dni = Console.ReadLine();

    //    Console.WriteLine("Dirección del cliente");
    //    string adress = Console.ReadLine();

    //    Console.WriteLine("Edad del cliente");
    //    int age = Convert.ToInt32(Console.ReadLine());

    //    var newClient = new Client()
    //    {
    //        Name = name,
    //        Genre = genre,
    //        Dni = dni,
    //        Address = adress,
    //        Age = age
    //    };

    //    clientsDB.Add(newClient);
    //    Console.WriteLine($"Cliente agregado: {newClient.Name}");
    //}

    public List<Client> GetAllClients()
    {
        return clientsDB;
    }

    public Client GetClientByDni(string dni)
    {
        
        //Console.WriteLine("Ingrese el DNI del cliente");
        //string dni = Console.ReadLine();

        //var client = clientsDB.FirstOrDefault(c => c.Dni == dni);

        //if(client == null)
        //{
        //    Console.WriteLine("Cliente no existe.");
        //    return;
        //}

        //Console.WriteLine("Cliente encontrado");
        //Console.WriteLine("ID\tNombre\tDNI\tDirección\tEdad");
        //Console.WriteLine("----------------------------------");
        //Console.WriteLine($"{client.Id}\t{client.Name}\t{client.Dni}\t{client.Address}\t{client.Age}\n");
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

        clientsDB.Add(client1);
        clientsDB.Add(client2);
    }

    
}
