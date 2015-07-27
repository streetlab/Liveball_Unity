using UnityEngine;
using System.Collections;

public class LobbyFCCommander : MonoBehaviour {
	public GameObject FCOrigin;
	public float FCHight;

	public void SetFCPosition(){
		if (transform.FindChild ("Featured Contest") != null) {
			transform.FindChild ("Featured Contest").localPosition = new Vector3 (-360f, (1280f / 2f) - GetComponent<LobbyTopCommander> ().TopHight);
		} else {
			Debug.Log("The \"Featured Contest\" does not exist.");
		}
		}
	public void CreateFC(){
		if (transform.FindChild ("Featured Contest") == null) {
			GameObject FC = (GameObject)Instantiate (FCOrigin, new Vector3 (0, 0, 0), new Quaternion (0, 0, 0, 0));
			FC.transform.name = "Featured Contest";
			FC.transform.parent = transform;
			FC.transform.localScale = new Vector3 (1, 1, 1);
			FC.transform.localPosition = new Vector3 (-360f, (1280f / 2f) - GetComponent<LobbyTopCommander> ().TopHight);
			FC.transform.FindChild ("BackGround").GetComponent<UISprite> ().SetRect (GetComponent<LobbyTopCommander> ().Gap.x, -FCHight+(GetComponent<LobbyNCCommander>().FCNCGap/2), GetComponent<LobbyTopCommander> ().Width - (GetComponent<LobbyTopCommander> ().Gap.x * 2), FCHight-(GetComponent<LobbyNCCommander>().FCNCGap/2));
			Debug.Log ("Featured Contest Create Complete");
		} else {
			Debug.Log("The \"Featured Contest\" already exists.");
		}

	}
	public void ResetFC(){
		Init ();
	}
	void Init() {
		#if UNITY_EDITOR_OSX 
		bool option = UnityEditor.EditorUtility.DisplayDialog(
			"Warning!",
			"The traditional \"Featured Contest\" GameObject is deleted when you reset.",
			"ReSet",
			"Cancle");
		if (option) {
			
			if(transform.FindChild("Featured Contest")!=null){
				DestroyImmediate(transform.FindChild("Featured Contest").gameObject);
				Debug.Log("Destroy Featured Contest");
			}
			CreateFC();
			Debug.Log ("\"Featured Contest\" Reset Complete");
		} else {
			Debug.Log("Cancle");
		}
		#endif 
	}
}
