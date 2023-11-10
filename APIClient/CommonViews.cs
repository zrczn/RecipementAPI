using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace APIClient
{
    internal static class CommonViews
    {

        private static void Header(string text)
        {
            Console.WriteLine("===============================================================================");
            Console.WriteLine(text);
            Console.WriteLine("===============================================================================\n");
        }

        internal static void MainMenu()
            => Header($"Welcome in Recipement Client\nplease select following Number to get into details\nor press 'n' to create a new reipe");

        internal static void Details()
            => Header($"Recipe details");

        internal static void ResetText()
            => Console.Clear();

    }
}
