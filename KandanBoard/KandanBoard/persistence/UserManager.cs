using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using KandanBoard.models;
using KandanBoard.persistence;

namespace KandanBoard.managers
{
    public static class UserManager
    {
        public static bool EmailExists(string checkedEmail)
        {
            List<User> userList = DataManager.LoadUser();
            string formattedEmail = checkedEmail.ToLower();

            IEnumerable<User> user = userList.Where(user => user.Email == formattedEmail);
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
            int newUserId = userList.Any() ? userList.Max(u => u.UserId) + 1: 1;

            User newUser = new User(newUserId, firstName, lastName, email,  password );

            userList.Add(newUser);
            DataManager.SaveUsers(userList);

            Console.WriteLine($"SUCCESS! {firstName} {lastName} has been registered as user: {newUserId}");
        }

        public static User Login(string email, string password)
        {
            List < User > userList = DataManager.LoadUser();
            string formatEmail = email.ToLower();

            User user = userList.SingleOrDefault(u => u.Email == formatEmail);            

            if (user != null && user.Password == password)
            {

                return user;
            }

            return null;
        }
    }
}