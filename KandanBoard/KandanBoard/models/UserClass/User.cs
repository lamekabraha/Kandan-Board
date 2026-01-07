using System;
using System.Text.RegularExpressions;

namespace KandanBoard.models.UserClass
{
    public class User
    {
        protected int userId;
        protected string firstName;
        protected string lastName;
        protected string email;
        protected string password;

        public int GetUserId()
        {
            return userId;
        }

        public string GetFirstName()
        {
            return firstName;
        }

        public string GetLastName()
        {
            return lastName;
        }

        public string GetEmail()
        {
            return email;
        }

        public string GetPassword()
        {
            return password;
        }

        public void SetUserId(int userId)
        {
            this.userId = userId;
        }

        public void SetFirstName(string firstName)
        {
            this.firstName = firstName;
        }

        public void SetEmail(string email)
        {
            this.email = email;
        }

        public void SetPassword(string password)
        {
            this.password = password;
        }

        public User(int userId, strin)
}