using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Maincontrol : MonoBehaviour {
	public GameObject bgs;
	public float gap = 682;
	public float bargap = 100f;
	public string yui;
	List<GameObject> bg_g = new List<GameObject>();
	List<string> teamname = new List<string>();
	List<string> teamimagename = new List<string>();
	List<string> hometeamname = new List<string>();
	List<string> hometeamimagename = new List<string>();
	List<string> district = new List<string>();
	List<string> time = new List<string>();
	List<string> day = new List<string>();
	List<string> date = new List<string>();
	List<string> ch = new List<string>();
	Vector3 position,positions;
	float ChuldNum;
	string aa ;
	string districtAtime;
	string todays;
	char [] array;
	string joint;
	int a;

	float num =0;


	GetScheduleEvent mScheduleEvent;

	public void editng(){
		positionset ();
		teamname.Clear ();
		teamimagename.Clear ();
		time.Clear ();
		district.Clear ();
		date.Clear ();
		day.Clear ();
		bg_g.Clear();
	}
	void positionset(){
		for (int i = 0; i<bgs.transform.childCount; i++) {
			bg_g.Add(bgs.transform.GetChild(i).gameObject);
		}
		//Debug.Log (bg_g.Count);
		position = bg_g[0].transform.localPosition;
		for (int i =0; i<bg_g.Count; i++) {
			bg_g[i].transform.localPosition = new Vector3(position.x,position.y-(gap*i),position.z);

			Cdata (bg_g[i].transform.GetChild(0).gameObject,i);
		}
	
	}
	void Start(){
	
		bgs.SetActive (false);


		//Debug.Log (todays);
		//Debug.Log (bgs.transform.childCount);
		mScheduleEvent = new GetScheduleEvent (new EventDelegate (this, "setarrray"));
		NetMgr.GetScheduleAll (mScheduleEvent);
//		setarrray ();

		transform.FindChild ("Scroll View").GetComponent<UIScrollView> ().ResetPosition ();

	}

	void whattoday(){
		for (int s = 0; s<4; s++) {
			for (int i = 0; i<mScheduleEvent.Response.data.Count; i+=5) {
				array = mScheduleEvent.Response.data [i].startDate.ToCharArray ();
				for (int z = 6; z<array.Length; z++) {
		
					ch.Add (array [z].ToString ());
			
				}
				aa = string.Join ("", ch.ToArray ());
				todays=System.DateTime.Now.Day.ToString();
				todays = (int.Parse(todays)+s).ToString();
				Debug.Log(aa +" : "+ todays);
				if (aa == todays) {
					bgs.transform.localPosition += new Vector3 (0, 730 * ((float)i / 5), 0);
					ch.Clear ();
					return;
				}
		
				ch.Clear ();
			}
		}
	}
	void setarrray(){

		teamname.Clear ();
		teamimagename.Clear ();
		time.Clear ();
		district.Clear ();
		date.Clear ();
		day.Clear ();
		//Debug.Log (mScheduleEvent.Response.data [0].extend [0].teamName);
		whattoday();
		for (int i = 0; i<mScheduleEvent.Response.data.Count; i+=5) {
			day.Add (mScheduleEvent.Response.data [i].onairDay+yui);
			array = mScheduleEvent.Response.data [i].startDate.ToCharArray ();
			for(int z = 0; z<array.Length;z++){
				ch.Add (array[z].ToString());
				if(z==3||z==5){
					ch.Add (".");
				}


			}
			aa = string.Join("", ch.ToArray());
			date.Add (aa);
			ch.Clear();
		}

		for(int i = 0;i<mScheduleEvent.Response.data.Count;i++){

			teamname.Add (mScheduleEvent.Response.data [i].extend [0].teamName);
			hometeamname.Add (mScheduleEvent.Response.data [i].extend [1].teamName);
			string imgName = UtilMgr.GetTeamEmblem(mScheduleEvent.Response.data [i].extend [0].imageName);
			teamimagename.Add (imgName);
			imgName = UtilMgr.GetTeamEmblem(mScheduleEvent.Response.data [i].extend [1].imageName);
			hometeamimagename.Add (imgName);
			array = mScheduleEvent.Response.data [i].subTitle.ToCharArray ();

			for(int z = 0; z<array.Length;z++){
				if(num==3){
					ch.Add (array[z].ToString());
				}
				if(num==2){
					num+=1;

				}
				if(array[z]==','){
					num+=1;

				}

			}
			num=0;
			aa = string.Join("", ch.ToArray());
			//Debug.Log(aa);
			district.Add(aa);
			ch.Clear();
			array = mScheduleEvent.Response.data [i].startTime.ToCharArray ();
			for(int z = 8; z<12;z++){
			
				//Debug.Log(array[z]);
				ch.Add (array[z].ToString());
				if(z==9){
					ch.Add (":");
				}

			}
			aa = string.Join("", ch.ToArray());
			//Debug.Log(aa);
			time.Add(aa);
			ch.Clear();
			
		}
		//setting/parser
		//List<use>;
		//Debug.Log (date);
		positionset ();
		

		bg_g.Clear ();
	}

	public void Cdata(GameObject g,int aa){
		a = aa;

		if (teamname.Count > 0) {
			//Debug.Log ("g : " + g.transform.parent);
			g.transform.GetChild (0).GetComponent<UILabel> ().text = day [a];
			g.transform.GetChild (0).GetChild (0).GetComponent<UILabel> ().text = date [a];
		}
		positions = g.transform.GetChild (1).GetChild (0).transform.localPosition;
		for(int i =0;i<g.transform.GetChild(1).childCount;i++){

			switch(i){
			case 0:
				setbars(i,g);
				break;
			case 1:
				setbars(i,g);
				break;
			case 2:
				setbars(i,g);
				break;
			case 3:
				setbars(i,g);
				break;
			case 4:
				setbars(i,g);
				break;
			}

			
			
		}
		bgs.SetActive (true);
	}
	void setbars(int i,GameObject g){

			g.transform.GetChild (1).GetChild (i).transform.localPosition = new Vector3 (positions.x, positions.y - (bargap * i), positions.z);
		if (teamname.Count > 0) {
			g.transform.GetChild (1).GetChild (i).GetChild (0).GetComponent<UISprite> ().spriteName = teamimagename [i + (a * 5)];
			g.transform.GetChild (1).GetChild (i).GetChild (0).GetChild (0).GetComponent<UISprite> ().spriteName = hometeamimagename [i + (a * 5)];
		
			g.transform.GetChild (1).GetChild (i).GetChild (1).GetComponent<UILabel> ().text = teamname [i + (a * 5)];
			g.transform.GetChild (1).GetChild (i).GetChild (1).GetChild (0).GetComponent<UILabel> ().text = hometeamname [i + (a * 5)];

			g.transform.GetChild (1).GetChild (i).GetChild (2).GetComponent<UILabel> ().text = district [i + (a * 5)];
			g.transform.GetChild (1).GetChild (i).GetChild (2).GetChild (0).GetComponent<UILabel> ().text = time [i + (a * 5)];
		}
	}
	public void buttening(int i){

		Debug.Log (i);
		//UserMgr.Schedule = mScheduleEvent.Response.data [i];
		//AutoFade.LoadLevel ("SceneMain", 0.5f, 1f);
		
	}

}
