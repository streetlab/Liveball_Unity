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

	enum TopState{
		Empty,
		Match,
		Player
	}
	TopState mTopState;

	enum BtmState{
		Empty,
		InfoTeam,
		RankTeam,
		InfoMatch,
		LineUp,
		History
	}
	BtmState mBtmState;

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

	// Use this for initialization
	void Start () {
		mSub1.SetActive(false);
		mSub2.SetActive(false);
		mToggles.SetActive(false);
		mTopState = TopState.Empty;
		mBtmState = BtmState.Empty;

		mBtnInfoTeam.SetActive(false);
//		MatchTopClicked();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Init(){
		if(mTopState == TopState.Empty){
			Debug.Log("is null");
			MatchTopClicked();
//			InfoTeamClicked();
			RankTeamClicked();
		}
	}

	public void MatchTopClicked(){
		if(mTopState == TopState.Match)
			return;

		mTopState = TopState.Match;
		mBtnMatchTop.transform.FindChild("Label").GetComponent<UILabel>().color = Color.white;
		mBtnMatchTop.transform.FindChild("Bar").gameObject.SetActive(true);
		mBtnPlayerTop.transform.FindChild("Label").GetComponent<UILabel>().color = Color.gray;
		mBtnPlayerTop.transform.FindChild("Bar").gameObject.SetActive(false);
		mSub1.SetActive(true);
		mSub2.SetActive(false);
	}

	public void PlayerTopClicked(){
		if(mTopState == TopState.Player)
			return;

		mTopState = TopState.Player;
		mBtnPlayerTop.transform.FindChild("Label").GetComponent<UILabel>().color = Color.white;
		mBtnPlayerTop.transform.FindChild("Bar").gameObject.SetActive(true);
		mBtnMatchTop.transform.FindChild("Label").GetComponent<UILabel>().color = Color.gray;
		mBtnMatchTop.transform.FindChild("Bar").gameObject.SetActive(false);
		mSub1.SetActive(false);
		mSub2.SetActive(true);
	}

	public void InfoTeamClicked(){
		if(mBtmState == BtmState.InfoTeam)
			return;

		mBtmState = BtmState.InfoTeam;
		mBtnInfoTeam.transform.FindChild("Label").GetComponent<UILabel>().color = Color.white;
		mBtnInfoMatch.transform.FindChild("Label").GetComponent<UILabel>().color = Color.gray;
		mBtnRankTeam.transform.FindChild("Label").GetComponent<UILabel>().color = Color.gray;
		mToggles.SetActive(true);
	}

	public void RankTeamClicked(){
		if(mBtmState == BtmState.RankTeam)
			return;

		mBtmState = BtmState.RankTeam;
		mBtnInfoTeam.transform.FindChild("Label").GetComponent<UILabel>().color = Color.gray;
		mBtnInfoMatch.transform.FindChild("Label").GetComponent<UILabel>().color = Color.gray;
		mBtnRankTeam.transform.FindChild("Label").GetComponent<UILabel>().color = Color.white;
		mToggles.SetActive(false);

		transform.FindChild("TF_Lineup").gameObject.SetActive(false);
		transform.FindChild("TF_Ranking").GetComponent<Rankcontrol>().Init ();
	}

	public void InfoMatchClicked(){
		if(mBtmState == BtmState.InfoMatch)
			return;

		mBtmState = BtmState.InfoMatch;
		mBtnInfoTeam.transform.FindChild("Label").GetComponent<UILabel>().color = Color.gray;
		mBtnInfoMatch.transform.FindChild("Label").GetComponent<UILabel>().color = Color.white;
		mBtnRankTeam.transform.FindChild("Label").GetComponent<UILabel>().color = Color.gray;
		mToggles.SetActive(true);
	}

	public void LineupClicked(){
		if(mBtmState == BtmState.LineUp)
			return;
		
		mBtmState = BtmState.LineUp;
		mBtnLineup.transform.FindChild("Label").GetComponent<UILabel>().color = Color.white;
		mBtnHistory.transform.FindChild("Label").GetComponent<UILabel>().color = Color.gray;
		mToggles.SetActive(true);
	}

	public void HistoryClicked(){
		if(mBtmState == BtmState.History)
			return;
		
		mBtmState = BtmState.History;
		mBtnLineup.transform.FindChild("Label").GetComponent<UILabel>().color = Color.gray;
		mBtnHistory.transform.FindChild("Label").GetComponent<UILabel>().color = Color.white;
		mToggles.SetActive(false);
	}

	void InitInfoTeam(){

	}

	void InitInfoMatch(){

	}

	void InitLineup(){
		transform.FindChild("TF_Ranking").gameObject.SetActive(false);
		transform.FindChild("TF_Lineup").GetComponent<LineupControl>().view(mTeamCode);
	}

	public void OnToggleClicked(string teamCode){
		Debug.Log("BtmState is"+mBtmState);
		Debug.Log("teamCode is "+teamCode);

		mTeamCode = teamCode;

		switch(mBtmState){
		case BtmState.InfoTeam:
			InitInfoTeam();
			break;
		case BtmState.InfoMatch:
			InitInfoMatch();
			break;
		case BtmState.LineUp:
			InitLineup();
			break;
		}
	}
}
