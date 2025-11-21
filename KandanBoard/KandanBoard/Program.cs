using Newtonsoft.Json;
using System.Diagnostics;
using Newtonsoft.Json;
using KandanBoard.models;
using KandanBoard.managers;


namespace KandanBoard
{
    internal class Program
    {
        static void Main()
        {
            Console.Clear();
            Console.SetWindowSize(300, 200);

            Console.WriteLine("\nWelcome to your Kandan Board!");
            KandanBoardApp app = new KandanBoardApp();
            app.runApp();
        }
    }
}
