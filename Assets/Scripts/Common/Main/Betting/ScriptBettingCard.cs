using UnityEngine;
using System.Collections;

public class ScriptBettingCard : UIDragDropItem {

	CardInfo mCardInfo;
	ItemStrategyInfo mStrategyInfo;

	public GameObject mBG;
	public GameObject mNumber;
	public GameObject mFace;
	public GameObject mTeam;
	public GameObject mRatio;
	public GameObject mName;

	public GameObject mSprItem;

	public enum TYPE{
		Hitter,
		Pitcher,
		Strategy
	}

	static string[] IMAGE_CARDS = {
		"cd_game_bg01_normal",
		"cd_game_bg02_special",
		"cd_game_bg03_rare",
		"cd_game_bg04_platinum",
		"cd_game_bg05_elite",
		"cd_game_bg06_legend"}; 

	public TYPE mType;

	protected override void Start(){
		base.Start();
		if(mType == TYPE.Hitter){
			mCardInfo = UserMgr.CardInvenInfo.hitter[0];
		} else if(mType == TYPE.Pitcher){
			mCardInfo = UserMgr.CardInvenInfo.pitcher[0];
		} else{
			mStrategyInfo = UserMgr.UserInfo.item[0];
		}

		Init();
	}

	void Init(){
		if(mType == TYPE.Strategy)
			InitStrategy();
		else
			InitCard();
	}

	void InitCard(){
		mBG.GetComponent<UISprite>().spriteName = IMAGE_CARDS[mCardInfo.classNo-1];
		mNumber.GetComponent<UILabel>().text = mCardInfo.backNum+"";
		mName.GetComponent<UILabel>().text = mCardInfo.cardName;
		mTeam.GetComponent<UISprite>().spriteName = UtilMgr.GetTeamEmblem(mCardInfo.teamCode);
		mRatio.GetComponent<UILabel>().text = mCardInfo.rewardRate+"";
		string path = Constants.IMAGE_SERVER_HOST+mCardInfo.cardImagePath+mCardInfo.cardImageName;
		WWW www = new WWW(path);
		StartCoroutine(GetFace(www));
	}

	IEnumerator GetFace(WWW www){
		yield return www;
		
		Texture2D temp = new Texture2D (0, 0);
		www.LoadImageIntoTexture (temp);
		mFace.GetComponent<UITexture>().mainTexture = temp;
	}


	void InitStrategy(){
		mSprItem.GetComponent<UISprite>().spriteName = GetStrategyImg(mStrategyInfo.itemId);
	}

	string GetStrategyImg(int itemId){
		switch(itemId){
		case 1010:
			return "";
		case 1200:
			return "item_2x";
		case 1300:
			return "item_3x";
		case 1500:
			return "item_5x";
		case 1020:
			return "item_bs";
		}
		return "";
	}

	protected override void OnDragDropRelease (GameObject surface)
	{
		base.OnDragDropRelease (surface);
		transform.localScale = new Vector3(1f, 1f, 1f);

//		Debug.Log ("x : " + transform.localPosition.x + ", y : " + transform.localPosition.y);
		if (surface != null) {
//			Debug.Log("surface.name : "+surface.name);
			CheckSurface(surface);
		}
	}

	void CheckSurface(GameObject surface){
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

		ReturnCard();
	}

	public void ReturnCard(){

	}
	
	void OpenBetWindow(Transform btnGroup, string name){
		gameObject.SetActive(false);
		btnGroup.FindChild(name).GetComponent<ScriptBettingItem>().OnClicked(name);
	}

	protected override void OnDragDropMove (Vector2 delta)
	{
//		Vector3 newDelta = new Vector3(delta.x/1f, delta.y/1f, 1f);
//		mTrans.localPosition += (Vector3)delta;

		float ratio = 720f / Screen.currentResolution.width;

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
