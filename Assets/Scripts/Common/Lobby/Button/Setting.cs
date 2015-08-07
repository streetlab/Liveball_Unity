using UnityEngine;
using System.Collections;

public class Setting : MonoBehaviour {

	public void Button(){
		float Y = transform.root.FindChild ("Camera").localPosition.y;
		if (transform.root.FindChild ("Setting").gameObject.activeSelf) {
			transform.root.FindChild ("Setting").gameObject.SetActive (false);
		

		} else {
			transform.root.FindChild ("Setting").gameObject.SetActive(true);
			transform.root.FindChild ("Camera").localPosition = new Vector3(0,Y);

		}
	}
}
