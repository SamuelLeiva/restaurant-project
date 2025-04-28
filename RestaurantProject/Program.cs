namespace RestaurantProject;

internal class Program
{
    static void Main(string[] args)
    {
        //RestaurantSystem system = new RestaurantSystem();
        //llenar de datos (clientes, mesas y reservas)

        bool exit = false;
        string opcion;
        try
        {
            while (!exit)
            {
                Console.WriteLine("========== RESTAURANT MANAGEMENT SYSTEM ==========");
                Console.WriteLine("1. Gestión de Clientes");
                Console.WriteLine("2. Gestión de Mesas");
                Console.WriteLine("3. Gestión de Reservas");
                Console.WriteLine("0. Salir del sistema");
                Console.WriteLine("Ingrese la opción deseada");

                opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        //ClientManagement();
                        break;
                    case "2":
                        //TableManagement();
                        break;
                    case "3":
                        //ReserveManagement();
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
            Console.WriteLine(ex.Message);
        }
    }
}
