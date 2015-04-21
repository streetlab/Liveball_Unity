using UnityEngine;
using System.Collections;

public class Rankcontrol : MonoBehaviour {
	public GameObject bars;
	public float gap=122;
	Vector3 posisions;
	float vgaps;

	// Use this for initialization
	void Start () {
		setposition ();
		transform.FindChild ("Scroll View").GetComponent<UIScrollView> ().ResetPosition ();
	}

	public void editng(){
		setposition ();
	}
	void setposition(){
		posisions = bars.transform.GetChild (0).transform.localPosition;
		for (int i =0; i<bars.transform.childCount; i++) {
			bars.transform.GetChild(i).transform.localPosition =new Vector3(posisions.x,posisions.y-(i*gap),posisions.z);
			bars.transform.GetChild(i).GetChild(0).GetComponent<UILabel>().text = (i+1).ToString();
			bars.transform.GetChild(i).GetChild(1).GetComponent<UILabel>().text = "T";
			bars.transform.GetChild(i).GetChild(2).GetComponent<UILabel>().text = "50";
			bars.transform.GetChild(i).GetChild(3).GetComponent<UILabel>().text = "20";
			bars.transform.GetChild(i).GetChild(4).GetComponent<UILabel>().text = "5";
			bars.transform.GetChild(i).GetChild(5).GetComponent<UILabel>().text = vgap(i).ToString();
			bars.transform.GetChild(i).GetChild(6).GetComponent<UISprite>().spriteName = "ic_doosan";
		}

	}
	float vgap(int i){
		vgaps = ((float.Parse (bars.transform.GetChild (0).GetChild (2).GetComponent<UILabel> ().text)
			- float.Parse (bars.transform.GetChild (i).GetChild (2).GetComponent<UILabel> ().text))+
			(float.Parse (bars.transform.GetChild (0).GetChild (3).GetComponent<UILabel> ().text)
			 - float.Parse (bars.transform.GetChild (i).GetChild (3).GetComponent<UILabel> ().text)))/2;
		//Debug.Log (vgaps);
		return vgaps;
	}
}
