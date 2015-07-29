using UnityEngine;
using System.Collections;

public class LobbyNCCommander : MonoBehaviour {
	public GameObject NCOrigin;
	public GameObject CItem;
	public float NCHight;
	public float FCNCGap;
	public int CCount;
	public float CGap;

	public void SetNCPosition(){
		if (transform.FindChild ("Nomal Contest") != null) {
			transform.FindChild ("Nomal Contest").localPosition = new Vector3 (-360f, (1280f / 2f) - GetComponent<LobbyTopCommander> ().TopHight-GetComponent<LobbyFCCommander>().FCHight);
		} else {
			Debug.Log("The \"Nomal Contest\" does not exist.");
		}
		}
	public void CreateNC(){
		if (transform.FindChild ("Nomal Contest") == null) {
			GameObject NC = (GameObject)Instantiate (NCOrigin, new Vector3 (0, 0, 0), new Quaternion (0, 0, 0, 0));
			NC.transform.name = "Nomal Contest";
			NC.transform.parent = transform;
			NC.transform.localScale = new Vector3 (1, 1, 1);
			NC.transform.localPosition = new Vector3 (-360f, (1280f / 2f) - GetComponent<LobbyTopCommander> ().TopHight-GetComponent<LobbyFCCommander>().FCHight);
			NC.transform.FindChild("BackGround Panel").FindChild ("BackGround").GetComponent<UISprite> ().SetRect (GetComponent<LobbyTopCommander> ().Width - (GetComponent<LobbyTopCommander> ().Gap.x * 2), NCHight-(FCNCGap/2));
			Debug.Log ("Nomal Contest Create Complete");
		} else {
			Debug.Log("The \"Nomal Contest\" already exists.");
		}

	}
	public void ResetNC(){
		Init ();
	}
	void Init() {
		#if UNITY_EDITOR_OSX 
		bool option = UnityEditor.EditorUtility.DisplayDialog(
			"Warning!",
			"The traditional \"Nomal Contest\" GameObject is deleted when you reset.",
			"ReSet",
			"Cancle");
		if (option) {
			
			if(transform.FindChild("Nomal Contest")!=null){
				DestroyImmediate(transform.FindChild("Nomal Contest").gameObject);
				Debug.Log("Destroy Nomal Contest");
			}
			CreateNC();
			Debug.Log ("\"Nomal Contest\" Reset Complete");
		} else {
			Debug.Log("Cancle");
		}
		#endif 
	}

	public void CreatCItem(){
		int Count = transform.FindChild ("Nomal Contest").FindChild ("Scroll View").childCount;
		for (int i = 0; i<Count; i++) {
			DestroyImmediate(transform.FindChild ("Nomal Contest").FindChild ("Scroll View").GetChild(0).gameObject);
		}
		for (int i = 0; i<CCount; i++) {
			GameObject Temp = (GameObject)Instantiate (CItem);
			Temp.transform.parent = transform.FindChild ("Nomal Contest").FindChild ("Scroll View");
			Temp.transform.localScale = new Vector3 (1, 1, 1);
			Temp.transform.localPosition = new Vector3(0,-(float)i*CGap,0);
			if(i <4){
				for(int a = 0; a<Temp.transform.childCount;a++){
					if(Temp.transform.GetChild(a).name!="BG"){
					Temp.transform.GetChild(a).GetComponent<UISprite>().color = new Color(1f,238f/255f,253f/255f,1f);
					}
				}
			}
			Temp.name = "Contest " + i.ToString();
		}

	}
	public void NCUpDown(string Value){
		float Y =  (1280f / 2f) - GetComponent<LobbyTopCommander> ().TopHight-GetComponent<LobbyFCCommander>().FCHight;
		if (Value == "Up") {
			transform.FindChild ("Nomal Contest").localPosition = new Vector3 (-360, Y);
		} else if(Value == "Down"){
			transform.FindChild ("Nomal Contest").localPosition = new Vector3 (-360, Y - GetComponent<LobbyAddSub> ().SubHight);
		}
	}
}
