// *********************************************************************
// Application:		The_Expert_System
// Author: 			Hoezee, Joseph D
// Description:		This program is based on suggestion B of the
//                  mission 2 instructions (Cost of Buying Two Items)
//
// Date Created: 1/18/2020
// *********************************************************************
using System;

namespace The_Expert_System
{
    class MainClass
    {
        public void openingScreen()
        {
            //
            // Set text color
            // My terminal theme overrides this, so it is
            // unknown to me weather-or-not this is working.
            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.Write("\n" +
                          "This program takes the price of\n" +
                          "Hydrogen Fuel, Liquid Oxygen\n" +
                          "from the game: Elite: Dangerous." +
                          "\n");
            Console.WriteLine("Continue (Y):\n");

            while (Console.ReadKey().Key != ConsoleKey.Y)
            {}
            Console.Clear();
        }

        //
        // Get quantity of hydrogen, oxygen from the user, then return said
        // values.
        public (float quantityHydrogen, float liquidOxygen) askUserForQuantity()
        {
            float x = 0;
            float y = 0;
            try
            {
                Console.WriteLine("Amount of hydrogen>\n");
                x = Convert.ToSingle(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine($"Input: is invalid.\n");
                System.Environment.Exit(1);
            }
            try
            {
                Console.WriteLine("Amount of oxygen>\n");
                y = Convert.ToSingle(Console.ReadLine());
                return (x, y);
            }
            catch
            {
                Console.WriteLine($"Input: is invalid.\n");
                System.Environment.Exit(1);
            }
            //
            // return otherwise we get a "not all code paths return a value" error.
            return (1, 1);
        }

        //
        // Calculate the cost of hydrogen and oxygen.
        public (float costHydrogen, float costOxygen) calcCost(float x, float y, float a, float b)
        {
            return (x * a,y * b);
        }

        public static void Main(string[] args)
        {
            //
            // Init
            MainClass MC = new MainClass();

            //
            // Define variables
            float total;
            string date;
            int hydrogenFuel = 0;
            int liquidOxygen = 0;

            //
            // Call the welcome screen
            MC.openingScreen();

            Console.Write("\n" +
                          "What station should we use?\n" +
                          "(effects the base cost)\n" +
                          "[1] Galileo\n" + 
                          "[2] Mars High" +
                          "\n");

            string station = Console.ReadLine();

            //
            // Ask user what station, date they want the base values from.
            if(station == "1")
            {
                Console.Write("\n" +
                              "From what date?\n" +
                              "[1] Jan 15th, 2020\n" +
                              "[2] Jan 22nd, 2020" +
                              "\n");

                date = Console.ReadLine();

                if(date == "1")
                {
                    hydrogenFuel = 102;
                    liquidOxygen = 165;
                }
                else if (date == "2")
                {
                    hydrogenFuel = 123;
                    liquidOxygen = 172;
                }
                else
                {
                    Console.Write("Not a valid date\n");
                    System.Environment.Exit(1);
                }
            }
            else if (station == "2")
            {
                Console.Write("\n" +
                              "From what date?\n" +
                              "[1] Jan 15th, 2020\n" +
                              "[2] Jan 22nd, 2020" +
                              "\n");

                date = Console.ReadLine();

                if (date == "1")
                {
                    hydrogenFuel = 90;
                    liquidOxygen = 172;
                }
                else if (date == "2")
                {
                    hydrogenFuel = 88;
                    liquidOxygen = 174;
                }
                else
                {
                    Console.Write("Not a valid date\n");
                    System.Environment.Exit(1);
                }
            }
            else
            {
                Console.Write("Not a valid starport\n");
                System.Environment.Exit(1);
            }

            var (quantityHydrogen, quantityOxygen) = MC.askUserForQuantity();
            var (costHydrogen, costOxygen) = MC.calcCost(hydrogenFuel, liquidOxygen, quantityHydrogen, quantityOxygen);

            //
            // Calculate combined total of hydrogen and oxygen.
            total = costHydrogen + costOxygen;

            //
            // Limit the number of decimals for the total amount.
            string totalIncludeDecimals = String.Format("{0:0.##}", total);

            Console.WriteLine("==========TOTALS==========");
            Console.WriteLine($"Hydrogen: \t{costHydrogen}CR for {quantityHydrogen} tons\nOxygen: \t{costOxygen}CR for {quantityOxygen} tons\nTotal Cost: \t{totalIncludeDecimals}CR");
        }
    }
}
