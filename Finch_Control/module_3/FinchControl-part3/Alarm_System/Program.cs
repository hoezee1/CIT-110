using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using FinchAPI;

namespace Project_FinchControl
{

    // **************************************************
    //
    // Title: Finch Control - Alarm System
    // Description: Trigger a set of beeps when light and temperature
    //              a specific threshold.
    // Application Type: Console
    // Author: Joseph Hoezee
    // Dated Created: 3/9/2020
    // Last Modified: 3/3/2020
    //
    // **************************************************

    class Program
    {
        /// <summary>
        /// first method run when the app starts up
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            SetTheme();

            DisplayWelcomeScreen();
            DisplayMenuScreen();
            DisplayClosingScreen();
        }

        /// <summary>
        /// setup the console theme
        /// </summary>
        static void SetTheme()
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.BackgroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// *****************************************************************
        /// *                     Main Menu                                 *
        /// *****************************************************************
        /// </summary>
        static void DisplayMenuScreen()
        {
            Console.CursorVisible = true;

            bool quitApplication = false;
            string menuChoice;

            Finch finchRobot = new Finch();

            do
            {
                DisplayScreenHeader("Main Menu");

                //
                // get user menu choice
                //
                Console.WriteLine("\ta) Connect Finch Robot");
                Console.WriteLine("\tb) Talent Show");
                Console.WriteLine("\tc) Data Recorder");
                Console.WriteLine("\td) Alarm System");
                Console.WriteLine("\te) User Programming");
                Console.WriteLine("\tf) Disconnect Finch Robot");
                Console.WriteLine("\tq) Quit");
                Console.Write("\t\tEnter Choice:");
                menuChoice = Console.ReadLine().ToLower();

                //
                // process user menu choice
                //
                switch (menuChoice)
                {
                    case "a":
                        DisplayConnectFinchRobot(finchRobot);
                        break;

                    case "b":
                        DisplayTalentShowMenuScreen(finchRobot);
                        break;

                    case "c":
                        DisplayDataRecorder(finchRobot);
                        break;

                    case "d":
                        DisplayAlarmSystem(finchRobot);
                        break;

                    case "e":
                        DisplayUserProgramming(finchRobot);
                        break;

                    case "f":
                        DisplayDisconnectFinchRobot(finchRobot);
                        break;

                    case "q":
                        DisplayDisconnectFinchRobot(finchRobot);
                        quitApplication = true;
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("\tPlease enter a letter for the menu choice.");
                        DisplayContinuePrompt();
                        break;
                }

            } while (!quitApplication);
        }

