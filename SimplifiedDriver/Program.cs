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
            string text = null;
            Console.Write("Command: ");
            bool flag = false;
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
                        Console.Write($"Text: {text}");
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
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
           
            
            try
            {
                //string text = "PS:1,0,255:E";
                string defineFunction = @"(P\S:\S*:E$)";
                string command = null;
                Match match = Regex.Match(text, defineFunction);
                if (match.Success)
                {
                    Console.WriteLine(match.Groups[1].Value);
                    command = match.Groups[1].Value;
                }
                char key = command[1];
                switch (key)
                {
                    case 'T':           // Command "Text" - Text Output
                        Match argument = Regex.Match(command, @":(\w*):");
                        if (argument.Success)
                        {
                            Commands.Text(argument.Groups[1].Value);
                        }
                        else
                            Console.WriteLine("The \"Text\" command argument is invalid");
                        break;
                    case 'S':
                        Console.WriteLine("Command 'S'");
                        Match arguments = Regex.Match(command, @":(\d*),(\d*):");
                        if (arguments.Success)
                        {
                            int a = Convert.ToInt32(arguments.Groups[1].Value);
                            int b = Convert.ToInt32(arguments.Groups[2].Value);
                            Console.WriteLine($"Argumet: {a}, {b}");
                        }
                        Console.WriteLine("Не верный формат аргументов");
                        break;
                    default:
                        Console.WriteLine("Совпадений не найдено");
                        break;

                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            } 
        }
    }
}
