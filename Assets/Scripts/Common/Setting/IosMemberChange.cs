using UnityEngine;
using System.Collections;

public class IosMemberChange : MonoBehaviour {
	public GameObject IOSChange;
	void Start(){


		if (UtilMgr.IsGuestAccount()) {
			IOSChange.SetActive(true);
		}
		#if(UNITY_IOS)
#endif
	}
}
