using DAL.Models;
using DAL;
using System;
using System.IO;
using UI.Controllers;
using System.Linq;

namespace UI
{
    class Program
    {
        static void Main(string[] args)
        {
            int tableChoice = 0;
            int actionChoice = 0;
            do
            {
                tableChoice = TableMenu();
                if (tableChoice == 0)
                {
                    return;
                }
                actionChoice = ActionMenu();


                BaseController controller = null;

                switch (tableChoice)
                {
                    case 1:
                        controller = new AbonnementController(actionChoice);
                        break;
                    case 2:
                        controller = new AuthorController(actionChoice);
                        break;
                    case 3:
                        controller = new BookController(actionChoice);
                        break;
                    case 4:
                        controller = new GenreController(actionChoice);
                        break;
                    case 6:
                        controller = new ReaderController(actionChoice);
                        break;
                }
                controller.doLogic();
            } while (true);
        }

        public static int ActionMenu()
        {
            var choice = 0;
            var success = false;
            do
            {
                Console.Clear();
                Console.WriteLine("Main menu");
                Console.WriteLine("Enter number in range 1-6 or 0 to stepBack:");
                Console.WriteLine("1.Create record");
                Console.WriteLine("2.Read all records");
                Console.WriteLine("3.Update record");
                Console.WriteLine("4.Delete record");
                Console.WriteLine("5.Generate random record");
                Console.WriteLine("6.Find records by some key");
                success = Int32.TryParse(Console.ReadLine(), out choice);
            } while (choice < 0 || choice > 6 || success == false);
            return choice;
        }

        public static int TableMenu()
        {
            var tableChoice = 0;
            var success = false;
            do
            {
                Console.Clear();
                Console.WriteLine("Choose table you wanna manipulate with:");
                Console.WriteLine("Enter table number in range 1-7 or 0 to exit:");
                Console.WriteLine("1.Abonnement");
                Console.WriteLine("2.Author");
                Console.WriteLine("3.Book");
                Console.WriteLine("4.Genre");
                Console.WriteLine("5.ReaderCard");
                Console.WriteLine("6.Reader");
                success = Int32.TryParse(Console.ReadLine(), out tableChoice);
            } while (tableChoice < 0 || tableChoice > 6 || success == false);


            return tableChoice;
        }


    }
}
