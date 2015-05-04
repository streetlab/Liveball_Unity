﻿using UnityEngine;
using System.Collections;

public class ScriptTHTop : MonoBehaviour {

	public GameObject mTimeline;
	public GameObject mAlbum;
//	public GameObject mSeason;
	public GameObject mSquad;

	public GameObject mNanoo;

	public GameObject mBtnTimeline;
	public GameObject mBtnAlbum;
	public GameObject mBtnSeason;
	public GameObject mBtnSquad;

	public AudioClip mAudioWelcome;

	GetScheduleEvent mScheduleEvent;

	void Start(){
//		mTimeline.SetActive (true);
//
//		mAlbum.SetActive (false);
//		mSeason.SetActive (false);
//		mSquad.SetActive (false);
//		OpenTimeline ();
		CheckFirst();
		InitTopInfo();
		OpenNanoo ();
	}

	void CheckFirst(){
		if(UserMgr.UserInfo.IsFirstLanding){
			UserMgr.UserInfo.IsFirstLanding = false;
			transform.root.GetComponent<AudioSource>().PlayOneShot(mAudioWelcome);
		}
	}

	void InitTopInfo(){
		transform.FindChild("TopInfoItem").GetComponent<ScriptTopInfoItem>().SetGroupInfo();
		mScheduleEvent = new GetScheduleEvent(new EventDelegate(this, "GotSchedule"));
		NetMgr.GetScheduleMore(
			null,
			UserMgr.UserInfo.teamSeq,
			mScheduleEvent);
	}

	public void GotSchedule(){
		ScheduleInfo schedule = null;


		foreach(ScheduleInfo info in mScheduleEvent.Response.data){
			bool bFoundMyTeam = false;
			if(UserMgr.UserInfo.GetTeamCode().Equals(info.extend[0].teamCode))
				bFoundMyTeam = true;
			else if(UserMgr.UserInfo.GetTeamCode().Equals(info.extend[1].teamCode))
				bFoundMyTeam = true;

			if(bFoundMyTeam
				&& info.gameStatus == ScheduleInfo.GAME_PLAYING){
				schedule = info;
				break;
			}
		}

		if(schedule != null)
			transform.FindChild("TopInfoItem").GetComponent<ScriptTopInfoItem>().SetVSInfo(schedule);
	}

	public void BtnClicked(string name){
		switch (name) {
		case "BtnTimeline":
			OpenTimeline();
			break;
		case "BtnAlbum":
			OpenAlbum();
			break;
		case "BtnSeason":
			OpenSeason();
			break;
		case "BtnSquad":
			OpenSquad();
			break;
		}
	}

	void OpenNanoo(){
		mNanoo.SetActive (true);

		mTimeline.SetActive (false);		
		mAlbum.SetActive (false);
//		mSeason.SetActive (false);
		mSquad.SetActive (false);
	}

	void OpenTimeline(){
		mBtnTimeline.GetComponent<UIButton> ().isEnabled = false;

		mBtnAlbum.GetComponent<UIButton> ().isEnabled = true;
		mBtnSeason.GetComponent<UIButton> ().isEnabled = true;
		mBtnSquad.GetComponent<UIButton> ().isEnabled = true;

		mTimeline.SetActive (true);
		
		mAlbum.SetActive (false);
//		mSeason.SetActive (false);
		mSquad.SetActive (false);
	}

	void OpenAlbum(){
		mBtnAlbum.GetComponent<UIButton> ().isEnabled = false;

		mBtnTimeline.GetComponent<UIButton> ().isEnabled = true;
		mBtnSeason.GetComponent<UIButton> ().isEnabled = true;
		mBtnSquad.GetComponent<UIButton> ().isEnabled = true;

		mAlbum.SetActive (true);
		mAlbum.GetComponent<ScriptTF_Album> ().OpenWebView ();
		
		mTimeline.SetActive (false);
//		mSeason.SetActive (false);
		mSquad.SetActive (false);
	}

	void OpenSeason(){
		mBtnSeason.GetComponent<UIButton> ().isEnabled = false;

		mBtnTimeline.GetComponent<UIButton> ().isEnabled = true;
		mBtnAlbum.GetComponent<UIButton> ().isEnabled = true;
		mBtnSquad.GetComponent<UIButton> ().isEnabled = true;

//		mSeason.SetActive (true);
		
		mAlbum.SetActive (false);
		mTimeline.SetActive (false);
		mSquad.SetActive (false);
	}

	void OpenSquad(){
		mBtnSquad.GetComponent<UIButton> ().isEnabled = false;

		mBtnAlbum.GetComponent<UIButton> ().isEnabled = true;
		mBtnSeason.GetComponent<UIButton> ().isEnabled = true;
		mBtnTimeline.GetComponent<UIButton> ().isEnabled = true;

		mSquad.SetActive (true);
		
		mAlbum.SetActive (false);
//		mSeason.SetActive (false);
		mTimeline.SetActive (false);
	}

}
