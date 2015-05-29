using UnityEngine;
using System.Collections;

public class Button4 : MonoBehaviour {
	GameObject UiRoot;
	Vector3 Reset = new Vector3(0,-1280,0);
	public void OnLoad(){
		UiRoot = this.transform.parent.parent.parent.gameObject;
		if (this.transform.parent.parent.parent.name != "UI Root") {
			UiRoot = this.transform.parent.parent.parent.parent.parent.gameObject;

			if(UiRoot.name !="UI Root"){
				UiRoot = this.transform.parent.parent.parent.parent.parent.parent.gameObject;
			}
		}
		//UiRoot.transform.FindChild ("TF_Highlight").transform.localPosition = Reset;
		//UiRoot.transform.FindChild ("TF_Lineup").transform.localPosition = Reset;
		//UiRoot.transform.FindChild("TF_Livetalk").transform.localPosition= Reset;
		//UiRoot.transform.FindChild("TF_Lineup").gameObject.SetActive(false);
		//UiRoot.transform.FindChild("TF_Livetalk").gameObject.SetActive(false);
		switch (transform.parent.name) {
		case "GameInfo":
			StartCoroutine(Up(UiRoot.transform.FindChild("TF_Highlight").gameObject));
			//UiRoot.transform.FindChild("TF_Highlight").transform.localPosition= new Vector3(0,25,0);
			break;
		case "OK Strategy":
		
			StartCoroutine(Up(UiRoot.transform.FindChild("TF_Lineup").gameObject));
			//UiRoot.transform.FindChild("TF_Lineup").transform.localPosition= new Vector3(0,25,0);
			break;
		case "Item":
			StartCoroutine(Up(UiRoot.transform.FindChild("TF_Items").gameObject));
			break;
		case "Community":
		
			StartCoroutine(Up(UiRoot.transform.FindChild("TF_Livetalk").gameObject));
			//UiRoot.transform.FindChild("TF_Livetalk").transform.localPosition= new Vector3(0,25,0);
			break;

		}
		if (transform.name == "BtnClose") {
			StartCoroutine(Down(UiRoot.transform.FindChild("TF_Highlight").gameObject));
			StartCoroutine(Down(UiRoot.transform.FindChild("TF_Lineup").gameObject));
			StartCoroutine(Down(UiRoot.transform.FindChild("TF_Items").gameObject));
			StartCoroutine(Down(UiRoot.transform.FindChild("TF_Livetalk").gameObject));
//			if(UiRoot.transform.FindChild("TF_Highlight").transform.localPosition.y==25){
//			StartCoroutine(Down(UiRoot.transform.FindChild("TF_Highlight").gameObject));
//			}else if(UiRoot.transform.FindChild("TF_Lineup").transform.localPosition.y==25){
//				StartCoroutine(Down(UiRoot.transform.FindChild("TF_Lineup").gameObject));
//			}else if(UiRoot.transform.FindChild("TF_Livetalk").transform.localPosition.y==25){
//				StartCoroutine(Down(UiRoot.transform.FindChild("TF_Livetalk").gameObject));
//			}



	
		}


	}
	IEnumerator Up(GameObject G){
		G.SetActive (true);
		if(G.name == "TF_Lineup"){
			G.transform.FindChild("Scroll View").gameObject.SetActive(true);
			G.transform.FindChild("Scroll View").GetComponent<UIScrollView>().ResetPosition();
		}
		for (int i =0; i<5; i++) {
			if(G.transform.localPosition.y!=25){
			G.transform.localPosition += new Vector3(0,1300/5,0);

			yield return new WaitForSeconds(0.01f);
			}else{
				//UiRoot.transform.FindChild("TF_Items").FindChild("TF_Items").FindChild("category 1").GetComponent<UIScrollView>().ResetPosition();
				break;
			}
		}
	}
	IEnumerator Down(GameObject G){
		if(G.name == "TF_Lineup"){
			G.transform.FindChild("Scroll View").gameObject.SetActive(false);
			G.transform.FindChild("Scroll View 1").gameObject.SetActive(false);
		}
		for (int i =0; i<5; i++) {
			if(G.transform.localPosition.y!=-1275){
				G.transform.localPosition -= new Vector3(0,1300/5,0);
				yield return new WaitForSeconds(0.02f);
			}else{
				break;
			}
		}
		if (G.name != "TF_Highlight") {
			G.SetActive(false);
		}
	}


}
