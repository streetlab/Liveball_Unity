using UnityEngine;
using System.Collections;

public class ScriptBettingCard : UIDragDropItem {

	public enum TYPE{
		Hitter,
		Pitcher,
		Strategy
	}

	public TYPE mType;

	protected override void OnDragDropRelease (GameObject surface)
	{
		base.OnDragDropRelease (surface);
		transform.localScale = new Vector3(1f, 1f, 1f);

//		Debug.Log ("x : " + transform.localPosition.x + ", y : " + transform.localPosition.y);
		if (surface != null) {
			Debug.Log("surface.name : "+surface.name);

			if(mType == TYPE.Hitter){
				if(surface.name.Contains("BtnHit")){
					Transform btnGroup = transform.parent.FindChild("SprHit");
					OpenBetWindow(btnGroup, surface.name);
					return;
				}
			} else if(mType == TYPE.Pitcher){
				if(surface.name.Contains("BtnOut")){
					Transform btnGroup = transform.parent.FindChild("SprOut");
					OpenBetWindow(btnGroup, surface.name);
					return;
				}
			} else{
				if(surface.name.Contains("BtnHit")){
					Transform btnGroup = transform.parent.FindChild("SprHit");
					OpenBetWindow(btnGroup, surface.name);
					return;
				}

				if(surface.name.Contains("BtnOut")){
					Transform btnGroup = transform.parent.FindChild("SprOut");
					OpenBetWindow(btnGroup, surface.name);
					return;
				}

				if(surface.name.Contains("BtnLoaded")){
					Transform btnGroup = transform.parent.FindChild("SprLoaded");
					OpenBetWindow(btnGroup, surface.name);
					return;
				}
			}
		}


	}

	void OpenBetWindow(Transform btnGroup, string name){
		gameObject.SetActive(false);
		btnGroup.FindChild(name).GetComponent<ScriptBettingItem>().OnClicked(name);
	}

	protected override void OnDragDropMove (Vector2 delta)
	{
//		Vector3 newDelta = new Vector3(delta.x/1f, delta.y/1f, 1f);
//		mTrans.localPosition += (Vector3)delta;

		float ratio = 720f / 298f;

		float touchedX = (UICamera.currentTouch.pos.x * ratio) -360f;
		float touchedY = (UICamera.currentTouch.pos.y * ratio) - 615f;
		mTrans.localPosition = new Vector3(touchedX, touchedY, 1f);
//		Debug.Log ("x is "+touchedX);
//		Debug.Log ("y is "+touchedY);
	}

	protected override void OnDragDropStart (){
//		cloneOnDrag = true;

		base.OnDragDropStart();

//		UtilMgr.GetScaledPositionY();
		float touchedX = UICamera.currentTouch.pos.x - 360f;
		float touchedY = UICamera.currentTouch.pos.y - 615f;
//		Debug.Log("cameraY : "+UICamera.currentTouch.pos.y);
//		Debug.Log("touchedY : "+touchedY);

		transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
//		Vector3 pos = transform.localPosition;//
//		pos.y += 300f;
//		transform.localPosition = pos;

	}
}
