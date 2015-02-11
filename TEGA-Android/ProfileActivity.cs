
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
using Android.Content.PM;
using TEGACore;

namespace TEGAAndroid
{
	[Activity (ScreenOrientation = ScreenOrientation.Portrait, LaunchMode = LaunchMode.SingleTask)]		
	public class ProfileActivity : Activity
	{
		private TextView fullNameEditTxt = null;
		private TextView birthDateEditTxt = null;
		Button menuBtn = null;
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Create your application here
			SetContentView(Resource.Layout.Profile);
			menuBtn=(Button)FindViewById<Button>(Resource.Id.buttonMenu);
			
			menuBtn.Click+=gotoMenuPage;
		}
		protected override void OnStart ()
		{
			base.OnStart ();

			fullNameEditTxt = FindViewById<TextView>(Resource.Id.textViewProfName);
			birthDateEditTxt = FindViewById<TextView>(Resource.Id.textViewBirthDate);
			
			
			long userId = UserDataService.Instance.userId;
			User user = UserDataService.Instance.getUser(userId);
			fullNameEditTxt.Text = user.firstName + " " + user.lastName;
			birthDateEditTxt.Text = user.email;
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

