using UnityEngine;
using System.Collections;

public class RankingManager : MonoBehaviour {
	GameObject W,D,M;
	Color B = new Color(37f/255f,170f/255f,225f/255f);
	Color G = new Color(147f/255f,147f/255f,147f/255f);
	// Use this for initialization
	void Start () {
		W = transform.FindChild("Scroll View").FindChild("bgs").FindChild("Weekly").gameObject;
		D = transform.FindChild("Scroll View").FindChild("bgs").FindChild("Daily").gameObject;
		M = transform.FindChild("Scroll View").FindChild("bgs").FindChild("My").gameObject;
	}
	
	// Update is called once per frame
	public void Weekly(){
		W.transform.FindChild ("BG").gameObject.SetActive (true);
		D.transform.FindChild ("BG").gameObject.SetActive (false);
		M.transform.FindChild ("BG").gameObject.SetActive (false);
		W.transform.FindChild ("Label").GetComponent<UILabel> ().color = B;
		D.transform.FindChild ("Label").GetComponent<UILabel> ().color = G;
		M.transform.FindChild ("Label").GetComponent<UILabel> ().color = G;
		W.transform.FindChild ("List").gameObject.SetActive (true);
		D.transform.FindChild ("List").gameObject.SetActive (false);
		M.transform.FindChild ("List").gameObject.SetActive (false);
	}
	public void Daily(){
		W.transform.FindChild ("BG").gameObject.SetActive (false);
		D.transform.FindChild ("BG").gameObject.SetActive (true);
		M.transform.FindChild ("BG").gameObject.SetActive (false);
		W.transform.FindChild ("Label").GetComponent<UILabel> ().color = G;
		D.transform.FindChild ("Label").GetComponent<UILabel> ().color = B;
		M.transform.FindChild ("Label").GetComponent<UILabel> ().color = G;
		W.transform.FindChild ("List").gameObject.SetActive (false);
		D.transform.FindChild ("List").gameObject.SetActive (true);
		M.transform.FindChild ("List").gameObject.SetActive (false);
	}
	public void My(){
		W.transform.FindChild ("BG").gameObject.SetActive (false);
		D.transform.FindChild ("BG").gameObject.SetActive (false);
		M.transform.FindChild ("BG").gameObject.SetActive (true);
		W.transform.FindChild ("Label").GetComponent<UILabel> ().color = G;
		D.transform.FindChild ("Label").GetComponent<UILabel> ().color = G;
		M.transform.FindChild ("Label").GetComponent<UILabel> ().color = B;
		W.transform.FindChild ("List").gameObject.SetActive (false);
		D.transform.FindChild ("List").gameObject.SetActive (false);
		M.transform.FindChild ("List").gameObject.SetActive (true);
	}
}
