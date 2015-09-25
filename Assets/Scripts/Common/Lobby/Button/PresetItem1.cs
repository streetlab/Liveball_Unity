using UnityEngine;
using System.Collections;

public class PresetItem1 : MonoBehaviour {

	public void BtnTop(){
		transform.GetComponentInChildren<PresetItem>().Button();
	}
}
