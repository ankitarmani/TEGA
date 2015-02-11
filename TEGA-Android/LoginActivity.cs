using System;
using System.Collections.Generic;
using System.Json;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Util;
using Android.OS;
using Mono.Data.Sqlite;
using Com.Facebook.Android;
using Java.IO;
using Java.Net;
using Object = Java.Lang.Object;
using TEGACore;

namespace TEGAAndroid
{
	[Activity (ScreenOrientation = ScreenOrientation.Portrait, LaunchMode = LaunchMode.SingleTask)]
	public class LoginActivity : BaseActivity
	{
		//		string registrationRequest = Intent.GetStringExtra (LogiAssistConstants.IntentExtraRegistrationRequest);
		//		User request = SimpleJson.SimpleJson.DeserializeObject<WsRegisterRequest> (registrationRequest);
		//		// Set the new fields
		
		
		//private SQLLiteQueryBuilder temp = SQLLiteQueryBuilder.Instance;
		//		private SQLLiteDatabaseConnector connector = SQLLiteDatabaseConnector.Instance;
		//		private User user;
		private const string TAG = "LoginActivity";
		private EditText loginEditTxt = null;
		private EditText passwordEditTxt = null;
		private Button logInBtn = null;
		private TextView registerScreen = null;
		private TextView logTextView = null;
		private const string logSeparator = "\n";
		
		//FB stuff
		// Your Facebook Application ID must be set before running this example
		// See http://www.facebook.com/developers/createapp.php
		const String APP_ID = "251718781623570";
		private LoginButton loginFb;
		private TextView mText;
		private Facebook mFacebook;
		private AsyncFacebookRunner mAsyncRunner;
		
		#region implemented abstract members of BaseActivity
		
		protected override void doOnCreate (Bundle bundle)
		{	
			// Set our view from the "login" layout resource
			SetContentView (Resource.Layout.Login);
			
			loginEditTxt = (EditText)FindViewById (Resource.Id.loginEditTxt);
			passwordEditTxt = (EditText)FindViewById (Resource.Id.passwordEditTxt);
			logInBtn = (Button)FindViewById (Resource.Id.logInBtn);
			
			loginFb = (LoginButton) FindViewById (Resource.Id.login);
			//mText = (TextView) FindViewById (Resource.Id.fb_text);
			logInBtn.Click += logIn;
			
			
			registerScreen = (TextView)FindViewById (Resource.Id.link_to_register);
			//click on register link in login page.....
			registerScreen.Click += gotoRegisterPage; 
			
			mFacebook = new Facebook (APP_ID);
			mAsyncRunner = new AsyncFacebookRunner (mFacebook);
			
			SessionStore.Restore (mFacebook, this);
			SessionEvents.AddAuthListener (new SampleAuthListener (this));
			SessionEvents.AddLogoutListener (new SampleLogoutListener (this));
			loginFb.Init (this, mFacebook);
			
		}
		protected override void OnActivityResult (int requestCode, Result resultCode, Intent data)
		{
			mFacebook.AuthorizeCallback (requestCode, (int) resultCode, data);
		}
		
#endregion
		
		void gotoRegisterPage (object sender, EventArgs e)
		{
			StartActivity (typeof(RegisterActivity));
			//			Intent registerIntent = new Intent();
			//			registerIntent.SetClass(this, typeof(RegisterActivity));
			//			registerIntent.SetFlags(ActivityFlags.ClearTop);
			//			StartActivity(registerIntent);
		}
		void gotoMenuPage (object sender, EventArgs e){
			//Intent menuIntent = new Intent();
			//menuIntent.SetClass(this, typeof(MenuActivity));
			//menuIntent.SetFlags(ActivityFlags.ClearTop);
			StartActivity(typeof(MenuActivity));
		}
		
		
		protected override void processReceivedMessage (WsMessage message)
		{
			if (message.protocol == WsMessageProtocol.QueryError.GetValue ()) {
				Log.Warn (TAG, "The Server has thrown an error");
				Toast.MakeText (this, GetString (Resource.String.wsCommunicationProblem), ToastLength.Short).Show ();
				return;
			}
			
			if (message.type == CoreConstants.WsMsgTypeLogIn) {
				if (string.IsNullOrEmpty (message.data)) {
					RunOnUiThread (() => Toast.MakeText (this, GetString (Resource.String.loginErrorLoginFailed), ToastLength.Short).Show ());
					return;
				}
				
				User response = SimpleJson.SimpleJson.DeserializeObject<User> (message.data);
				if (response != null && response.userId > 0) {
					//					// Case : login success
					UserDataService.Instance.addUser (response);
					UserDataService.Instance.userId = response.userId;
					RunOnUiThread (delegate() {
						Toast.MakeText (this, "You are now logged in as " + response.firstName + " " + response.lastName, ToastLength.Short).Show (); 
					});
					
					StartActivity (typeof(MenuActivity));
				} else {
					// Case : login failed
					RunOnUiThread (() => Toast.MakeText (this, GetString (Resource.String.loginErrorLoginFailed), ToastLength.Short).Show ());
				}
				
			} else {
				Log.Warn (TAG, "The message type " + message.type + " is not supported by this Context");
			}
		}
		
