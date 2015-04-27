using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class fanleaguecontrol : MonoBehaviour {

	public GameObject bgs;
	public float gap = 536;
	public float bargap = 122;
	Vector3 positions;
	Vector3 barposition;
	List<List<List<List<string>>>> ALL = new List<List<List<List<string>>>>();
	public List<GameObject> scv = new List<GameObject>();
	public List<string> Labels = new List<string>();
	public List<List<string>> myrank = new List<List<string>>();
	
	//public GameObject bar4;
	GameObject exbar;
	public float allp=10;
	public float allu=10;
	public void editng(){
		setposition ();
	}
	void Start(){
		Debug.Log (allp);
		for (int i = 0; i<scv.Count; i++) {
			myrank.Add(new List<string>());
			ALL.Add(new List<List<List<string>>>());
			for (int a = 0; a<2;a++){
				ALL[i].Add(new List<List<string>>());
			
			}
			for (int w = 0; w<allp;w++){
				ALL[i][0].Add(new List<string>());
				
				
			}
			for (int w = 0; w<allp*allu;w++){
				ALL[i][1].Add(new List<string>());
				
				
			}
		}
		getdata ();
		setposition ();
		child ();
		transform.GetChild(0).FindChild ("Scroll View").GetComponent<UIScrollView> ().ResetPosition ();
	}
	void getdata(){
		for (int i = 0; i<scv.Count; i++) {
			for(int a = 0; a<allp;a++){
				ALL[i][0][a].Add("1");
				ALL[i][0][a].Add("teamname");
				ALL[i][0][a].Add("teamimage");
				ALL[i][0][a].Add("2");
			//	Debug.Log(ALL[i][0][a][1]);

			}
			for(int a = 0; a<allp*allu;a++){
				ALL[i][1][a].Add("1");
				ALL[i][1][a].Add("name");
				ALL[i][1][a].Add("userimage");
				ALL[i][1][a].Add("2");
				ALL[i][1][a].Add("note");
				
			}
			for (int a = 0; a<allu;a++){
				myrank[i].Add("my : " + i.ToString() + " : " + i.ToString() );
			}
		}
	

	}

	void setposition(){
		positions = bgs.transform.GetChild(0).transform.localPosition;
		for(int i = 0;i<scv.Count;i++){
			
			bgs.transform.GetChild(i).transform.localPosition = new Vector3(positions.x,positions.y-(gap*i),positions.z);
			
			bgs.transform.GetChild(i).GetChild(0).GetChild(2).GetComponent<UILabel>().text = Labels[i];
			barposition = bgs.transform.GetChild(i).GetChild(0).GetChild(0).GetChild(0).transform.localPosition;
			for(int a = 0; a<bgs.transform.GetChild(i).GetChild(0).GetChild(0).childCount-1;a++){
				
				bgs.transform.GetChild(i).GetChild(0).GetChild(0).GetChild(a).transform.localPosition = new Vector3(
					barposition.x,barposition.y-(a*bargap),barposition.z);
				//bgs.transform.GetChild(i).GetChild(0).GetChild(0).GetChild(a).GetChild(1).
				//	GetComponent<UISprite>().spriteName = ALL[i][0][a][2];
				
				//Debug.Log("ssssss");

				rankswitch(i,a,0,bgs.transform.GetChild(i).GetChild(0).GetChild(0).GetChild(a).GetChild(2).gameObject);
				
				bgs.transform.GetChild(i).GetChild(0).GetChild(0).GetChild(a).GetChild(3).GetComponent<UILabel>().text = ALL[i][0][a][1];

				
				
			}
		}
		
		
	}

	





	void child(){





		for (int q = 0; q<scv.Count; q++) {
			scv[q].transform.GetChild (5).GetComponent<UISprite> ().SetRect (0, -0, 720, 740 + (bargap * (allp-5)));
			scv[q].transform.GetChild (5).transform.localPosition = new Vector2 (0, 7 - (bargap * (allp-5) / 2));
			scv[q].transform.GetChild (5).GetComponent<BoxCollider2D> ().size = new Vector2 (720, 740 + (bargap * allp));
		
			scv[q].transform.GetChild (4).GetComponent<UISprite> ().SetRect (0, -0, 680, 700 + (bargap * (allp-5)));
			scv[q].transform.GetChild (4).transform.localPosition = new Vector2 (0, 7 - (bargap * (allp-5) / 2));
		
			scv[q].transform.GetChild (3).GetComponent<UISprite> ().SetRect (0, -0, 676, 696 + (bargap * (allp-5)));
			scv[q].transform.GetChild (3).transform.localPosition = new Vector2 (0, 7 - (bargap * (allp-5) / 2));
		
		
		
		
			//transform.FindChild ("Scroll View").GetComponent<UIScrollView> ().ResetPosition ();
			//scv[q].transform.GetChild (2).GetChild (0).GetComponent<UILabel> ().text = "768";
			//scv.transform.GetChild (2).GetChild (0).GetChild (0).GetComponent<UISprite> ().spriteName = "getdata";
			scv[q].transform.GetChild (2).GetComponent<UILabel> ().text = Labels[q];

		
//			switch ("non") {

		

			for (int i =0; i<4; i++) {
				//scv.transform.GetChild(0).GetChild(i).GetChild(1).GetComponent<UISprite>().spriteName = "getdata";
				rankswitch (q,i,0,scv[q].transform.GetChild (0).GetChild (i).GetChild (2).gameObject);
				scv[q].transform.GetChild (0).GetChild (i).GetChild (3).GetComponent<UILabel> ().text = ALL[q][0][i][1] + (i + 1).ToString ();

			}
			for (int i =0; i<allp-4; i++) {
				//Debug.Log(scv.transform.GetChild(0).GetChild(3).transform.localPosition.y+(bargap*((float)i+1)));
				exbar = (GameObject)Instantiate (scv[q].transform.GetChild (0).GetChild (3).gameObject, new Vector2 (0, 0), scv[q].transform.GetChild (0).GetChild (3).transform.localRotation);
				exbar.transform.parent = scv[q].transform.GetChild (0);
			
				exbar.transform.localScale = new Vector3 (1, 1, 1);
				exbar.transform.localPosition = new Vector2 (scv[q].transform.GetChild (0).GetChild (3).transform.localPosition.x,
				                                             scv[q].transform.GetChild (0).GetChild (3).transform.localPosition.y - (bargap * ((float)i + 1)));
			
				exbar.name = "bar" + (5 + i).ToString ();
				exbar.transform.GetChild (0).GetComponent<UILabel> ().text = (5 + i).ToString ();
				//exbar.transform.GetChild(1).GetComponent<UISprite>().spriteName = "getdata";
				rankswitch (q,i + 4,0,exbar.transform.GetChild (2).gameObject);
				exbar.transform.GetChild (3).GetComponent<UILabel> ().text = ALL[q][0][i+4][1] + (i + 5).ToString ();
			
				//Debug.Log (exbar);
			}
		
		

		
			for(int i = 0 ; i<allp;i++){
				//Debug.Log(i);
				transform.GetChild(q+1).GetChild(i+2).GetChild(0).GetChild (5).GetComponent<UISprite> ().SetRect (0,-0,720,797+(bargap*(allu-4)));
				transform.GetChild(q+1).GetChild(i+2).GetChild(0).GetChild (5).transform.localPosition = new Vector2 (0,-23-(bargap*(allu-4)/2));
				transform.GetChild(q+1).GetChild(i+2).GetChild(0).GetChild (5).GetComponent<BoxCollider2D> ().size = new Vector2 (720,740+(bargap*(allu-4)));
				
				transform.GetChild(q+1).GetChild(i+2).GetChild(0).GetChild (4).GetComponent<UISprite> ().SetRect (0,-0,680,757+(bargap*(allu-4)));
				transform.GetChild(q+1).GetChild(i+2).GetChild(0).GetChild (4).transform.localPosition = new Vector2 (0,-23-(bargap*(allu-4)/2));
				
				transform.GetChild(q+1).GetChild(i+2).GetChild(0).GetChild (3).GetComponent<UISprite> ().SetRect (0,-0,676,753+(bargap*(allu-4)));
				transform.GetChild(q+1).GetChild(i+2).GetChild(0).GetChild (3).transform.localPosition = new Vector2 (0,-23-(bargap*(allu-4)/2));
			//	Debug.Log(	transform.GetChild(q+1).GetChild(i+2).GetChild(0).GetChild(2).GetChild(0) + "    " + myrank[q][i] );
				//Debug.Log( myrank[q][i] + " :: " +q + " :: "+ i + " :: "+ myrank.Count+ " :: " +myrank[q].Count ) ;
				transform.GetChild(q+1).GetChild(i+2).GetChild(0).GetChild(2).GetChild(0).GetComponent<UILabel>().text = myrank[q][i];
				transform.GetChild(q+1).GetChild(i+2).GetChild(0).GetChild(2).GetChild(0).GetChild(2).GetComponent<UILabel>().text = "myname";
				transform.GetChild(q+1).GetChild(i+2).GetChild(0).GetChild(2).GetComponent<UILabel>().text = Labels[q+3];




				for (int a =0; a<4; a++) {
					//transform.GetChild(q+1).GetChild(i+2).GetChild(0).GetChild(0).GetChild(i).GetChild(1).GetComponent<UISprite>().spriteName = "getdata";
					//Debug.Log (ALL [0] [1].Count);
					//Debug.Log(ALL[q][1][(i*10)+a][1]  + " : " + i + " : " + a);
					transform.GetChild(q+1).GetChild(i+2).GetChild(0).GetChild(0).GetChild(a).GetChild(3).GetComponent<UILabel>().text = ALL[q][1][(i*10)+a][1];
					rankswitch (q,i,1,transform.GetChild(q+1).GetChild(i+2).GetChild(0).GetChild(0).GetChild(a).GetChild(2).gameObject);

				}
				for (int t =0; t<allu-4; t++) {
				//Debug.Log(scv.transform.GetChild(0).GetChild(3).transform.localPosition.y+(bargap*((float)i+1)));
					exbar = (GameObject)Instantiate (transform.GetChild(q+1).GetChild(i+2).GetChild(0).GetChild(0).GetChild(3).gameObject, new Vector2 (0, 0),transform.GetChild(q+1).GetChild(i+2).GetChild(0).GetChild(0).GetChild(3).transform.localRotation);
				
					exbar.transform.parent = transform.GetChild(q+1).GetChild(i+2).GetChild(0).GetChild(0);
			
				exbar.transform.localScale = new Vector3 (1, 1, 1);
					exbar.transform.localPosition = new Vector2 (transform.GetChild(q+1).GetChild(i+2).GetChild(0).GetChild(0).GetChild(3).transform.localPosition.x,
					                                             transform.GetChild(q+1).GetChild(i+2).GetChild(0).GetChild(0).GetChild(3).transform.localPosition.y - (bargap * ((float)t + 1)));
			
					//Debug.Log(transform.GetChild(q+1).GetChild(t+2).GetChild(0).GetChild(0).GetChild(3) +":>:"+t);
					exbar.name = "bar" + (5 + t).ToString ();
					//Debug.Log(exbar.gameObject.ToString ()+"??"+t);

				exbar.transform.GetChild (0).GetComponent<UILabel> ().text = (5 + t).ToString ();
				//exbar.transform.GetChild(1).GetComponent<UISprite>().spriteName = "getdata";
					rankswitch (q,(i*10)+t+4,1,exbar.transform.GetChild (2).gameObject);
					exbar.transform.GetChild (3).GetComponent<UILabel> ().text = ALL[q][1][(i*10)+t+4][1] + (t + 5).ToString ();
			
				//Debug.Log (exbar);
			}

				//transform.GetChild(q+1).GetChild(i+2).GetChild(0).GetChild(0);

			}



		}
		
	}



	void rankswitch(int i,int a,int v,GameObject g){
		//Debug.Log (ALL[i][1].Count);
		//Debug.Log (a);

		if (int.Parse(ALL[i][v][a][0]) == int.Parse(ALL[i][v][a][3])) {
			g.GetComponent<UISprite>().color = new Color(0.855f,0.86f,0.888f,1);
			g.GetComponent<UISprite>().spriteName = "bg_circle";
			g.transform.localRotation = Quaternion.Euler(new Vector3 (0,0,0));
		}else if(int.Parse(ALL[i][v][a][0]) > int.Parse(ALL[i][v][a][3])){
			g.GetComponent<UISprite>().color = new Color(0.145f,0.68f,0.88f,1);
			g.GetComponent<UISprite>().spriteName = "ic_arrow";
			g.transform.localRotation = Quaternion.Euler(new Vector3 (0,0,0));
		}else if(int.Parse(ALL[i][v][a][0]) < int.Parse(ALL[i][v][a][3])){
			g.GetComponent<UISprite>().color = new Color(0.88f,0.23f,0.255f,1);
			g.GetComponent<UISprite>().spriteName = "ic_arrow";
			g.transform.localRotation = Quaternion.Euler(new Vector3 (0,0,180));
		}

	}

	public void allview(int i){
		//Debug.Log ("what the i!!!!! : " + i);
		transform.GetChild (i + 1).gameObject.SetActive (true);
		transform.GetChild (0).gameObject.SetActive (false);
	

		transform.parent.GetChild (1).GetChild (1).gameObject.SetActive (false);
		transform.parent.GetChild (1).GetChild (0).GetChild(1).gameObject.SetActive (false);
		transform.parent.GetChild (1).GetChild (0).GetChild(2).gameObject.SetActive (false);
		transform.parent.GetChild (1).GetChild (0).GetChild(4).gameObject.SetActive (true);
		transform.parent.GetChild (1).GetChild (0).GetChild(5).gameObject.SetActive (true);
	

	}
	public void inallview(int i,int a){
		Debug.Log (i+" "+a);
		for(int q = 0; q < 10;q++){
			transform.GetChild (1+a).GetChild(2+i).gameObject.SetActive (false);
		}

		transform.GetChild (1+a).GetChild(i+2).gameObject.SetActive (true);
		transform.GetChild (1+a).GetChild(1).gameObject.SetActive (false);
		transform.GetChild (1+a).GetChild(0).gameObject.SetActive (false);
	}
}
