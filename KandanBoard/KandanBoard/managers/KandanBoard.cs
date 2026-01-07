using System;
using KandanBoard.managers;
using KandanBoard.models.UserClass;

namespace KandanBoard
{
    public class KandanBoardApp()
    {
        // Used to hold the state of the user currently logged in
        private User currentUser;

        public void runApp()
        {
            while (true)
            {
               if (currentUser == null)
               {
                    Console.Clear();
                    Console.WriteLine("\n Welcome to your Kandan Board app.");
                    Console.WriteLine("\n Please select an option from bellow:");
                    Console.WriteLine("\n\n 1. Login \n 2. Register \n 3. Exit \n");

                    string input = Console.ReadLine().ToLower();
                    switch (input)
                    {
                        case "1":
                        case "login":
                            currentUser = UserManager.Login();
                            break;
                        case "2":
                        case "register":
                            UserManager.RegisterNewUser();
                            currentUser = UserManager.Login();
                            break;
                        case "3":
                        case "exit":
                            Console.WriteLine(" Kandan Board App is now closing");
                            Thread.Sleep(2000);
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine(" Please enter a valid option.");
                            break;
                    }
               }
               else
               {
                    Console.Clear();
                    Console.WriteLine("\n ~~~~~~~~~~ MAIN MENU ~~~~~~~~~~ ");
                    Console.WriteLine($"\n Welcome, {currentUser.GetFirstName()}");
                    Console.WriteLine("\n Select a number for an option bellow: ");
                    Console.WriteLine("\n 1. View Board\n 2. Create Task\n 3. Logout\n 4.Exit");

                    string input = Console.ReadLine();
                    switch (input)
                    {
                        case "1":
                            TaskManager.viewBoard();
                            break;
                        case "2":
                            TaskManager.createTask();
                            break;
                        case "3":
                            currentUser = null;
                            break;
                        case "4":
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine(" Please enter a valid option.");
                            break;
                    }
               }
            }

        }
    }
}