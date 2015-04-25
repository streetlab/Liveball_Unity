using UnityEngine;
using System.Collections;

public class fanleaguecontrol : MonoBehaviour {

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
			
			//bgs.transform.GetChild(i).GetChild(0).GetChild(2).GetComponent<UILabel>().text = "set";
			barposition = bgs.transform.GetChild(i).GetChild(0).GetChild(0).GetChild(0).transform.localPosition;
			for(int a = 0; a<bgs.transform.GetChild(i).GetChild(0).GetChild(0).childCount-1;a++){
				
				bgs.transform.GetChild(i).GetChild(0).GetChild(0).GetChild(a).transform.localPosition = new Vector3(
					barposition.x,barposition.y-(a*bargap),barposition.z);
				//bgs.transform.GetChild(i).GetChild(0).GetChild(0).GetChild(a).GetChild(1).
				//	GetComponent<UISprite>().spriteName = "";
				
				//Debug.Log("ssssss");
				switch("seticon"){
				case "non":
					bgs.transform.GetChild(i).GetChild(0).GetChild(0).GetChild(a).GetChild(2).GetComponent<UISprite>().color = new Color(0.855f,0.86f,0.888f,1);
					bgs.transform.GetChild(i).GetChild(0).GetChild(0).GetChild(a).GetChild(2).GetComponent<UISprite>().spriteName = "bg_circle";
					break;
				case "up":
					Debug.Log("upupup");
					bgs.transform.GetChild(i).GetChild(0).GetChild(0).GetChild(a).GetChild(2).GetComponent<UISprite>().color = new Color(0.145f,0.68f,0.88f,1);
					bgs.transform.GetChild(i).GetChild(0).GetChild(0).GetChild(a).GetChild(2).GetComponent<UISprite>().spriteName = "ic_arrow";
					break;
				case "down":
					bgs.transform.GetChild(i).GetChild(0).GetChild(0).GetChild(a).GetChild(2).GetComponent<UISprite>().color = new Color(0.88f,0.23f,0.255f,1);
					bgs.transform.GetChild(i).GetChild(0).GetChild(0).GetChild(a).GetChild(2).GetComponent<UISprite>().spriteName = "ic_arrow";
					break;
				}
				
				bgs.transform.GetChild(i).GetChild(0).GetChild(0).GetChild(a).GetChild(3).GetComponent<UILabel>().text = "name";
				bgs.transform.GetChild(i).GetChild(0).GetChild(0).GetChild(a).GetChild(4).GetComponent<UILabel>().text = "team name";
				
				
			}
		}
		
		
	}
	public void allview(int i){
		Debug.Log ("what the i!!!!! : " + i);
		transform.parent.GetChild (i + 1).gameObject.SetActive (true);
		transform.parent.GetChild (0).gameObject.SetActive (false);
	

		transform.parent.parent.GetChild (1).GetChild (1).gameObject.SetActive (false);
		transform.parent.parent.GetChild (1).GetChild (0).GetChild(1).gameObject.SetActive (false);
		transform.parent.parent.GetChild (1).GetChild (0).GetChild(2).gameObject.SetActive (false);
		transform.parent.parent.GetChild (1).GetChild (0).GetChild(4).gameObject.SetActive (true);
		transform.parent.parent.GetChild (1).GetChild (0).GetChild(5).gameObject.SetActive (true);
	

	}
}
