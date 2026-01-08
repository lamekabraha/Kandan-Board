using System;
using System.Threading;
using KandanBoard.managers;
using KandanBoard.models.UserClass;

namespace KandanBoard
{
    public class KandanBoardApp()
    {
        // Used to hold the state of the user currently logged in
        private User? currentUser;

        public void runApp()
        {
            while (true)
            {
               if (currentUser == null)
               {
                    Console.Clear();
                    Console.WriteLine("\n Welcome to your Kandan Board app.");
                    Console.WriteLine("\n Please select an option from below:");
                    Console.WriteLine("\n\n  1. Login \n  2. Register \n  3. Exit \n");

                    string input = Console.ReadLine().ToLower();
                    switch (input)
                    {
                        case "1":
                        case "login":
                            Console.Clear();
                            Console.WriteLine("\n ~~~~~ LOGIN ~~~~~");
                            Console.Write("\n Email: ");
                            string emailInput = Console.ReadLine();
                            Console.Write("\n Password: ");
                            string passwordInput = Console.ReadLine() ;
                            currentUser = UserManager.Login(emailInput, passwordInput);
                            break;
                        case "2":
                        case "register":
                            UserManager.RegisterNewUser();
                            Console.Clear();
                            Console.WriteLine("\n ~~~~~ LOGIN ~~~~~");
                            Console.Write("\n Email: ");
                            emailInput = Console.ReadLine();
                            Console.Write("\n Password: ");
                            passwordInput = Console.ReadLine();
                            currentUser = UserManager.Login(emailInput, passwordInput);
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
                    // run main menu if the user is logged in
                    mainMenu();
               }
            }

        }

        public void mainMenu()
        {
            Console.Clear();
            Console.WriteLine("\n ~~~~~~~~~~ MAIN MENU ~~~~~~~~~~ ");
            Console.WriteLine($"\n Welcome, {currentUser.GetFirstName().ToUpper().Split(' ')[0] + " " + currentUser.GetLastName().ToUpper().Split(' ')[0]}");
            Console.WriteLine("\n Select a number for an option below: ");
            Console.WriteLine("\n 1. View Board\n 2. Create Task\n 3. Logout\n 4. Exit");

            string input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    TaskManager.viewBoard(this, currentUser);
                    break;
                case "2":
                    TaskManager.createTask(currentUser);
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