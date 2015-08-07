using UnityEngine;
using System.Collections;

public class CoverFlowItems : MonoBehaviour {

	public void Button(){
		transform.root.FindChild ("Scroll").FindChild ("Giveaway").gameObject.SetActive (true);


		if (LobbyGiftCommander.mGift != null) {
			
//			for(int i = 0; i<7;i++){
//				Debug.Log(i);
//				try{
//					transform.FindChild("Scroll View").FindChild("Item " + (i).ToString())
//						.GetComponent<UITexture>().mainTexture = LobbyGiftCommander.mGift.image[i];
//					transform.FindChild("Scroll View").FindChild("Item " + (i).ToString())
//						.FindChild("Sprite").FindChild("Label").
//							.GetComponent<UILabel>().text = LobbyGiftCommander.mGift.gift;
//				}catch{
//					Debug.Log("image "+i + "is null");
//					transform.FindChild("Scroll View").FindChild("Item " + (i).ToString()).gameObject.SetActive(false);
//				}
//				
		//	}
		}
		transform.gameObject.SetActive (true);
		transform.localPosition = new Vector3(0,0,0);

	}
	public void Off(){
		transform.root.FindChild ("Scroll").FindChild ("Giveaway").gameObject.SetActive (false);
	}
}
