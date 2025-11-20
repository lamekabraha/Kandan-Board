using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using KandanBoard.models;
using KandanBoard.persistence;
using System.Threading;

namespace KandanBoard.managers
{
    public static class UserManager
    {
        public static bool EmailExists(string checkedEmail)
        {
            List<User> userList = DataManager.LoadUser();
            string formattedEmail = checkedEmail.ToLower();

            IEnumerable<User> user = userList.Where(user => user.GetEmail() == formattedEmail);
            bool exists = user.Any();
            return exists;
        }

        public static void RegisterNewUser()
        {
            Console.Write("First Name: ");
            string firstName = Console.ReadLine();
            Console.Write("Last Name: ");
            string lastName = Console.ReadLine();
            string email;
            while (true)
            {
                Console.Write("Email: ");
                email = Console.ReadLine().ToLower();
                if (EmailExists(email))
                {
                    Thread.Sleep(10000); 
                    Console.WriteLine($"ERROR: {email} already exists. Please try using a different email.");
                }
                else
                {
                    break;
                }
            }

            Console.Write("Password: ");
            string password = Console.ReadLine();

            //get the largest userId integer
            List<User> userList = DataManager.LoadUser();
            int newUserId = userList.Any() ? userList.Max(user => user.GetUserId()) + 1: 1;

            User newUser = new User(newUserId, firstName, lastName, email,  password );

            userList.Add(newUser);
            DataManager.SaveUsers(userList);

            Console.WriteLine($"SUCCESS! {firstName} {lastName} has been registered as user: {newUserId}");
        }

        public static User Login()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("\n ~~~~~ LOGIN ~~~~~");
                Console.Write("Email: ");
                string emailInput = Console.ReadLine();
                Console.Write("Password: ");
                string passwordInput = Console.ReadLine();

                try
                {
                    List < User > userList = DataManager.LoadUser();
                    string formatEmail = emailInput.ToLower();
                    // Using email to parse through userList to find user
                    User user = userList.SingleOrDefault(user => user.GetEmail() == formatEmail);            

                    if (user != null && user.GetPassword() == passwordInput)
                    {
                        return user;
                    }
                    else
                    {
                        Console.WriteLine("ERROR: Invalid email or password. Please try again.");
                        Thread.Sleep(2000);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"ERROR: Failed to find your account: {ex.Message}");
                    return null;
                }
            }
        }
    }
}