		private void logIn (object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty (loginEditTxt.Text)) {
				Toast.MakeText (this, GetString (Resource.String.loginErrorEnterUserName), ToastLength.Short).Show (); 
			}
			
			if (string.IsNullOrEmpty (passwordEditTxt.Text)) {
				Toast.MakeText (this, GetString (Resource.String.loginErrorEnterPassword), ToastLength.Short).Show (); 
			}
			
			// Send the registration request
			WsLogInRequest request = new WsLogInRequest ();
			request.userName = loginEditTxt.Text;
			request.password = passwordEditTxt.Text;
			WebSocketManager.Instance.sendMessage (this, WsMessageProtocol.Query, CoreConstants.WebSocketServerAddress, CoreConstants.WsMsgTypeLogIn, SimpleJson.SimpleJson.SerializeObject (request));
			
		}
		
		// Facebook classes inside BaseActivity
		
		public class SampleAuthListener : SessionEvents.IAuthListener
		{
			public SampleAuthListener (LoginActivity parent)
			{
				this.parent = parent;
			}
			LoginActivity parent;
			
			public void OnAuthSucceed ()
			{
				Console.WriteLine("OnAuthSucceed");
				//parent.mText.Text = ("You have logged in! ");
				/*WsLogInRequest request = new WsLogInRequest ();
			request.userName = ;
			request.password = ;
			WebSocketManager.Instance.sendMessage (this, WsMessageProtocol.Query, CoreConstants.WebSocketServerAddress, CoreConstants.WsMsgTypeLogIn, SimpleJson.SimpleJson.SerializeObject (request));
			*/
				//Android.Content.Context.StartActivity = new Android.Content.Context.StartActivity(); 
				//Type c = Android.Content.Context.StartActivity(typeof(MenuActivity));
				//StartActivity(typeof(MenuActivity));
				
				Console.WriteLine("bingo..logged in to FB");
				//Intent menunIntent = new Intent();
				//menunIntent.SetClass(this.parent, typeof(MenuActivity));
				//menunIntent.SetFlags(ActivityFlags.ClearTop);
				//StartActivity(menunIntent);
				parent.StartActivity (typeof(MenuActivity));
			}
			
			public void OnAuthFail (String error)
			{
				//parent.mText.Text = ("Login Failed: " + error);
			}
		}
		
		public class SampleLogoutListener : SessionEvents.ILogoutListener
		{
			public SampleLogoutListener (LoginActivity parent)
			{
				this.parent = parent;
			}
			LoginActivity parent;
			
			public void OnLogoutBegin ()
			{
				//parent.mText.Text = ("Logging out...");
			}
			
			public void OnLogoutFinish ()
			{
				//parent.mText.Text = ("You have logged out! ");
			}
		}
		
