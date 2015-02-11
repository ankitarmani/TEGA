package tegaandroid;


public class FriendsActivity_AppRequestsListener
	extends tegaandroid.BaseDialogListener
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("TEGAAndroid.FriendsActivity/AppRequestsListener, TEGA-Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", FriendsActivity_AppRequestsListener.class, __md_methods);
	}


	public FriendsActivity_AppRequestsListener ()
	{
		super ();
		if (getClass () == FriendsActivity_AppRequestsListener.class)
			mono.android.TypeManager.Activate ("TEGAAndroid.FriendsActivity/AppRequestsListener, TEGA-Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
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
