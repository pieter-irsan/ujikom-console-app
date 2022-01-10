using System;

namespace Praktek
{
    class Program
    {
        static void Main(string[] args)
        {
            Menu menu = new Menu();
            menu.Option();
        }
    }
    
    class Menu
    {
        public void Option()
        {
            Console.WriteLine("\n=============================");
            Console.WriteLine("PT ICL ABADI");
            Console.WriteLine("=============================");
            Console.WriteLine("[1] Calculate costs");
            Console.WriteLine("[2] See previous costs");
<<<<<<< HEAD
            Console.WriteLine("[3] Delete a row");
            Console.WriteLine("[4] Reset table");
            Console.WriteLine("[5] Exit");
=======
            Console.WriteLine("[3] Exit");
>>>>>>> d73549fa871ae891a65b426fc182037223ac6e3e

            string option = "";
            Method method = new Method();
            option = Console.ReadLine();
            switch (option)
            {
                case "1":
                    method.Result();
<<<<<<< HEAD
                    method.FinishTask();
                    break;
                case "2":
                    method.GetCosts();
                    method.FinishTask();
                    break;
                case "3":
                    method.GetCosts();
                    Console.Write("\nEnter which row to delete based on quantity: ");
                    int quantity = Convert.ToInt32(Console.ReadLine());
                    method.DeleteRow(quantity);
                    method.FinishTask();
                    break;
                case "4":
                    method.ResetTable();
                    method.FinishTask();
                    break;
                case "5":
=======
                    break;
                case "2":
                    method.GetCosts();
                    break;
                case "3":
                    Console.WriteLine(">Exiting program with 'return;'");
>>>>>>> d73549fa871ae891a65b426fc182037223ac6e3e
                    return;
                default:
                    Console.WriteLine("Choose from the available option!");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey(true);
                    Option();
                    break;
                }
        }
    }
}
