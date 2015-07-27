using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[System.Serializable]
public class SubItem{
 List<string> _SubItemName;
	public List<string> SubItemName =  new List<string>();// {
//		get {
//			return _SubItemName;
//		}
//		set {
//			_SubItemName = value;
//		}
//	}
}
public class LobbyAddSubInSub : MonoBehaviour {
	public GameObject Item;
	public GameObject LabelOrigin;
	public GameObject BG_G;
	public List<SubItem> SubItemList = new List<SubItem>();
	public List<float> Width = new List<float>();
	// Use this for initialization

	public void CreateAddInSub(){
		GameObject G = (GameObject)Instantiate (BG_G);
		G.transform.parent = transform.FindChild ("Top").FindChild ("Sub");
		G.transform.localPosition = new Vector2 (360,-640-transform.parent.localPosition.y);
		G.name = "BG_B";
		for (int i = 0; i<SubItemList.Count; i++) {
			GameObject Temp = (GameObject)Instantiate (Item);
			Temp.transform.parent = transform.FindChild ("Top").FindChild ("Sub").FindChild (GetComponent<LobbyAddSub> ().SubMenuName [i]);
			Temp.transform.name = GetComponent<LobbyAddSub> ().SubMenuName [i] + "Box";
			if(Temp.GetComponent<BoxCollider2D>()==null){
				Temp.AddComponent<BoxCollider2D>();
			}
			Temp.GetComponent<BoxCollider2D>().size = new Vector2(Width [i], ((float)SubItemList [i].SubItemName.Count * 60f) + 34f);
			Temp.GetComponent<BoxCollider2D>().offset = new Vector2(0,-(((float)SubItemList [i].SubItemName.Count * 60f) + 34f)*0.5f);
			Temp.transform.localScale = new Vector3 (1, 1, 1);
			float Hight = -((GetComponent<LobbyAddSub> ().SubHight / 2) + 20);
			Temp.transform.localPosition = new Vector3 (0, Hight, 0);
			Temp.GetComponent<UISprite> ().SetRect (Width [i], ((float)SubItemList [i].SubItemName.Count * 60f) + 34f);
			if (SubItemList.Count - 1 == i) {
				Temp.transform.localPosition = new Vector3 (-((Width [i] - (GetComponent<LobbyTopCommander> ().Width / GetComponent<LobbyAddSub> ().SubMenuName.Length)) * 0.5f), Hight, 0);
			} else if (i == 0) {
				Temp.transform.localPosition = new Vector3 (((Width [i] - (GetComponent<LobbyTopCommander> ().Width / GetComponent<LobbyAddSub> ().SubMenuName.Length)) * 0.5f), Hight, 0);
			}
			for (int a = 0; a< SubItemList[i].SubItemName.Count; a++) {
				GameObject Label = (GameObject)Instantiate(LabelOrigin);
				Label.transform.parent = Temp.transform;
				Label.transform.localScale = new Vector3 (1, 1, 1);
				Label.GetComponent<BoxCollider2D>().size = new Vector2(Width [i],60);
				Label.GetComponent<UIButton>().disabledColor = Color.yellow;
				if (a == 0) {
				
						Label.transform.localPosition = new Vector3 (0,-35);
					//Label.GetComponent<UILabel>().color = Color.white;
			

					Label.GetComponent<BoxCollider2D>().size = new Vector2(Width [i],70);
					Label.GetComponent<BoxCollider2D>().enabled = false;
					} else {
					Label.transform.localPosition = new Vector3 (0,-(90f+((a-1)*60f)));
				}
				Label.GetComponent<UILabel>().text = SubItemList[i].SubItemName[a];
				Label.GetComponent<UILabel>().depth = 13;
				Label.transform.name = "Menu " + a.ToString();
			}
		}
	}
	
	// Update is called once per frame
	public void DeleteInSub(){
		for (int i = 0; i<SubItemList.Count; i++) {
			if(transform.FindChild("Top").FindChild("Sub").FindChild(GetComponent<LobbyAddSub>().SubMenuName[i]).FindChild(GetComponent<LobbyAddSub>().SubMenuName[i]+"Box")!=null){
				DestroyImmediate(transform.FindChild("Top").FindChild("Sub").FindChild(GetComponent<LobbyAddSub>().SubMenuName[i]).FindChild(GetComponent<LobbyAddSub>().SubMenuName[i]+"Box").gameObject);
			};
		}
	}
	public void DisableSub(){

		for(int i = 0; i<GetComponent<LobbyAddSub>().SubMenuName.Length;i++){
			transform.FindChild("Top").FindChild("Sub").
				FindChild(GetComponent<LobbyAddSub>().SubMenuName[i]).
					FindChild(GetComponent<LobbyAddSub>().SubMenuName[i]+"Box").
					gameObject.SetActive(false);
			
		}
	}
	public void ResetSubInSub(){
		for (int a = 0; a<GetComponent<LobbyAddSub>().SubMenuName.Length; a++) {
			for (int i = 0; i<GetComponent<LobbyAddSubInSub>().SubItemList[a].SubItemName.Count; i++) {
				transform.FindChild ("Top").FindChild ("Sub").
				FindChild (GetComponent<LobbyAddSub> ().SubMenuName [a]).
					FindChild (GetComponent<LobbyAddSub> ().SubMenuName [a] + "Box").
						FindChild("Menu " + i.ToString()).GetComponent<BoxCollider2D>().enabled = true;
				transform.FindChild ("Top").FindChild ("Sub").
					FindChild (GetComponent<LobbyAddSub> ().SubMenuName [a]).
						FindChild (GetComponent<LobbyAddSub> ().SubMenuName [a] + "Box").
						FindChild("Menu " + i.ToString()).gameObject.SetActive(false);
				transform.FindChild ("Top").FindChild ("Sub").
					FindChild (GetComponent<LobbyAddSub> ().SubMenuName [a]).
						FindChild (GetComponent<LobbyAddSub> ().SubMenuName [a] + "Box").
						FindChild("Menu " + i.ToString()).gameObject.SetActive(true);
			
			}
		}
	}

}
