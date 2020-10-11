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
            Console.Write("Text: ");
            bool flag = false;
            while (flag == false)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                int code = keyInfo.KeyChar;
                if (text != null && text.Length >= 1 && code == 8) // условие для бекспейса
                {
                    text = text.Remove(text.Length - 1);
                    Console.Clear();
                    Console.Write($"Text: {text}");
                }

                if (code >= 32 && code <= 127) // разрешенный диапазон по таблице ASCII входных символов
                {
                    char ch = keyInfo.KeyChar;
                    text += ch;
                    Console.Write(ch);
                    if (ch == 'E' && text[text.Length-2] == ':') // окнчание функции
                    {
                        flag = true;
                    }
                }
            }
            //string text = "PS:1,0,255:E";
            string defineFunction = @"(P\S:\S*:E$)";
            string command = null;
            Match match = Regex.Match(text, defineFunction);
            if(match.Success)
            {
                Console.WriteLine(match.Groups[1].Value);
                command = match.Groups[1].Value;
            }
            char key = command[1];
            switch (key)
            {
                case 'T':
                    Console.WriteLine("Command 'T'");
                    Match argument = Regex.Match(command, @":(\w*):");
                    Console.WriteLine($"Argumet: {argument.Groups[1].Value}");
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
    }
}
