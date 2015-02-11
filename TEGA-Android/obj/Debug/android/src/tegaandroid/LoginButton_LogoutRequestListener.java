package tegaandroid;


public class LoginButton_LogoutRequestListener
	extends tegaandroid.BaseRequestListener
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("TEGAAndroid.LoginButton/LogoutRequestListener, TEGA-Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", LoginButton_LogoutRequestListener.class, __md_methods);
	}


	public LoginButton_LogoutRequestListener ()
	{
		super ();
		if (getClass () == LoginButton_LogoutRequestListener.class)
			mono.android.TypeManager.Activate ("TEGAAndroid.LoginButton/LogoutRequestListener, TEGA-Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
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
