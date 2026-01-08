using Newtonsoft.Json;
using System.Diagnostics;
using KandanBoard.models;
using KandanBoard.managers;


namespace KandanBoard
{
    internal class Program
    {
        static void Main(string[] args)
        {
            KandanBoardApp app = new KandanBoardApp();
            if (args.Length == 2)
            {
                UserManager.Login(args[0], args[1]);
                app.mainMenu();
            }
            else if (args.Length == 0)
            {
                Console.Clear();
                Console.SetWindowSize(300, 200);

                Console.WriteLine("\n Welcome to your Kandan Board!");
                app.runApp();
            }
            else
            {
                Console.WriteLine("No Command Line Argument Found");
                Console.Clear();
            }
        }
    }
}
