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

            while (true)
            {
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
                    case 1: //Code to add a new custommer to the customerNames List
                        Console.Clear();
                        string newCustomer;
                        Console.WriteLine("What is the Customer's name?");
                        newCustomer = Console.ReadLine();

                        if (customerNames.Contains(newCustomer)) //Check if the customer entered doesn't already exist in the List
                        {
                            Console.Clear();
                            Console.WriteLine($"The customer {newCustomer} already exists.");
                        }
                        else
                        {
                            Console.Clear();
                            customerNames.Add(newCustomer);
                            Console.WriteLine($"{newCustomer} was added successfully!");
                        }
                        break;
                    case 2: //Code to add a new book to the magicBooks Dictionary along with their specified category
                        string newBook;
                        string category;

                        Console.Clear();
                        Console.WriteLine("What is the new Book's name?");
                        newBook = Console.ReadLine();
                        if (magicBooks.ContainsKey(newBook))
                        {
                            Console.Clear();
                            Console.WriteLine($"The book {newBook} is already in the library.");
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine($"What category does {newBook} fall under?");
                            category = Console.ReadLine();
                            if (category.ToLower() == "spell tomes" || category.ToLower() == "enchanted scrolls" || category.ToLower() == "magical novels")
                            {
                                magicBooks.Add(newBook, category);  //Book name = TKey, category = TValue
                                Console.WriteLine($"{newBook} was added successfully under catergory: {category}!");
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine($"{category} is not a valid book category.\nPlease try again.");
                            }
                        }
                        break;
                    default:
                        break;
                }
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
