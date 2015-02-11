
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
using TEGACore;

namespace TEGAAndroid
{
	[Activity (Label = "LeaderboardActivity")]			
	public class LeaderboardActivity : Activity
	{
		private TextView fullNameEditTxt = null;
		//private ImageView userPicture = null;
		Button menuBtn = null;
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Create your application here
			SetContentView(Resource.Layout.Leaderboard);

			menuBtn=(Button)FindViewById<Button>(Resource.Id.buttonMenu);			
			menuBtn.Click+=gotoMenuPage;
		}
	
		protected override void OnStart ()
		{
			base.OnStart ();
			
			fullNameEditTxt = FindViewById<TextView>(Resource.Id.textViewLeadName1);
			//userPicture = FindViewById<ImageView>(Resource.Id.imageViewLeadPic1);

			long userId = UserDataService.Instance.userId;
			User user = UserDataService.Instance.getUser(userId);
			fullNameEditTxt.Text = user.firstName + " " + user.lastName;
	
		}

		public void gotoMenuPage (object sender, EventArgs e)
		{
			Intent menuIntent = new Intent();
			menuIntent.SetClass(this, typeof(MenuActivity));
			menuIntent.SetFlags(ActivityFlags.ClearTop);
			StartActivity(menuIntent);
		}
	}
}

