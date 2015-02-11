
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using Android.Util;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Media;
using Android.Provider;
using Android.Graphics;
using Android.Content.PM;
using TEGACore;

namespace TEGAAndroid
{
	[Activity (ScreenOrientation = ScreenOrientation.Portrait, LaunchMode = LaunchMode.SingleTask)]
	public class RegisterActivity : BaseActivity
	{
		//private readonly static int SelectPicture = 2;
		private const string TAG = "RegistrationActivity";
		private const int DATE_PICKER_DIALOG_ID = 11;
		//private const int GENDER_PICKER_DIALOG_ID = 1001;
		//private const int LANGUAGE_PICKER_DIALOG_ID = 1002;
		private DateTime dateOfBirth = DateTime.Today;
		private int selectedGender = 0;
		//private int selectedGender = 0;
		//private int selectedLanguage = 0;
		// ActionBar
		//private Button backBtn = null;
		private Button registerBtn = null;
		private Button imageUploadBtn = null;
		// Base Info
		private EditText firstNameEditTxt = null;
		private EditText lastNameEditTxt = null;
		private EditText userNameEditTxt = null;
		private TextView userNameCheckResultTxtView = null;
		private EditText emailEditTxt = null;
		private EditText passwordEditTxt = null;
		private EditText retypePasswordEditTxt = null;
		private EditText dateOfBirthEditTxt = null;
		private RadioButton radioMale = null;
		private RadioButton radioFemale = null;
		private Spinner vehicleTypeSpinner = null;
		private ImageView carImageView = null;
		private TextView logTextView = null;
		private const string logSeparator = "\n";
		// SVN TEsting........
		//menuButton
		Button menuBtn = null;
		
		
		protected override void doOnCreate (Bundle bundle)
		{
			SetContentView(Resource.Layout.Register);
//			menuBtn=(Button)FindViewById<Button>(Resource.Id.menuButton);
			//			menuBtn.Click+=gotoMenuPage;
			
			firstNameEditTxt = (EditText) FindViewById(Resource.Id.name);
			lastNameEditTxt = (EditText) FindViewById(Resource.Id.lastName);
			userNameEditTxt = (EditText) FindViewById(Resource.Id.username);
			emailEditTxt = (EditText) FindViewById(Resource.Id.email);
			passwordEditTxt = (EditText) FindViewById(Resource.Id.password);
			retypePasswordEditTxt = FindViewById<EditText> (Resource.Id.passwordRetype);
			dateOfBirthEditTxt = (EditText) FindViewById(Resource.Id.birthDate);
			radioMale = (RadioButton) FindViewById(Resource.Id.radioMale);
			radioFemale = (RadioButton) FindViewById(Resource.Id.radioFemale);
			registerBtn = (Button) FindViewById(Resource.Id.btnRegister);
			imageUploadBtn = (Button) FindViewById(Resource.Id.btnImageUpload);
			vehicleTypeSpinner = (Spinner) FindViewById(Resource.Id.vehicleTypeSpinner);
			carImageView = (ImageView) FindViewById(Resource.Id.carImageView);
			userNameCheckResultTxtView = (TextView) FindViewById(Resource.Id.userNameCheckResultTxtView);
			
			imageUploadBtn.Click  += uploadPhotoOnClick;
			
			//Checking how to get data from spinner
			//			vehicleTypeSpinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs> (spinner_ItemSelected);
			//			var adapter = ArrayAdapter.CreateFromResource (
			//				this, Resource.Array.vehicleType, Android.Resource.Layout.SimpleSpinnerItem);
			//			
			//			adapter.SetDropDownViewResource (Android.Resource.Layout.SimpleSpinnerDropDownItem);
			//			vehicleTypeSpinner.Adapter = adapter;
			
			registerBtn.Click += registerButtonClicked;
			
			userNameEditTxt.AfterTextChanged += userNameTextChanged;
			dateOfBirthEditTxt.Touch += delegate(object sender, Android.Views.View.TouchEventArgs e) {
				ShowDialog (DATE_PICKER_DIALOG_ID);
			};
			
			radioMale.Click += RadioButtonClick;
			radioFemale.Click += RadioButtonClick;
		}
		
		//		//gotoMenu
		//		public void gotoMenuPage (object sender, EventArgs e)
		//		{
		//			Intent menuIntent = new Intent();
		//			menuIntent.SetClass(this, typeof(menuActivity));
		//			menuIntent.SetFlags(ActivityFlags.ClearTop);
		//			StartActivity(menuIntent);
		//		}
		//		//private string _imageUri;
		
		private Boolean isMounted
		{
			get
			{
				return Android.OS.Environment.ExternalStorageState.Equals(Android.OS.Environment.MediaMounted);
			}
		}

