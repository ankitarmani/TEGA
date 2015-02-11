
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

namespace TEGAAndroid
{
	[Activity (Label = "MenuActivity")]			
	public class MenuActivity : Activity
	{
		private Button homeBtn=null;
		private Button profileBtn=null;
		private Button carBtn=null;
		private Button friendsBtn=null;
		private Button awardsBtn=null;
		private Button leadBtn=null;
		private Button historyBtn=null;
		private Button settingsBtn=null;
		private Button logoutBtn=null;
		
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			
			// Create your application here
			SetContentView(Resource.Layout.Menu);
			
			//Click home page
			homeBtn=(Button)FindViewById(Resource.Id.buttonHome);
			homeBtn.Click+=gotoHome;
			
			//Click profile page
			profileBtn=(Button)FindViewById(Resource.Id.buttonProfile);
			profileBtn.Click+=gotoProfile;
			
			//Click Car page
			carBtn=(Button)FindViewById(Resource.Id.buttonCar);
			carBtn.Click+=gotoCar;
			
			//Click Friends page
			friendsBtn=(Button)FindViewById(Resource.Id.buttonFriends);
			friendsBtn.Click+=gotoFriends;
			
			//Click Awards page
			awardsBtn=(Button)FindViewById(Resource.Id.buttonAwards);
			awardsBtn.Click+=gotoAwards;
			
			//Click Leaderboard page
			leadBtn=(Button)FindViewById(Resource.Id.buttonLeaderboard);
			leadBtn.Click+=gotoLeaderboard;
			
			//Click history page
			historyBtn=(Button)FindViewById(Resource.Id.buttonHistory);
			historyBtn.Click+=gotoHistory;
			
			//Click settings page
			settingsBtn=(Button)FindViewById(Resource.Id.buttonSettings);
			settingsBtn.Click+=gotoSettings;
			
			//Click Login page 
			logoutBtn=(Button)FindViewById(Resource.Id.buttonLogout);
			logoutBtn.Click+=gotoLogin;
		}
		
		public void gotoLogin (object sender, EventArgs e)
		{
			Intent loginIntent = new Intent();
			loginIntent.SetClass(this, typeof(LoginActivity));
			loginIntent.SetFlags(ActivityFlags.ClearTop);
			StartActivity(loginIntent);
		}
		
		
		public void gotoHome (object sender, EventArgs e)
		{
			Intent homeIntent = new Intent();
			homeIntent.SetClass(this, typeof(HomeActivity));
			homeIntent.SetFlags(ActivityFlags.ClearTop);
			StartActivity(homeIntent);
		}
		public void gotoProfile (object sender, EventArgs e)
		{
			Intent profileIntent = new Intent();
			profileIntent.SetClass(this, typeof(ProfileActivity));
			profileIntent.SetFlags(ActivityFlags.ClearTop);
			StartActivity(profileIntent);
		}
		public void gotoCar (object sender, EventArgs e)
		{
			Intent carIntent = new Intent();
			carIntent.SetClass(this, typeof(CarActivity));
			carIntent.SetFlags(ActivityFlags.ClearTop);
			StartActivity(carIntent);
		}
		public void gotoFriends (object sender, EventArgs e)
		{
			Intent friendsIntent = new Intent();
			friendsIntent.SetClass(this, typeof(FriendsActivity));
			friendsIntent.SetFlags(ActivityFlags.ClearTop);
			StartActivity(friendsIntent);
		}
		public void gotoAwards (object sender, EventArgs e)
		{
			Intent awardsIntent = new Intent();
			awardsIntent.SetClass(this, typeof(AwardsActivity));
			awardsIntent.SetFlags(ActivityFlags.ClearTop);
			StartActivity(awardsIntent);
		}
		public void gotoLeaderboard (object sender, EventArgs e)
		{
			Intent leaderboardIntent = new Intent();
			leaderboardIntent.SetClass(this, typeof(LeaderboardActivity));
			leaderboardIntent.SetFlags(ActivityFlags.ClearTop);
			StartActivity(leaderboardIntent);
		}
		public void gotoHistory (object sender, EventArgs e)
		{
			Intent homeIntent = new Intent();
			homeIntent.SetClass(this, typeof(HistoryActivity));
			homeIntent.SetFlags(ActivityFlags.ClearTop);
			StartActivity(homeIntent);
		}
		public void gotoSettings (object sender, EventArgs e)
		{
			Intent settingsIntent = new Intent();
			settingsIntent.SetClass(this, typeof(SettingsActivity));
			settingsIntent.SetFlags(ActivityFlags.ClearTop);
			StartActivity(settingsIntent);
		}
		public void gotoLogout (object sender, EventArgs e)
		{
			//to be implemeted
		}
	}
}

