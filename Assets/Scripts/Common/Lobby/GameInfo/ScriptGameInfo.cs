using UnityEngine;
using System.Collections;

public class ScriptGameInfo : MonoBehaviour {

	public GameObject mBtnMatchTop;
	public GameObject mBtnPlayerTop;
	public GameObject mBtnInfoTeam;
	public GameObject mBtnRankTeam;
	public GameObject mBtnInfoMatch;

	enum TopState{
		Match,
		Player
	}
	TopState mTopState;

	enum BtmState{
		InfoTeam,
		RankTeam,
		InfoMatch
	}
	BtmState mBtmState;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void MatchTopClicked(){
		mTopState = TopState.Match;
	}

	public void PlayerTopClicked(){
		mTopState = TopState.Player;
	}

	public void InfoTeamClicked(){
		mBtmState = BtmState.InfoTeam;
	}

	public void RankTeamClicked(){
		mBtmState = BtmState.RankTeam;
	}

	public void InfoMatchClicked(){
		mBtmState = BtmState.InfoMatch;
	}
}
