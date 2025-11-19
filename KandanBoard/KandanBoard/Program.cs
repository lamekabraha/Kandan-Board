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
            //UserManager newUser = new UserManager();

            //newUser.RegisterNewUser();

            Console.Write("Register or Login: ");
            string input = Console.ReadLine();
            if (input == "register")
            {
                UserManager.RegisterNewUser();
            }
            if (input == "login")
            {
                Console.Write("Email: ");
                string email = Console.ReadLine();
                Console.Write("Password: ");
                string password = Console.ReadLine();
                User loggedInUser = UserManager.Login(email, password);

                if (loggedInUser != null)
                {
                    Console.Write($"Welcome {loggedInUser.FirstName} {loggedInUser.LastName}");
                }
            }
        }
    }
}
