// *********************************************************************
// Application:     testingFileIO
// Author:          Hoezee, Joseph D
// Description:     
//
// Date Created: 4/3/2020
// *********************************************************************
using System;
using System.Collections.Generic;
using System.IO;
using FinchAPI;

namespace user_programming
{
    public class testingFileIO
    {
        static Finch robot = new Finch();

        static void Main(string[] args)
        {
            authentication();

            string menuChoice;
            bool finished = false;

            // Terminal Colors
            string foregroundColor = "";
            string backgroundColor = "";

            // Finch Data
            string temperatureData = "";
            string lightData = "";

            do
            {
                DisplayHeader("Main Menu");

                Console.WriteLine("1. Connect to Finch Robot");
                Console.WriteLine("2. Configure Program");
                Console.WriteLine("3. Get Finch Data");
                Console.WriteLine("4. Save Data");
                Console.WriteLine("5. Load Data");
                Console.WriteLine("6. Display Data");
                Console.WriteLine("7. User Programming");
                Console.WriteLine("8. Quit");
                Console.Write("\nChoose Option:\n>");
                menuChoice = Console.ReadLine();

                switch (menuChoice)
                {
                    case "1":
                        DisplayConnectToRobot();
                        break;

                    case "2":
                        (foregroundColor, backgroundColor) = DisplayConfigureProgram();
                        break;

                    case "3":
                        (temperatureData, lightData) = DisplayGetFinchData(robot);
                        break;

                    case "4":
                        DisplaySaveDataSet(temperatureData, lightData, foregroundColor, backgroundColor);
                        break;

                    case "5":
                        (temperatureData, lightData, foregroundColor, backgroundColor) = DisplayLoadDataSet();
                        break;

                    case "6":
                        DisplayDataSet(temperatureData, lightData, foregroundColor, backgroundColor);
                        break;

                    case "7":
                        DisplayUserProgramming(robot);
                        break;

                    case "8":
                        finished = true;
                        break;

                    default:
                        break;
                }

            } while (!finished);
        }

        /// <summary>
        /// Connect to finch robot
        /// </summary>
        static void DisplayConnectToRobot()
        {
            robot.connect();
            DisplayContinuePrompt();
        }

        /// <summary>
        /// Configure the program
        /// </summary>
        static (string foregroundColor, string backgroundColor) DisplayConfigureProgram()
        {
            string foregroundColor = "";
            string backgroundColor = "";

            string colorLower = "";

            DisplayHeader("Configure Terminal Colors Program");

            Console.WriteLine("Enter the background and foreground colors (space delimited. Ex: ConsoleColor.White ConsoleColor.Blue)>\n");

            int counter = 0;
            foreach (string color in Console.ReadLine().Split())
            {
                colorLower = color.ToLower();
                if (counter == 0)
                {
                    foregroundColor = colorLower;
                }
                else if (counter == 1)
                {
                    backgroundColor = colorLower;
                }
                counter++;
            }

            DisplayContinuePrompt();

            return (foregroundColor, backgroundColor);
        }

        /// <summary>
        /// Get temp data from finch.
        /// </summary>
        static (string temperatureData, string lightData) DisplayGetFinchData(Finch robot)
        {
            string temperatureData = Convert.ToString(robot.getTemperature());
            string lightData = Convert.ToString(robot.getLightSensors());

            DisplayContinuePrompt();

            return (temperatureData, lightData);
        }

        /// <summary>
        /// Save color, light, and temperature data into file.
        /// </summary>
        static void DisplaySaveDataSet(string temperatureData, string lightData, string foregroundColor, string backgroundColor)
        {
            string[] lines = { temperatureData, lightData, foregroundColor, backgroundColor };

            try
            {
                File.Exists(@"./theme/saved_data.cfg");
            }
            // Prevent a file-not-found exception from being fatal.
            catch (FileNotFoundException){}

            // If any of the variables that are passed into this function are empty, exit the function.
            if (string.IsNullOrEmpty(temperatureData))
            {
                Console.WriteLine("No temperature data was set. Check the Finch and run option 3 again.");
                DisplayContinuePrompt();
                return;
            }
            else if (string.IsNullOrEmpty(lightData))
            {
                Console.WriteLine("No light data was set. Check the Finch and run option 3 again.");
                DisplayContinuePrompt();
                return;
            }
            else if (string.IsNullOrEmpty(foregroundColor)) {
                Console.WriteLine("No foreground color was set. You need to run option 2 before running this one.");
                DisplayContinuePrompt();
                return;
            }
            else if (string.IsNullOrEmpty(backgroundColor))
            {
                Console.WriteLine("No background color was set. You need to run option 2 before running this one.");
                DisplayContinuePrompt();
                return;
            }
            else
            {
                System.IO.File.WriteAllLines(@"./theme/saved_data.cfg", lines);
            }
            DisplayContinuePrompt();
        }