        #region ALARM-SYSTEM
        /// <summary>
        /// *****************************************************************
        /// *                     Talent Show Menu                          *
        /// *****************************************************************
        /// </summary>
        static void DisplayTalentShowMenuScreen(Finch finchRobot)
        {
            Console.CursorVisible = true;

            bool quitTalentShowMenu = false;
            string menuChoice;

            do
            {

                DisplayScreenHeader("Talent Show Menu");

                //
                // Warn the user this section is under development.
                //
                Console.WriteLine("Talent show is currently under development. Certain functionality can be faulty or missing.\n");
                DisplayContinuePrompt();

                //
                // get user menu choice
                //
                Console.WriteLine("\ta) Light and Sound");
                Console.WriteLine("\tb) Dance");
                Console.WriteLine("\tc) Mixing It Up");
                Console.WriteLine("\td) Light Sensors");
                Console.WriteLine("\tq) Main Menu");
                Console.Write("\t\tEnter Choice:");
                menuChoice = Console.ReadLine().ToLower();

                //
                // process user menu choice
                //
                switch (menuChoice)
                {
                    case "a":
                        Console.CursorVisible = false;

                        DisplayScreenHeader("Light and Sound");

                        Console.WriteLine("\tThe Finch robot will now show off its glowing talent!");
                        DisplayContinuePrompt();

                        Console.WriteLine("Choose a value from 0-255 for the starting light/sound value:");
                        int lightSoundValue = Convert.ToInt32(Console.ReadLine());

                        Parallel.Invoke(() =>
                        {
                            DisplayLight(finchRobot, lightSoundValue);
                        },
                                         () =>
                                         {
                                             DisplaySound(finchRobot, lightSoundValue);
                                         }
                        );

                        DisplayMenuPrompt("Talent Show Menu");
                        break;

                    case "b":
                        // DisplayDance
                        Console.CursorVisible = false;

                        DisplayScreenHeader("Dance");

                        Console.WriteLine("\tThe Finch robot will now dance!");
                        DisplayContinuePrompt();

                        // Sides
                        Console.WriteLine("What dance should the Finch perform?");
                        Console.WriteLine("[1] Square\n[2] Triangle\n[3] hexagon");

                        int danceType = Convert.ToInt32(Console.ReadLine());

                        // Distance
                        Console.WriteLine("How large (in inches) should the area of the dance be?");
                        int danceArea = Convert.ToInt32(Console.ReadLine());

                        // Time
                        Console.WriteLine("How much time (in seconds) should each movement by the Finch take?");
                        int danceTime = Convert.ToInt32(Console.ReadLine());

                        Dance(finchRobot, danceType, danceArea, danceTime);

                        DisplayMenuPrompt("Talent Show Menu");
                        break;

                    case "c":
                        DisplayScreenHeader("Mixing It Up");

                        MixingItUp(finchRobot);
                        break;

                    case "d":
                        DisplayScreenHeader("Light Sensors");

                        float average = lightSensors(finchRobot);
                        Console.WriteLine($"The average between both light sensors is {average}\n");

                        DisplayContinuePrompt();
                        DisplayMenuPrompt("Talent Show Menu");
                        break;

                    case "q":
                        quitTalentShowMenu = true;
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("\tPlease enter a letter for the menu choice.");
                        DisplayContinuePrompt();
                        break;
                }

            } while (!quitTalentShowMenu);
        }

        /// <summary>
        /// *****************************************************************
        /// *               Talent Show > Light and Sound                   *
        /// *****************************************************************
        /// </summary>
        /// <param name="finchRobot">finch robot object</param>
        static void DisplayLight(Finch finchRobot, int lightSoundValue)
        {
            for (int lightLevel = lightSoundValue; lightLevel > 0; lightLevel--)
            {
                finchRobot.setLED(lightLevel, lightLevel, lightLevel);
            }
        }

        static void DisplaySound(Finch finchRobot, int lightSoundValue)
        {
            for (int SoundLevel = lightSoundValue; SoundLevel > 0; SoundLevel--)
            {
                Console.Write($"Sound level: {SoundLevel}\n");
                //finchRobot.noteOn(SoundLevel * 100);
            }
        }

        /// <summary>
        /// *****************************************************************
        /// *                     Talent Show > Dance                       *
        /// *****************************************************************
        /// </summary>
        /// <param name="finchRobot">finch robot object</param>
        static void Dance(Finch finchRobot, int danceType, int danceArea, int danceTime)
        {
            Console.CursorVisible = false;
            Console.WriteLine("Still in development");

            finchRobot.setMotors(-255, 255);
            Thread.Sleep(2);
            finchRobot.setMotors(255, -255);
            Thread.Sleep(2);
            finchRobot.setMotors(0, 0);
        }

        /// <summary>
        /// *****************************************************************
        /// *                   Talent Show > Mixing It Up                  *
        /// *****************************************************************
        /// </summary>
        /// <param name="finchRobot">finch robot object</param>
        static void MixingItUp(Finch finchRobot)
        {
            Console.CursorVisible = false;

            Console.WriteLine("Still in development");
        }
        #endregion

        #region FINCH ROBOT MANAGEMENT
        /// <summary>
        /// *****************************************************************
        /// *                    Disconnect the Finch Robot                 *
        /// *****************************************************************
        /// </summary>
        /// <param name="finchRobot">finch robot object</param>
        static void DisplayDisconnectFinchRobot(Finch finchRobot)
        {
            Console.CursorVisible = false;

            DisplayScreenHeader("Disconnect Finch Robot");

            Console.WriteLine("\tAbout to disconnect from the Finch robot.");
            DisplayContinuePrompt();

            finchRobot.disConnect();

            Console.WriteLine("\tThe Finch robot is now disconnect.");

            DisplayMenuPrompt("Main Menu");
        }

