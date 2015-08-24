using UnityEngine;
using System.Collections;

public class StatisticsBack : MonoBehaviour {

	public void Button(){
		transform.parent.parent.parent.parent.parent.parent.FindChild ("TF_ALLRank 0").gameObject.SetActive (false);
		transform.parent.parent.parent.parent.parent.parent.FindChild ("TF_ALLRank 1").gameObject.SetActive (false);
		transform.parent.parent.parent.parent.parent.parent.FindChild ("TF_ALLRank 2").gameObject.SetActive (false);
		transform.parent.parent.parent.parent.parent.parent.FindChild ("TF_ALLRank 3").gameObject.SetActive (false);
		transform.parent.parent.parent.parent.parent.parent.FindChild ("Scroll View").gameObject.SetActive (true);
	}
}
