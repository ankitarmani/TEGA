
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace TEGACore
{
	public class User
	{
		public long Id { get; set; }
		public string Fullname { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
		public string Email { get; set; }
		public string Gender { get; set; }
		public DateTime BirthDate { get; set; }

		
		public User ()
		{
			Id = -1;
			Fullname = string.Empty;
			Username = string.Empty;
			Password = string.Empty;
			Email = string.Empty;
			Gender = string.Empty;
			BirthDate = DateTime.Now;
		}
		
		public User (long id, string fullname,  string username,  string password,  string email,  string gender, DateTime birthdate)
		{
			Id = id;
			Fullname = fullname;
			Username = username;
			Password = password;
			Email = email;
			Gender = gender;
			BirthDate = birthdate;
		}
	}
}

