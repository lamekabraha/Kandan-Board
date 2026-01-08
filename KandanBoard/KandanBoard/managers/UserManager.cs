using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using KandanBoard.persistence;
using System.Threading;
using KandanBoard.models.UserClass;

namespace KandanBoard.managers
{
    public static class UserManager
    {
        private const int SaltSize = 128 / 8; 
        private const int HashSize = 256 / 8; 
        private const int IterationCount = 10000;

        private static string HashPassword(string password, byte[] salt)
        {
            byte[] hash = KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: IterationCount,
                numBytesRequested: HashSize);

            byte[] saltAndHash = new byte[SaltSize + HashSize];
            Array.Copy(salt, 0, saltAndHash, 0, SaltSize);
            Array.Copy(hash, 0, saltAndHash, SaltSize, HashSize);
            return Convert.ToBase64String(saltAndHash);
        }

        private static string HashPassword(string password)
        {
            byte[] salt = new byte[SaltSize];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return HashPassword(password, salt);
        }

        private static bool VerifyPassword(string password, string hashedPassword)
        {
            try
            {
                byte[] saltAndHash = Convert.FromBase64String(hashedPassword);
                byte[] salt = new byte[SaltSize];
                Array.Copy(saltAndHash, 0, salt, 0, SaltSize);
                string computedHash = HashPassword(password, salt);
                return computedHash == hashedPassword;
            }
            catch
            {
                return false;
            }
        }

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
            Console.Write(" First Name: ");
            string? firstNameInput = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(firstNameInput))
            {
                Console.WriteLine("\n ERROR: First name cannot be empty. Registration cancelled.");
                return;
            }
            string firstName = firstNameInput;

            Console.Write(" Last Name: ");
            string? lastNameInput = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(lastNameInput))
            {
                Console.WriteLine("\n ERROR: Last name cannot be empty. Registration cancelled.");
                return;
            }
            string lastName = lastNameInput;

            string email;
            while (true)
            {
                Console.Write(" Email: ");
                string? emailInput = Console.ReadLine();
                email = emailInput?.ToLower() ?? "";
                if (string.IsNullOrWhiteSpace(email))
                {
                    Console.WriteLine("\n ERROR: Email cannot be empty. Please try again.");
                    continue;
                }
                if (EmailExists(email))
                {
                    Console.WriteLine($"\n ERROR: {email} already exists. Please try using a different email.");
                }
                else
                {
                    break;
                }
            }

            Console.Write(" Password: ");
            string? passwordInput = Console.ReadLine();
            string password = passwordInput ?? "";
            
            if (string.IsNullOrWhiteSpace(password))
            {
                Console.WriteLine("\n ERROR: Password cannot be empty. Registration cancelled.");
                return;
            }

            // Ask for user type
            Console.WriteLine("\n Select user type:");
            Console.WriteLine("  1. Admin");
            Console.WriteLine("  2. Member");
            Console.Write(" Type (1 or 2): ");
            string? userTypeInput = Console.ReadLine();
            int userType;
            
            if (!int.TryParse(userTypeInput, out userType) || (userType != 1 && userType != 2))
            {
                Console.WriteLine("\n ERROR: Invalid user type. Registration cancelled.");
                return;
            }

            // Hash the password before storing
            string hashedPassword = HashPassword(password);

            //get the largest userId integer
            List<User> userList = DataManager.LoadUser();
            int newUserId = userList.Any() ? userList.Max(user => user.GetUserId()) + 1: 1;

            User newUser;
            if (userType == 1)
            {
                newUser = new Admin(newUserId, firstName, lastName, email, hashedPassword);
            }
            else
            {
                newUser = new Member(newUserId, firstName, lastName, email, hashedPassword);
            }

            userList.Add(newUser);
            
            DataManager.SaveUsers(userList);

            Console.Clear();
            string userTypeName = userType == 1 ? "Admin" : "Member"; // Return user type name
            Console.WriteLine($"\n Registration successful! You have been registered as {userTypeName}. Please login with your credentials.");
        }

        public static User? Login(string email, string password)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                {
                    Console.WriteLine("\n ERROR: Email and password cannot be empty.");
                    return null;
                }

                List<User> userList = DataManager.LoadUser();
                string formatEmail = email.ToLower();
                
                // Using email to parse through userList to find user
                User? user = userList.SingleOrDefault(user => user.GetEmail() == formatEmail);            

                if (user != null && VerifyPassword(password, user.GetPassword()))
                {
                    Console.WriteLine($"\n Welcome back, {user.GetFirstName()}!");
                    return user;
                }
                else
                {
                    Console.WriteLine("\n ERROR: Invalid email or password. Please try again.");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n ERROR: Failed to find your account: {ex.Message}");
                return null;
            }
        }
    }
}