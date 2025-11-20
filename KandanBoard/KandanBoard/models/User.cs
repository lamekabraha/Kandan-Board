using System;
using System.Text.RegularExpressions;

namespace KandanBoard.models
{
    public class User
    {
        private int _userId;
        private string _firstName;
        private string _lastName;
        private string _email;
        private string _password;

        public int    GetUserId()       { return _userId; }
        public string GetFirstName()    { return _firstName; }
        public string GetLastName()     { return _lastName; }
        public string GetEmail()        { return _email; }
        public string GetPassword()     { return _password; }

        
        public void SetUserId(int userId) {  _userId = userId; }
        public void SetFirstName(string firstName) { _firstName = firstName; }
        public void SetLastName(string lastName) { _lastName = lastName; }
        public void SetEmail(string email) { _email = email; }
        public void SetPassword(string password) { _password = password; }

        public User(int userId, string firstName, string lastName, string email, string password)
        {
            _userId = userId;
            _firstName = firstName;
            _lastName = lastName;
            _email = email;
            _password = password;
        }

    }
}