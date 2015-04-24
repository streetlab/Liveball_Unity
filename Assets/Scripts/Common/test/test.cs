using UnityEngine;
using System.Collections;

public class test : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<ParticleRenderer> ().material.renderQueue = 3090;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
