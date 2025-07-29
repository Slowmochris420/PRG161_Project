using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG161_Project
{
    internal class Program
    {

        enum Choices
        {
            Add_customer = 1,
            Add_book,
            Checkout,
            Exit
        }

        public static Dictionary<string, string> magicBooks = new Dictionary<string, string>();

        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;   //Set default font color to white

            //Fixed Prices of book categories
            const int spellTomes = 40;
            const int enchantedScrolls = 25;
            const int magicalNovels = 25;

            //Lists to store customer names and book names
            Dictionary<string, int> customerNames = new Dictionary<string, int>();  //Stores the names of the customers and their date if register
            List<string> cart = new List<string>();   //stores which ever book the user enters during checkout
            
            /*
            Dictionary magicBooks
            The TKey represents the name of the book
            while the TValue represents the category of the TKey(Book name)
            */

            

            while (true) //Program Loops back to the Menu
            {
                int option;

                Console.WriteLine(@"
 What would you like to do?
 Enter the number to the corresponding option:
 ============================================
");
                int counter = 1;

                foreach (string item in Enum.GetNames(typeof(Choices)))
                {
                    Console.WriteLine($"{counter}. {item}");
                    counter++;
                }

                option = int.Parse(Console.ReadLine());

                switch (option)
                {
                    case 1: //Code to add a new custommer to the customerNames List
                        Console.Clear();
                        string newCustomer;
                        Console.WriteLine("What is the Customer's name?");
                        newCustomer = Console.ReadLine();

                        if (customerNames.ContainsKey(newCustomer)) //Check if the customer entered doesn't already exist in the List
                        {
                            Console.Clear();
                            Console.WriteLine($"The customer {newCustomer} already exists.");
                        }
                        else
                        {
                            int date = DateTime.Now.Year; //Saves date when new customer was entered

                            Console.Clear();
                            customerNames.Add(newCustomer, date);
                            Console.ForegroundColor = ConsoleColor.Green;   //Change success message to green font
                            Console.WriteLine($"{newCustomer} was added successfully!");
                            Console.ForegroundColor = ConsoleColor.White;
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
                                magicBooks.Add(newBook, category.ToLower());  //Book name = TKey, category = TValue
                                Console.WriteLine($"{newBook} was added successfully under catergory: {category}!");
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine($"{category} is not a valid book category.\nPlease try again.");
                            }
                        }
                        break;
                    case 3: //Check out
                        bool isLoyal = false; //To check if the customer is in the customerNames list
                        Console.WriteLine("Checkout for:");
                        string custName = Console.ReadLine();

                        int registerYear = 0;

                        foreach (KeyValuePair<string, int>  item in customerNames)
                        {
                            if (custName == item.Key)   //Are they on the loyalty system? (Are they found in the customerNames dictionary)
                            {
                                registerYear = item.Value;
                                isLoyal = true;
                                break; //Break out of the loop when customer is found
                            }
                        }

                        string bookChosen;
                        int amountBeforeDiscount = 0;
                        double discountPercentage = 0;

                        do
                        {
                            Console.WriteLine("Enter the book names one at a time to be rented:");
                            Console.WriteLine("Enter 0 to stop adding books to cart");
                            bookChosen = Console.ReadLine();

                            if (bookChosen != "0") //Decides if the program will skip the add to cart  section
                            {
                                int checkNum = Total(bookChosen); //checkNum stores the value returned before its added to the total, if it returns -1
                                                                  //it means the book name entered wasn't found int the magic book dictionary and
                                                                  //therefore won't be added to the cart

                                if (checkNum != -1) //if the book is in the magic book dictionary
                                {
                                    amountBeforeDiscount += checkNum;
                                    cart.Add(bookChosen);
                                }
                                else
                                {
                                    Console.WriteLine("Invalid book name");
                                }
                            }
                        } while (bookChosen != "0"); //Stops until the user enters a 0

                        if (isLoyal) //Is on the system (i.e. is on the customerNames list)
                        {
                            //Owethu's code
                            int yearsRegistered = DateTime.Now.Year - registerYear; //Subtract the present year by the year the customer registered the
                                                                                    //get the period the customer was registered
                            discountPercentage = CalcDiscount(yearsRegistered);

                            //Determine the number of Reward rentals the customer gets
                            Console.WriteLine("Enter the customer's Number of Rentals:"); 
                            int numRentals = int.Parse(Console.ReadLine());

                            int rewardRentals = Coupons(numRentals);    //call Coupons function

                            //Warick's code
                            

                            string reward = MagicalBonus(yearsRegistered, numRentals);

                            break;
                        }
                        else  //Is not on the system (i.e. is not on the customerNames list)
                        {
                            //Do not ask for coupons / Bonuses etc.
                            //Just Ask which books to check out.
                        }

                        Console.WriteLine(amountBeforeDiscount);
                        break;
                    case 4: //Close the program
                        Console.Clear();
                        Console.BackgroundColor = ConsoleColor.DarkMagenta;
                        Console.WriteLine(" --- Have a nice day :) --- ");
                        Console.BackgroundColor = ConsoleColor.Black;
                        Environment.Exit(0);
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("That is not an avalible option.");
                        break;
                    }
            }
            



        }

        //Function to determine the number of free rentals the customer gets(Coupons)
        public static int Coupons(int numRentals)
        {
            if (numRentals >= 10 && numRentals <= 24)
            {
                return 1;
            }
            else if (numRentals > 24 && numRentals <= 49)
            {
                return 2;
            }
            else if (numRentals > 49 && numRentals <= 74)
            {
                return 4;
            }
            else
            {
                return 8;
            }

            
        }

        //Function to determine what magical bonus the customer gets(Magical bonus)
        public static string MagicalBonus(int yearsRegistered,int numOfRentals)
        {
            string reward = "";
            if (yearsRegistered > 5 && numOfRentals < 25)
            {
                reward = "You don’t qualify for magical bonus.";
            }
            else if ((yearsRegistered >= 5 && yearsRegistered < 10) && (numOfRentals >= 25 && numOfRentals < 50))
            {
                reward = "You qualify for 1 bronze tier book!";
            }
            else if ((yearsRegistered >= 10 && yearsRegistered < 15) && (numOfRentals >= 50 && numOfRentals < 75))
            {
                reward = "You qualify for 3 bronze and 1 silver tier books!";
            }
            else if (yearsRegistered >= 15 && numOfRentals >= 75)
            {
                reward = "You qualify for 5 bronze, 2 silver and 1 gold tier books!";
            }

            return reward;
        }

        //Function that returns the discount percentage according to the period the customer has registered
        public static double CalcDiscount(int yearsRegistered)
        {
            double discountPercentage = 0.0;

            if (yearsRegistered == 4)
            {
                discountPercentage = 0.05;
            }
            else if (yearsRegistered >=5 && yearsRegistered <= 9)
            {
                discountPercentage = 0.1;
            }
            else if (yearsRegistered >= 10 && yearsRegistered <= 14)
            {
                discountPercentage = 0.2;
            }
            else if (yearsRegistered >= 15)
            {
                discountPercentage = 0.35;
            }

            return discountPercentage;
        }

        //Function to identify price based on the book catergory
        public static int Total(string nameOfBook)
        {
            int price = 0;

            foreach (KeyValuePair<string, string> item in magicBooks)
            {
                if (nameOfBook == item.Key)   //Is the book in the magicBook dictionary?
                {
                    if (item.Value == "spell tomes")   //Check wat the category is of that book to determine the price of it
                    {
                        price = 40;
                    }
                    else if (item.Value == "enchanted scrolls")
                    {
                        price = 25;
                    }
                    else if (item.Value == "magic novels")
                    {
                        price = 25;
                    }
                }
                else
                {
                    price = -1;
                }
            }

            

            return price;
        }
    }
}
