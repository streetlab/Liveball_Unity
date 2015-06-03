using UnityEngine;
using System.Collections;

public class PostButton : MonoBehaviour {

	public void onoff(){
		if(transform.FindChild ("TF_Post").gameObject.activeSelf)
		transform.FindChild ("TF_Post").gameObject.SetActive (false);
		else
		transform.FindChild ("TF_Post").gameObject.SetActive (true);
	}
}
