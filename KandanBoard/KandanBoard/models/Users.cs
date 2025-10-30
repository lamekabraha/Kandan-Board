using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;


public class Users
{
	public int UserId { get; set; }
	public String FirstName { get; set; }
	public String LastName { get; set; }
	public String Email { get; set; }
	public String Password { get; set; }

	public Users(int userid, String firstname, String lastname, String email, String password)
	{
		this.UserId = userid;
		this.FirstName = firstname;
		this.LastName = lastname;
		this.Email = email;
		this.Password = password;
	}

	public Users()
	{

	}

	public bool EmailExists(string Email)
	{
		Console.WriteLine($"Checking if {Email} exists");
        // used https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/deserialization to learn how to deserialise json file into a list
        string jsonPath = File.ReadAllText("Data/users.json");
		List<Users>? userList = JsonSerializer.Deserialize<List<Users>>(jsonPath);
		IEnumerable<string> emails = userList.Select(u => u.Email);
		foreach (string email in emails)
		{
			if (Email.ToLower() == email.ToLower())
			{
				Console.WriteLine($"ERROR: {email} already exists.");
				return true;
			}
        }
		return false;

		//if (userList != null)
		//{
		//	Users? searchEmail = userList.FirstOrDefault(user => user.Email == Email);
		//	while (true)
		//	{
		//		if (searchEmail != null)
		//		{
		//			Console.WriteLine($"{Email} aleady exists");
		//			break;
		//		}
		//	}
		//}
		//else
		//{
		//	Console.WriteLine($"File not found: {userList} does not exist");
		//}
	}

	public void Register()
	{
		Users user = new Users();

		while (true)
		{
			Console.WriteLine("Email: ");
			user.Email = Console.ReadLine().ToLower();
			if (EmailExists(user.Email) == false)
			{
				break;
			}
		}
		Console.WriteLine("First Name: ");
		user.FirstName = Console.ReadLine().ToLower();
    }
}