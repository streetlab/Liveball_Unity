using UnityEngine;
using System.Collections;

public class ScriptGameInfo : MonoBehaviour {

	public GameObject mBtnMatchTop;
	public GameObject mBtnPlayerTop;
	public GameObject mBtnInfoTeam;
	public GameObject mBtnRankTeam;
	public GameObject mBtnInfoMatch;
	public GameObject mBtnLineup;
	public GameObject mBtnHistory;

	public GameObject mSub1;
	public GameObject mSub2;
	public GameObject mToggles;

//	public GameObject mShadow;

	enum TopState{
		Empty,
		Match,
		Player
	}
	TopState mTopState;

	enum Sub1State{
		Empty,
		InfoTeam,
		RankTeam,
		InfoMatch
	}
	Sub1State mSub1State;

	enum Sub2State{
		Empty,
		LineUp,
		History
	}
	Sub2State mSub2State;

	public string TeamCodeLG ="LG";
	public string TeamCodeLT ="LT";
	public string TeamCodeHH ="HH";
	public string TeamCodeOB ="OB";
	public string TeamCodeHT ="HT";
	public string TeamCodeSS ="SS";
	public string TeamCodeWO ="WO";
	public string TeamCodeSK ="SK";
	public string TeamCodeNC ="NC";
	public string TeamCodeKT ="kt";
	string mTeamCode;
	string mInfoMatchTeamCode = "";
	string mLineupTeamCode = "";

	// Use this for initialization
	void Start() {
		mBtnInfoTeam.SetActive(false);
	}

	void Awake(){
		mTopState = TopState.Empty;
		mSub1State = Sub1State.Empty;
		mSub2State = Sub2State.Empty;
	}

	public void Init(){
		if(mTopState == TopState.Empty){
			MatchTopClicked();
		}
	}

	void DisableTop(){
		mBtnPlayerTop.transform.FindChild("Label").GetComponent<UILabel>().color = Color.gray;
		mBtnPlayerTop.transform.FindChild("Bar").gameObject.SetActive(false);
		mBtnMatchTop.transform.FindChild("Label").GetComponent<UILabel>().color = Color.gray;
		mBtnMatchTop.transform.FindChild("Bar").gameObject.SetActive(false);

		mSub1.SetActive(false);
		mSub2.SetActive(false);
	}

	void DisableSub1(){
		mBtnInfoTeam.transform.FindChild("Label").GetComponent<UILabel>().color = Color.gray;
		mBtnInfoMatch.transform.FindChild("Label").GetComponent<UILabel>().color = Color.gray;
		mBtnRankTeam.transform.FindChild("Label").GetComponent<UILabel>().color = Color.gray;
		mToggles.SetActive(false);
	}

	void DisableSub2(){
		mBtnLineup.transform.FindChild("Label").GetComponent<UILabel>().color = Color.gray;
		mBtnHistory.transform.FindChild("Label").GetComponent<UILabel>().color = Color.gray;
		mToggles.SetActive(false);
	}

	void DisableTF(){
		transform.FindChild("TF_Ranking").gameObject.SetActive(false);
		transform.FindChild("TF_Lineup").gameObject.SetActive(false);
		transform.FindChild("TF_Highlight").gameObject.SetActive(false);
		transform.FindChild("TF_Statistics").gameObject.SetActive(false);
	}

	public void MatchTopClicked(){
		if(mTopState == TopState.Match)
			return;

		DisableTop();
		mTopState = TopState.Match;
		mBtnMatchTop.transform.FindChild("Label").GetComponent<UILabel>().color = Color.white;
		mBtnMatchTop.transform.FindChild("Bar").gameObject.SetActive(true);
		mSub1.SetActive(true);

		if(mSub1State == Sub1State.InfoMatch)
			InfoMatchClicked();
		else 
			RankTeamClicked();

		foreach(UIToggle toggle in mToggles.GetComponentsInChildren<UIToggle>()){
			if(toggle.name.Equals(mInfoMatchTeamCode)){
				toggle.value = true;
			} else{
				toggle.Set(false);
			}
		}
	}