        /// <summary>
        /// *****************************************************************
        /// *                  Connect the Finch Robot                      *
        /// *****************************************************************
        /// </summary>
        /// <param name="finchRobot">finch robot object</param>
        /// <returns>notify if the robot is connected</returns>
        static bool DisplayConnectFinchRobot(Finch finchRobot)
        {
            Console.CursorVisible = false;

            bool robotConnected;

            DisplayScreenHeader("Connect Finch Robot");

            Console.WriteLine("\tAbout to connect to Finch robot. Please be sure the USB cable is connected to the robot and computer now.");
            DisplayContinuePrompt();

            robotConnected = finchRobot.connect();

            // TODO test connection and provide user feedback - text, lights, sounds

            //
            // Test connection and provide user feedback
            //
            Console.WriteLine("Testing Finch systems\n");
            while (true)
            {

                for (int TestFinchSystems = 0; TestFinchSystems < 256; TestFinchSystems++)
                {
                    finchRobot.setLED(TestFinchSystems, TestFinchSystems, TestFinchSystems);
                    finchRobot.noteOn(TestFinchSystems * 100);
                }
                break;

            }
            Console.WriteLine("Testing complete\n");
            DisplayContinuePrompt();

            //
            // reset finch robot
            //
            finchRobot.setLED(0, 0, 0);
            finchRobot.noteOff();

            DisplayMenuPrompt("Main Menu");

            return robotConnected;
        }

        /// <summary>
        /// *****************************************************************
        /// *                  Display data to the user                     *
        /// *****************************************************************
        /// </summary>
        /// <param name="finchRobot">finch robot object</param>
        /// <returns>Record and display data to the user</returns>
        static void DisplayDataRecorder(Finch finchRobot)
        {
            int numberOfDataPoints = 0;
            double dataPointFrequency = 0;

            double[] temperatures = new double[numberOfDataPoints];
            DisplayScreenHeader("Finch Data Recorder");

            bool quitDataRecorderShowMenu = false;
            string menuChoice;

            do
            {
                DisplayScreenHeader("Main Menu");

                //
                // get user menu choice
                //
                Console.WriteLine("\ta) Number of Data Points");
                Console.WriteLine("\tb) Frequency of Data Points");
                Console.WriteLine("\tc) Get Data");
                Console.WriteLine("\td) Show Data");
                Console.WriteLine("\te) Return to Main Menu");
                Console.Write("\t\tEnter Choice:");
                menuChoice = Console.ReadLine().ToLower();

                //
                // process user menu choice
                //
                switch (menuChoice)
                {
                    case "a":
                        numberOfDataPoints = DisplayNumberOfDataPoints(finchRobot);
                        Console.WriteLine($"Number of Datapoints: {numberOfDataPoints}\n");
                        DisplayContinuePrompt();
                        break;
                    case "b":
                        dataPointFrequency = DisplayDataPointFrequency(finchRobot);
                        Console.WriteLine($"Datapoint Frequency: {dataPointFrequency}\n");
                        DisplayContinuePrompt();
                        break;
                    case "c":
                        DisplayGetData(numberOfDataPoints, dataPointFrequency, finchRobot);
                        break;
                    case "d":
                        DisplayData(temperatures);
                        break;
                    case "e":
                        quitDataRecorderShowMenu = true;
                        break;
                    default:
                        Console.WriteLine();
                        Console.WriteLine("\tPlease enter a letter for the menu choice.");
                        DisplayContinuePrompt();
                        break;
                }
            } while (!quitDataRecorderShowMenu);
        }

