using UnityEngine;
using System.Collections;

public class ScriptTF_Livetalk : MonoBehaviour {

	public GameObject mTop;
	public GameObject mMainMenu;
	public GameObject mRight;

	// Use this for initialization
	const string DEFAULT_CHANNEL = "Liveballchat.";

	void Start () {
		string appId = "1C0C2894-E73D-4711-B9A0-A55C2C4DEBF6";
//		string userId = SystemInfo.deviceUniqueIdentifier;
		string userName = UserMgr.UserInfo.memberName;
		string userId = userName;
//		string channelUrl = DEFAULT_CHANNEL;
				
		Jiver.Init (appId);
		Jiver.Login (userId, userName);
		Jiver.QueryChannelList (false);

		string channelUrl1 = DEFAULT_CHANNEL
			+UserMgr.Schedule.extend [0].teamCode+UserMgr.Schedule.extend [1].teamCode;
		string channelUrl2 = DEFAULT_CHANNEL
			+UserMgr.Schedule.extend [1].teamCode+UserMgr.Schedule.extend [0].teamCode;
		Debug.Log(channelUrl1+","+channelUrl2);
		Jiver.Join (channelUrl1, channelUrl2);

	}

	void Update(){
		string menuStatus = mMainMenu.GetComponent<PlayMakerFSM>().FsmVariables.FindFsmString("StatusAnimation").Value;
		bool isOpen = mRight.GetComponent<ScriptMainMenuRight>().IsOpen;
//		Debug.Log ("menuStatus : " + menuStatus);
		if (menuStatus.Equals ("Closed") && !isOpen) {
			transform.FindChild ("Panel").gameObject.SetActive (true);
		} else {
			transform.FindChild ("Panel").gameObject.SetActive (false);
		}
	}


}
