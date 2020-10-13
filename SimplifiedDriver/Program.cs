using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace SimplifiedDriver
{
    class Program
    {
        static void Main(string[] args)
        {
            OnCommandAsync();
            Console.Read();
        }

        static async private void OnCommandAsync() // Waiting for a command in the console
        {
            string text = null; // input data variable
            Console.Write("Command: ");
            bool flag = false; // false - until the packet stream sequence ends with an 'E' (69).
            try
            {
                while (flag == false)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                    int code = keyInfo.KeyChar;
                    if (text != null && text.Length >= 1 && code == 8) // Condition for backspace
                    {
                        text = text.Remove(text.Length - 1);
                        Console.Clear();
                        Console.Write($"Command: {text}");
                    }

                    if (code >= 32 && code <= 127) // Allowed range of ASCII input characters
                    {
                        char ch = keyInfo.KeyChar;
                        text += ch;
                        Console.Write(ch);
                        if (ch == 'E' && text[text.Length - 2] == ':') // Condition end of function
                        {
                            flag = true;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            await Task.Run(()=> SelectCommand(text)); // Passing command to function
        }

        static private void SelectCommand(string text) // Command validation and selection
        {
            try
            {
                string defineFunction = @"(P[A-Z]:[ -~]*:E$)"; // command check condition
                string command = null; // variable for command package
                Match match = Regex.Match(text, defineFunction);
                if (match.Success)
                {
                    command = match.Groups[1].Value; // Get command package
                    char key = command[1]; // Get command character
                    switch (key)
                    {
                        case 'T':           // Command "Text" - Text Output
                            Text(command);
                            break;
                        case 'S':           // Command "Sound"
                            Sound(command);
                            break;
                        default:
                            Console.WriteLine("\nCommand not found");
                            break;
                    }
                }
                else
                    Console.WriteLine("\nCommand not found");
               
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static private void Text(string command) // Command "Text" - Text Output
        {
            Console.Clear();
            Match argument = Regex.Match(command, @":([ -~]*):"); // checking the validity of a command parameter
            if (argument.Success)
            {
                Console.WriteLine(argument.Groups[1].Value);
            }
            else
                Console.WriteLine("The \"Text\" invalid payload");
            
        }

        static private void Sound(string command) // Command "Sound"
        {
            Console.Clear();
            Match arguments = Regex.Match(command, @":(\d*),(\d*):"); // checking the validity of a command parameter
            if (arguments.Success)
            {
                int freq = Convert.ToInt32(arguments.Groups[1].Value);
                int duration = Convert.ToInt32(arguments.Groups[2].Value);
                Console.Beep(freq, duration);
            }
            else
                Console.WriteLine("The \"Sound\" invalid payload");
            
        }
    }
}
