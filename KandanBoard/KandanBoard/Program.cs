using Newtonsoft.Json;
using System.Diagnostics;
using Newtonsoft.Json;
using KandanBoard.models;
using KandanBoard.managers;


namespace KandanBoard
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 2)
            {
                Console.WriteLine($"Hello {args[1]} {args[2]}");
            }
            else if (args.Length == 4)
            {
                // run register
            }
            else if (args.Length == 0)
            {
                //Console.Clear();
                //Console.SetWindowSize(300, 200);

                //Console.WriteLine("\nWelcome to your Kandan Board!");
                //KandanBoardApp app = new KandanBoardApp();
                //app.runApp();
                Console.WriteLine("run login");
            }
            else
            {
                Console.WriteLine("No Command Line Argument Found");
                Console.Clear();
            }
        }
    }
}