		public class SampleRequestListener : BaseRequestListener
		{
			public SampleRequestListener (LoginActivity parent)
			{
				this.parent = parent;
			}
			LoginActivity parent;
			
			
			public override void OnComplete (String response, Object state)
			{
				try {
					// process the response here: executed in background thread
					Log.Debug ("Tega-Facebook", "Response: " + response);
					var json = (JsonObject) JsonValue.Parse (response);
					String name = json ["name"];
					String uid = json ["id"];
					String gender = json ["gender"];
					Console.WriteLine("Your Facebook Personal Data :" + json.ToString());
					
					// then post the processed result back to the UI thread
					// if we do not do this, an runtime exception will be generated
					// e.g. "CalledFromWrongThreadException: Only the original
					// thread that created a view hierarchy can touch its views."
					parent.RunOnUiThread (delegate {
						//parent.mText.Text = ("Hello there, " + name + uid + gender + "!");
					});
					gotoMenuePage();
					//} catch (JSONException e) {
					//	Log.Warn ("Facebook-Example", "JSON Error in response");
					
					
				} catch (FacebookError e) {
					Log.Warn ("Facebook-Example", "Facebook Error: " + e.Message);
				}
			}
			void gotoMenuePage ()
			{
				/*//StartActivity (typeof(RegisterActivity));
				Intent menuIntent = new Intent();
				menuIntent.SetClass(this, typeof(TEGAAndroid.MenuActivity));
				menuIntent.SetFlags(ActivityFlags.ClearTop);
				StartActivity(menuIntent);*/
					Console.WriteLine("GotoMenuePage");
			}
		}
		
		
		
		
		
		
		
		
		
	}
	
	//Facebook classes outside Base Activity
	public abstract class BaseDialogListener : Object, Facebook.IDialogListener
	{
		public abstract void OnComplete (Bundle bundle);
		
		public void OnFacebookError (FacebookError e)
		{
			e.PrintStackTrace ();
		}
		
		public void OnError (DialogError e)
		{
			e.PrintStackTrace ();        
		}
		
		public void OnCancel ()
		{        
		}
	}
	
	public abstract class BaseRequestListener : Object, AsyncFacebookRunner.IRequestListener
	{
		
		public void OnFacebookError (FacebookError e, Object state)
		{
			Log.Error ("Facebook", e.Message);
			e.PrintStackTrace ();
		}
		
		public void OnFileNotFoundException (FileNotFoundException e,
		                                     Object state)
		{
			Log.Error ("Facebook", e.Message);
			e.PrintStackTrace ();
		}
		
		public void OnIOException (Java.IO.IOException e, Object state)
		{
			Log.Error ("Facebook", e.Message);
			e.PrintStackTrace ();
		}
		
		public void OnMalformedURLException (MalformedURLException e,
		                                     Object state)
		{
			Log.Error ("Facebook", e.Message);
			e.PrintStackTrace ();
		}    
		
		public abstract void OnComplete (string response, Java.Lang.Object state);
	}
	
	public class LoginButton : ImageButton
	{
		
		private Facebook mFb;
		private Handler mHandler;
		private SessionListener mSessionListener;
		private String[] mPermissions;
		private Activity mActivity;
		
		public LoginButton (Context context)
			: base (context)
		{
			mSessionListener = new SessionListener (this);
		}
		
		public LoginButton (Context context, IAttributeSet attrs)
			: base (context, attrs)
		{
			mSessionListener = new SessionListener (this);
		}
		
		public LoginButton (Context context, IAttributeSet attrs, int defStyle)
			: base (context, attrs, defStyle)
		{
			mSessionListener = new SessionListener (this);
		}
		
		public void Init (Activity activity, Facebook fb)
		{
			Init (activity, fb, new String[] {});
		}
		
		public void Init (Activity activity, Facebook fb, String[] permissions)
		{
			mActivity = activity;
			mFb = fb;
			mPermissions = permissions;
			mHandler = new Handler ();
			
			SetBackgroundColor (Color.Transparent);
			SetAdjustViewBounds (true);
			SetImageResource (fb.IsSessionValid ?
			                  TEGAAndroid.Resource.Drawable.logout_button : 
			                  TEGAAndroid.Resource.Drawable.login_button);
			DrawableStateChanged ();
			
			SessionEvents.AddAuthListener (mSessionListener);
			SessionEvents.AddLogoutListener (mSessionListener);
			SetOnClickListener (new ButtonOnClickListener (this));
		}
		
		class ButtonOnClickListener : Object, IOnClickListener
		{
			public ButtonOnClickListener (LoginButton parent)
			{
				this.parent = parent;
			}
			LoginButton parent;
			
			public void OnClick (View arg0)
			{
				if (parent.mFb.IsSessionValid) {
					SessionEvents.OnLogoutBegin ();
					AsyncFacebookRunner asyncRunner = new AsyncFacebookRunner (parent.mFb);
					asyncRunner.Logout (parent.Context, new LogoutRequestListener (parent));
				} else {
					parent.mFb.Authorize (parent.mActivity, parent.mPermissions,
					                      new LoginDialogListener ());
				}
			}
		}
		
		class LoginDialogListener : Object, Facebook.IDialogListener
		{
			public void OnComplete (Bundle values)
			{
				SessionEvents.OnLoginSuccess ();
			}
			
			public void OnFacebookError (FacebookError error)
			{
				SessionEvents.OnLoginError (error.Message);
			}
			
			public void OnError (DialogError error)
			{
				SessionEvents.OnLoginError (error.Message);
			}
			
			public void OnCancel ()
			{
				SessionEvents.OnLoginError ("Action Canceled");
			}
		}
		
		private class LogoutRequestListener : BaseRequestListener
		{
			public LogoutRequestListener (LoginButton parent)
			{
				this.parent = parent;
			}
			
			LoginButton parent;
			
			public override void OnComplete (String response, Object state)
			{
				// callback should be run in the original thread, 
				// not the background thread
				parent.mHandler.Post (delegate {
					SessionEvents.OnLogoutFinish ();
				});
			}
		}
		
		class SessionListener : Object, SessionEvents.IAuthListener, SessionEvents.ILogoutListener
		{        
			public SessionListener (LoginButton parent)
			{
				this.parent = parent;
			}
			
			LoginButton parent;
			
			public void OnAuthSucceed ()
			{
				parent.SetImageResource (TEGAAndroid.Resource.Drawable.logout_button);
				SessionStore.Save (parent.mFb, parent.Context);
			}
			
			public void OnAuthFail (String error)
			{
			}
			
			public void OnLogoutBegin ()
			{           
			}
			
			public void OnLogoutFinish ()
			{
				SessionStore.Clear (parent.Context);
				parent.SetImageResource (TEGAAndroid.Resource.Drawable.login_button);
			}
		}    
	}
	public class SessionEvents
	{
		
		private static LinkedList<IAuthListener> mAuthListeners = 
			new LinkedList<IAuthListener> ();
		private static LinkedList<ILogoutListener> mLogoutListeners = 
			new LinkedList<ILogoutListener> ();
		
		/**
     * Associate the given listener with this Facebook object. The listener's
     * callback interface will be invoked when authentication events occur.
     * 
     * @param listener
     *            The callback object for notifying the application when auth
     *            events happen.
     */
		public static void AddAuthListener (IAuthListener listener)
		{
			mAuthListeners.AddLast (listener);
		}
		
		/**
     * Remove the given listener from the list of those that will be notified
     * when authentication events occur.
     * 
     * @param listener
     *            The callback object for notifying the application when auth
     *            events happen.
     */
		public static void RemoveAuthListener (IAuthListener listener)
		{
			mAuthListeners.Remove (listener);
		}
		
		/**
     * Associate the given listener with this Facebook object. The listener's
     * callback interface will be invoked when logout occurs.
     * 
     * @param listener
     *            The callback object for notifying the application when log out
     *            starts and finishes.
     */
		public static void AddLogoutListener (ILogoutListener listener)
		{
			mLogoutListeners.AddLast (listener);
		}
		
		/**
     * Remove the given listener from the list of those that will be notified
     * when logout occurs.
     * 
     * @param listener
     *            The callback object for notifying the application when log out
     *            starts and finishes.
     */
		public static void RemoveLogoutListener (ILogoutListener listener)
		{
			mLogoutListeners.Remove (listener);
		}
		
		public static void OnLoginSuccess ()
		{
			foreach (var listener in mAuthListeners) {
				listener.OnAuthSucceed ();
			}
		}
		
		public static void OnLoginError (String error)
		{
			foreach (var listener in mAuthListeners) {
				listener.OnAuthFail (error);
			}
		}
		
		public static void OnLogoutBegin ()
		{
			foreach (var l in mLogoutListeners) {
				l.OnLogoutBegin ();
			}
		}
		
		public static void OnLogoutFinish ()
		{
			foreach (var l in mLogoutListeners) {
				l.OnLogoutFinish ();
			}   
		}
		
		/**
     * Callback interface for authorization events.
     *
     */
		public interface IAuthListener
		{
			
			/**
         * Called when a auth flow completes successfully and a valid OAuth 
         * Token was received.
         * 
         * Executed by the thread that initiated the authentication.
         * 
         * API requests can now be made.
         */
			void OnAuthSucceed ();
			
			/**
         * Called when a login completes unsuccessfully with an error. 
         *  
         * Executed by the thread that initiated the authentication.
         */
			void OnAuthFail (String error);
		}
		
		/**
     * Callback interface for logout events.
     *
     */ 
		public interface ILogoutListener
		{
			/**
         * Called when logout begins, before session is invalidated.  
         * Last chance to make an API call.  
         * 
         * Executed by the thread that initiated the logout.
         */
			void OnLogoutBegin ();
			
			/**
         * Called when the session information has been cleared.
         * UI should be updated to reflect logged-out state.
         * 
         * Executed by the thread that initiated the logout.
         */
			void OnLogoutFinish ();
		}
		
	}
	
	public class SessionStore
	{    
		const string TOKEN = "access_token";
		const string EXPIRES = "expires_in";
		const string KEY = "facebook-session";
		
		public static bool Save (Facebook session, Context context)
		{
			var editor = context.GetSharedPreferences (KEY,FileCreationMode.Private).Edit ();
			editor.PutString (TOKEN, session.AccessToken);
			editor.PutLong (EXPIRES, session.AccessExpires);
			return editor.Commit ();
		}
		
		public static bool Restore (Facebook session, Context context)
		{
			var savedSession = context.GetSharedPreferences (KEY, FileCreationMode.Private);
			session.AccessToken = savedSession.GetString (TOKEN, null);
			session.AccessExpires = savedSession.GetLong (EXPIRES, 0);
			return session.IsSessionValid;
		}
		
		public static void Clear (Context context)
		{
			var editor = context.GetSharedPreferences (KEY, FileCreationMode.Private).Edit ();
			editor.Clear ();
			editor.Commit ();
		}    
	}
	
}