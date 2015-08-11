using UnityEngine;
using System.Collections;

public class giftSwing : MonoBehaviour {
	public void Fireball(){
		transform.FindChild("Fireball").gameObject.SetActive (true);
		//transform.parent.parent.FindChild ("PostDialogue").gameObject.SetActive (true);
		//transform.parent.parent.GetComponent<PostButton> ().GachaOK ();
		transform.root.FindChild ("Scroll").FindChild ("Giveaway").FindChild ("Bots").FindChild ("Sprite").FindChild ("Gacha")
			.GetComponent<GiftGacha> ().Gacha ();

		//transform.root.FindChild("scroll").FindChild("Giveaway")
	}
	public void spark(){
		transform.FindChild("spark 08s").gameObject.SetActive (true);
	}
	public void ground16(){
	//	transform.FindChild("ground 16").gameObject.SetActive (true);
	}
	public void ground23(){
		transform.FindChild("ground 23").gameObject.SetActive (true);
		transform.FindChild("Plasma 11").gameObject.SetActive (true);
	//	transform.parent.parent.FindChild ("PostDialogue").FindChild("Panel").FindChild("Sprite").GetComponent<BoxCollider2D>().enabled = true;
	}
}