		public void uploadPhotoOnClick (object sender, EventArgs eventArgs)
		{
			//var intent = new Intent(MediaStore.Images.Media);
			
			Intent browseFile = new Intent(Intent.ActionGetContent); 
			browseFile.SetType("image/*"); 
			browseFile.AddCategory(Intent.CategoryOpenable); 
			StartActivityForResult(browseFile,0); 
			//			string selection = "";
			//			string[] selectionArgs = null;
			
			//			var mediaCursor = ContentResolver.Query(MediaStore.Images.Thumbnails.ExternalContentUri,
			//			                                        new string[] {MediaStore.MediaColumns.Id}, 
			//													selection,selectionArgs, null);
			//			
			//			var imageId = mediaCursor.GetInt(0);
			//			
			//			var bitmap = MediaStore.Images.Thumbnails.GetThumbnail(
			//				ContentResolver, imageId, ThumbnailKind.MicroKind,
			//				new BitmapFactory.Options() {InSampleSize = 1});
			//			carImageView.SetImageBitmap(bitmap);
		}


		
		public void RadioButtonClick (object sender, EventArgs e)
		{
			RadioButton rb = (RadioButton)sender;
			Toast.MakeText (this, rb.Text, ToastLength.Short).Show();
			if (rb.Text == "Female")
				selectedGender = 1;
			if (rb.Text == "Male")
				selectedGender = 0;
		}
		

		protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
		{
			base.OnActivityResult(requestCode, resultCode, data); 
			switch (requestCode) 
			{ 
			case 0: 
				if (resultCode == Result.Ok) 
				{ 
					Android.Net.Uri selectedURI = data.Data; 
					// at this stage you have a URI for the selected image and, in most cases, you can work directly with that 
					// and you don't need to know what the file path and name are 
					//data.Data
					Android.Graphics.Bitmap mBitmap = null;
					mBitmap = Android.Provider.MediaStore.Images.Media.GetBitmap(this.ContentResolver, selectedURI);
					carImageView.SetImageBitmap(mBitmap);
				} 
				break; 
			}
			/*var uploadIntent = new Intent();
			uploadIntent.PutExtra("ImageUri", data.DataString);
			this.StartActivity(uploadIntent);*/
		}
		
		
		
		
		private void spinner_ItemSelected (object sender, AdapterView.ItemSelectedEventArgs e)
		{
			Spinner spinner = (Spinner)sender;
			
			string toast = string.Format ("Vehicle Type is {0}", spinner.GetItemAtPosition (e.Position));
			Toast.MakeText (this, toast, ToastLength.Short).Show ();
		}
		
		private void userNameTextChanged (object sender, Android.Text.AfterTextChangedEventArgs e)
		{
			if (string.IsNullOrEmpty (userNameEditTxt.Text)) {
				//				userNameCheckResultTxtView.Text = "";
				//				string userExtra = Intent.GetStringExtra ("user");
				//				User loggedInUser = SimpleJson.SimpleJson.DeserializeObject<User> (userExtra);
				//				UserDataService.Instance.addUser (loggedInUser);
				//				writeLogStatus (CoreConstants.WsDataOk);
				return;
			}
			
			WsCheckUserNameAvailRequest request = new WsCheckUserNameAvailRequest ();
			request.userName = userNameEditTxt.Text;
			WebSocketManager.Instance.sendMessage (this, WsMessageProtocol.Query, CoreConstants.WebSocketServerAddress, CoreConstants.WsMsgTypeCheckUserNameAvail, SimpleJson.SimpleJson.SerializeObject (request));
			
		}
		private bool isFormValid ()
		{
			
			// Check the mandatory fields
			if (string.IsNullOrEmpty (firstNameEditTxt.Text)
			    || string.IsNullOrEmpty (lastNameEditTxt.Text)
			    || string.IsNullOrEmpty (userNameEditTxt.Text)
			    || string.IsNullOrEmpty (emailEditTxt.Text)
			    || string.IsNullOrEmpty (passwordEditTxt.Text)
			   	|| string.IsNullOrEmpty (retypePasswordEditTxt.Text)
			    || string.IsNullOrEmpty (dateOfBirthEditTxt.Text))
				//				|| string.IsNullOrEmpty (genderEditTxt.Text))
				//				|| string.IsNullOrEmpty (languageEditTxt.Text))
			{
				Toast.MakeText (this, GetString (Resource.String.registrationMandatoryFieldNull), ToastLength.Short).Show ();
				return false;
			}
			//checked The password
						if (passwordEditTxt.Text != retypePasswordEditTxt.Text) {
							Toast.MakeText (this, GetString (Resource.String.registrationRetypePassword), ToastLength.Short).Show ();
							return false;
						}
			
			return true;
		}
		/*protected override void OnStop ()
		{
			fullNameEditTxt = null;
			userNameEditTxt = null;
			emailEditTxt = null;
			passwordEditTxt = null;
			dateOfBirthEditTxt = null;
			radioMale.Checked = true;
			radioFemale = null;
			registerBtn = null;
		}*/
		
