using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class LobbyBotCommander : MonoBehaviour {
	public enum BtmState{
		Main,
		Info,
		Challenge,
		Mail,
		Menu
	}
	public static BtmState mBtnState;

	public GameObject mBotTemp;
	public Vector2 Gap;
	public float BotHeight;

	public string[] mBotMenuName;
	public string[] mBotMenuValue;

	public void CreateBot(){
		if (transform.FindChild("Bot")==null) {
			GameObject Bot = new GameObject ("Bot");
			Bot.transform.parent = transform;
			Bot.AddComponent<UIPanel>().depth = 5;
			Bot.transform.localScale = new Vector3 (1, 1, 1);
			Bot.transform.localPosition = new Vector2 (-360, 640-GetComponent<LobbyTopCommander>().TopHight-
			                                           GetComponent<LobbyFCCommander>().FCHight-
			                                           GetComponent<LobbyNCCommander>().NCHight);
			for (int i = 0; i<mBotMenuName.Length; i++) {
				GameObject Temp = (GameObject)Instantiate (mBotTemp, new Vector3 (0, 0, 0), new Quaternion (0, 0, 0, 0));
				Temp.transform.parent = Bot.transform;
				//Temp.transform.localRotation = new Quaternion(0,0,0,0);
				Temp.transform.localScale = new Vector3 (1, 1, 1);
				float PositionX = GetComponent<LobbyTopCommander>().Width / (float)mBotMenuName.Length;
				//Temp.transform.localPosition = new Vector3 (((i + 1) * Gap.x) + (Getbuttonsize ().x * i), -TopHight, 1);
				Vector2 LocalPosition = new Vector3 (((i + 1) * Gap.x) + (Getbuttonsize ().x * i), -BotHeight, 0);
				Temp.transform.GetComponent<UISprite> ().SetRect (LocalPosition.x, LocalPosition.y+Gap.y, Getbuttonsize ().x, Getbuttonsize ().y);
				Temp.transform.GetComponent<BoxCollider2D>().size = Getbuttonsize();
				Temp.name = mBotMenuName [i];
				Temp.transform.FindChild ("Sprite").GetComponent<UISprite> ().spriteName = mBotMenuValue [i];

			}
			GameObject Shadow = (GameObject)Instantiate (GetComponent<LobbyTopCommander>().mShadow, new Vector3 (0, 0, 0), new Quaternion (0, 0, 0, 0));
			Shadow.GetComponent<UISprite>().depth = 5;
			Shadow.transform.parent = Bot.transform;
			Shadow.transform.localScale = new Vector3 (1, -1, 1);
			Shadow.transform.localPosition = new Vector3(360,0);
			Shadow.transform.name = "Shadow";
			Debug.Log ("Bot Create Complete");
			GetComponent<LobbyFCCommander>().SetFCPosition();
		} else {
			Debug.Log("The \"Bot\" already exists.");
		}
	}
	public void ResetBot(){
		Init ();
	}
	Vector2 Getbuttonsize(){
		float Count = (float)mBotMenuName.Length;
		Vector2 Result = new Vector2 ((GetComponent<LobbyTopCommander>().Width-((Count+1)*Gap.x))/Count,BotHeight-(Gap.y*2));
		return Result;
	}

	void Init() {
		#if UNITY_EDITOR_OSX 
		bool option = UnityEditor.EditorUtility.DisplayDialog(
			"Warning!",
			"The traditional \"Bot\" GameObject is deleted when you reset.",
			"ReSet",
			"Cancle");
		if (option) {
		
			if(transform.FindChild("Bot")!=null){
				DestroyImmediate(transform.FindChild("Bot").gameObject);
				Debug.Log("Destroy Bot");
			}
			CreateBot();
			Debug.Log ("\"Bot\" Reset Complete");
		} else {
			Debug.Log("Cancle");
		}
		#endif 
	}

}
