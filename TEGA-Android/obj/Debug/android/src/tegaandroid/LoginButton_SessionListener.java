package tegaandroid;


public class LoginButton_SessionListener
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("TEGAAndroid.LoginButton/SessionListener, TEGA-Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", LoginButton_SessionListener.class, __md_methods);
	}


	public LoginButton_SessionListener ()
	{
		super ();
		if (getClass () == LoginButton_SessionListener.class)
			mono.android.TypeManager.Activate ("TEGAAndroid.LoginButton/SessionListener, TEGA-Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	public LoginButton_SessionListener (tegaandroid.LoginButton p0)
	{
		super ();
		if (getClass () == LoginButton_SessionListener.class)
			mono.android.TypeManager.Activate ("TEGAAndroid.LoginButton/SessionListener, TEGA-Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "TEGAAndroid.LoginButton, TEGA-Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", this, new java.lang.Object[] { p0 });
	}

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
