using UnityEngine;
using System.Collections;

public class PreSettingCommander : MonoBehaviour {

	public GameObject PreSettingItem;
	public void CreatItem(){
		for(int i = 0 ; i<9;i++){
		GameObject Temp = (GameObject)Instantiate (PreSettingItem);
		Temp.transform.parent = 
			transform.FindChild ("Mid").FindChild ("Scroll View").FindChild ("Position");
		Temp.transform.localScale = new Vector3 (1,1,1);
		Temp.transform.localPosition = new Vector3 (0,-(45f+10f+((float)i*95f))+325);
			Temp.name = "Item " + (1+i).ToString();
			Temp.transform.FindChild("Num").FindChild("Label").GetComponent<UILabel>().text = (i+1).ToString();
	
			Temp.transform.FindChild("L_name").name = "L_name " + (1+i).ToString();
			Temp.transform.FindChild("L_name " + (1+i).ToString()).FindChild("L_pre").name ="L_name " + (1+i).ToString()+"_pre";
			Temp.transform.FindChild("R_name").name = "R_name " + (1+i).ToString();
			Temp.transform.FindChild("R_name " + (1+i).ToString()).FindChild("R_pre").name ="R_name " + (1+i).ToString()+"_pre";

		}
	}
	public void DeleteItem(){
		int Count = transform.FindChild ("Mid").FindChild ("Scroll View").FindChild ("Position").childCount;
		for (int i = 0; i<Count; i++) {
		
			DestroyImmediate(transform.FindChild ("Mid").FindChild ("Scroll View").FindChild ("Position").GetChild(0).gameObject);
		}
	}

}
