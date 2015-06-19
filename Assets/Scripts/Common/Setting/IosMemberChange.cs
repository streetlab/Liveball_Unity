using UnityEngine;
using System.Collections;

public class IosMemberChange : MonoBehaviour {
	public GameObject IOSChange;
	void Start(){

#if(UNITY_IOS)
		if (UtilMgr.IsGuestAccount()) {
			IOSChange.SetActive(true);
		}
#endif
	}
}
