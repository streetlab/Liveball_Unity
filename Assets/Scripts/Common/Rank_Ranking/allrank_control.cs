using UnityEngine;
using System.Collections;

public class allrank_control : MonoBehaviour {
	public GameObject scv;
	public float bargap = 122;
	//public GameObject bar4;
	GameObject exbar;
	public float allp=10;
	// Use this for initialization
	void Start () {
		float allp=10;
		transform.FindChild ("Scroll View").GetComponent<UIScrollView> ().ResetPosition ();
		scv.transform.GetChild (5).GetComponent<UISprite> ().SetRect (0,-0,720,740+(bargap*allp));
		scv.transform.GetChild (5).transform.localPosition = new Vector2 (0,7-(bargap*allp/2));
		scv.transform.GetChild (5).GetComponent<BoxCollider2D> ().size = new Vector2 (720,740+(bargap*allp));

		scv.transform.GetChild (4).GetComponent<UISprite> ().SetRect (0,-0,680,700+(bargap*allp));
		scv.transform.GetChild (4).transform.localPosition = new Vector2 (0,7-(bargap*allp/2));

		scv.transform.GetChild (3).GetComponent<UISprite> ().SetRect (0,-0,676,696+(bargap*allp));
		scv.transform.GetChild (3).transform.localPosition = new Vector2 (0,7-(bargap*allp/2));




		transform.FindChild ("Scroll View").GetComponent<UIScrollView> ().ResetPosition ();
		scv.transform.GetChild (2).GetChild (0).GetComponent<UILabel>().text = "768";
		//scv.transform.GetChild (2).GetChild (0).GetChild (0).GetComponent<UISprite> ().spriteName = "getdata";
		scv.transform.GetChild (2).GetChild (0).GetChild (2).GetComponent<UILabel>().text = "myname";
		scv.transform.GetChild (2).GetChild (0).GetChild (3).GetComponent<UILabel>().text = "myinfo";

		switch("non"){
		case "non":
			scv.transform.GetChild (2).GetChild (0).GetChild (1).GetComponent<UISprite>().color = new Color(0.855f,0.86f,0.888f,1);
			scv.transform.GetChild (2).GetChild (0).GetChild (1).GetComponent<UISprite>().spriteName = "bg_circle";
			break;
		case "up":
			Debug.Log("upupup");
			scv.transform.GetChild (2).GetChild (0).GetChild (1).GetComponent<UISprite>().color = new Color(0.145f,0.68f,0.88f,1);
			scv.transform.GetChild (2).GetChild (0).GetChild (1).GetComponent<UISprite>().spriteName = "ic_arrow";
			break;
		case "down":
			scv.transform.GetChild (2).GetChild (0).GetChild (1).GetComponent<UISprite>().color = new Color(0.88f,0.23f,0.255f,1);
			scv.transform.GetChild (2).GetChild (0).GetChild (1).GetComponent<UISprite>().spriteName = "ic_arrow";
			break;
		}

		//allp ="getdata";
		for (int i =0; i<4; i++) {
			//scv.transform.GetChild(0).GetChild(i).GetChild(1).GetComponent<UISprite>().spriteName = "getdata";
			rankswitch(i);
			scv.transform.GetChild(0).GetChild(i).GetChild(3).GetComponent<UILabel>().text = "getdata"+(i+1).ToString();
			scv.transform.GetChild(0).GetChild(i).GetChild(4).GetComponent<UILabel>().text = "getdata";
		}
		for (int i =0; i<allp; i++) {
			//Debug.Log(scv.transform.GetChild(0).GetChild(3).transform.localPosition.y+(bargap*((float)i+1)));
			exbar = (GameObject)Instantiate (scv.transform.GetChild(0).GetChild(3).gameObject, new Vector2(0,0), scv.transform.GetChild(0).GetChild(3).transform.localRotation);
			exbar.transform.parent = scv.transform.GetChild(0);

			exbar.transform.localScale = new Vector3(1,1,1);
			exbar.transform.localPosition = new Vector2(scv.transform.GetChild(0).GetChild(3).transform.localPosition.x,
			                                            scv.transform.GetChild(0).GetChild(3).transform.localPosition.y-(bargap*((float)i+1)));
		
			exbar.name = "bar"+(5+i).ToString();
			exbar.transform.GetChild(0).GetComponent<UILabel>().text = (5+i).ToString();
			//exbar.transform.GetChild(0).GetComponent<UISprite>().spriteName = "getdata";
			rankswitch(i+4);
			exbar.transform.GetChild(3).GetComponent<UILabel>().text = "getdata"+(i+5).ToString();
			exbar.transform.GetChild(4).GetComponent<UILabel>().text = "getdata";
			//Debug.Log (exbar);
		}
	


	
	}
	void rankswitch(int i){
		Debug.Log (scv.transform.GetChild(0).GetChild(i).GetChild(1));
		switch("non"){
		case "non":
			scv.transform.GetChild(0).GetChild(i).GetChild(2).GetComponent<UISprite>().color = new Color(0.855f,0.86f,0.888f,1);
			scv.transform.GetChild(0).GetChild(i).GetChild(2).GetComponent<UISprite>().spriteName = "bg_circle";
			break;
		case "up":
			Debug.Log("upupup");
			scv.transform.GetChild(0).GetChild(i).GetChild(2).GetComponent<UISprite>().color = new Color(0.145f,0.68f,0.88f,1);
			scv.transform.GetChild(0).GetChild(i).GetChild(2).GetComponent<UISprite>().spriteName = "ic_arrow";
			break;
		case "down":
			scv.transform.GetChild(0).GetChild(i).GetChild(2).GetComponent<UISprite>().color = new Color(0.88f,0.23f,0.255f,1);
			scv.transform.GetChild(0).GetChild(i).GetChild(2).GetComponent<UISprite>().spriteName = "ic_arrow";
			break;
		}
	}
	// Update is called once per frame
	void Update () {
	
	}
}
