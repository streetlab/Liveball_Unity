using UnityEngine;
using System.Collections;

public class SeasonControl : MonoBehaviour {
	public GameObject bgs;
	public float gap1;
	public float gap2;
	public float bargap;
	Vector3 positions;
	// Use this for initialization
	void Start () {
	
	}
	public void editng(){
		setposition ();
	}
	void setposition(){
		positions = bgs.transform.GetChild (0).transform.localPosition;
		for(int i = 0;i<bgs.transform.childCount;i++){
			bgs.transform.GetChild(i).transform.localPosition = new Vector3(positions.x,positions.y-(bargap*i),positions.x);
		}
	}
	

}
