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
            Console.Write("\nRegister or Login: ");
            string input = Console.ReadLine();
            Console.WriteLine("Type 'Exit' to return to this point.");

            if (input.ToLower() == "register")
            {
                UserManager.RegisterNewUser();
            }


            if (input.ToLower() == "login")
            {
                User loggedInUser = UserManager.Login();

                if (loggedInUser != null)
                {
                    Console.Write($"Welcome {loggedInUser.GetFirstName()} {loggedInUser.GetLastName()}");
                }
            }
        }
    }
}
