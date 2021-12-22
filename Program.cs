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
            Console.WriteLine("[3] Exit");

            string option = "";
            Method method = new Method();
            option = Console.ReadLine();
            switch (option)
            {
                case "1":
                    method.Result();
                    break;
                case "2":
                    method.GetCosts();
                    break;
                case "3":
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
