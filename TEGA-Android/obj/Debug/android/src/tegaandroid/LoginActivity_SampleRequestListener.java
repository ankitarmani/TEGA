package tegaandroid;


public class LoginActivity_SampleRequestListener
	extends tegaandroid.BaseRequestListener
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("TEGAAndroid.LoginActivity/SampleRequestListener, TEGA-Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", LoginActivity_SampleRequestListener.class, __md_methods);
	}


	public LoginActivity_SampleRequestListener ()
	{
		super ();
		if (getClass () == LoginActivity_SampleRequestListener.class)
			mono.android.TypeManager.Activate ("TEGAAndroid.LoginActivity/SampleRequestListener, TEGA-Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
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
