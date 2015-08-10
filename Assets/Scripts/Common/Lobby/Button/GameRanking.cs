using UnityEngine;
using System.Collections;

public class GameRanking : MonoBehaviour {

	public void Button(){
		AllOff ();
		switch (name) {
		case "GameInfo":
			transform.root.FindChild("Scroll").FindChild("ContestIn").FindChild("GameInfo").transform.localPosition
				= new Vector3(0,0,0);
			transform.root.FindChild("Scroll").FindChild("Ranking").gameObject.SetActive(false);
			transform.root.FindChild ("Scroll").FindChild ("ContestIn").FindChild("Top").FindChild("Sub").FindChild ("GameInfo").FindChild ("Bar").gameObject.SetActive (true);
			break;

		case "PlayersInfo":
			transform.root.FindChild("Scroll").FindChild("Ranking").GetComponent<GameRankingCommander>().Button();
			transform.root.FindChild ("Scroll").FindChild ("ContestIn").FindChild("Top").FindChild("Sub").FindChild ("PlayersInfo").FindChild ("Bar").gameObject.SetActive (true);
			break;

		case "SetPreset":
			transform.root.FindChild ("Scroll").FindChild ("ContestIn").FindChild("Top").FindChild("Sub").FindChild ("SetPreset").FindChild ("Bar").gameObject.SetActive (true);
			break;

		}
	}
	void AllOff(){
		transform.root.FindChild ("Scroll").FindChild ("ContestIn").FindChild("Top").FindChild("Sub").FindChild ("GameInfo").FindChild ("Bar").gameObject.SetActive (false);
		transform.root.FindChild ("Scroll").FindChild ("ContestIn").FindChild("Top").FindChild("Sub").FindChild ("PlayersInfo").FindChild ("Bar").gameObject.SetActive (false);
		transform.root.FindChild ("Scroll").FindChild ("ContestIn").FindChild("Top").FindChild("Sub").FindChild ("SetPreset").FindChild ("Bar").gameObject.SetActive (false);
	}
}
