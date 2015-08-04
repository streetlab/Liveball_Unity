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
	public GameObject LabelLeftOrigin;
	public GameObject BG_G;
	public GameObject BuleBlackBar;
	public List<SubItem> SubItemList = new List<SubItem>();
	public List<float> Width = new List<float>();
	// Use this for initialization

	public void CreateAddInSub(){
		if (transform.FindChild ("Top").FindChild ("Sub").FindChild ("BG_B") == null) {
			GameObject G = (GameObject)Instantiate (BG_G);
			G.transform.parent = transform.FindChild ("Top").FindChild ("Sub");
			G.transform.localPosition = new Vector2 (360, -544 - transform.parent.localPosition.y);
			G.transform.localScale = new Vector3 (1, 1, 1);
			G.name = "BG_B";
		}
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
			float Hight = -((GetComponent<LobbyAddSub> ().SubHight / 2) + 30);
			Temp.transform.localPosition = new Vector3 (0, Hight, 0);
			Temp.GetComponent<UISprite> ().SetRect (Width [i], ((float)SubItemList [i].SubItemName.Count * 60f) + 34f);
			if (SubItemList.Count - 1 == i) {
				Temp.transform.localPosition = new Vector3 (-((Width [i] - (GetComponent<LobbyTopCommander> ().Width / GetComponent<LobbyAddSub> ().SubMenuName.Length)) * 0.5f), Hight, 0);
			} else if (i == 0) {
				Temp.transform.localPosition = new Vector3 (((Width [i] - (GetComponent<LobbyTopCommander> ().Width / GetComponent<LobbyAddSub> ().SubMenuName.Length)) * 0.5f), Hight, 0);
			}
			for (int a = 0; a< SubItemList[i].SubItemName.Count; a++) {
				if(SubItemList[i].SubItemName[a]==""&&BuleBlackBar!=null){
					GameObject Bar = (GameObject)Instantiate(BuleBlackBar);
					Bar.transform.parent = Temp.transform;
					Bar.transform.localScale = new Vector3 (1, 1, 1);
					if (a == 0) {
						
						Bar.transform.localPosition = new Vector3 (-122,-35);
						//Label.GetComponent<UILabel>().color = Color.white;
				
					} else {
						Bar.transform.localPosition = new Vector3 (-122,-(90f+((a-1)*60f)));
					}

					Bar.transform.name = "Menu " + a.ToString();

				}else if(Temp.transform.parent.name == "People"){
					GameObject Label = (GameObject)Instantiate(LabelLeftOrigin);
					Label.transform.parent = Temp.transform;
					Label.transform.localScale = new Vector3 (1, 1, 1);
					Label.GetComponent<BoxCollider2D>().size = new Vector2(Width [i],60);
						Label.GetComponent<BoxCollider2D>().offset = new Vector2(64,0);
					Label.GetComponent<UIButton>().disabledColor = Color.yellow;
					if (a == 0) {
						
						Label.transform.localPosition = new Vector3 (-60,-35);
						//Label.GetComponent<UILabel>().color = Color.white;
						
						
						Label.GetComponent<BoxCollider2D>().size = new Vector2(Width [i],70);
					//	Label.GetComponent<UIButton>().isEnabled = false;
					} else {
						Label.transform.localPosition = new Vector3 (-60,-(90f+((a-1)*60f)));
					}
					Label.GetComponent<UILabel>().text = "";
					Label.GetComponent<UILabel>().depth = 13;
					Label.transform.name = "Menu " + a.ToString();
					GameObject Label2 = (GameObject)Instantiate(LabelLeftOrigin);
					Label2.transform.parent = Label.transform;
					Label2.transform.localScale = new Vector3 (1, 1, 1);
					Label2.transform.localPosition = new Vector3 (0, 0, 0);
					Label2.GetComponent<UILabel>().text = SubItemList[i].SubItemName[a];
					Label2.GetComponent<UILabel>().color = Color.white;
					Label2.GetComponent<UILabel>().depth = 14;
					Label2.name ="white";
					DestroyImmediate(Label2.GetComponent<UIButton>());
					DestroyImmediate(Label2.GetComponent<BoxCollider2D>());
					DestroyImmediate(Label2.GetComponent<SubInSub>());
							
					GameObject Label3 = (GameObject)Instantiate(LabelLeftOrigin);
					Label3.transform.parent = Label.transform;
					Label3.transform.localScale = new Vector3 (1, 1, 1);
					Label3.transform.localPosition = new Vector3 (0, 0, 0);
					Label3.GetComponent<UILabel>().text = SubItemList[i].SubItemName[a];
					Label3.GetComponent<UILabel>().color = Color.yellow;
					Label3.GetComponent<UILabel>().depth = 14;
					Label3.name ="yellow";
					DestroyImmediate(Label2.GetComponent<UIButton>());
					DestroyImmediate(Label2.GetComponent<BoxCollider2D>());
					DestroyImmediate(Label2.GetComponent<SubInSub>());
				
						Label3.SetActive(false);

						GameObject Arrow = (GameObject)Instantiate(GetComponent<LobbyAddSub>().Arrow);
					Arrow.transform.parent = Label.transform;
					Arrow.transform.localScale = new Vector3 (1, 1, 1);
					Arrow.transform.localPosition = new Vector3 (107, 0, 0);
					Arrow.GetComponent<UISprite>().SetRect(20,10);
					Arrow.name = "Arrow";
				}
				else{
				GameObject Label = (GameObject)Instantiate(LabelOrigin);
				Label.transform.parent = Temp.transform;
				Label.transform.localScale = new Vector3 (1, 1, 1);
				Label.GetComponent<BoxCollider2D>().size = new Vector2(Width [i],60);
				Label.GetComponent<UIButton>().disabledColor = Color.yellow;
				if (a == 0) {
				
						Label.transform.localPosition = new Vector3 (0,-35);
					//Label.GetComponent<UILabel>().color = Color.white;
			

					Label.GetComponent<BoxCollider2D>().size = new Vector2(Width [i],70);
					//Label.GetComponent<BoxCollider2D>().enabled = false;
						Label.GetComponent<UIButton>().isEnabled = false;
					} else {
					Label.transform.localPosition = new Vector3 (0,-(90f+((a-1)*60f)));
				}
				Label.GetComponent<UILabel>().text = SubItemList[i].SubItemName[a];
				Label.GetComponent<UILabel>().depth = 13;
				Label.transform.name = "Menu " + a.ToString();
				}
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
				if(				transform.FindChild ("Top").FindChild ("Sub").
				   FindChild (GetComponent<LobbyAddSub> ().SubMenuName [a]).
				   FindChild (GetComponent<LobbyAddSub> ().SubMenuName [a] + "Box").
				   FindChild("Menu " + i.ToString()).GetComponent<BoxCollider2D>()!=null){
					if(i==0){
						transform.FindChild ("Top").FindChild ("Sub").
							FindChild (GetComponent<LobbyAddSub> ().SubMenuName [a]).
								FindChild (GetComponent<LobbyAddSub> ().SubMenuName [a] + "Box").
								FindChild("Menu " + i.ToString()).GetComponent<UIButton>().isEnabled = false;
						if(GetComponent<LobbyAddSub> ().SubMenuName [a] == "People"){
							if(transform.FindChild ("Top").FindChild ("Sub").
							   FindChild (GetComponent<LobbyAddSub> ().SubMenuName [a]).
							   FindChild (GetComponent<LobbyAddSub> ().SubMenuName [a] + "Box").
							   FindChild("Menu " + i.ToString()).FindChild("Arrow")!=null){
								transform.FindChild ("Top").FindChild ("Sub").
									FindChild (GetComponent<LobbyAddSub> ().SubMenuName [a]).
										FindChild (GetComponent<LobbyAddSub> ().SubMenuName [a] + "Box").
										FindChild("Menu " + i.ToString()).FindChild("Arrow").
										GetComponent<UISprite>().color = Color.yellow;
							}
							if(	transform.FindChild ("Top").FindChild ("Sub").
							   FindChild (GetComponent<LobbyAddSub> ().SubMenuName [a]).
							   FindChild (GetComponent<LobbyAddSub> ().SubMenuName [a] + "Box").
							   FindChild("Menu " + i.ToString()).GetComponent<UIButton>()!=null){
							transform.FindChild ("Top").FindChild ("Sub").
								FindChild (GetComponent<LobbyAddSub> ().SubMenuName [a]).
									FindChild (GetComponent<LobbyAddSub> ().SubMenuName [a] + "Box").
									FindChild("Menu " + i.ToString()).GetComponent<UIButton>().isEnabled = true;
							}
						}
					}else{
						if(	transform.FindChild ("Top").FindChild ("Sub").
						   FindChild (GetComponent<LobbyAddSub> ().SubMenuName [a]).
						   FindChild (GetComponent<LobbyAddSub> ().SubMenuName [a] + "Box").
						   FindChild("Menu " + i.ToString()).GetComponent<UIButton>()!=null){
				transform.FindChild ("Top").FindChild ("Sub").
				FindChild (GetComponent<LobbyAddSub> ().SubMenuName [a]).
					FindChild (GetComponent<LobbyAddSub> ().SubMenuName [a] + "Box").
						FindChild("Menu " + i.ToString()).GetComponent<UIButton>().isEnabled = true;
						}
						if(GetComponent<LobbyAddSub> ().SubMenuName [a] == "People"){
							if(transform.FindChild ("Top").FindChild ("Sub").
							   FindChild (GetComponent<LobbyAddSub> ().SubMenuName [a]).
							   FindChild (GetComponent<LobbyAddSub> ().SubMenuName [a] + "Box").
							   FindChild("Menu " + i.ToString()).FindChild("Arrow")!=null){
								transform.FindChild ("Top").FindChild ("Sub").
									FindChild (GetComponent<LobbyAddSub> ().SubMenuName [a]).
										FindChild (GetComponent<LobbyAddSub> ().SubMenuName [a] + "Box").
										FindChild("Menu " + i.ToString()).FindChild("Arrow").
										GetComponent<UISprite>().color = Color.white;
							}
						}
					}
				

				}
//				transform.FindChild ("Top").FindChild ("Sub").
//					FindChild (GetComponent<LobbyAddSub> ().SubMenuName [a]).
//						FindChild (GetComponent<LobbyAddSub> ().SubMenuName [a] + "Box").
//						FindChild("Menu " + i.ToString()).gameObject.SetActive(false);
//				transform.FindChild ("Top").FindChild ("Sub").
//					FindChild (GetComponent<LobbyAddSub> ().SubMenuName [a]).
//						FindChild (GetComponent<LobbyAddSub> ().SubMenuName [a] + "Box").
//						FindChild("Menu " + i.ToString()).gameObject.SetActive(true);
			
			}
		}
	}

}
