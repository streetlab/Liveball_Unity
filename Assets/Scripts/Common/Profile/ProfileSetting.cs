using UnityEngine;
using System.Collections;

public class ProfileSetting : MonoBehaviour {
	GetProfileEvent mProfileEvent;

	// Use this for initialization
	public string UserName;
	public string UserState;
	public void settingpage(){
		//SetMemberName ("nonnames");
		//SetMemberTeam ("SK");
		//Debug.Log("settingpage");
		mProfileEvent = new GetProfileEvent (new EventDelegate (this, "setarray"));
		NetMgr.GetProfile (UserMgr.UserInfo.memSeq,mProfileEvent);
	
	}
	void setarray(){
		string imgName = UtilMgr.GetTeamEmblem(mProfileEvent.Response.data.GetTeamName());
		Debug.Log ("imgName : " + imgName);
		//Debug.Log ("imgName : " + this);
		transform.FindChild ("Bg_g").FindChild ("Bg_w").FindChild ("var2").FindChild ("team").FindChild ("img").GetComponent<UISprite>().spriteName =  imgName;
		transform.FindChild ("Bg_g").FindChild ("Bg_w").FindChild ("var2").FindChild ("team").FindChild ("team name").GetComponent<UILabel> ().text = mProfileEvent.Response.data.GetTeamFullName();

		transform.FindChild ("Bg_g").FindChild ("Bg_w").FindChild ("var1").FindChild ("name").FindChild("name 1").GetComponent<UILabel> ().text = mProfileEvent.Response.data.memberName;
		//transform.FindChild ("Bg_g").FindChild ("Bg_w").FindChild ("var2").FindChild ("state").FindChild("state 1").GetComponent<UILabel> ().text = mProfileEvent.Response.data.memberEmail;

		UserName = mProfileEvent.Response.data.memberName;
		UserState = mProfileEvent.Response.data.memberEmail;
		transform.parent.FindChild ("Scroll View").gameObject.SetActive (false);
		transform.parent.parent.FindChild ("Top").FindChild ("Panel").FindChild("GroupInfoTop").gameObject.SetActive(false);
		transform.parent.parent.FindChild ("Top").FindChild ("Panel").FindChild("BtnMenu").gameObject.SetActive(false);
		transform.parent.parent.FindChild ("Top").FindChild ("Panel").FindChild("BtnBack").gameObject.SetActive(true);
		transform.parent.parent.FindChild ("Top").FindChild ("Panel").FindChild("ProfileSettings").gameObject.SetActive(true);
		transform.parent.GetComponent<ScriptTFProfile> ().SetProfile (mProfileEvent.Response.data.memberName,imgName);
		gameObject.SetActive (true);
	}
	public void back(){
		if (gameObject.activeSelf) {
			gameObject.SetActive (false);
			transform.parent.FindChild ("Scroll View").gameObject.SetActive (true);
			transform.parent.parent.FindChild ("Top").FindChild ("Panel").FindChild ("GroupInfoTop").gameObject.SetActive (true);
			transform.parent.parent.FindChild ("Top").FindChild ("Panel").FindChild ("BtnMenu").gameObject.SetActive (true);
			transform.parent.parent.FindChild ("Top").FindChild ("Panel").FindChild ("BtnBack").gameObject.SetActive (false);
			transform.parent.parent.FindChild ("Top").FindChild ("Panel").FindChild ("ProfileSettings").gameObject.SetActive (false);

		}
	else{

			if(transform.parent.parent.FindChild ("Top").FindChild ("Panel").FindChild("SetnamePages").gameObject.activeSelf){
				transform.parent.parent.FindChild ("Top").FindChild ("Panel").FindChild("SetnamePages").gameObject.SetActive(false);
				transform.parent.FindChild ("SetNamePage").gameObject.SetActive (false);
			}else if(transform.parent.parent.FindChild ("Top").FindChild ("Panel").FindChild("SetStatePages").gameObject.activeSelf){
				transform.parent.parent.FindChild ("Top").FindChild ("Panel").FindChild("SetStatePages").gameObject.SetActive(false);
				transform.parent.FindChild ("SetStatePage").gameObject.SetActive (false);
			}
			transform.parent.FindChild ("SetNamePage").FindChild("Input").GetComponent<UIInput>().value = "";
			transform.parent.FindChild ("SetStatePage").FindChild("Input").GetComponent<UIInput>().value = "";
			transform.parent.parent.FindChild ("Top").FindChild ("Panel").FindChild("GroupInfoTop").gameObject.SetActive(false);
			transform.parent.parent.FindChild ("Top").FindChild ("Panel").FindChild("BtnMenu").gameObject.SetActive(false);
			transform.parent.parent.FindChild ("Top").FindChild ("Panel").FindChild("BtnBack").gameObject.SetActive(true);
			transform.parent.parent.FindChild ("Top").FindChild ("Panel").FindChild("ProfileSettings").gameObject.SetActive(true);
			
			gameObject.SetActive (true);

	}


	}
	public void SetMemberName(string name){
		JoinMemberInfo memInfo = new JoinMemberInfo ();
		UpdateMemberInfoEvent event1 = null;

		//memInfo.MemberID
		memInfo.MemberName = name;
		memInfo.MemberEmail = UserMgr.UserInfo.memberEmail;
		memInfo.MemImage = UserMgr.UserInfo.memberEmail;
		memInfo.FavoBB = UserMgr.UserInfo.GetTeamCode();
		event1 = new UpdateMemberInfoEvent (new EventDelegate (this, "non"));
		NetMgr.UpdateMemberInfo (memInfo, event1, UtilMgr.IsTestServer (), false);


	}
	public void SetMemberTeam(string Team){
		Debug.Log ("setteam : " + Team);
		JoinMemberInfo memInfo = new JoinMemberInfo ();
		UpdateMemberInfoEvent event1 = null;

		//memInfo.MemberID
		memInfo.MemberName = UserMgr.UserInfo.memberName;
		memInfo.MemberEmail = UserMgr.UserInfo.memberEmail;
		memInfo.MemImage = UserMgr.UserInfo.memberEmail;
		memInfo.FavoBB = Team;
		event1 = new UpdateMemberInfoEvent (new EventDelegate (this, "non"));
		NetMgr.UpdateMemberInfo (memInfo, event1, UtilMgr.IsTestServer (), false);

		
	}
	void non(){
		settingpage ();
	}
}