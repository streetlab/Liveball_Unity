using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Sider : MonoBehaviour {
	public int DivisionCount;
	public List<string> Value = new List<string>();
	public Dictionary<float,string> List1 = new Dictionary<float,string>();
	public Dictionary<float,string> List2 = new Dictionary<float,string>();
	public GameObject mLblLabel;
	public GameObject mLblMenu0;

	void Start(){
//		Debug.Log("start");
		if (name != "InSlider") {
			if (DivisionCount != 0) {
				GetComponent<UISlider> ().DivisionCount = DivisionCount;
				transform.FindChild ("InSlider").GetComponent<UISlider> ().DivisionCount = DivisionCount;
				float num = (float)DivisionCount;
				for(float i = 0; i < num+1 ; i++){
				//	Debug.Log(i/num);
					List1.Add(i,Value[(int)i]);
					
				}
				transform.FindChild ("InSlider").GetComponent<Sider> ().List1 = List1;
			//	List.Clear();
				for(float i = 0; i < num +1; i++){
					List2.Add(i,Value[Value.Count-1-(int)i]);

				}
				transform.FindChild ("InSlider").GetComponent<Sider> ().List2 = List2;
		
//				GetComponent<UISlider> ().onDragFinished += OnDragFinished;

			}

		}

	}

	public void OnDragFinished(){
		Debug.Log("Step : "+GetComponent<UISlider> ().value*11f);
	}

	// Use this for initialization
	public void Button(){
		float L;
		float R;
		if (name != "InSlider") {
			L = GetComponent<UISlider> ().value;
			R = transform.FindChild ("InSlider").GetComponent<UISlider> ().value;
			if(L >= 1f-R)
				GetComponent<UISlider> ().value = L = 1f-R;
		} else{
			L = transform.parent.GetComponent<UISlider> ().value;
			R = GetComponent<UISlider> ().value;
			if(R >= 1f-L)
				GetComponent<UISlider> ().value = R = 1f-L;
		}

		if(L == 0&&R == 0){
			mLblMenu0.GetComponent<UILabel>().text = "모든 입장료";
		}else if(Mathf.Round(L*11f)==Mathf.Round((1f-R)*11f)){					
			mLblMenu0.GetComponent<UILabel>().text 
				= "루비"+List2[Mathf.Round(R*11f)];
		}else{
				mLblMenu0.GetComponent<UILabel>().text 
				= "루비" + List1[Mathf.Round(L*11f)]+" - "+List2[Mathf.Round(R*11f)];
		}

		mLblLabel.GetComponent<UILabel>().text = mLblMenu0.GetComponent<UILabel>().text;
	}

}
