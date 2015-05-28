using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class IOSMgr : MonoBehaviour
{
	EventDelegate mEventDelegate;
	static IOSMgr _instance;
	string mMsg;
	bool gotToken = false;

	[DllImport("__Internal")]
	private static extern void OpenGallery(string str);

	#if(UNITY_EDITOR)
	public static void CallIOSFunc( string strFuncName, string str){}
	#elif(UNITY_ANDROID)
	public static void CallIOSFunc( string strFuncName, string str)
	{
	}
	#else
	public static void CallIOSFunc( string strFuncName, string str){
		switch(strFuncName){
		case "OpenGallery":
			OpenGallery(str);
			break;
		}
	}

	void Update(){
		if(UnityEngine.iOS.NotificationServices.remoteNotifications.Length > 0){
			UnityEngine.iOS.NotificationServices.ClearRemoteNotifications();
			Debug.Log("noti received");
		}

		if(gotToken)
			return;
		
		if(UnityEngine.iOS.NotificationServices.deviceToken != null){
//			string token = System.Text.Encoding.UTF8.GetString(UnityEngine.iOS.NotificationServices.deviceToken);
			mMsg = System.BitConverter.ToString(UnityEngine.iOS.NotificationServices.deviceToken)
				.Replace("-", "").ToLower();
			Debug.Log("token is "+mMsg);
			gotToken = true;
			mEventDelegate.Execute();
		}
		//		else
		//			Debug.Log("token is null");
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

	void Awake()
	{
		DontDestroyOnLoad (this);
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


	/////////////////////////


	public static void RegistAPNS(EventDelegate eventDelegate){
		Instance.gotToken = false;
		Instance.mEventDelegate = eventDelegate;
		#if(UNITY_ANDROID)
		#else
		UnityEngine.iOS.NotificationServices.RegisterForNotifications(
			UnityEngine.iOS.NotificationType.Alert |
			UnityEngine.iOS.NotificationType.Badge |
			UnityEngine.iOS.NotificationType.Sound
			);
		#endif

	}

	public void DisagreePush(string str){
		DialogueMgr.ShowDialogue("disagree", "disagree", DialogueMgr.DIALOGUE_TYPE.Alert, null);
	}

}