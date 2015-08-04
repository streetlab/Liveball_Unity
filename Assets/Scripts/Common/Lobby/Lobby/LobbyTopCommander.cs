using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LobbyTopCommander : MonoBehaviour {

	public GameObject mTopTemp;
	public GameObject mBar;
	public GameObject mShadow;
	public Vector2 Gap;
	public float TopHight;

	public string[] mTopMenuName;
	public string[] mTopMenuValue;

	float _Width = 720f;

	public float Width {
		get {
			return _Width;
		}
	}

	public void CreateTop(){


		if (transform.FindChild("Top")==null) {
			GameObject Top = new GameObject ("Top");
			Top.transform.parent = transform;
			Top.transform.localScale = new Vector3 (1, 1, 1);
			Top.transform.localPosition = new Vector2 (-360, 640);
			Top.AddComponent<UIPanel>().depth = 4;
			for (int i = 0; i<mTopMenuName.Length; i++) {
				GameObject Temp = (GameObject)Instantiate (mTopTemp, new Vector3 (0, 0, 0), new Quaternion (0, 0, 0, 0));
				Temp.transform.parent = Top.transform;
				Temp.GetComponent<UISprite>().depth = 0;
				//Temp.transform.localRotation = new Quaternion(0,0,0,0);
				Temp.transform.localScale = new Vector3 (1, 1, 1);
				float PositionX = Width / (float)mTopMenuName.Length;
				//Temp.transform.localPosition = new Vector3 (((i + 1) * Gap.x) + (Getbuttonsize ().x * i), -TopHight, 1);
				Vector2 LocalPosition = new Vector3 (((i + 1) * Gap.x) + (Getbuttonsize ().x * i), -TopHight, 0);
				Vector2 buttonsize = Getbuttonsize();
				Temp.transform.GetComponent<UISprite> ().SetRect (LocalPosition.x, LocalPosition.y+Gap.y, buttonsize.x, buttonsize.y);
				Temp.transform.GetComponent<BoxCollider2D>().size = buttonsize;
				Temp.name = mTopMenuName [i];
				Temp.transform.FindChild ("Label").GetComponent<UILabel> ().text = mTopMenuValue [i];
				GameObject Temp2 = (GameObject)Instantiate (mBar, new Vector3 (0, 0, 0), new Quaternion (0, 0, 0, 0));
				Temp2.transform.name = "Bar";
				Temp2.transform.parent = Temp.transform;
				Temp2.transform.localScale = new Vector3 (1, 1, 1);
				Temp2.GetComponent<UISprite>().SetRect(Width/3f,4f);
				Temp2.GetComponent<UISprite>().color = Color.yellow;
				Temp2.GetComponent<UISprite>().depth = 9;
				Temp2.transform.localPosition = new Vector3 (0, -(TopHight/2)-2);
				DestroyImmediate(Temp2.transform.FindChild("bar").gameObject);

			}
			GameObject Bar = (GameObject)Instantiate (mBar, new Vector3 (0, 0, 0), new Quaternion (0, 0, 0, 0));
			Bar.GetComponent<UISprite>().depth = 7;
			Bar.transform.parent = Top.transform;
			Bar.transform.localScale = new Vector3 (1, 1, 1);
			Bar.transform.localPosition = new Vector3(360,-TopHight-2);
			Bar.transform.name = "Bar";
			Bar.GetComponent<UISprite>().SetRect(Width,4f);
			Bar.transform.FindChild("bar").GetComponent<UISprite>().SetRect(Width,4f);
			Bar.transform.FindChild("bar").GetComponent<UISprite>().depth = 8;

			GameObject Shadow = (GameObject)Instantiate (mShadow, new Vector3 (0, 0, 0), new Quaternion (0, 0, 0, 0));
			Shadow.GetComponent<UISprite>().depth = 5;
			Shadow.transform.parent = Top.transform;
			Shadow.transform.localScale = new Vector3 (1, 1, 1);
			Shadow.transform.localPosition = new Vector3(360,-TopHight);
			Shadow.transform.name = "Shadow";
		
			Debug.Log ("Top Create Complete");
			if(GetComponent<LobbyFCCommander>()!=null){
			GetComponent<LobbyFCCommander>().SetFCPosition();
			}
		} else {
			Debug.Log("The \"TOP\" already exists.");
		}
	}
	public void ResetTop(){
		Init ();
	}
	Vector2 Getbuttonsize(){
		float Count = (float)mTopMenuName.Length;
		Vector2 Result = new Vector2 ((Width-((Count+1)*Gap.x))/Count,TopHight-(Gap.y*2));
		return Result;
	}

	void Init() {
		#if UNITY_EDITOR_OSX 
		bool option = UnityEditor.EditorUtility.DisplayDialog(
			"Warning!",
			"The traditional \"TOP\" GameObject is deleted when you reset.",
			"ReSet",
			"Cancle");
		if (option) {
		
				if(transform.FindChild("Top")!=null){
				DestroyImmediate(transform.FindChild("Top").gameObject);
				Debug.Log("Destroy Top");
			}
			CreateTop();
			Debug.Log ("\"Top\" Reset Complete");
		} else {
			Debug.Log("Cancle");
		}
		#endif 
	}

	public void CreateTop2(){
		
		for (int a = 0; a < Mathf.Infinity; a++) {
			if (transform.FindChild ("Top " + a.ToString()) == null) {
				GameObject Top = new GameObject ("Top " + a.ToString());
				Top.transform.parent = transform;
				Top.transform.localScale = new Vector3 (1, 1, 1);
				Top.transform.localPosition = new Vector2 (-360, 640);
				Top.AddComponent<UIPanel> ().depth = 4;
				for (int i = 0; i<mTopMenuName.Length; i++) {
					GameObject Temp = (GameObject)Instantiate (mTopTemp, new Vector3 (0, 0, 0), new Quaternion (0, 0, 0, 0));
					Temp.transform.parent = Top.transform;
					Temp.GetComponent<UISprite> ().depth = 0;
					//Temp.transform.localRotation = new Quaternion(0,0,0,0);
					Temp.transform.localScale = new Vector3 (1, 1, 1);
					float PositionX = Width / (float)mTopMenuName.Length;
					//Temp.transform.localPosition = new Vector3 (((i + 1) * Gap.x) + (Getbuttonsize ().x * i), -TopHight, 1);
					Vector2 LocalPosition = new Vector3 (((i + 1) * Gap.x) + (Getbuttonsize ().x * i), -TopHight, 0);
					Vector2 buttonsize = Getbuttonsize ();
					Temp.transform.GetComponent<UISprite> ().SetRect (LocalPosition.x, LocalPosition.y + Gap.y, buttonsize.x, buttonsize.y);
					Temp.transform.GetComponent<BoxCollider2D> ().size = buttonsize;
					Temp.name = mTopMenuName [i];
					Temp.transform.FindChild ("Label").GetComponent<UILabel> ().text = mTopMenuValue [i];
					GameObject Temp2 = (GameObject)Instantiate (mBar, new Vector3 (0, 0, 0), new Quaternion (0, 0, 0, 0));
					Temp2.transform.name = "Bar";
					Temp2.transform.parent = Temp.transform;
					Temp2.transform.localScale = new Vector3 (1, 1, 1);
					Temp2.GetComponent<UISprite> ().SetRect (Width / 3f, 4f);
					Temp2.GetComponent<UISprite> ().color = Color.yellow;
					Temp2.GetComponent<UISprite> ().depth = 9;
					Temp2.transform.localPosition = new Vector3 (0, -(TopHight / 2) - 2);
					DestroyImmediate (Temp2.transform.FindChild ("bar").gameObject);
				
				}
				GameObject Bar = (GameObject)Instantiate (mBar, new Vector3 (0, 0, 0), new Quaternion (0, 0, 0, 0));
				Bar.GetComponent<UISprite> ().depth = 7;
				Bar.transform.parent = Top.transform;
				Bar.transform.localScale = new Vector3 (1, 1, 1);
				Bar.transform.localPosition = new Vector3 (360, -TopHight - 2);
				Bar.transform.name = "Bar";
				Bar.GetComponent<UISprite> ().SetRect (Width, 4f);
				Bar.transform.FindChild ("bar").GetComponent<UISprite> ().SetRect (Width, 4f);
				Bar.transform.FindChild ("bar").GetComponent<UISprite> ().depth = 8;
			
				GameObject Shadow = (GameObject)Instantiate (mShadow, new Vector3 (0, 0, 0), new Quaternion (0, 0, 0, 0));
				Shadow.GetComponent<UISprite> ().depth = 5;
				Shadow.transform.parent = Top.transform;
				Shadow.transform.localScale = new Vector3 (1, 1, 1);
				Shadow.transform.localPosition = new Vector3 (360, -TopHight);
				Shadow.transform.name = "Shadow";
			
				Debug.Log ("Top Create Complete");
				GetComponent<LobbyFCCommander> ().SetFCPosition ();
				break;
			} else {
				Debug.Log ("The \"TOP\" "+a + " already exists.");
			}
		}
	}
}
