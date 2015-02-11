
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Json;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Com.Facebook.Android;
using Object = Java.Lang.Object;

namespace TEGAAndroid
{
	[Activity (Label = "FriendsActivity")]			
	public class FriendsActivity : Activity
	{ 
		private Facebook mFacebook;
		private AsyncFacebookRunner mAsyncRunner;
		const String APP_ID = "251718781623570"; 
	
		Button menuBtn = null;
		Button fbFriendsBtn = null;
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			mFacebook = new Facebook (APP_ID);
			mAsyncRunner = new AsyncFacebookRunner (mFacebook);
			
			SessionStore.Restore (mFacebook, this);

			// Create your application here
			SetContentView(Resource.Layout.Friends);

			
			menuBtn=(Button)FindViewById<Button>(Resource.Id.buttonMenu);
			
			menuBtn.Click+=gotoMenuPage;

			fbFriendsBtn=(Button)FindViewById<Button>(Resource.Id.btnfbfriends);
			
			fbFriendsBtn.Click+=inviteFBfriends;
		}

		public void inviteFBfriends (object sender, EventArgs e)
		{
			Bundle parameters = new Bundle();
			parameters.PutString("message", "TEGA-Take the challenge. An gamification idea. You are heartly invited to join TEGA App.");
			parameters.PutString("title", "Invite Friends");
			mFacebook.Dialog(this, "apprequests", parameters, new AppRequestsListener(this));
		}

		//gotoMenu
		public void gotoMenuPage (object sender, EventArgs e)
		{
			Intent menuIntent = new Intent();
			menuIntent.SetClass(this, typeof(MenuActivity));
			menuIntent.SetFlags(ActivityFlags.ClearTop);
			StartActivity(menuIntent);
		}


		public class AppRequestsListener : BaseDialogListener 
		{
			public AppRequestsListener (FriendsActivity parent)
			{
				this.parent = parent;
			}
			FriendsActivity parent;
			
			public override void OnComplete(Bundle values) {
				Console.WriteLine("App request send");
				parent.StartActivity (typeof(FriendsActivity));
			}
			
			public void OnFacebookError(String error) {
				Console.WriteLine("Error: "+ error);
			}
			
			public void OnCancel() {
				Console.WriteLine("App request cancelled");
			}			
		} 
	}	
}



