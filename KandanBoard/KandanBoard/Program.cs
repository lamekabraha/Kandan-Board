using Newtonsoft.Json;
using System.Diagnostics;
using Newtonsoft.Json;


namespace KandanBoard
{
    internal class Program
    {
        static void Main()
        {
            Console.Clear();
            //Console.Title = "Kandan Board";
            //Console.WriteLine("\n\n--------------------Kandan Board--------------------");

            //Console.WriteLine("\n\nDo you wish to Login or Register?\n\n");
            //Users newUser = new Users();

            //newUser.ListUsers();

            Users user = new Users();

            user.Register();


        }
    }
}
