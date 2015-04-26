using UnityEngine;
using System.Collections;

public class AdjustListSize : MonoBehaviour {

	// Use this for initialization
	void Start () {
		UIScrollView scrollView = GetComponent<UIScrollView>();
		if(scrollView != null){
			UtilMgr.ResizeList(gameObject);
			scrollView.ResetPosition();
		}
	}
}
