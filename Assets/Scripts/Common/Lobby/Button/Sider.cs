using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Sider : MonoBehaviour {
	public int DivisionCount;
	public List<string> Value = new List<string>();
	public Dictionary<float,string> List1 = new Dictionary<float,string>();
	public Dictionary<float,string> List2 = new Dictionary<float,string>();
	void Start(){
		if (name != "InSlider") {
			if (DivisionCount != 0) {
				GetComponent<UISlider> ().DivisionCount = DivisionCount;
				transform.FindChild ("InSlider").GetComponent<UISlider> ().DivisionCount = DivisionCount;
				float num = (float)DivisionCount;
				for(float i = 0; i < num+1 ; i++){
				//	Debug.Log(i/num);
					List1.Add(i/num,Value[(int)i]);
					
				}
				transform.FindChild ("InSlider").GetComponent<Sider> ().List1 = List1;
			//	List.Clear();
				for(float i = 0; i < num +1; i++){
					List2.Add(i/num,Value[Value.Count-1-(int)i]);

				}
				transform.FindChild ("InSlider").GetComponent<Sider> ().List2 = List2;
		


			}

		}

	}
	// Use this for initialization
	public void Button(){
		if (name != "InSlider") {
			float L = GetComponent<UISlider> ().value;
			float R = transform.FindChild ("InSlider").GetComponent<UISlider> ().value;
			float Num = L * (float)DivisionCount;
			L = Mathf.Round (Num) * 1f / (float)DivisionCount;
		
			if (Mathf.Round ((R + L) * 1000f) * 0.001f > 1f) {
				GetComponent<UISlider> ().P = true;
			} else {
				GetComponent<UISlider> ().P = false;
			}
			Num = R * (float)DivisionCount;
			R = Mathf.Round (Num) * 1f / (float)DivisionCount;
			//	Debug.Log(R);
			if(transform.parent.name=="AllEntranceFeeBox"){
				if(L == 0&&R == 0){

					transform.parent.FindChild("Menu 0").GetComponent<UILabel>().text 
						= "모든 입장료";

				}else if(L==0){
					
					transform.parent.FindChild("Menu 0").GetComponent<UILabel>().text 
						= List1[L]+" - 루비"+List2[R];
				}else{

					transform.parent.FindChild("Menu 0").GetComponent<UILabel>().text 
						= "루비" + List1[L]+" - "+List2[R];

				}
				transform.parent.parent.FindChild("Label").GetComponent<UILabel>().text = 
					transform.parent.FindChild("Menu 0").GetComponent<UILabel>().text;
			}
		} else {
			float L = 	transform.parent.GetComponent<UISlider> ().value;
			float R =  GetComponent<UISlider> ().value;

	
			float Num = R * (float)GetComponent<UISlider> ().DivisionCount;
			//Debug.Log(Num);
			R = Mathf.Round (Num) * 1f / (float)GetComponent<UISlider> ().DivisionCount;
			if (Mathf.Round ((R + L) * 1000f) * 0.001f > 1f) {
				GetComponent<UISlider> ().P = true;
			} else {
				GetComponent<UISlider> ().P = false;
			}
		//	Debug.Log(Mathf.Round((R+L)*1000f)*0.001f);
			Num = L * (float)GetComponent<UISlider> ().DivisionCount;
			//Debug.Log(Num);
			L = Mathf.Round (Num) * 1f / (float)GetComponent<UISlider> ().DivisionCount;
		
			if(transform.parent.parent.name=="AllEntranceFeeBox"){
//				if(R==0){
//					
//					transform.parent.parent.parent.FindChild("Menu 0").GetComponent<UILabel>().text 
//						= List2[L]+" - 루비"+List1[R];
//				}else 
				if(L == 0&&R == 0){
					
					transform.parent.parent.FindChild("Menu 0").GetComponent<UILabel>().text 
						= "모든 입장료";
					
				}else{
					
					transform.parent.parent.FindChild("Menu 0").GetComponent<UILabel>().text 
						= "루비" + List1[L]+" - "+List2[R];
					
				}
				transform.parent.parent.parent.FindChild("Label").GetComponent<UILabel>().text = 
					transform.parent.parent.FindChild("Menu 0").GetComponent<UILabel>().text;
			}
		
		}

	}

}
