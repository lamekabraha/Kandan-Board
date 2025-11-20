using System;
using System.Collections.Generic;
using System.IO;
using KandanBoard.models;

namespace KandanBoard.persistence
{
    public static class DataManager
    {
        private const string usersFilePath = "data/users.dat";

        private static void FileExists()
        {
            if (!Directory.Exists("data"))
            {
                Directory.CreateDirectory("data");
            }
        }

        public static void SaveUsers(List<User> userList)
        {
            FileExists();
            FileStream file = File.Open(usersFilePath, FileMode.Create); // opens file if exists, creates file if doesn't exist
            BinaryWriter bw = new BinaryWriter(file);

            try
            {
                bw.Write(userList.Count); //returns number of users 
                
                foreach(User user in userList)
                {
                    bw.Write(user.GetEmail());
                    bw.Write(user.GetEmail());
                    bw.Write(user.GetEmail());
                    bw.Write(user.GetEmail());
                    bw.Write(user.GetEmail());
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"ERROR: Failed to save binary data. {ex.Message}");
            }
            finally
            {
                //used to close the writer and file connection
                bw.Close();
                file.Close();
            }
        }

        public static List<User> LoadUser()
        {
            if (!File.Exists(usersFilePath))
            {
                return new List<User>();
            }

            List<User> userList = new List<User>();

            FileStream file = File.Open(usersFilePath, FileMode.Open);

            BinaryReader br = new BinaryReader(file);

            try
            {
                int userCount = br.ReadInt32();

                // loop through .dat to read user info
                for (int i=0; i< userCount; i++)
                {
                    int userId = br.ReadInt32();
                    string firstName = br.ReadString();
                    string lastName = br.ReadString();
                    string email = br.ReadString();
                    string password = br.ReadString();

                    User user = new User(userId, firstName, lastName, email, password);
                    userList.Add(user);
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: Failed to load user data. {ex.Message}");
            }
            finally
            {
                br.Close();
                file.Close();
            }
            return userList;
        }
    }
}