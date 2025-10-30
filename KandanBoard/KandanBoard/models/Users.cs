using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text.Json;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Threading.Tasks;


public class Users
{
	public int UserId { get; set; }
	public String FirstName { get; set; }
	public String LastName { get; set; }
	public String Email { get; set; }
	public String HashedPassword { get; set; }

	public Users(int userid, String firstname, String lastname, String email, String hashedpassword)
	{
		this.UserId = userid;
		this.FirstName = firstname;
		this.LastName = lastname;
		this.Email = email;
		this.HashedPassword = hashedpassword;
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
		Console.WriteLine("Last Name: ");
		user.LastName = Console.ReadLine().ToLower();
		Console.WriteLine(user.LastName);
		Console.WriteLine("Password: ");
		string unhashedPassword = Console.ReadLine();

		user.HashedPassword = HashPassword(unhashedPassword);
		Console.WriteLine(user.HashedPassword);
	}

    // using https://learn.microsoft.com/en-us/aspnet/core/security/data-protection/consumer-apis/password-hashing?view=aspnetcore-9.0
    public string HashPassword(string unhashedPassword)
	{
		byte[] salt = RandomNumberGenerator.GetBytes(128 / 8);
		Console.WriteLine($"Salt: {Convert.ToBase64String(salt)}");

		string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
			password: unhashedPassword!,
			salt: salt,
			prf: KeyDerivationPrf.HMACSHA256,
			iterationCount: 100000,
			numBytesRequested: 256 / 8));

		return hashed;
	}
}
