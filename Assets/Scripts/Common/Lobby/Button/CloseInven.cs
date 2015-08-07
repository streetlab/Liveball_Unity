using UnityEngine;
using System.Collections;

public class CloseInven : MonoBehaviour {

	public void Button(){
		transform.root.FindChild ("Item").gameObject.SetActive (false);
	}
}
