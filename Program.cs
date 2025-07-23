using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG161_Project
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Fixed Prices of book categories
            const int spellTomes = 40;
            const int enchantedScrolls = 25;
            const int magicalNovels = 25;

            //Lists to store customer names and book names
            List<string> customerNames = new List<string>();
            Dictionary<string, string> magicBooks = new Dictionary<string, string>();

            int option;

            Console.WriteLine(@"
 What would you like to do?
 Enter the number to the corresponding option:
 ============================================
 1. Add a new customer
 2. Add a new book to the library
 3. Take order.
 ============================================
");

            option = int.Parse(Console.ReadLine());

            switch (option)
            {
                case 1:
                    string newCustomer;
                    Console.WriteLine("What is the Customer's name?");
                    newCustomer = Console.ReadLine();
                    customerNames.Add(newCustomer);
                    Console.WriteLine($"{newCustomer} was added successfully!");
                    break;
                case 2:
                    string newBook;
                    string category;
                    Console.WriteLine("What is the new Book's name?");
                    newBook = Console.ReadLine();
                    Console.WriteLine("What category does this book fall under?");
                    category = Console.ReadLine();
                    magicBooks.Add(newBook, category);
                    Console.WriteLine($"{newBook} was added successfully!");
                    break;
                default:
                    break;
            }



        }

        static string Coupons(int numRentals)
        {
            if (numRentals >= 10 && numRentals <= 24)
            {
                return "You recieved 1 Reward Rental.";
            }
            else if (numRentals > 24 && numRentals <= 49)
            {
                return "You recieved 2 Reward Rental.";
            }
            else if (numRentals > 49 && numRentals <= 74)
            {
                return "You recieved 4 Reward Rental.";
            }
            else
            {
                return "You recieved 8 Reward Rental.";
            }
        }
    }
}
