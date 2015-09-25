using UnityEngine;
using System.Collections;

public class RightMenuDesktop : MonoBehaviour {

	int num = 5;
	float WatiTime = 0.02f;

	//오른쪽 메뉴 그림자 클릭시 닫힘
	public void Button(){
		StartCoroutine (LeftMoveCamera());
		transform.root.FindChild("Scroll").FindChild("Bot").FindChild("Home").GetComponent<BotMenu>()
			.UnsetHighlight(BotMenu.ActivityName.Menu);
	}

	IEnumerator LeftMoveCamera(){ 
		float Y = transform.root.FindChild ("Camera").localPosition.y;
		transform.FindChild("Shadow").GetComponent<BoxCollider2D> ().enabled = false;
		for (int i = 0; i<num; i++) {
			transform.root.FindChild("Camera").localPosition-= new Vector3(550f/num,0);
			yield return new WaitForSeconds(WatiTime);
			if(transform.root.FindChild("Camera").localPosition.x<=0){

				break;
			}
		}
		transform.root.FindChild("Camera").localPosition = new Vector3(0,Y);
	}
}
