
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
using Android.Util;
using TEGACore;
using System.IO;

namespace TEGAAndroid
{
	[Activity (MainLauncher = true, NoHistory = true)]			
	public class SplashActivity : Activity
	{
		private const string TAG = "SplashActivity";

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			if (isDatabaseReady () == false) {
				Toast.MakeText (this, "The database could not be prepared...", ToastLength.Short).Show (); 
				Finish ();
				return;
			}
			// Create your application here
			StartActivity (typeof (LoginActivity));
			
		}

		private bool isDatabaseReady ()
		{
			Log.Info (TAG, "Checking ExternalStorage state...");
			if (FileManager.Instance.isExternalStorageMounted () == false) {
				Log.Error (TAG, "Could not prepare the Database because the ExternalStorage is not mounted");
				return false;
			}
			Log.Info (TAG, "ExternalStorage mounted");
			
			string databaseLocation = Path.Combine (Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, CoreConstants.FolderNameLogiAssist, CoreConstants.DatabaseName);
			Log.Info (TAG, "Checking if Database exists : " + databaseLocation);
			if (FileManager.Instance.fileExists (databaseLocation) == false) {
				Log.Info (TAG, "Database doesn't exist -> Creating a new database");
				try {
					Log.Info (TAG, "Creating database file on ExternalStorage");
					string databasePath = Path.Combine (Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, CoreConstants.FolderNameLogiAssist);
					FileManager.Instance.createDirectory(databasePath);
					FileManager.Instance.createFile (databaseLocation);
					Log.Info (TAG, "Database file created on ExternalStorage");
					
					// Create the necessary tables
					Log.Info (TAG, "Creating tables in the database");
					createLiferayTables ();
					Log.Info (TAG, "Tables created in the database");
					return true;
				} catch (Java.Lang.Exception e) {
					Log.Error (TAG, e, "Could not create database");
					return false;
				}
			} else {
				Log.Info (TAG, "Database already exists");
				return true;
			}
		}

		private void createLiferayTables ()
		{
			UserDataService.Instance.createTable ();
			ContactDataService.Instance.createTable ();
			AddressDataService.Instance.createTable ();
			PhoneDataService.Instance.createTable ();
			ListTypeDataService.Instance.createTable ();
			OrganizationDataService.Instance.createTable ();
		}
	}

}