        static (string temperatureData, string lightData, string foregroundColor, string backgroundColor) DisplayLoadDataSet()
        {
            string[] lines = System.IO.File.ReadAllLines(@"./theme/saved_data.cfg");

            return (lines[0], lines[1], lines[2], lines[3]);
        }

        static void DisplayDataSet(string temperatureData, string lightData, string foregroundColor, string backgroundColor)
        {
            Console.WriteLine($"Temperature: {temperatureData}");
            Console.WriteLine($"Light Data: {lightData}");
            Console.WriteLine($"Foreground color: {foregroundColor}");
            Console.WriteLine($"Background color: {backgroundColor}");

            // I got lazy, I'm sorry.
            if (foregroundColor == "black")
            {
                Console.ForegroundColor = ConsoleColor.Black;
            }
            else if (foregroundColor == "darkblue")
            {
                Console.ForegroundColor = ConsoleColor.DarkBlue;
            }
            else if (foregroundColor == "darkgreen")
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
            }
            else if (foregroundColor == "darkcyan")
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
            }
            else if (foregroundColor == "darkred")
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
            }
            else if (foregroundColor == "darkmagenta")
            {
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
            }
            else if (foregroundColor == "darkyellow")
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
            }
            else if (foregroundColor == "gray")
            {
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            else if (foregroundColor == "darkgray")
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
            }
            else if (foregroundColor == "blue")
            {
                Console.ForegroundColor = ConsoleColor.Blue;
            }
            else if (foregroundColor == "green")
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            else if (foregroundColor == "cyan")
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
            }
            else if (foregroundColor == "red")
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            else if (foregroundColor == "magenta")
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
            }
            else if (foregroundColor == "yellow")
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            else if (foregroundColor == "white")
            {
                Console.ForegroundColor = ConsoleColor.White;
            }
            else if (foregroundColor == "black")
            {
                Console.ForegroundColor = ConsoleColor.Black;
            }
            else if (foregroundColor == "darkblue")
            {
                Console.ForegroundColor = ConsoleColor.DarkBlue;
            }
            else if (foregroundColor == "darkgreen")
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
            }
            else if (foregroundColor == "darkcyan")
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
            }
            else if (foregroundColor == "darkred")
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
            }
            else if (foregroundColor == "darkmagenta")
            {
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
            }
            else if (foregroundColor == "darkyellow")
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
            }
            else if (foregroundColor == "gray")
            {
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            else if (foregroundColor == "darkgray")
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
            }
            else if (foregroundColor == "blue")
            {
                Console.ForegroundColor = ConsoleColor.Blue;
            }
            else if (foregroundColor == "green")
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            else if (foregroundColor == "cyan")
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
            }
            else if (foregroundColor == "red")
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            else if (foregroundColor == "magenta")
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
            }
            else if (foregroundColor == "yellow")
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            else if (foregroundColor == "white")
            {
                Console.ForegroundColor = ConsoleColor.White;
            }

            if (backgroundColor == "black")
            {
                Console.BackgroundColor = ConsoleColor.Black;
            }
            else if (backgroundColor == "darkblue")
            {
                Console.BackgroundColor = ConsoleColor.DarkBlue;
            }
            else if (backgroundColor == "darkgreen")
            {
                Console.BackgroundColor = ConsoleColor.DarkGreen;
            }
            else if (backgroundColor == "darkcyan")
            {
                Console.BackgroundColor = ConsoleColor.DarkCyan;
            }
            else if (backgroundColor == "darkred")
            {
                Console.BackgroundColor = ConsoleColor.DarkRed;
            }
            else if (backgroundColor == "darkmagenta")
            {
                Console.BackgroundColor = ConsoleColor.DarkMagenta;
            }
            else if (backgroundColor == "darkyellow")
            {
                Console.BackgroundColor = ConsoleColor.DarkYellow;
            }
            else if (backgroundColor == "gray")
            {
                Console.BackgroundColor = ConsoleColor.Gray;
            }
            else if (backgroundColor == "darkgray")
            {
                Console.BackgroundColor = ConsoleColor.DarkGray;
            }
            else if (backgroundColor == "blue")
            {
                Console.BackgroundColor = ConsoleColor.Blue;
            }
            else if (backgroundColor == "green")
            {
                Console.BackgroundColor = ConsoleColor.Green;
            }
            else if (backgroundColor == "cyan")
            {
                Console.BackgroundColor = ConsoleColor.Cyan;
            }
            else if (backgroundColor == "red")
            {
                Console.BackgroundColor = ConsoleColor.Red;
            }
            else if (backgroundColor == "magenta")
            {
                Console.BackgroundColor = ConsoleColor.Magenta;
            }
            else if (backgroundColor == "yellow")
            {
                Console.BackgroundColor = ConsoleColor.Yellow;
            }
            else if (backgroundColor == "white")
            {
                Console.BackgroundColor = ConsoleColor.White;
            }
            else if (backgroundColor == "black")
            {
                Console.BackgroundColor = ConsoleColor.Black;
            }
            else if (backgroundColor == "darkblue")
            {
                Console.BackgroundColor = ConsoleColor.DarkBlue;
            }
            else if (backgroundColor == "darkgreen")
            {
                Console.BackgroundColor = ConsoleColor.DarkGreen;
            }
            else if (backgroundColor == "darkcyan")
            {
                Console.BackgroundColor = ConsoleColor.DarkCyan;
            }
            else if (backgroundColor == "darkred")
            {
                Console.BackgroundColor = ConsoleColor.DarkRed;
            }
            else if (backgroundColor == "darkmagenta")
            {
                Console.BackgroundColor = ConsoleColor.DarkMagenta;
            }
            else if (backgroundColor == "darkyellow")
            {
                Console.BackgroundColor = ConsoleColor.DarkYellow;
            }
            else if (backgroundColor == "gray")
            {
                Console.BackgroundColor = ConsoleColor.Gray;
            }
            else if (backgroundColor == "darkgray")
            {
                Console.BackgroundColor = ConsoleColor.DarkGray;
            }
            else if (backgroundColor == "blue")
            {
                Console.BackgroundColor = ConsoleColor.Blue;
            }
            else if (backgroundColor == "green")
            {
                Console.BackgroundColor = ConsoleColor.Green;
            }
            else if (backgroundColor == "cyan")
            {
                Console.BackgroundColor = ConsoleColor.Cyan;
            }
            else if (backgroundColor == "red")
            {
                Console.BackgroundColor = ConsoleColor.Red;
            }
            else if (backgroundColor == "magenta")
            {
                Console.BackgroundColor = ConsoleColor.Magenta;
            }
            else if (backgroundColor == "yellow")
            {
                Console.BackgroundColor = ConsoleColor.Yellow;
            }
            else if (backgroundColor == "white")
            {
                Console.BackgroundColor = ConsoleColor.White;
            }
        }

        /// <summary>
        /// Authentication parent function.
        /// </summary>
        static void authentication()
        {
            string[] username = { };
            string[] passphrase = { };

            string menuChoice;
            bool finished = false;

            do
            {
                DisplayHeader("Main Menu");

                Console.WriteLine("1. Set username and password");
                Console.WriteLine("2. Authenticate");
                Console.Write("\nChoose Option:\n>");
                menuChoice = Console.ReadLine();

                switch (menuChoice)
                {
                    case "1":
                        DisplaySetUsernamePassword();
                        break;
                    case "2":
                        finished = DisplayAuthenticate();
                        break;
                    default:
                        break;
                }
            } while (!finished);
        }

        /// <summary>
        /// Set username and password
        /// </summary>
        static void DisplaySetUsernamePassword()
        {
            try
            {
                File.Exists(@"./theme/auth.cfg");
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine("[INFO] The ./theme directory does not exist. Creating it.");
                Directory.CreateDirectory(@"./theme");
            }
            catch (FileNotFoundException){ }

            Console.WriteLine("Enter username and password (separated by spaces. Ex: John Password1)>");

            File.AppendAllText(@"./theme/auth.cfg",Console.ReadLine());
            File.AppendAllText(@"./theme/auth.cfg", "\n");

            DisplayContinuePrompt();
        }

        /// <summary>
        /// Authenticate the user and password
        /// </summary>
        static bool DisplayAuthenticate()
        {
            string InputUsername = "";
            string InputPassphrase = "";

            string lineuser = "";
            string linepassphrase = "";

            Console.WriteLine("Login>");
            Console.WriteLine("(EX: username passphrase)");

            int counter = 0;
            foreach (string input in Console.ReadLine().Split())
            {
                if (counter == 0)
                {
                    InputUsername = input;
                }

                if (counter == 1)
                {
                    InputPassphrase = input;
                }
                counter++;
            }

            counter = 0;
            foreach (string whole in File.ReadAllLines(@"./theme/auth.cfg"))
            {
                foreach (string split in whole.Split())
                {
                    if (counter == 0)
                    {
                        lineuser = split;
                    }

                    if (counter == 1)
                    {
                        linepassphrase = split;
                    }
                    counter++;
                }
                counter = 0;

                if (InputUsername == lineuser && InputPassphrase == linepassphrase)
                {
                    Console.WriteLine("Login Successful");
                    DisplayContinuePrompt();
                    return true;
                }
            }

            Console.WriteLine("Invalid username or passphrase");
            DisplayContinuePrompt();
            return false;
        }

        /// <summary>
        /// *****************************************************************
        /// *                User programming for Finch robot               *
        /// *****************************************************************
        /// </summary>
        /// <param name="finchRobot">finch robot object</param>
        /// <returns>Provides custom programming for the user</returns>
        static void DisplayUserProgramming(Finch finchRobot)
        {
            List<string> commands = new List<string>();

            int motorSpeed = 0;
            int ledBrightness = 0;
            double waitSeconds = 0;

            DisplayHeader("User Programming for Finch robot");

            string menuChoice;
            bool quitProgramming = false;
            do
            {
                DisplayHeader("User Programming");

                Console.WriteLine("\ta) Set Command Parameters");
                Console.WriteLine("\tb) Add Commands");
                Console.WriteLine("\tc) View Commands");
                Console.WriteLine("\td) Save Commands");
                Console.WriteLine("\te) Load Commands");
                Console.WriteLine("\tf) Execute Commands");
                Console.WriteLine("\tg) Return to Main Menu");
                menuChoice = Console.ReadLine().ToLower();

                switch (menuChoice)
                {
                    case "a":
                        (motorSpeed, ledBrightness, waitSeconds) = UserProgrammingDisplayGetCommandParameters();
                        break;
                    case "b":
                        UserProgrammingDisplayGetFinchCommands(commands);
                        break;
                    case "c":
                        DisplayFinchCommands(commands);
                        break;
                    case "d":
                        DisplaySaveFinchCommands(commands);
                        break;
                    case "e":
                        DisplayLoadFinchCommands(commands);
                        break;
                    case "f":
                        DisplayExecuteFinchCommands(finchRobot, commands, motorSpeed, ledBrightness, waitSeconds);
                        break;
                    case "g":
                        quitProgramming = true;
                        break;
                    default:
                        Console.WriteLine();
                        Console.WriteLine("\tPlease enter a letter for the menu choice.");
                        DisplayContinuePrompt();
                        break;
                }
            } while (!quitProgramming);
        }

        /// <summary>
        /// Set values to pass to the Finch.
        /// </summary>
        static (int motorSpeed, int ledBrightness, double waitSeconds) UserProgrammingDisplayGetCommandParameters()
        {
            int motorSpeed = 0;
            int ledBrightness = 0;
            double waitSeconds = 0;

            DisplayHeader("Set Command Parameters");

            Console.WriteLine("Set motor speed:\n");
            motorSpeed = Convert.ToInt16(Console.ReadLine());

            Console.WriteLine("Set LED brightness:\n");
            ledBrightness = Convert.ToInt16(Console.ReadLine());

            Console.WriteLine("Wait time (secs):\n");
            waitSeconds = Convert.ToDouble(Console.ReadLine());

            waitSeconds = waitSeconds * 1000;

            Console.WriteLine($"Motor speed: {motorSpeed}\n");
            Console.WriteLine($"LED brightness: {ledBrightness}\n");
            Console.WriteLine($"Wait time: {waitSeconds}\n");

            return (motorSpeed, ledBrightness, waitSeconds);
        }

        /// <summary>
        /// Add a set of user-choosen commands for the Finch to execute.
        /// </summary>
        static void UserProgrammingDisplayGetFinchCommands(List<String> commands)
        {
            DisplayHeader("Add Commands");
            var userChoice = "";

            Console.WriteLine("INSTRUCTIONS\n");
            Console.WriteLine("\tAdd any of the following commands:\n");
            Console.WriteLine("\tMOVEFORWARD\n");
            Console.WriteLine("\tMOVEBACKWARD\n");
            Console.WriteLine("\tSTOPMOTORS\n");
            Console.WriteLine("\tWAIT\n");
            Console.WriteLine("\tTURNRIGHT\n");
            Console.WriteLine("\tTURNLEFT\n");
            Console.WriteLine("\tLEDON\n");
            Console.WriteLine("\tLEDOFF\n");
            Console.WriteLine("\tGETTEMPERATURE\n");
            Console.WriteLine("To quit enter DONE\n");

            while (true)
            {
                userChoice = Console.ReadLine().ToUpper();

                if (userChoice == "DONE")
                {
                    break;
                }
                else
                {
                    commands.Add(userChoice);
                }
            }

            Console.WriteLine("The user entered the following commands:\n");

            foreach (var userCommand in commands)
            {
                Console.WriteLine($"Command: {userCommand}\n");
            }
            DisplayContinuePrompt();
        }

        /// <summary>
        /// Show the choosen user commands.
        /// </summary>
        static void DisplayFinchCommands(List<String> commands)
        {
            DisplayHeader("View Commands");
            foreach (var userCommand in commands)
            {
                Console.WriteLine($"Command: {userCommand}\n");
            }
            DisplayContinuePrompt();
        }

        /// <summary>
        /// Write the current commands to a file.
        /// </summary>
        static void DisplaySaveFinchCommands(List<String> commands)
        {
            DisplayHeader("Save Finch Commands");
            try
            {
                File.Exists(@"./theme/finchCommands.txt");
            }
            catch (FileNotFoundException) { }

            foreach (var userCommand in commands)
            {
                File.AppendAllText(@"./theme/saved_data.cfg", userCommand);
                File.AppendAllText(@"./theme/saved_data.cfg","\n");
            }
            DisplayContinuePrompt();
        }

        /// <summary>
        /// Load saved finch commands from file.
        /// </summary>
        static void DisplayLoadFinchCommands(List<String> commands)
        {
            DisplayHeader("Load Finch Commands");
            foreach (string line in File.ReadAllLines(@"./theme/saved_data.cfg"))
            {
                if (line != null)
                {
                    commands.Add(line);
                }
            }
            DisplayContinuePrompt();
        }

        /// <summary>
        /// Execute the choosen Finch commands.
        /// </summary>
        static void DisplayExecuteFinchCommands(Finch finchRobot, List<String> commands, int motorSpeed, int ledBrightness, double waitSeconds)
        {
            DisplayHeader("Execute Commands");
            Console.WriteLine("Will execute all specified commands");

            foreach (var userCommand in commands)
            {
                if (userCommand == "MOVEFORWARD")
                {
                    Console.WriteLine("Moving forwards\n");
                    finchRobot.setMotors(motorSpeed, motorSpeed);
                }
                else if (userCommand == "MOVEBACKWARD")
                {
                    Console.WriteLine("Moving backwards\n");
                    finchRobot.setMotors(motorSpeed - (motorSpeed * 2), motorSpeed - (motorSpeed * 2));
                }
                else if (userCommand == "STOPMOTORS")
                {
                    Console.WriteLine("Stopping\n");
                    finchRobot.setMotors(0, 0);
                }
                else if (userCommand == "WAIT")
                {
                    Console.WriteLine("Waiting\n");
                    finchRobot.wait(Convert.ToInt32(waitSeconds));
                }
                else if (userCommand == "TURNRIGHT")
                {
                    Console.WriteLine("Turning right\n");
                    finchRobot.setMotors(motorSpeed, 0);
                }
                else if (userCommand == "TURNLEFT")
                {
                    Console.WriteLine("Turning left\n");
                    finchRobot.setMotors(0, motorSpeed);
                }
                else if (userCommand == "LEDON")
                {
                    Console.WriteLine("Turning LED on\n");
                    finchRobot.setLED(ledBrightness, ledBrightness, ledBrightness);
                }
                else if (userCommand == "LEDOFF")
                {
                    Console.WriteLine("Turning LED off\n");
                    finchRobot.setLED(0, 0, 0);
                }
                else if (userCommand == "GETTEMPERATURE")
                {
                    Console.WriteLine("Getting temperature\n");
                    Console.WriteLine($"Temperature (C): {0}\n", finchRobot.getTemperature());
                }
            }
            DisplayContinuePrompt();
        }

        /// <summary>
        /// Display continue prompt
        /// </summary>
        static void DisplayContinuePrompt()
        {
            Console.CursorVisible = false;
            Console.WriteLine("\nPress any key to continue.");
            Console.ReadKey();
            Console.CursorVisible = true;
        }

        /// <summary>
        /// Display header
        /// </summary>
        static void DisplayHeader(string headerTitle)
        {
            Console.Clear();
            Console.WriteLine($"~~~{headerTitle}~~~\n");
        }
    }
}
