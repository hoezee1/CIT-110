// *********************************************************************
// Application:		Program
// Author: 			Hoezee, Joseph D
// Description:		
//
// Date Created: 1/14/2020
// *********************************************************************
using System;
using System.Text.RegularExpressions;

namespace The_Conversation
{
    class MainClass
    {
        //
        // Wait for user input, then clear the screen at the end of each question.
        //
        public void waitForInput()
        {
            Console.Write("<Press ANY KEY to continue to next question>\n");
            Console.ReadKey();
            Console.Clear();
        }

        //
        // Check if passed argument (pertaining to a variable) is empty. If so, terminate.
        //
        public void checkIFEMPTY(string var)
        {
            if (string.IsNullOrEmpty(var))
            {
                Console.Write("Blank input not accepted\n");
                System.Environment.Exit(1);
            }
        }

        //
        // Main program
        //
        public static void Main(string[] args)
        {
            //
            // Define variables
            //
            string staticName = "John";
            string username;
            string realName;
            string input;
            int userAge;
            string favoriteColor;
            // Create object of class MainClass
            MainClass MC = new MainClass();

            //
            // Setup console
            //
            Console.CursorVisible = false;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            //Console.WindowWidth = 80;
            //Console.WindowHeight = 20;
            //Console.SetBufferSize(80, 20);
            Console.Clear();

            //
            // Ask for username
            //
            Console.Write("Enter your username:\n>");
            username = Console.ReadLine();
            MC.checkIFEMPTY(username);

            //
            // Handle username
            //
            if (username == "John")
            {
                Console.WriteLine($"Hello {staticName}\n");
            }
            else
            {
                Console.WriteLine($"Hello {username}\n");
            }
            MC.waitForInput();

            //
            // Ask for realname
            //
            Console.Write("Enter your real name:\n>");
            realName = Console.ReadLine();
            MC.checkIFEMPTY(realName);
            Regex pRealName = new Regex(@"\d+");
            Match matchRealName = pRealName.Match(realName);
            if (matchRealName.Success)
            {
                Console.WriteLine("A name cannot contain an integer.\n");
                System.Environment.Exit(1);
            }
            else
            {
                Console.WriteLine($"Your real name is {realName}\n");
            }
            MC.waitForInput();

            //
            // Ask for the user's age
            //
            Console.Write("What is your age?\n>");
            input = Console.ReadLine();
            MC.checkIFEMPTY(input);
            Regex pUserAge = new Regex(@"\D");
            Match matchAge = pUserAge.Match(input);
            if (matchAge.Success)
            {
                Console.WriteLine("Non-integer characters are not permitted in the age field.\n");
                System.Environment.Exit(1);
            }
            else
            {
                userAge = int.Parse(input);
                Console.WriteLine($"Your age is {userAge}\n");
            }
            MC.waitForInput();

            //
            // Ask user for their favorite color.
            //
            Console.Write("What is your favorite color?:\n>");
            favoriteColor = Console.ReadLine();
            MC.checkIFEMPTY(favoriteColor);
            Console.WriteLine($"Hmm... {favoriteColor}, mine too!");

            //
            // Print finished message
            //
            Console.WriteLine("\n<Press ANY KEY to terminate>");
            Console.ReadKey();
            Console.Beep();
            System.Environment.Exit(1);
        }
    }
}