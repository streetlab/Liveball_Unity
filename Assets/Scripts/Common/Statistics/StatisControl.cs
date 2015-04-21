using UnityEngine;
using System.Collections;

public class StatisControl : MonoBehaviour {

	public GameObject bgs;
	public float gap = 536;
	public float bargap = 122;
	Vector3 positions;
	Vector3 barposition;
	public void editng(){
		setposition ();
	}
	void Start(){
		setposition ();
		transform.FindChild ("Scroll View").GetComponent<UIScrollView> ().ResetPosition ();
	}
	void setposition(){
		positions = bgs.transform.GetChild(0).transform.localPosition;
		for(int i = 0;i<bgs.transform.childCount;i++){

			bgs.transform.GetChild(i).transform.localPosition = new Vector3(positions.x,positions.y-(gap*i),positions.z);

			bgs.transform.GetChild(i).GetChild(0).GetChild(2).GetComponent<UILabel>().text = "set";
			barposition = bgs.transform.GetChild(i).GetChild(0).GetChild(0).GetChild(0).transform.localPosition;
			for(int a = 0; a<bgs.transform.GetChild(i).GetChild(0).GetChild(0).childCount-1;a++){

				bgs.transform.GetChild(i).GetChild(0).GetChild(0).GetChild(a).transform.localPosition = new Vector3(
					barposition.x,barposition.y-(a*bargap),barposition.z);
				//bgs.transform.GetChild(i).GetChild(0).GetChild(0).GetChild(a).GetChild(0).
				//	GetComponent<UISprite>().spriteName = "";
			
			//	Debug.Log(bgs.transform.GetChild(i).GetChild(0).GetChild(0).GetChild(a).GetChild(1).gameObject);
				bgs.transform.GetChild(i).GetChild(0).GetChild(0).GetChild(a).GetChild(1).GetComponent<UILabel>().text = "name";
				bgs.transform.GetChild(i).GetChild(0).GetChild(0).GetChild(a).GetChild(2).GetComponent<UILabel>().text = "team name";
				bgs.transform.GetChild(i).GetChild(0).GetChild(0).GetChild(a).GetChild(3).GetComponent<UILabel>().text = "0.000";
					
			}
		}


	}
}
