package md593b8d625023f6802361dd1b8a6546be5;


public class MyPreviewCallback
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		android.hardware.Camera.PreviewCallback
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onPreviewFrame:([BLandroid/hardware/Camera;)V:GetOnPreviewFrame_arrayBLandroid_hardware_Camera_Handler:Android.Hardware.Camera/IPreviewCallbackInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"";
		mono.android.Runtime.register ("App5.MyPreviewCallback, App5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", MyPreviewCallback.class, __md_methods);
	}


	public MyPreviewCallback ()
	{
		super ();
		if (getClass () == MyPreviewCallback.class)
			mono.android.TypeManager.Activate ("App5.MyPreviewCallback, App5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onPreviewFrame (byte[] p0, android.hardware.Camera p1)
	{
		n_onPreviewFrame (p0, p1);
	}

	private native void n_onPreviewFrame (byte[] p0, android.hardware.Camera p1);

	private java.util.ArrayList refList;
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
