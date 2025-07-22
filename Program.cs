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

            //Arrays to store customer names and book names
            string[] customerNames = new string[10];
            string[] books = new string[10];

            string numCoupon = Coupons(3);


            
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
