using UnityEngine;
using System.Collections;

public class CoverFlowOndrag : MonoBehaviour {
	public Vector2 DefaultSize;
	public Vector2 MaxSize;
	public Vector2 Size;
	public float BenchmarkSize;
	// Use this for initialization
	void OnTriggerStay2D(Collider2D other){
		if (other.tag == "item") {
//			Debug.Log("ss : " + (Mathf.Abs(other.transform.position.x/transform.root.localScale.x)));
//			Debug.Log("sss : " + (Mathf.Abs(other.transform.position.x/transform.root.localScale.x)/150));
//			Debug.Log("sssss : " + ((1-(Mathf.Abs(other.transform.position.x/transform.root.localScale.x)/150))+1));
			if (other.gameObject.GetComponent<UISprite> () != null)
			if ((Mathf.Abs (other.transform.position.x / transform.root.localScale.x)) <= (Size.x / 2 + BenchmarkSize / 2)) {
				other.gameObject.GetComponent<UISprite> ().SetRect (((1 - (Mathf.Abs (other.transform.position.x / transform.root.localScale.x) / (Size.x / 2 + BenchmarkSize / 2))) + 1) * DefaultSize.x,
				                                                    ((1 - (Mathf.Abs (other.transform.position.x / transform.root.localScale.x) / (Size.x / 2 + BenchmarkSize / 2))) + 1) * DefaultSize.y);
			}
		}

	}
	void OnTriggerExit2D(Collider2D other){
		if (other.tag == "item") {
			if(other.gameObject.GetComponent<UISprite> ()!=null)
				other.gameObject.GetComponent<UISprite> ().SetRect (DefaultSize.x,
				                                                    DefaultSize.y);
			Debug.Log("ss : " + (Mathf.Abs(other.transform.position.x/transform.root.localScale.x)));
		}
	}

}
