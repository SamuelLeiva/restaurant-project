﻿namespace RestaurantProject;

internal class Program
{
    static void Main(string[] args)
    {
        RestaurantSystem restaurantSystem = new RestaurantSystem();
        //llenar de datos (clientes, mesas y reservas)
        restaurantSystem.FillData();
        

        bool exit = false;
        string opcion;
        try
        {
            while (!exit)
            {
                Console.WriteLine("\n========== RESTAURANT MANAGEMENT SYSTEM ==========");
                Console.WriteLine("1. Gestión de Clientes");
                Console.WriteLine("2. Gestión de Mesas");
                Console.WriteLine("3. Gestión de Reservas");
                Console.WriteLine("0. Salir del sistema");
                Console.WriteLine("Ingrese la opción deseada");

                opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        restaurantSystem.ClientManagement();
                        break;
                    case "2":
                        restaurantSystem.TableManagement();
                        break;
                    case "3":
                        restaurantSystem.ReserveManagement();
                        break;
                    case "0":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Opción incorrecta. Ingrese una opción válida");
                        break;
                }


            }
        } catch (Exception ex)
        {
            Console.WriteLine($"Error inesperado: {ex.Message}");
        }
    }
}
