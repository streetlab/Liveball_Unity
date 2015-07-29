using UnityEngine;
using System.Collections;

public class RightMenuCommander : MonoBehaviour {
	public GameObject RightSubMenu;
	public float TopGap;
	public Vector2 Size;
	public string[] MenuName;
	public string[] MenuValue;
	void Awake(){
		GetComponent<BoxCollider2D> ().enabled = false;
	}
	// Use this for initialization
	public void CreatRightSubMenu(){
		if (transform.FindChild ("Bot").childCount != MenuName.Length) {
			for(int i = 0; i<transform.FindChild("Bot").childCount;i++){
				DestroyImmediate(transform.FindChild("Bot").GetChild(0).gameObject);
			}

			for(int i = 0; i<MenuName.Length;i++){
				GameObject Temp = (GameObject)Instantiate(RightSubMenu);
				Temp.name = MenuName[i];
				Temp.transform.parent = transform.FindChild ("Bot");
				Temp.GetComponent<UISprite>().SetRect(Size.x,Size.y);
				Temp.GetComponent<BoxCollider2D>().size = Size;
				Temp.transform.localScale = new Vector3(1,1,1);
				Temp.transform.localPosition = new Vector3(0,-((TopGap+(Size.y*0.5f))+(i*Size.y)),0);
				Temp.transform.FindChild("Label").GetComponent<UILabel>().text = MenuValue[i];
				Temp.transform.FindChild("Icon").GetComponent<UISprite>().spriteName = MenuName[i];
			}
		}
	}
	public void DeleteRightSubMenu(){
		int Count = transform.FindChild ("Bot").childCount;
		for(int i = 0; i<Count;i++){
			DestroyImmediate(transform.FindChild("Bot").GetChild(0).gameObject);
		}
	}
}
