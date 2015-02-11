
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
	[Activity (Label = "CarActivity")]			
	public class CarActivity : Activity
	{
		TextView carType=null;
		ImageView carImg= null;
		Button menuBtn=null;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			var metrics = Resources.DisplayMetrics;
				var widthInDp = ConvertPixelsToDp(metrics.WidthPixels);
				var heightInDp = ConvertPixelsToDp(metrics.HeightPixels);


			// Create your application here
			SetContentView(Resource.Layout.Car);

			//Giving values to Car Type
			carType=(TextView)(FindViewById(Resource.Id.textViewCarType));
			carType.Text="Mercedes";//Text should be taken from DB...TODO

			//Value of tablet number
			//tabletNum=(TextView)FindViewById(Resource.Id.textViewTabletNumResult);
			//tabletNum.Text="DE 30 505-- "; //Text should be taken from DB...TODO
			//Set the size of the image
			carImg=(ImageView)FindViewById(Resource.Id.imageViewCar);
			//var oldWidth=carImg.Width;
			//carImg.Width.Equals(widthInDp/4);
			//var oldHeight=carImg.Height;
			//carImg.Height.Equals(oldHeight/4);

			//-------------------------------------

			//give the position of menu and click function
			menuBtn=(Button)FindViewById(Resource.Id.buttonMenu);
			menuBtn.Click+=gotoMenuPage;
			//menuBtn.Layout.gro


		}
		// menu click function
		public void gotoMenuPage (object sender, EventArgs e)
		{
			Intent menuIntent = new Intent();
			menuIntent.SetClass(this, typeof(MenuActivity));
			menuIntent.SetFlags(ActivityFlags.ClearTop);
			StartActivity(menuIntent);
		}
		private int ConvertPixelsToDp(float pixelValue)
		{
			var dp = (int) ((pixelValue)/Resources.DisplayMetrics.Density);
			return dp;
		}
	}
}

