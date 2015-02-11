
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
	[Activity (Label = "HistoryActivity")]			
	public class HistoryActivity : Activity
	{
		Button menuBtn = null;
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView(Resource.Layout.History);
			
			// Create your application here

			menuBtn=(Button)FindViewById<Button>(Resource.Id.buttonMenu);
			
			menuBtn.Click+=gotoMenuPage;
		}

		//gotoMenu
		public void gotoMenuPage (object sender, EventArgs e)
		{
			Intent menuIntent = new Intent();
			menuIntent.SetClass(this, typeof(MenuActivity));
			menuIntent.SetFlags(ActivityFlags.ClearTop);
			StartActivity(menuIntent);
		}

	}
}
