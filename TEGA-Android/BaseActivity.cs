using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Android.Util;
using TEGACore;

namespace TEGAAndroid
{
	public abstract class BaseActivity : Activity, LogiAssistContext
	{
		public const string SharedPrefsName = "logiassist-shared-prefs";
		private const string TAG = "BaseActivity";
		protected ISharedPreferences prefs = null;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			prefs = GetSharedPreferences (SharedPrefsName, FileCreationMode.Append);

			// Check the connection with the Server
			// If it is not connected yet, open a connection
			WebSocketManager.Instance.identifyDevice (this);
			WebSocketManager.Instance.connect ();

			doOnCreate (bundle);

			// Set the Typeface for all texts
			ViewGroup baseLayout = (ViewGroup)FindViewById (Resource.Id.baseLayout);
			setTypefaceForViewGroup (baseLayout);
		}

		protected abstract void doOnCreate (Bundle bundle);

		protected override void OnDestroy ()
		{
			base.OnDestroy ();

			// Force Gabarge Collection to recycle resources
			GC.Collect (0);
		}

		public void messageReceived (WsMessage message)
		{
			// REMARK : This method can't be directly overriden by the subclasses...
			Log.Info (TAG, "Message Received called with message " + message.ToString ());
			processReceivedMessage (message);
		}

		protected virtual void processReceivedMessage (WsMessage message)
		{
			Log.Info (TAG, "Processing Received message " + message.ToString ());
		}

		protected void goBackToPreviousActivity ()
		{
			Finish ();
		}

		public void setTypefaceForViewGroup (ViewGroup viewGroup)
		{
			if (viewGroup == null) {
				Log.Warn (TAG, "Could not get BaseLayout to set Typeface");
				return;
			}
			// Parse the ViewGroup children for TextView and set the Typeface
		/*	for (int i = 0; i < viewGroup.ChildCount; i++) {
				View view = viewGroup.GetChildAt (i);
				if (view is TextView) {
					((TextView)view).SetTypeface (TypefaceUtil.Instance.getTypeface (this, TypefaceUtil.RobotoRegular), Android.Graphics.TypefaceStyle.Normal);
				} else if (view is ImageView) {
					// Do nothing : simply ignore
				} else {
					setTypefaceForViewGroup ((ViewGroup)view);
				}
			}*/
		}
	}
}

