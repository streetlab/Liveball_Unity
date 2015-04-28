using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Maincontrol : MonoBehaviour {
	public GameObject bgs;
	public float gap = 720;
	float addsum,addsumint;
	public float bargap = 122;
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
	List<int> dayandday = new List<int>();
	List<string> ch = new List<string>();
	List<int>  daycount = new List<int>();
	List<int>  wheresumint = new List<int>();
	List<Vector3> oldlist = new List<Vector3>(); 
	List<int>  when = new List<int>();
	
	
	Vector3 position,positions;
	float ChuldNum;
	string aa ;
	string districtAtime;
	string todays;
	char [] array;
	string joint;
	int a,b;
	int sumint=0;
	float sum;
	float num =0;
	float add =0;
	float dayadd =0;
	float daycountadd =0;
	float maxheight;

	bool editbll = false;
	bool b_nongame = false;
	
	
	GetScheduleEvent mScheduleEvent;
	
	public void editng(){
		editbll = true;
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
		add = 0;
		b_nongame = false;
		for (int i =0; i<bg_g.Count; i++) {
			if(b_nongame){
				if(daycount[i-1]==0){
					add+=1;
					bg_g[i].transform.localPosition = new Vector3(position.x,position.y-((gap*i)-(sumint*bargap)-(gap-((bargap*5)*add))),position.z);
					
					//nongame = true;
				}else{
					bg_g[i].transform.localPosition = new Vector3(position.x,position.y-((gap*i)-(sumint*bargap)-(gap-((bargap*5)*add))),position.z);
				}
			}else{
				if(i>0){
					if(daycount[i-1]==0){
						add+=1;
						bg_g[i].transform.localPosition = new Vector3(position.x,position.y-((gap*i)-(sumint*bargap)-(gap-((bargap*5)*add))),position.z);
						
						b_nongame = true;
					}else{
						bg_g[i].transform.localPosition = new Vector3(position.x,position.y-((gap*i)-(sumint*bargap)),position.z);
					}
				}else {
					bg_g[i].transform.localPosition = new Vector3(position.x,position.y-((gap*i)-(sumint*bargap)),position.z);
				}
			}
			if(daycount[i]!=0){
			Cdata (bg_g[i].transform.GetChild(0).gameObject,i);
			}
			//Debug.Log(bg_g[i].transform.GetChild(0).transform.parent.GetChild(1).GetComponent<UISprite>().localSize);
			//Debug.Log(bg_g[i].transform.GetChild(0).transform.parent.GetChild(1).GetComponent<UISprite>().transform.position);
			if(!editbll){
				
				bg_g[i].transform.GetChild(0).transform.parent.GetChild(1).GetComponent<UISprite>().SetRect(-338,-353-(((float)daycount[i]-5)*bargap),676,706+(((float)daycount[i]-5)*bargap));
				bg_g[i].transform.GetChild(0).transform.parent.GetChild(2).GetComponent<UISprite>().SetRect(-340,-355-(((float)daycount[i]-5)*bargap),680,710+(((float)daycount[i]-5)*bargap));
			}
			editbll = false;
			//bg_g[i].transform.GetChild(0).transform.parent.GetChild(1).GetComponent<UISprite>().SetRect(-338,-353,676,706);
		}
		
	}
	void Start(){
		
		wheresumint.Clear ();
		sumint = 0;
		bgs.SetActive (false);
		
		
		//Debug.Log (todays);
		//Debug.Log (bgs.transform.childCount);
		mScheduleEvent = new GetScheduleEvent (new EventDelegate (this, "setarrray"));
		NetMgr.GetScheduleAll (mScheduleEvent);
		//        setarrray ();
		
		transform.FindChild ("Scroll View").GetComponent<UIScrollView> ().ResetPosition ();
		
	}
	void chacktoday(){
		dayandday.Clear ();
		when.Clear ();
		for (int i = 0; i<mScheduleEvent.Response.data.Count; i+=1) {
			array = mScheduleEvent.Response.data [i].startDate.ToCharArray ();
			for (int z = 6; z<array.Length; z++) {
				
				ch.Add (array [z].ToString ());
				
			}
			aa = string.Join ("", ch.ToArray ());
			dayandday.Add(int.Parse(aa));

			if(i>0){
				if(
					dayandday[i]!=dayandday[i-1]){
					when.Add(i);
				}
			}else{
				when.Add(i);
			}
		//	Debug.Log(aa+"i : " + i);
			ch.Clear ();
		}
		daycount.Clear ();
		for (int i =0; i<7; i++) {
			daycount.Add(0);
		}
		for (int i =0; i<dayandday.Count; i++) {
			//Debug.Log(dayandday[i]+ " I : "+i+ " : " +(dayandday[0]+4));
		
			if(dayandday[i]==dayandday[when[0]]){
				daycount[0]+=1;
			}else if(dayandday[i]==dayandday[when[1]]){
				daycount[1]+=1;
			}else if(dayandday[i]==dayandday[when[2]]){
				daycount[2]+=1;
			}else if(dayandday[i]==dayandday[when[3]]){
				daycount[3]+=1;
			}else if(dayandday[i]==dayandday[when[4]]){
			
				daycount[4]+=1;
			}else if(dayandday[i]==dayandday[when[5]]){
				daycount[5]+=1;
			}else if(dayandday[i]==dayandday[when[6]]){
				daycount[6]+=1;
			}
			
			
			
			
			
		}
		
	}
	void nongame(){
		for (int i =daycount.Count-1; i>0; i--) {
			bg_g[i].SetActive(true);
			if(daycount[i]==0){
				Debug.Log(i);
				bg_g[i].SetActive(false);
			}
		}
	}
	
	void whattoday(){
		addsum = 0;
		addsumint = 0;
		dayadd = 0;

		maxheight = 0;
		for (int i = 0; i<daycount.Count; i++) {
			if(daycount[i] == 0){
				//addsum+=1;
				//addsumint-=5;
			}
			maxheight += (gap - (bargap * 5))+((float) daycount[i])*(bargap);
		}
		maxheight -= (gap - (bargap * 5)) * addsum;
		for (int s = 0; s<4; s++) {
			for (int i = mScheduleEvent.Response.data.Count-1; i>=0; i-=1) {
				array = mScheduleEvent.Response.data [i].startDate.ToCharArray ();
				for (int z = 6; z<array.Length; z++) {
					
					ch.Add (array [z].ToString ());
					
				}
				aa = string.Join ("", ch.ToArray ());
				todays=(System.DateTime.Now.Day).ToString();
				
				
				todays = (int.Parse(todays)+s).ToString();
				//Debug.Log(aa +" : "+ todays);
				dayadd =i;
				if (aa == todays) {


					for(int k = 0; k<dayandday.Count;k++){
					//	Debug.Log(aa + "  :  " +dayandday[k]+"   k");
						if(int.Parse(aa)==dayandday[k]){
							for(int t = 0; t<when.Count;t++){
								//Debug.Log(dayandday[when[t]] + "  :  " +dayandday[k]+"   t"+t);
								if(dayandday[k]==dayandday[when[t]]){
								//	Debug.Log(t);
									sum = when.Count-(t+1);
									break;
								}
							}

						
						}
					
					}

					//Debug.Log(aa+" : "+dayandday[dayandday.Count-1]);
					daycountadd = 0;
					for(int d = 0; d<7;d++){
						//Debug.Log(d);
						//Debug.Log(dayandday[i]+" : "+int.Parse(aa));
						daycountadd += daycount[6-d];
						Debug.Log(daycountadd+  "    :    " +dayadd);
						if(daycountadd>dayadd){
							if(daycount[6-d]==0){
								sum-=1;
								Debug.Log(daycount[6-d]+"    "+d);
								Debug.Log(sum+"    2");
							}
						}
					}
					//Debug.Log(aa+" : "+dayandday[0]);
					//Debug.Log(gap+ "  :  "+ sum);
					//Debug.Log(sumint+ "  :  "+bargap);
					//Debug.Log(addsumint+ "  :  "+addsum);
					float y = (gap * (sum))+(((float)sumint+addsumint)*bargap)-((gap - (bargap * 5))*addsum);
					//float y = maxheight-((gap * (sum))-(((float)sumint+addsumint)*bargap));
					//float y = maxheight;
					//Debug.Log(maxheight);
					//Debug.Log((gap * (sum))-(((float)sumint+addsumint)*bargap)+((gap - (bargap * 5))*addsum));
					//Debug.Log(maxheight-((gap * (sum))-(((float)sumint+addsumint)*bargap))+((gap - (bargap * 5))*addsum));
					y -= UtilMgr.GetScaledPositionY();
					bgs.transform.localPosition += new Vector3 (0, y, 0);



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
		chacktoday ();
		
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
		whattoday();
		
		nongame ();
	
		bg_g.Clear ();
		for (int i = 0; i<bgs.transform.childCount; i++) {
			//Debug.Log(">?!");

				oldlist.Add (bgs.transform.GetChild(i).transform.localPosition);

		}
		int g = 0;
		for (int i = 0; i<oldlist.Count; i++) {
			//Debug.Log(">?"+i);
			//if(i>0){
				if(daycount[6-i]==0){
					g -=1;
				}
			//}
			//Debug.Log(6-i+g+" fff  ");
			if(6-i+g>=0 ){
			bgs.transform.GetChild(6-i+g).transform.localPosition = oldlist[i];
			}
		}


		bgs.SetActive (true);


	}
	
	public void Cdata(GameObject g,int aaa){
		a = aaa;
		
		if (teamname.Count > 0) {
			//Debug.Log ("g : " + g.transform.parent);
		//	Debug.Log(a);
		//	Debug.Log(g.transform.GetChild (0));
		//	Debug.Log(daycount[a]);
		//	Debug.Log(day [a]);
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
		
	}
	void setbars(int i,GameObject g){
		
		g.transform.GetChild (1).GetChild (i).transform.localPosition = new Vector3 (positions.x, positions.y - (bargap * i), positions.z);
		if (teamname.Count > 0) {
			g.transform.GetChild (1).GetChild (i).gameObject.SetActive(true);
			if(daycount[a]<=i){
				g.transform.GetChild (1).GetChild (i).gameObject.SetActive(false);
				sumint+=1;
				wheresumint.Add(i + (a * 5));
			}else{
				g.transform.GetChild (1).GetChild (i).GetChild (0).GetComponent<UISprite> ().spriteName = teamimagename [i + (a * 5)-sumint];
				g.transform.GetChild (1).GetChild (i).GetChild (0).GetChild (0).GetComponent<UISprite> ().spriteName = hometeamimagename [i + (a * 5)-sumint];
				
				g.transform.GetChild (1).GetChild (i).GetChild (1).GetComponent<UILabel> ().text = teamname [i + (a * 5)-sumint];
				g.transform.GetChild (1).GetChild (i).GetChild (1).GetChild (0).GetComponent<UILabel> ().text = hometeamname [i + (a * 5)-sumint];
				
				g.transform.GetChild (1).GetChild (i).GetChild (2).GetComponent<UILabel> ().text = district [i + (a * 5)-sumint];
				g.transform.GetChild (1).GetChild (i).GetChild (2).GetChild (0).GetComponent<UILabel> ().text = time [i + (a * 5)-sumint];
			}
		}
	}
	public void buttening(int i){
		a = i;
		for (int w = 0; w<wheresumint.Count; w++) {
			//Debug.Log(i+" : "+wheresumint[w]);
			if(i>=wheresumint[w]+1){
				a-=1;
			}
			
			
		}
		//}
		//Debug.Log (i+" and "+a);
		UserMgr.Schedule = mScheduleEvent.Response.data [a-1];
		AutoFade.LoadLevel ("SceneMain", 0.5f, 1f);
		
	}
	
}