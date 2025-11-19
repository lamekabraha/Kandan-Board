using System;
using System.Text.RegularExpressions;

namespace KandanBoard.models
{
    public class User
    {
        private int UserId;
        private string FirstName;
        private string LastName;
        private string Email;
        private string Password;

        public User() { }

        public User(int userId,  string firstName, string lastName, string email, string password)
        {
            UserId = userId;
            FirstName = firstName;
            LastName = lastName;
            Email = email.ToLower();
            Password = password;
        }

        public int GetUserId()
        {
            return UserId;
        }
        public void SetUserId(int userId)
        {
            UserId=userId;
        }

        public string GetFirstName()
        {
            return FirstName;
        }
        public void SetFirstName(string firstName)
        {
            firstName = FirstName;
        }

        public string GetLastName()
        {
            return LastName;
        }
        public void SetLastName(string lastName)
        {
            LastName = lastName;
        }

        public string GetEmail()
        {
            return Email;
        }
        public void SetEmail(string email)
        {
            Email = email;
        }

        public string GetPassword()
        {
            return Password;
        }
        public void SetPassword(string password)
        {
            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = 
            Password = password;
        }
    }
}