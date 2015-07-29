using UnityEngine;
using System.Collections;

public class LobbyAddSub : MonoBehaviour {
	public GameObject SubMenu;
	public GameObject Arrow;
	public string[] SubMenuName;
	public string[] SubMenuValue;
	public float SubHight;
	public void AddSub(){
		{				
			if(transform.FindChild ("Top").FindChild ("Sub")==null){
				GameObject Sub = new GameObject("Sub");
				Sub.transform.parent = transform.FindChild ("Top");
				Sub.transform.localScale = new Vector3(1,1,1);
				Sub.transform.localPosition = new Vector3(0,-(GetComponent<LobbyTopCommander> ().TopHight)-4);
				for (int i = 0; i<SubMenuName.Length; i++) {
					GameObject Temp = (GameObject)Instantiate (SubMenu);
					Temp.transform.parent = transform.FindChild ("Top").FindChild ("Sub");
					Temp.transform.name = SubMenuName[i];
					Temp.transform.FindChild("Label").GetComponent<UILabel>().text = SubMenuValue[i];
					Temp.transform.FindChild("Label").GetComponent<UILabel>().depth = 11;
					Temp.transform.localScale = new Vector3(1,1,1);
					Temp.transform.localPosition = new Vector3((GetComponent<LobbyTopCommander> ().Width/(float)(SubMenuName.Length)*0.5f)+((GetComponent<LobbyTopCommander> ().Width/(float)(SubMenuName.Length))*(float)i),-(SubHight/2));
					Temp.GetComponent<UISprite>().SetRect(GetComponent<LobbyTopCommander> ().Width/(float)(SubMenuName.Length),SubHight);
					Temp.GetComponent<BoxCollider2D>().size = new Vector2(GetComponent<LobbyTopCommander> ().Width/(float)(SubMenuName.Length),SubHight);
					if(SubMenuName[i]=="People"){
						GameObject AddArrow = (GameObject)Instantiate(Arrow);
						AddArrow.transform.parent = Temp.transform;
						AddArrow.transform.localScale = new Vector3(1,1,1);
						AddArrow.transform.localPosition = new Vector3(44,0);
						AddArrow.GetComponent<UISprite>().SetRect(20,10);
						AddArrow.name = "Arrow";

					}				
				}
			}
		
				
		
	}
}
	public void DeleteSub(){
		if (transform.FindChild ("Top").FindChild ("Sub") != null) {
			DestroyImmediate(transform.FindChild ("Top").FindChild ("Sub").gameObject);
		}
	}
	public void DisableSub(){
		transform.FindChild("Top").FindChild ("Sub").gameObject.SetActive (false);
		transform.FindChild("Top").FindChild ("Sub").FindChild("BG_B").gameObject.SetActive (false);
		for(int i = 0; i<GetComponent<LobbyAddSub>().SubMenuName.Length;i++){
			transform.FindChild("Top").FindChild("Sub").
				FindChild(GetComponent<LobbyAddSub>().SubMenuName[i]).
					FindChild(GetComponent<LobbyAddSub>().SubMenuName[i]+"Box").
					gameObject.SetActive(false);
			
		}
	}
	public void ResetAddSub(){
		for(int i = 0; i < SubMenuName.Length;i++){
		transform.FindChild("Top").FindChild("Sub").FindChild(SubMenuName[i]).FindChild("Label").GetComponent<UILabel>()
				.text = SubMenuValue[i];
		}

	}
}