        /// <summary>
        /// *****************************************************************
        /// *                     Datapoint Frequency                       *
        /// *****************************************************************
        /// </summary>
        /// <param name="finchRobot">finch robot object</param>
        /// <returns>Ask the user for the datapoint frequency</returns>
        static double DisplayDataPointFrequency(Finch finchRobot)
        {
            DisplayScreenHeader("Frequency of Data Points");

            Console.WriteLine("Enter frequency of readings in seconds");
            return Convert.ToDouble(Console.ReadLine());
        }

        /// <summary>
        /// *****************************************************************
        /// *                     Number of Datapoints                      *
        /// *****************************************************************
        /// </summary>
        /// <param name="finchRobot">finch robot object</param>
        /// <returns>Ask the user for the number of datapoints</returns>
        static int DisplayNumberOfDataPoints(Finch finchRobot)
        {
            DisplayScreenHeader("Number of Data Points");

            Console.WriteLine("Enter the number of datapoints\n");

            int check = 0;

            try
            {
                check = (int)Convert.ToInt64(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("An error occured with the previous input\n");
                DisplayNumberOfDataPoints(finchRobot);
            }
            return check;
        }

        /// <summary>
        /// *****************************************************************
        /// *                          Get Data                             *
        /// *****************************************************************
        /// </summary>
        /// <param name="finchRobot">finch robot object</param>
        /// <returns>Populate an array with data from the Finch's sensors</returns>
        static void DisplayGetData(int numberOfDataPoints, double dataPointFrequency, Finch finchRobot)
        {
            DisplayScreenHeader("Get Data");

            double[] temperatures = new double[numberOfDataPoints];

            double Fahrenheit = 0;

            Console.WriteLine($"Number of datapoints: {numberOfDataPoints}\nFrequency of datapoints: {dataPointFrequency}\n");

            Console.WriteLine("Ready to record data\n");

            DisplayContinuePrompt();

            for (int loop = 0; loop < numberOfDataPoints; loop++)
            {
                Fahrenheit = finchRobot.getTemperature() * 1.8 + 32;

                temperatures[loop] = Fahrenheit;
                Console.WriteLine($"Temperature: {temperatures[loop]}\n");
                Thread.Sleep((int)dataPointFrequency);
            }

            Console.WriteLine("Done recording data\n");

            DisplayContinuePrompt();
        }

        /// <summary>
        /// *****************************************************************
        /// *                          Show Data                            *
        /// *****************************************************************
        /// </summary>
        /// <param name="finchRobot">finch robot object</param>
        /// <returns>Display the contents of the datatable</returns>
        static void DisplayDataTable(double[] temperatures)
        {
            for (int key = 0; key < temperatures.Length; key++)
            {
                Console.WriteLine($"[{key}]\n\tTemperature: {temperatures[key]}\n");
            }
        }

        /// <summary>
        /// *****************************************************************
        /// *                        Show Data - User                       *
        /// *****************************************************************
        /// </summary>
        /// <param name="finchRobot">finch robot object</param>
        /// <returns>Display the contents in the data table (this method is called by the user)</returns>
        static void DisplayData(double[] temperatures)
        {
            DisplayScreenHeader("Show Data");
            DisplayDataTable(temperatures);
            DisplayContinuePrompt();
        }

        /// <summary>
        /// *****************************************************************
        /// *                  Display Alarm System                         *
        /// *****************************************************************
        /// </summary>
        /// <param name="finchRobot">finch robot object</param>
        /// <returns>Alarm system for Finch robot</returns>
        static void DisplayAlarmSystem(Finch finchRobot)
        {
            string sensorType = "";
            string sensorsToMonitor = "";
            string rangeType = "";
            int minMaxThresholdValue = 0;
            int timeToMonitor = 0;

            bool quitAlarmMenu = false;
            string menuChoice;

            DisplayScreenHeader("Finch Alarm System");

            do
            {
                Console.WriteLine("\ta) Sensor Type");
                Console.WriteLine("\tb) Sensors to Monitor");
                Console.WriteLine("\tc) Range Type");
                Console.WriteLine("\td) Min/Max Threshold");
                Console.WriteLine("\te) Time to Monitor");
                Console.WriteLine("\tf) Set Alarm");
                Console.WriteLine("\tg) Return to Main Menu");
                Console.Write("\t\tEnter Choice:");
                menuChoice = Console.ReadLine().ToLower();

                switch (menuChoice)
                {
                    case "a":
                        Console.WriteLine("What type of sensors do you want to use?\n");
                        Console.WriteLine("Light:\n");
                        Console.WriteLine("or\n");
                        Console.WriteLine("Temp:\n");
                        sensorType = Console.ReadLine().ToLower();
                        break;
                    case "b":
                        if (sensorType == "light")
                        {
                            sensorsToMonitor = DisplaySensorsToMonitor(finchRobot);
                            Console.WriteLine($"Sensors to monitor: {sensorsToMonitor}\n");
                        }
                        else if (sensorType == "temp")
                        {
                            Console.WriteLine("There is only one temperature sensor, this option is not needed\n");
                        }
                        DisplayContinuePrompt();
                        break;
                    case "c":
                        rangeType = DisplayRangeType(finchRobot);
                        Console.WriteLine($"Range Type: {rangeType}\n");
                        DisplayContinuePrompt();
                        break;
                    case "d":
                        if (sensorType == "light")
                        {
                            Console.WriteLine("Enter value from 0 to 255\n");
                        }
                        else if (sensorType == "temp")
                        {
                            Console.WriteLine("Enter value in Fahrenheit\n");
                        }
                        minMaxThresholdValue = DisplayMinMaxThresholdValue(finchRobot);
                        Console.WriteLine($"Min/Max Threshold: {minMaxThresholdValue}\n");
                        DisplayContinuePrompt();
                        break;
                    case "e":
                        timeToMonitor = DisplayTimeToMonitor(finchRobot);
                        Console.WriteLine($"Time to monitor: {timeToMonitor}\n");
                        DisplayContinuePrompt();
                        break;
                    case "f":
                        SetAlarm(finchRobot, sensorType, sensorsToMonitor, rangeType, minMaxThresholdValue, timeToMonitor);
                        DisplayContinuePrompt();
                        break;
                    case "g":
                        quitAlarmMenu = true;
                        break;
                    default:
                        Console.WriteLine();
                        Console.WriteLine("\tPlease enter a letter for the menu choice.");
                        DisplayContinuePrompt();
                        break;
                }
            } while (!quitAlarmMenu);
            DisplayMenuPrompt("Main Menu");
        }

        static string DisplaySensorsToMonitor(Finch finchRobot)
        {
            DisplayScreenHeader("Sensors to Monitor");
            Console.WriteLine($"Left Light Sensor: {finchRobot.getLeftLightSensor()}\n");
            Console.WriteLine($"Right Light Sensor: {finchRobot.getRightLightSensor()}\n");

            Console.WriteLine("Which light sensor(s) should be monitored (left, right, or both)?:\n");
            return Console.ReadLine().ToLower();
        }

        static string DisplayRangeType(Finch finchRobot)
        {
            DisplayScreenHeader("Range Type");
            Console.WriteLine("Enter either minimum or maximum for the range type:\n");
            return Console.ReadLine().ToLower();
        }

        static int DisplayMinMaxThresholdValue(Finch finchRobot)
        {
            DisplayScreenHeader("Min/Max Threshold");
            Console.WriteLine("Enter the threshold value:\n");
            return (int)Convert.ToInt64(Console.ReadLine());
        }

        static int DisplayTimeToMonitor(Finch finchRobot)
        {
            DisplayScreenHeader("Time To Monitor");
            Console.WriteLine("Enter the amount of time (in seconds) to monitor:\n");
            return (int)Convert.ToInt64(Console.ReadLine());
        }

        static void SetAlarm(Finch finchRobot, string sensorType, string sensorsToMonitor, string rangeType, int minMaxThresholdValue, int timeToMonitor)
        {
            DisplayScreenHeader("Running Alarm Program");

            Console.WriteLine($"Sensors to monitor: {sensorsToMonitor}\n");
            Console.WriteLine($"Range Type: {rangeType}\n");
            Console.WriteLine($"{rangeType} Threshold: {minMaxThresholdValue}\n");
            Console.WriteLine($"Will monitor for {timeToMonitor} seconds\n");

            DisplayContinuePrompt();

            double sensorTemp = 0;

            // do-while loop goes below, monitor sensors.
            do
            {
                if (sensorType == "light")
                {
                    if (sensorsToMonitor == "both")
                    {
                        float averageLight = lightSensors(finchRobot);
                        Console.WriteLine($"Average light:\t{averageLight.ToString("F2")}\n");

                        float minMaxThresholdValueToFloat = (float)minMaxThresholdValue;

                        if (minMaxThresholdValueToFloat == averageLight)
                        {
                            finchRobot.noteOn(127);
                            Thread.Sleep(3);
                            finchRobot.noteOff();
                        }
                    }
                    else if (sensorsToMonitor == "left") 
                    {
                        if (minMaxThresholdValue == finchRobot.getLeftLightSensor())
                        {
                            finchRobot.noteOn(127);
                            Thread.Sleep(3);
                            finchRobot.noteOff();
                        }
                    }
                    else if (sensorsToMonitor == "right")
                    {
                        if (minMaxThresholdValue == finchRobot.getRightLightSensor())
                        {
                            finchRobot.noteOn(127);
                            Thread.Sleep(3);
                            finchRobot.noteOff();
                        }
                    }
                }
                else if (sensorType == "temp")
                {
                    sensorTemp = finchRobot.getTemperature() * 1.8 + 32;

                    double minMaxThresholdValueToDouble = (double)minMaxThresholdValue;

                    if (sensorTemp == minMaxThresholdValueToDouble)
                    {
                        finchRobot.noteOn(127);
                        Thread.Sleep(3);
                        finchRobot.noteOff();
                    }

                    Console.WriteLine($"Temperature:\t{sensorTemp}\n");
                }

                Console.WriteLine($"Time to monitor:\t{timeToMonitor}\n");

                timeToMonitor = timeToMonitor - 1;
            } while (timeToMonitor != 0);
            // Loop ends
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
            DisplayScreenHeader("User Programming for Finch robot");

            Console.WriteLine("This module is under development\n");

            DisplayContinuePrompt();

            DisplayMenuPrompt("Main Menu");
        }

        /// <summary>
        /// *****************************************************************
        /// *                        Light Sensors                          *
        /// *****************************************************************
        /// </summary>
        /// <param name="finchRobot">finch robot object</param>
        /// <returns>Gets the average light level between the left and right light sensors</returns>
        static float lightSensors(Finch finchRobot)
        {
            return (finchRobot.getLeftLightSensor() + finchRobot.getRightLightSensor()) / 2;
        }
        #endregion

        #region USER INTERFACE
        /// <summary>
        /// *****************************************************************
        /// *                     Welcome Screen                            *
        /// *****************************************************************
        /// </summary>
        static void DisplayWelcomeScreen()
        {
            Console.CursorVisible = false;

            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\tFinch Control");
            Console.WriteLine();

            DisplayContinuePrompt();
        }

        /// <summary>
        /// *****************************************************************
        /// *                     Closing Screen                            *
        /// *****************************************************************
        /// </summary>
        static void DisplayClosingScreen()
        {
            Console.CursorVisible = false;

            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\tThank you for using Finch Control!");
            Console.WriteLine();

            DisplayContinuePrompt();
        }

        /// <summary>
        /// display continue prompt
        /// </summary>
        static void DisplayContinuePrompt()
        {
            Console.WriteLine();
            Console.WriteLine("\tPress any key to continue.");
            Console.ReadKey();
        }

        /// <summary>
        /// display menu prompt
        /// </summary>
        static void DisplayMenuPrompt(string menuName)
        {
            Console.WriteLine();
            Console.WriteLine($"\tPress any key to return to the {menuName} Menu.");
            Console.ReadKey();
        }

        /// <summary>
        /// display screen header
        /// </summary>
        static void DisplayScreenHeader(string headerText)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\t" + headerText);
            Console.WriteLine();
        }
        #endregion
    }
}
