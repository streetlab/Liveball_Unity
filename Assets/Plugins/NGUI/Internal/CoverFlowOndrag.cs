using UnityEngine;
using System.Collections;

public class CoverFlowOndrag : MonoBehaviour {
	public Vector2 DefaultSize;
	public Vector2 MaxSize;
	public Vector2 Size;
	public float Gap;
	public float BenchmarkSize;
	// Use this for initialization

	void OnTriggerStay2D(Collider2D other){
		if (other.tag == "item") {
//			Debug.Log("ss : " + (Mathf.Abs(other.transform.position.x/transform.root.localScale.x)));
//			Debug.Log("sss : " + (Mathf.Abs(other.transform.position.x/transform.root.localScale.x)/150));
//			Debug.Log("sssss : " + ((1-(Mathf.Abs(other.transform.position.x/transform.root.localScale.x)/150))+1));
			if (other.gameObject.GetComponent<UITexture> () != null)
			if ((Mathf.Abs (other.transform.position.x / transform.root.localScale.x)) <= (Size.x / 2 + BenchmarkSize / 2)) {
				float num = (1 - (Mathf.Abs (other.transform.position.x / transform.root.localScale.x) / (Size.x / 2 + BenchmarkSize / 2)));
				other.gameObject.GetComponent<UITexture> ().SetRect ((DefaultSize.x*(1f-num))+(MaxSize.x*num),
				                                                     (DefaultSize.y*(1f-num))+(MaxSize.y*num));
				other.gameObject.GetComponent<BoxCollider2D>().size = new Vector2((DefaultSize.x*(1f-num))+(MaxSize.x*num),
				                                                                  (DefaultSize.y*(1f-num))+(MaxSize.y*num));
				float Gaps;
				if(other.transform.position.x / transform.root.localScale.x>=0){
					Gaps = -Gap;
				}else{
					Gaps = Gap;
				}
			
			
				
					float Mark = BenchmarkSize*0.5f*0.5f;
					if(Mathf.Abs(other.transform.position.x / transform.root.localScale.x)<=Mark){	
						if(other.gameObject.name =="Item 2"){
							}

						float here = Mathf.Abs(other.transform.position.x / transform.root.localScale.x)/Mark;
						other.gameObject.transform.localPosition = new Vector3((float.Parse(other.name[5].ToString())*Size.x)-(here*Gaps),other.gameObject.transform.localPosition.y);
						if(other.gameObject.name =="Item 2"){
				
						}
					}else{
						Mark = BenchmarkSize*0.5f*0.5f;
						float here = ((BenchmarkSize*0.5f)-Mathf.Abs(other.transform.position.x / transform.root.localScale.x))/Mark;
						other.gameObject.transform.localPosition = new Vector3((float.Parse(other.name[5].ToString())*Size.x)-(here*Gaps),other.gameObject.transform.localPosition.y);

					}

		
			}
		}

	}
	void OnTriggerExit2D(Collider2D other){
		if (other.tag == "item") {
			if(other.gameObject.GetComponent<UITexture> ()!=null){
				other.gameObject.GetComponent<UITexture> ().SetRect (DefaultSize.x,
				                                                    DefaultSize.y);
			other.gameObject.GetComponent<BoxCollider2D>().size = new Vector2(DefaultSize.x,
			                                                                  DefaultSize.y);
				other.gameObject.transform.localPosition = new Vector3(float.Parse(other.name[5].ToString())*Size.x,other.gameObject.transform.localPosition.y);
			}
			//Debug.Log("ss : " + (Mathf.Abs(other.transform.position.x/transform.root.localScale.x)));
		}
	}

}