	public void PlayerTopClicked(){
		if(mTopState == TopState.Player)
			return;

		DisableTop();
		mTopState = TopState.Player;
		mBtnPlayerTop.transform.FindChild("Label").GetComponent<UILabel>().color = Color.white;
		mBtnPlayerTop.transform.FindChild("Bar").gameObject.SetActive(true);
		mSub2.SetActive(true);

		if(mSub2State == Sub2State.History)
			HistoryClicked();
		else
			LineupClicked();

		foreach(UIToggle toggle in mToggles.GetComponentsInChildren<UIToggle>()){
			if(toggle.name.Equals(mLineupTeamCode)){
				toggle.value = true;
			} else{
				toggle.Set(false);
			}
		}
	}

	public void InfoTeamClicked(){
		if(mSub1State == Sub1State.InfoTeam)
			return;

		DisableSub1();
		mSub1State = Sub1State.InfoTeam;
		mBtnInfoTeam.transform.FindChild("Label").GetComponent<UILabel>().color = Color.white;
		mToggles.SetActive(true);
	}

	public void RankTeamClicked(){
		DisableTF();
		transform.FindChild("TF_Ranking").gameObject.SetActive(true);
		DisableSub1();
		mBtnRankTeam.transform.FindChild("Label").GetComponent<UILabel>().color = Color.white;

		if(mSub1State == Sub1State.RankTeam)
			return;

		mSub1State = Sub1State.RankTeam;

		transform.FindChild("TF_Ranking").GetComponent<Rankcontrol>().Init ();
	}

	public void InfoMatchClicked(){
		DisableTF();
		transform.FindChild("TF_Highlight").gameObject.SetActive(true);
		DisableSub1();
		mBtnInfoMatch.transform.FindChild("Label").GetComponent<UILabel>().color = Color.white;
		mToggles.SetActive(true);

		if(mSub1State == Sub1State.InfoMatch)
			return;

		mSub1State = Sub1State.InfoMatch;
	}

	public void LineupClicked(){
		DisableTF();
		transform.FindChild("TF_Lineup").gameObject.SetActive(true);
		DisableSub2();
		mBtnLineup.transform.FindChild("Label").GetComponent<UILabel>().color = Color.white;
		mToggles.SetActive(true);

		if(mSub2State == Sub2State.LineUp)
			return;
		
		mSub2State = Sub2State.LineUp;
	}

	public void HistoryClicked(){
		DisableTF();
		transform.FindChild("TF_Statistics").gameObject.SetActive(true);
		DisableSub2();
		mBtnHistory.transform.FindChild("Label").GetComponent<UILabel>().color = Color.white;

		if(mSub2State == Sub2State.History)
			return;
		
		mSub2State = Sub2State.History;

		transform.FindChild("TF_Statistics").GetComponent<StatisControl>().Init();
	}

	void InitInfoTeam(){

	}

	void InitInfoMatch(){
		transform.FindChild("TF_Highlight").FindChild("MatchPlaying").
			GetComponent<ScriptHighlightPlaying>().Init(mTeamCode);
	}

	void InitLineup(){
		transform.FindChild("TF_Lineup").GetComponent<LineupControl>().view(mTeamCode);
	}

	public void OnToggleClicked(string teamCode){
		Debug.Log("BtmState is"+mSub1State);
		Debug.Log("teamCode is "+teamCode);

		mTeamCode = teamCode;

		if(mTopState == TopState.Match){
			switch(mSub1State){
			case Sub1State.InfoTeam:
				InitInfoTeam();
				break;
			case Sub1State.InfoMatch:
				mInfoMatchTeamCode = teamCode;
				InitInfoMatch();
				break;
			}
		} else{
			switch(mSub2State){			
			case Sub2State.LineUp:
				mLineupTeamCode = teamCode;
				InitLineup();
				break;
			}
		}

	}
}
