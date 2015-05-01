using UnityEngine;
using System.Collections;

public class ScriptTopInfoItem : MonoBehaviour {

	public string mStrDefault;
	public string mStrLive;

	public enum STATE{
		GROUP,
		GOLD,
		VS,
		CARD
	}
	STATE mState;

//	void Update(){
//		if(mState == STATE.GOLD){
//
//		}
//	}

	public void SetGroupInfo(){
		mState = STATE.GROUP;

		transform.FindChild("GroupInfo").gameObject.SetActive(true);

		transform.FindChild("GoldInfo").gameObject.SetActive(false);
		transform.FindChild("VSInfo").gameObject.SetActive(false);
		transform.FindChild("CardInfo").gameObject.SetActive(false);

		Transform tfInfo = transform.FindChild("GroupInfo");
		Debug.Log("Teamcode is "+UserMgr.UserInfo.GetTeamCode());
//		Debug.Log("emblem is "+)
		tfInfo.FindChild("SprTeamLogo").GetComponent<UISprite>().spriteName =
			UtilMgr.GetTeamEmblem(UserMgr.UserInfo.GetTeamCode());
		tfInfo.FindChild("LblTitle").GetComponent<UILabel>().text =
			UserMgr.UserInfo.GetTeamFullName();
		tfInfo.FindChild("SprVSLogo").GetComponent<UISprite>().spriteName =
			UtilMgr.GetTeamEmblem("");
		tfInfo.FindChild("LblInfo").GetComponent<UILabel>().text = mStrDefault;
	}

	public void SetGoldInfo(){
		mState = STATE.GOLD;

		transform.FindChild("GoldInfo").gameObject.SetActive(true);
		
		transform.FindChild("GroupInfo").gameObject.SetActive(false);
		transform.FindChild("VSInfo").gameObject.SetActive(false);
		transform.FindChild("CardInfo").gameObject.SetActive(false);
	}

	public void SetVSInfo(ScheduleInfo scheduleInfo){
		mState = STATE.VS;

		transform.FindChild("VSInfo").gameObject.SetActive(true);
		
		transform.FindChild("GoldInfo").gameObject.SetActive(false);
		transform.FindChild("GroupInfo").gameObject.SetActive(false);
		transform.FindChild("CardInfo").gameObject.SetActive(false);

		Transform tfInfo = transform.FindChild("VSInfo");
		tfInfo.FindChild("SprTeamLogo").GetComponent<UISprite>().spriteName =
			UtilMgr.GetTeamEmblem(scheduleInfo.extend[0].imageName);
		tfInfo.FindChild("LblScoreLeft").GetComponent<UILabel>().text = scheduleInfo.extend[0].score+"";
		tfInfo.FindChild("SprVSLogo").GetComponent<UISprite>().spriteName =
			UtilMgr.GetTeamEmblem(scheduleInfo.extend[1].imageName);
		tfInfo.FindChild("LblScoreRight").GetComponent<UILabel>().text = scheduleInfo.extend[1].score+"";
		tfInfo.FindChild("LblInfo").GetComponent<UILabel>().text = mStrLive;
	}

	public void SetCardInfo(){
		mState = STATE.CARD;

		transform.FindChild("CardInfo").gameObject.SetActive(true);

		transform.FindChild("GoldInfo").gameObject.SetActive(false);
		transform.FindChild("VSInfo").gameObject.SetActive(false);
		transform.FindChild("GroupInfo").gameObject.SetActive(false);
	}
}
