using UnityEngine;
using System.Collections;

public class giftspark : MonoBehaviour {
	float num = 1.36f;
	// Use this for initialization
	void Start () {
		StartCoroutine("U");


	}

	IEnumerator U(){
		StopCoroutine("D");
		for(int i = 0; i<999;i++){
			num=num+0.03f;
			GetComponent<ParticleRenderer> ().maxParticleSize= num;
	
			yield return new WaitForSeconds(0.05f);
			if(num>2){
				StartCoroutine("D");
				break;
			}
		}
	}
	IEnumerator D(){
		StopCoroutine("U");
		for(int i = 0; i<999;i++){
			num=num-0.03f;
			GetComponent<ParticleRenderer> ().maxParticleSize= num;
			yield return new WaitForSeconds(0.05f);
			if(num<1.36){
				StartCoroutine("U");
				break;
			}
		}
	}
}
