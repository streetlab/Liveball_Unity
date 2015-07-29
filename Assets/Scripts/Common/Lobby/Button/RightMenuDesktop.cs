using UnityEngine;
using System.Collections;

public class RightMenuDesktop : MonoBehaviour {

	public void Button(){
		StartCoroutine (LeftMoveCamera());
	}
	int num = 5;
	float WatiTime = 0.02f;
	IEnumerator LeftMoveCamera(){ 
		GetComponent<BoxCollider2D> ().enabled = false;
		for (int i = 0; i<num; i++) {
			transform.root.FindChild("Camera").localPosition-= new Vector3(550f/num,0);
			yield return new WaitForSeconds(WatiTime);
			if(transform.root.FindChild("Camera").localPosition.x<=0){
				
				break;
			}
		}
		transform.root.FindChild("Camera").localPosition = new Vector3(0,0);
	}
}
