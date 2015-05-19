using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class IOSMgr : MonoBehaviour
{
	EventDelegate mEventDelegate;
	static IOSMgr _instance;
	string mMsg;

	[DllImport("__Internal")]
	private static extern void OpenGallery(string str);

	#if(UNITY_EDITOR)
	public static void CallIOSFunc( string strFuncName, string str){}
	#elif(UNITY_ANDROID)
	private AndroidJavaObject curActivity;

	void Awake()
	{
		AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		curActivity = jc.GetStatic<AndroidJavaObject>("currentActivity");
		DontDestroyOnLoad (this);
	}

	public static void CallIOSFunc( string strFuncName, string str)
	{
		if( Instance.curActivity == null )
			return;

		Instance.curActivity.Call( strFuncName, str);
	}

//	public void SetGalleryImage(string image)
//	{
//		ScriptItemPhoto sip = mReceiver as ScriptItemPhoto;
//		sip.SetImgData (image);
//	}

	public void SetGalleryImages(string images)
	{
//		if(images.Length < 1)
//		{
//			IOSMgr.Instance.strLog = "no Images";
//		}
//
//		JSONObject json = new JSONObject (images);
//
//		ScriptUpload su = mReceiver as ScriptUpload;
//
//		su.setImageDictionary (json);

	}

#else
	public static void CallIOSFunc( string strFuncName, string str){
		switch(strFuncName){
		case "OpenGallery":
			OpenGallery(str);
			break;
		}
	}

#endif
	private static IOSMgr Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = FindObjectOfType(typeof(IOSMgr)) as IOSMgr;
				Debug.Log("IOSMgr is null");
				if (_instance == null)
				{
					GameObject container = new GameObject();  
					container.name = "IOSMgr";  
					_instance = container.AddComponent(typeof(IOSMgr)) as IOSMgr;
					Debug.Log("and makes new one");
					
				}
			}
			
			return _instance;
		}
	}

	public static string GetMsg()
	{
		return Instance.mMsg;
	}

	public void MsgReceived(string msg)
	{
		Debug.Log ("Android Msg Received : " + msg);
		mMsg = msg;
		mEventDelegate.Execute ();
	}

	public void ErrorReceived(string msg)
	{
		Debug.Log (msg);
	}

	public void NotiReceived(string msg)
	{
		Debug.Log("NotiReceived : "+UtilMgr.OnPause);
		if(!UtilMgr.OnPause)
			QuizMgr.NotiReceived (msg);
	}

	public static void OpenCamera(EventDelegate eventDelegate){
		Instance.mEventDelegate = eventDelegate;
		string timeStr = UtilMgr.GetDateTime ("yyyy-MM-dd HH:mm:ss");
		timeStr += " by lb.jpg";
		IOSMgr.CallIOSFunc("OpenCamera", timeStr);
	}

	public static void OpenGallery(EventDelegate eventDelegate){
		Instance.mEventDelegate = eventDelegate;
		IOSMgr.CallIOSFunc("OpenGallery", "");
	}

	public static void GetGalleryImages(EventDelegate eventDelegate){
		Instance.mEventDelegate = eventDelegate;
		IOSMgr.CallIOSFunc("GetGalleryImages", "");
	}

	public static void RegistGCM(EventDelegate eventDelegate){
		Instance.mEventDelegate = eventDelegate;
		IOSMgr.CallIOSFunc("RegisterGCM", "");
	}

	public static void ViberateDevice(long millSec){
		IOSMgr.CallIOSFunc("ViberateDevice", string.Format("{0}", millSec));
	}

	public static void OpenFB(EventDelegate eventDelegate){
		Instance.mEventDelegate = eventDelegate;
		IOSMgr.CallIOSFunc("OpenFB", "");
	}

	public static void GetHeightStatusBar(){
		IOSMgr.CallIOSFunc("GetHeightStatusBar", "");
	}
	public void GotHeightStatusBar(string height){
		Constants.HEIGHT_STATUS_BAR = int.Parse(height);
		Debug.Log("Size of StatusBar is "+Constants.HEIGHT_STATUS_BAR);
	}


}