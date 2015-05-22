using UnityEngine;
using System.Collections;

public class ScriptGameTop : MonoBehaviour {

	public GameObject mSchedule;
	public GameObject mRanking;
	public GameObject mLeague;
	public GameObject mStatistics;

	public GameObject mBtnSchedule;
	public GameObject mBtnRanking;
	public GameObject mBtnLeague;
	public GameObject mBtnStatistics;

	public AudioClip mAudioWelcome;

	public GameObject mEmblem;

	GetScheduleEvent mScheduleEvent;

	// Use this for initialization
	void Start () {
//		mSchedule.SetActive (true);
//
//		mRanking.SetActive (false);
//		mLeague.SetActive (false);
//		mStatistics.SetActive (false);
		//mScheduleEvent = new GetScheduleEvent (new EventDelegate ("OpenSchedule"));
		//NetMgr.GetScheduleAll (mScheduleEvent);

//		OpenSchedule ();
		CheckFirst();

		mEmblem.GetComponent<UISprite>().spriteName =
			UtilMgr.GetTeamEmblem(UserMgr.UserInfo.GetTeamCode());
	}

	void CheckFirst(){
		if(UserMgr.UserInfo.IsFirstLanding){
			UserMgr.UserInfo.IsFirstLanding = false;
			transform.root.GetComponent<AudioSource>().PlayOneShot(mAudioWelcome);
		}
	}

	public void ScheduleClicked(){
		OpenSchedule();
	}

	public void RankingClicked(){
		OpenRanking();
	}

	public void LeagueClicked(){
		OpenLeague();
	}

	public void StatisticsClicked(){
		OpenStatistics();
	}

//	public void BtnClicked(string name){
//		switch(name){
//		case "BtnSchedule":
//			OpenSchedule();
//			break;
//		case "BtnRanking":
//			OpenRanking();
//			break;
//		case "BtnLeague":
//			OpenLeague();
//			break;
//		case "BtnStatistics":
//			OpenStatistics();
//			break;
//		}
//	}

	void OpenSchedule(){
		mBtnSchedule.GetComponent<UIButton> ().isEnabled = false;

		mBtnRanking.GetComponent<UIButton> ().isEnabled = true;
		mBtnLeague.GetComponent<UIButton> ().isEnabled = true;
		mBtnStatistics.GetComponent<UIButton> ().isEnabled = true;

		mSchedule.SetActive (true);
		
		mRanking.SetActive (false);
		mLeague.SetActive (false);
		mStatistics.SetActive (false);

		//mScheduleEvent.Response.data[0].
	}

	void OpenRanking(){
		mBtnRanking.GetComponent<UIButton> ().isEnabled = false;
		
		mBtnSchedule.GetComponent<UIButton> ().isEnabled = true;
		mBtnLeague.GetComponent<UIButton> ().isEnabled = true;
		mBtnStatistics.GetComponent<UIButton> ().isEnabled = true;

		mRanking.SetActive (true);
		
		mSchedule.SetActive (false);
		mLeague.SetActive (false);
		mStatistics.SetActive (false);
	}

	void OpenLeague(){
		mBtnLeague.GetComponent<UIButton> ().isEnabled = false;
		
		mBtnRanking.GetComponent<UIButton> ().isEnabled = true;
		mBtnSchedule.GetComponent<UIButton> ().isEnabled = true;
		mBtnStatistics.GetComponent<UIButton> ().isEnabled = true;

		mLeague.SetActive (true);
		
		mRanking.SetActive (false);
		mSchedule.SetActive (false);
		mStatistics.SetActive (false);
	}

	void OpenStatistics(){
		mBtnStatistics.GetComponent<UIButton> ().isEnabled = false;
		
		mBtnRanking.GetComponent<UIButton> ().isEnabled = true;
		mBtnLeague.GetComponent<UIButton> ().isEnabled = true;
		mBtnSchedule.GetComponent<UIButton> ().isEnabled = true;

		mStatistics.SetActive (true);
		
		mRanking.SetActive (false);
		mLeague.SetActive (false);
		mSchedule.SetActive (false);
	}


}