		protected override Dialog OnCreateDialog (int id)
		{
			AlertDialog.Builder alert = new AlertDialog.Builder (this);
			if (id == DATE_PICKER_DIALOG_ID) {
				return new DatePickerDialog (this, setDateOfBirth, dateOfBirth.Year, dateOfBirth.Month, dateOfBirth.Day);
			}
			else
				return base.OnCreateDialog(id);
		}
		
		private void setDateOfBirth (object sender, DatePickerDialog.DateSetEventArgs e)
		{
			dateOfBirth = e.Date;
			dateOfBirthEditTxt.Text = e.Date.ToString ("d");
		}
		
		void registerButtonClicked (object sender, EventArgs e)
		{
			if (isFormValid () == true) {
				WsRegisterRequest request = new WsRegisterRequest ();
				request.firstName = firstNameEditTxt.Text;
				request.lastName = lastNameEditTxt.Text;
				request.userName = userNameEditTxt.Text;
				request.email = emailEditTxt.Text;
				request.jobTitle = "dadad";
				request.password = passwordEditTxt.Text;
				request.dateOfBirth = dateOfBirth.Ticks;
				request.dateOfBirthDay = dateOfBirth.Day;
				request.dateOfBirthMonth = dateOfBirth.Month - 1;
				request.dateOfBirthYear = dateOfBirth.Year;
				request.gender = selectedGender;
				request.language = "en";
				WebSocketManager.Instance.sendMessage (this, WsMessageProtocol.Query, CoreConstants.WebSocketServerAddress, CoreConstants.WsMsgTypeRegister, SimpleJson.SimpleJson.SerializeObject (request));
				
			}
		}
		protected override void processReceivedMessage (WsMessage message)
		{
			if (message.protocol == WsMessageProtocol.QueryError.GetValue ()) {
				Log.Warn (TAG, "The Server has thrown an error");
				Toast.MakeText (this, GetString (Resource.String.wsCommunicationProblem), ToastLength.Short).Show ();
				return;
			}
			
			if (message.type == CoreConstants.WsMsgTypeCheckUserNameAvail) {
				if (message.data == CoreConstants.WsDataOk) {
					RunOnUiThread(delegate {
						userNameCheckResultTxtView.Visibility = ViewStates.Visible;
						userNameCheckResultTxtView.Text = GetString (Resource.String.registrationMsgUserNameAvailable);
					});
					//					RunOnUiThread (() => userNameCheckResultTxtView.Text = GetString (Resource.String.registrationMsgUserNameAvailable));
				} else {
					RunOnUiThread(delegate {
						userNameCheckResultTxtView.Visibility = ViewStates.Visible;
						userNameCheckResultTxtView.Text = GetString (Resource.String.registrationMsgUserNameNotAvailable);
					});
					//					RunOnUiThread (() => userNameCheckResultTxtView.Text = GetString (Resource.String.registrationMsgUserNameNotAvailable));
				}
				
			} else if (message.type == CoreConstants.WsMsgTypeRegister) {
				if (message.data == CoreConstants.WsDataOk) {
					RunOnUiThread (() => Toast.MakeText (this, GetString (Resource.String.registrationSuccessful), ToastLength.Short).Show ());
					
					// REMARK : we don't go back to the previous activity,
					// we need to go back to the Login activity and empty the activity stack
					Intent loginIntent = new Intent();
					loginIntent.SetClass(this, typeof(LoginActivity));
					loginIntent.SetFlags(ActivityFlags.ClearTop);
					StartActivity(loginIntent);
					
					//					string userExtra = Intent.GetStringExtra ("user");
					//					User loggedInUser = SimpleJson.SimpleJson.DeserializeObject<User> (userExtra);
					//					UserDataService.Instance.addUser (loggedInUser);
					//					writeLogStatus (CoreConstants.WsDataOk);
					
				} else {
					RunOnUiThread (() => Toast.MakeText (this, GetString (Resource.String.registrationFailed), ToastLength.Short).Show ());
				}
				
			} else {
				Log.Warn (TAG, "The message type " + message.type + " is not supported by this Context");
			}
		}
		private void writeLog (string logMessage)
		{
			logTextView.Text = logTextView.Text + logMessage + logSeparator;
		}
		
		
		void writeLogStatus (string wsDataOk)
		{
			logTextView.Text = logTextView.Text + wsDataOk + logSeparator;
		}
	}
	
	
}



