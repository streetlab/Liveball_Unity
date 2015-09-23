using UnityEngine;
using System.Collections;

public class ScriptSuperRoot : MonoBehaviour {
	string mSceneName;
	string mContentNum;


	void Start () {
	
		transform.FindChild ("Camera").transform.localPosition = new Vector3(0, UtilMgr.GetScaledPositionY(), 0);
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		#if(UNITY_EDITOR)
		#elif(UNITY_ANDROID)

		#else
//		iPhoneSettings.screenCanDarken = false;

		#endif
	}

	void Awake(){
//		DontDestroyOnLoad(this);
//		if (Application.loadedLevelName.Equals ("SceneMain")) {
//			if(ScriptMainTop.LandingState==4){
//				if(UserMgr.UserInfo!=null){
//				string TeamColor = UserMgr.UserInfo.favoBB.teamColor;
//				TeamColor = TeamColor.Replace("#","");
//				transform.FindChild("GameObject").FindChild("TF_Landing").GetComponent<LandingManager>().SetTeamColor(TeamColor);
//				Debug.Log("SetTeamColor!");
//				}
//			}
//		}
	
		if(GetComponent<AudioSource>() == null){
			gameObject.AddComponent<AudioSource>();
		}
		Debug.Log("frameRate is "+Application.targetFrameRate);

		Debug.Log("vSyncCount is "+QualitySettings.vSyncCount);

		QualitySettings.vSyncCount = 0;
		Application.targetFrameRate = 20;
	}

	void Update () {



			if (Input.GetKeyDown (KeyCode.Escape)) {
			if(Application.loadedLevelName.Equals("SceneMain")){
				if(!transform.FindChild("TF_Betting").gameObject.activeSelf){
					OnBackPressed ();
				}
			}else{
				OnBackPressed ();
			}
			}

	}

	void OnApplicationFocus(bool focus){
		UtilMgr.OnFocus = focus;
//		Debug.Log("Application focus : "+focus);
	}

	void OnApplicationPause(bool pause){
		UtilMgr.OnPause = pause;
//		Debug.Log("Application pause : "+pause);
		if(!pause)
			UtilMgr.DismissLoading();
	}

	public void OnBackPressed()
	{
//		Debug.Log ("DialogueMgr.IsShown : " + DialogueMgr.IsShown);
		if (DialogueMgr.IsShown) {
			DialogueMgr.Instance.BtnCancelClicked();
		} else {
			UtilMgr.OnBackPressed ();
			if(Application.loadedLevelName.Equals("SceneMain")){

				if(transform.FindChild("TF_Livetalk").gameObject.activeSelf){
					transform.FindChild("TF_Livetalk").FindChild("Panel").FindChild("Input").GetComponent<UIInput>().OpenKeboard();
				}
			}
		}

//		if (!UtilMgr.OnBackPressed ()) {
//			UtilMgr.SetBackEvent (new EventDelegate (this, "DismissDialogue"));
//		}
	}
	
//	public void DismissDialogue()
//	{
//		DialogueMgr.DismissDialogue ();
//	}
}
