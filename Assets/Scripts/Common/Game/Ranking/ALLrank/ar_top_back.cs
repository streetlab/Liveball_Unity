using UnityEngine;
using System.Collections;

public class ar_top_back : MonoBehaviour {

	public void onhit(){

		if (transform.parent.parent.parent.GetChild (3).gameObject.activeSelf) {
		
			transform.parent.parent.parent.GetChild (3).GetChild (0).gameObject.SetActive (true);

			transform.parent.parent.parent.GetChild (3).GetChild (2).gameObject.SetActive (false);
			transform.parent.parent.parent.GetChild (3).GetChild (3).gameObject.SetActive (false);
			transform.parent.parent.parent.GetChild (3).GetChild (4).gameObject.SetActive (false);
			transform.parent.parent.parent.GetChild (3).GetChild (5).gameObject.SetActive (false);

		
			transform.parent.parent.GetChild (1).gameObject.SetActive (true);
			transform.parent.GetChild (1).gameObject.SetActive (true);
			transform.parent.GetChild (2).gameObject.SetActive (true);
			transform.parent.GetChild (4).gameObject.SetActive (false);
			transform.parent.GetChild (5).gameObject.SetActive (false);
		} else if (transform.parent.parent.parent.GetChild (5).gameObject.activeSelf) {
			for(int i = 0 ; i<4;i++){
				if(transform.parent.parent.parent.GetChild (5).GetChild(i+1).gameObject.activeSelf){
					if(i==3){
						transform.parent.parent.parent.GetChild (5).GetChild (0).gameObject.SetActive (true);
						
						transform.parent.parent.parent.GetChild (5).GetChild (1).gameObject.SetActive (false);
						transform.parent.parent.parent.GetChild (5).GetChild (2).gameObject.SetActive (false);
						transform.parent.parent.parent.GetChild (5).GetChild (3).gameObject.SetActive (false);
						
						
						
						transform.parent.parent.GetChild (1).gameObject.SetActive (true);
						transform.parent.GetChild (1).gameObject.SetActive (true);
						transform.parent.GetChild (2).gameObject.SetActive (true);
						transform.parent.GetChild (4).gameObject.SetActive (false);
						transform.parent.GetChild (5).gameObject.SetActive (false);
						return;
					}
					//Debug.Log("ion");
					for(int a = 0; a<11;a++){
					
						if(a==10){
						//	Debug.Log("ao10");
							transform.parent.parent.parent.GetChild (5).GetChild (0).gameObject.SetActive (true);
							
							transform.parent.parent.parent.GetChild (5).GetChild (1).gameObject.SetActive (false);
							transform.parent.parent.parent.GetChild (5).GetChild (2).gameObject.SetActive (false);
							transform.parent.parent.parent.GetChild (5).GetChild (3).gameObject.SetActive (false);
							
							
							
							transform.parent.parent.GetChild (1).gameObject.SetActive (true);
							transform.parent.GetChild (1).gameObject.SetActive (true);
							transform.parent.GetChild (2).gameObject.SetActive (true);
							transform.parent.GetChild (4).gameObject.SetActive (false);
							transform.parent.GetChild (5).gameObject.SetActive (false);
							return;
						}
						if(transform.parent.parent.parent.GetChild (5).GetChild(i+1).GetChild(a+2).gameObject.activeSelf){
						//	Debug.Log("act");
							transform.parent.parent.parent.GetChild (5).GetChild(i+1).GetChild(0).gameObject.SetActive(true);
							transform.parent.parent.parent.GetChild (5).GetChild(i+1).GetChild(1).gameObject.SetActive(true);
//							for(int q = 0; q<10;q++){
//								transform.parent.parent.parent.GetChild (5).GetChild(i+1).GetChild(q+2).gameObject.SetActive(false);
//							}
							transform.parent.parent.parent.GetChild (5).GetChild(i+1).GetChild(a+2).gameObject.SetActive(false);




							return;

						}
					}
				}
			}
		



		}
		
	}
}
