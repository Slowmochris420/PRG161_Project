using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

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


        //Global variables-----------------------------------------------------------------------
        public static Dictionary<string, string> magicBooks = new Dictionary<string, string>(); //stores the book name and it's category
        public static Dictionary<string, int> cart = new Dictionary<string, int>();   //stores which ever book the user enters during checkout whith thier prices
        public static int amountBeforeDiscount = 0; //Total price for all books in cart list (Used in the method AddToCart)
        //---------------------------------------------------------------------------------------

        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;   //Set default font color to white

            //Stores the names of the customers and their year if register
            Dictionary<string, int> customerNames = new Dictionary<string, int>();

            //Added Names to customerNames dictionary
            customerNames.Add("Margaret Ellison", 1957);
            customerNames.Add("Darius Mitchell", 1983);
            customerNames.Add("Leila Romero", 2001);
            customerNames.Add("Connor Blakewood", 2016);
            customerNames.Add("Ayana Chen", 2023);

            //Added Names to magicBooks dictionary
            magicBooks.Add("The Ember Codex", "spell tomes");
            magicBooks.Add("Scroll of Whispers", "enchanted scrolls");
            magicBooks.Add("Chronicles of Arcanum", "magic novels");
            magicBooks.Add("Mystic Grimoire", "spell tomes");
            magicBooks.Add("Tales of the Aether", "magic novel");

            while (true) //Program Loops back to the Menu
            {
                int option;

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(@"
 What would you like to do?
 Enter the number to the corresponding option:
 ============================================");
                
                int counter = 1;

                foreach (string item in Enum.GetNames(typeof(Choices)))
                {
                    Console.WriteLine($" {counter}. {item}");
                    counter++;
                }

                Console.WriteLine(" ============================================");

                Console.ForegroundColor = ConsoleColor.White;

                option = int.Parse(Console.ReadLine());

                switch (option)
                {
                    case 1: //Code to add a new custommer to the customerNames List
                        Console.Clear();
                        string newCustomer;
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("What is the Customer's name?");
                        Console.ForegroundColor = ConsoleColor.White;
                        newCustomer = Console.ReadLine();

                        if (customerNames.ContainsKey(newCustomer)) //Check if the customer entered doesn't already exist in the List
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"The customer {newCustomer} already exists.");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        else
                        {
                            int date = DateTime.Now.Year; //Saves date when new customer was entered

                            Console.Clear();
                            customerNames.Add(newCustomer, date);
                            Console.ForegroundColor = ConsoleColor.Green;   //Change success message to green font
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"{newCustomer} was added successfully!");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        break;
                    case 2: //Code to add a new book to the magicBooks Dictionary along with their specified category
                        string newBook;
                        string category;

                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("What is the new Book's name?");
                        Console.ForegroundColor = ConsoleColor.White;
                        newBook = Console.ReadLine();
                        if (magicBooks.ContainsKey(newBook))    //Is the book already in the magic books dictionary?
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"The book {newBook} is already in the library.");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        else //if the book isn't in the magic books dictionary
                        {
                            Console.Clear();

                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine($"What category does {newBook} fall under?");
                            Console.ForegroundColor = ConsoleColor.White;
                            category = Console.ReadLine();

                            if (category.ToLower() == "spell tomes" || category.ToLower() == "enchanted scrolls" || category.ToLower() == "magical novels")
                            {
                                magicBooks.Add(newBook, category.ToLower());  //Book name = TKey, category = TValue
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine($"{newBook} was added successfully under catergory: {category}!");
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                            else
                            {
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine($"{category} is not a valid book category.\nPlease try again.");
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                        }
                        break;
                    case 3: //Check out
                        bool isLoyal = false; //To check if the customer is in the customerNames list
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Checkout for:");
                        Console.ForegroundColor = ConsoleColor.White;
                        string custName = Console.ReadLine();

                        int registerYear = 0;

                        foreach (KeyValuePair<string, int>  item in customerNames)
                        {
                            if (custName == item.Key)   //Are they on the loyalty system? (Are they found in the customerNames dictionary)
                            {
                                registerYear = item.Value;  //Storing the year the customer registered to
                                                            //help calculate how long they've been registered
                                break; //Break out of the loop when customer is found
                            }
                        }

                        
                        double discountPercentage = 0;
                        int rewardRentals = 0;
                        string bonus = "";

                        AddToCart(); //Keeps asking what book the customer wants to rent and adds the price of each book to totalBeforeDiscount
                            
                        int yearsRegistered = DateTime.Now.Year - registerYear; //Subtract the present year by the year the customer registered the
                                                                                //get the period the customer was registered
                        discountPercentage = CalcDiscount(yearsRegistered); //call Discount function

                        Console.Clear();

                        //Determine the number of Reward rentals the customer gets
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Enter the customer's Number of Rentals:");
                        Console.ForegroundColor = ConsoleColor.White;
                        int numRentals = int.Parse(Console.ReadLine());

                        rewardRentals = Coupons(numRentals);    //call Coupons function

                        bonus = MagicalBonus(yearsRegistered, numRentals);  //Call magical bonus function
                        

                        //Display the customer's reciept

                        Console.Clear();
                        double amountAfterDiscount = amountBeforeDiscount - (amountBeforeDiscount * discountPercentage);
                        CultureInfo currencyRands = new CultureInfo("en-ZA");

                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("Magic books library");
                        Console.WriteLine("=====================================");
                        Console.ForegroundColor = ConsoleColor.White;
                        foreach (KeyValuePair<string, int> item in cart)    //Display all the book names and prices from the cart
                        {
                            Console.WriteLine($"{item.Key}          \t{item.Value.ToString("C", currencyRands),5}");
                        }
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("-------------------------------------");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine($"Before discount         \t{amountBeforeDiscount.ToString("C", currencyRands),5}");
                        Console.WriteLine($"After discount          \t{amountAfterDiscount.ToString("C", currencyRands),5}");
                        Console.WriteLine($"Free rentals coupon     \t{rewardRentals.ToString("C", currencyRands),5}");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("-------------------------------------");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine($"Magical Bonus: \n{bonus}");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("=====================================");
                        Console.ForegroundColor = ConsoleColor.White;

                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine("\npress any key to exit to menu....");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.ReadKey();

                        cart.Clear();   //Clear the cart once the reciept it complete

                        Console.Clear();

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

        //==========================================================================================================

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
                //else
                //{
                //    price = -1;
                //}
            }

            

            return price;
        }


        //Method that Keeps asking what book the customer wants to rent and adds the price of each book to totalBeforeDiscount
        public static void AddToCart()
        {

            string bookChosen = "";
            do
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Enter the book names one at a time to be rented:");
                Console.WriteLine("Enter 0 to stop adding books to cart");
                Console.ForegroundColor = ConsoleColor.White;
                bookChosen = Console.ReadLine();

                if (bookChosen != "0") //Decides if the program will skip the add to cart  section
                {
                    int checkNum = Total(bookChosen); //checkNum stores the value returned before its added to the total, if it returns -1
                                                      //it means the book name entered wasn't found int the magic book dictionary and
                                                      //therefore won't be added to the cart

                    if (checkNum != -1) //if the book is in the magic book dictionary
                    {
                        amountBeforeDiscount += checkNum;
                        cart.Add(bookChosen, checkNum);
                    }
                    else
                    {
                        Console.WriteLine("Invalid book name");
                    }
                }
            } while (bookChosen != "0"); //Stops until the user enters a 0
        }

    }
}
