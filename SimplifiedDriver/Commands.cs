using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplifiedDriver
{
    static class Commands
    {
         static public void Text(string text) // Command "Text" - Text Output
        {
            Console.Clear();
            Console.WriteLine(text);
        }
    }
}
