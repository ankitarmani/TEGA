package tegaandroid;


public class LoginButton_LoginDialogListener
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.facebook.android.Facebook.DialogListener
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onCancel:()V:GetOnCancelHandler:Com.Facebook.Android.Facebook/IDialogListenerInvoker, Mono.Facebook\n" +
			"n_onComplete:(Landroid/os/Bundle;)V:GetOnComplete_Landroid_os_Bundle_Handler:Com.Facebook.Android.Facebook/IDialogListenerInvoker, Mono.Facebook\n" +
			"n_onError:(Lcom/facebook/android/DialogError;)V:GetOnError_Lcom_facebook_android_DialogError_Handler:Com.Facebook.Android.Facebook/IDialogListenerInvoker, Mono.Facebook\n" +
			"n_onFacebookError:(Lcom/facebook/android/FacebookError;)V:GetOnFacebookError_Lcom_facebook_android_FacebookError_Handler:Com.Facebook.Android.Facebook/IDialogListenerInvoker, Mono.Facebook\n" +
			"";
		mono.android.Runtime.register ("TEGAAndroid.LoginButton/LoginDialogListener, TEGA-Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", LoginButton_LoginDialogListener.class, __md_methods);
	}


	public LoginButton_LoginDialogListener ()
	{
		super ();
		if (getClass () == LoginButton_LoginDialogListener.class)
			mono.android.TypeManager.Activate ("TEGAAndroid.LoginButton/LoginDialogListener, TEGA-Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCancel ()
	{
		n_onCancel ();
	}

	private native void n_onCancel ();


	public void onComplete (android.os.Bundle p0)
	{
		n_onComplete (p0);
	}

	private native void n_onComplete (android.os.Bundle p0);


	public void onError (com.facebook.android.DialogError p0)
	{
		n_onError (p0);
	}

	private native void n_onError (com.facebook.android.DialogError p0);


	public void onFacebookError (com.facebook.android.FacebookError p0)
	{
		n_onFacebookError (p0);
	}

	private native void n_onFacebookError (com.facebook.android.FacebookError p0);

	java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
