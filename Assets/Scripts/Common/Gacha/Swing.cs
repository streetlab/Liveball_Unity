﻿using UnityEngine;
using System.Collections;

public class Swing : MonoBehaviour {
	public void Fireball(){
		transform.FindChild("Fireball").gameObject.SetActive (true);
		transform.parent.parent.FindChild ("PostDialogue").gameObject.SetActive (true);
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
		transform.parent.parent.FindChild ("PostDialogue").FindChild("Panel").FindChild("Sprite").GetComponent<BoxCollider2D>().enabled = true;
	}
}
