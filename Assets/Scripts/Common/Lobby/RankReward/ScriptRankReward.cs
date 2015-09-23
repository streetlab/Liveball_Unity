using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScriptRankReward : MonoBehaviour {

	string mContent;
	ContestListInfo mContestListInfo;
	PresetAddEvent mPresetAddEvent;
	Contest mContest;
	PresetListEvent mPresetListEvent;
	int mPresetSeq;

	public void Init(ContestListInfo contestListInfo, Contest contest){
		mContest = contest;
		mContestListInfo = contestListInfo;
//		경기명 경기 방식 참여 가능 인원 필요 루비 경기 보상 순위별 보상
		mContent = "경기명 : "+contestListInfo.contestName;
		string matchType = contestListInfo.contestType == 1 ? "50 / 50s" : "Ranking";
		mContent += "\n\n경기 방식 : "+matchType;
		mContent += "\n\n참여 가능 인원 : "+contestListInfo.totalEntry;
		mContent += "\n\n필요 루비 : "+contestListInfo.entryFee;
		mContent += "\n\n경기 보상 : ";
		if(contestListInfo.totalReward == null || contestListInfo.totalReward.Equals("0")){
		} else{
			mContent += contestListInfo.totalReward + " ";
		}
		mContent += contestListInfo.itemName;
		mContent += "\n\n순위별 보상";
		mContent += "\n\n"+contestListInfo.GetRewardText();

		transform.FindChild("Scroll View").FindChild("Label").GetComponent<UILabel>().text = mContent;
		transform.FindChild("Scroll View").FindChild("Label").GetComponent<BoxCollider2D>().size
			= transform.FindChild("Scroll View").FindChild("Label").GetComponent<UILabel>().localSize;
		transform.FindChild("Scroll View").GetComponent<UIScrollView>().ResetPosition();
	}

	public void JoinClicked(){
		DialogueMgr.ShowDialogue("참여하기", "해당 경기에 참여하시겠습니까?\n실제 경기 입장은 \x22[b]참여 경기[/b]\x22탭에서\n라이브 경기 시작 후 가능합니다.", DialogueMgr.DIALOGUE_TYPE.YesNo, JoinHandler);
	}

	public void CancelClicked(){
		gameObject.SetActive(false);
	}

	void JoinHandler(DialogueMgr.BTNS btn){
		if(btn == DialogueMgr.BTNS.Btn1){
			if(int.Parse(UserMgr.UserInfo.userRuby) < mContestListInfo.entryFee){
				int cnt = mContestListInfo.entryFee - int.Parse(UserMgr.UserInfo.userRuby);
				DialogueMgr.ShowDialogue ("등록 취소", "루비가 "+cnt+"개 부족합니다." , DialogueMgr.DIALOGUE_TYPE.Alert ,null);
			} else{
				mPresetAddEvent = new PresetAddEvent (new EventDelegate (this, "Preset"));
				List<int> list = new List<int>();
				for(int i = 0; i < 18; i++)
					list.Add(0);
				NetMgr.PresetAdd (mContestListInfo.contestSeq, list, mPresetAddEvent);
			}
		} else
			CancelClicked();
	}

	void Preset(){
		mPresetSeq = mPresetAddEvent.Response.data.presetSeq;
		UserMgr.UserInfo.userRuby = (int.Parse(UserMgr.UserInfo.userRuby) - mContestListInfo.entryFee).ToString();

		mPresetListEvent = new PresetListEvent(new EventDelegate(this, "GetPresetList"));
		NetMgr.GetPresetList(mPresetListEvent);

//		transform.root.FindChild("Scroll").FindChild("Main").FindChild("PreSetting").FindChild("Top")
//			.FindChild("Top Menu").FindChild("Register").GetComponent<PresettingRC>().PresetUpdate();


	}

	void GetPresetList(){
		UserMgr.PresetList = mPresetListEvent.Response.data;
		transform.root.FindChild ("Scroll").FindChild ("Main").FindChild ("Top").FindChild ("Preset").FindChild ("Num").GetComponent<UILabel> ().text =
			mPresetListEvent.Response.data.Count.ToString ();
		
		transform.root.FindChild ("Scroll").FindChild ("Main").FindChild ("PreSetting").gameObject.SetActive (false);

		transform.root.FindChild("Scroll").FindChild("Main").FindChild("Top").FindChild("Preset").GetComponent<TopMenu>().Button();

		DialogueMgr.ShowDialogue ("참여 완료", "[b]경기 참여가 완료되었습니다!!![/b]\n" +
		                          "경기 전 예측 데이터를 입력하시겠습니까?\n경기 예측을 하시면 라이브 경기 시간에\n직접 참여하지 않아도 스코어를 획득할 수 있습니다." , DialogueMgr.DIALOGUE_TYPE.YesNo, PresetHandler);	
	}

	void PresetHandler(DialogueMgr.BTNS btn){
		CancelClicked();
		if(btn == DialogueMgr.BTNS.Btn1){
			//show preset input + end then go to my games
//			mContest.SetPreset();
//			mContest.SetPresettingSetting2();
			PresetItem[] children = transform.root.FindChild("Scroll").FindChild("Main").FindChild("PreSet Contest").FindChild("Scroll View")
				.FindChild("Position").GetComponentsInChildren<PresetItem>();
			Debug.Log("children cnt is "+children.Length);
			foreach(PresetItem child in children){
				Debug.Log("preset seq is "+child.transform.FindChild ("BG").FindChild ("presetSeq").GetComponent<UILabel> ().text);
				int presetSeq = int.Parse (child.transform.FindChild ("BG").FindChild ("presetSeq").GetComponent<UILabel> ().text);
				if(mPresetSeq == presetSeq){
					child.Button();
					break;
				}
			}
		} else{
			//go to my games
//			transform.root.FindChild("Scroll").FindChild("Main").FindChild("Top").FindChild("Preset")
//				.GetComponent<TopMenu>().Button();
		}
	}
}
