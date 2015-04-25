using UnityEngine;
using System.Collections;

public class Allrankcontrol : MonoBehaviour {

	public GameObject bars;
	public float gap=122;
	Vector3 posisions;
	float vgaps;

	
	// Use this for initialization

	void Start () {
		setposition ();
		
		transform.FindChild ("Scroll View").GetComponent<UIScrollView> ().ResetPosition ();
	}
	

	
	public void editng(){
		setposition ();
	}
	
	void setposition(){
		posisions = bars.transform.GetChild (0).transform.localPosition;
		for (int i =0; i<bars.transform.childCount; i++) {
			bars.transform.GetChild(i).transform.localPosition =new Vector3(posisions.x,posisions.y-(i*gap),posisions.z);

			
			

		}
		
	}



}
