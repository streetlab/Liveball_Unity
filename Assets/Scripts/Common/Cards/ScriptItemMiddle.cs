using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScriptItemMiddle : MonoBehaviour {
	//public string grade,maxlv,posi,team,num,name,nowlv,add;
	//public static bool Delete;
	public static bool UseItem;
	public static string name;
	public GameObject mainItem;
	GetInvenItemEvent mEvent;
	GetProfileEvent mProfileEvent;

	List<UIListItem> ItemList = new List<UIListItem>();
	void ActiveCount(){
		UserMgr.UserInfo = mProfileEvent.Response.data;
	
	}

	 public void Starts(){
		if (UseItem) {
			DialogueMgr.ShowDialogue ("사용 성공", "["+name+"]이 사용되었습니다.", DialogueMgr.DIALOGUE_TYPE.Alert, null);
			UseItem = false;
		}
//		mProfileEvent = new GetProfileEvent (new EventDelegate (this, "ActiveCount"));
//		NetMgr.GetProfile (UserMgr.UserInfo.memSeq, mProfileEvent);
		//		if (Delete) {
		//			DialogueMgr.ShowDialogue ("삭제 성공", "삭제되었습니다.", DialogueMgr.DIALOGUE_TYPE.Alert, null);
		//			Delete = false;
		//		} else 
	
		mEvent = new GetInvenItemEvent (new EventDelegate (this, "GotItemsInven"));
		NetMgr.GetInvenItem (mEvent);
	}
	public void Reset(){
//		mProfileEvent = new GetProfileEvent (new EventDelegate (this, "ActiveCount"));
//		NetMgr.GetProfile (UserMgr.UserInfo.memSeq, mProfileEvent);
		//		if (Delete) {
		//			DialogueMgr.ShowDialogue ("삭제 성공", "삭제되었습니다.", DialogueMgr.DIALOGUE_TYPE.Alert, null);
		//			Delete = false;
		//		} else 
		if (UseItem) {
			DialogueMgr.ShowDialogue ("사용 성공", "["+name+"]이 사용되었습니다.", DialogueMgr.DIALOGUE_TYPE.Alert, null);
			UseItem = false;
		}



		transform.FindChild ("ListCards").GetComponent<UIDraggablePanel2> ().RemoveAll ();
		mEvent = new GetInvenItemEvent (new EventDelegate (this, "GotItemsInven"));
		NetMgr.GetInvenItem (mEvent);


	}
	void GotItemsInven(){
		ItemList.Clear ();
		int invencount = mEvent.Response.data.Count;
		List<int> UseItems = new List<int> ();
		for (int i = 0; i <invencount; i++) {
			if(mEvent.Response.data[i].itemType>=6){
				UseItems.Add(i);
			}

		}

		transform.FindChild("ListCards"). GetComponent<UIDraggablePanel2>().Init(UseItems.Count, 
		                                                              delegate(UIListItem item, int index) {
			
	
			item.Target.gameObject.transform.FindChild("BG_g").FindChild("BG_w").FindChild("name").GetComponent<UILabel>().text = mEvent.Response.data[UseItems[index]].itemName;
			if(mEvent.Response.data[UseItems[index]].itemType==6){
				item.Target.gameObject.transform.FindChild("BG_g").FindChild("BG_w").FindChild("image2").gameObject.SetActive(false);
				item.Target.gameObject.transform.FindChild("BG_g").FindChild("BG_w").FindChild("image").GetComponent<UISprite>().spriteName = "gift_c";
			}else if(mEvent.Response.data[UseItems[index]].itemType==7){
				item.Target.gameObject.transform.FindChild("BG_g").FindChild("BG_w").FindChild("image2").gameObject.SetActive(false);
				item.Target.gameObject.transform.FindChild("BG_g").FindChild("BG_w").FindChild("image").GetComponent<UISprite>().spriteName = "gift_p";
			}else{

				try{
					item.Target.gameObject.transform.FindChild("BG_g").FindChild("BG_w").FindChild("image2").gameObject.SetActive(true);
					item.Target.gameObject.transform.FindChild("BG_g").FindChild("BG_w").FindChild("image2").GetComponent<UITexture>().mainTexture = 
					LobbyGiftCommander.mGift.Textures[mEvent.Response.data[UseItems[index]].imageName];
					item.Target.gameObject.transform.FindChild("BG_g").FindChild("BG_w").FindChild("image").gameObject.SetActive(false);
				}catch{
					Debug.Log("mEvent.Response.data[UseItems[index]].itemName : " + mEvent.Response.data[UseItems[index]].imageName);
				}


			}

			item.Target.gameObject.transform.FindChild("BG_g").FindChild("BG_w").FindChild("itemid").GetComponent<UILabel>().text = mEvent.Response.data[UseItems[index]].itemId.ToString();
			item.Target.gameObject.transform.FindChild("BG_g").FindChild("BG_w").FindChild("itemNo").GetComponent<UILabel>().text = mEvent.Response.data[UseItems[index]].itemNo.ToString();
			if(mEvent.Response.data[UseItems[index]].registTime!=null){
				string limit = mEvent.Response.data[UseItems[index]].registTime;
				char [] limitlist;
				List<string> result = new List<string>();
				if(limit.Length>10){
					limitlist = limit.ToCharArray();
					for(int q = 0; q<limitlist.Length-2;q++){
						result.Add(limitlist[q].ToString());
						if(q==3){
							result.Add("년");
						}else if(q==5){
							result.Add("월");
						}else if(q==7){
							result.Add("일");
						}else if(q==9){
							result.Add("시");
						}else if(q==11){
							result.Add("분");
						}
					}
				}
					limit = string.Join("",result.ToArray());
				item.Target.gameObject.transform.FindChild("BG_g").FindChild("BG_w").FindChild("value").GetComponent<UILabel>().text = "수신일 : "+ limit;
				if(mEvent.Response.data[UseItems[index]].itemUseYn>0){
					item.Target.gameObject.transform.FindChild("BG_g").FindChild("BG_w").FindChild("use button").GetChild(0).GetComponent<UILabel>().text = 
						mEvent.Response.data[UseItems[index]].statusMsg;
					item.Target.gameObject.transform.FindChild("BG_g").FindChild("BG_w").FindChild("use button").GetComponent<UIButton>().isEnabled = false;
				}else{
					item.Target.gameObject.transform.FindChild("BG_g").FindChild("BG_w").FindChild("use button").GetChild(0).GetComponent<UILabel>().text ="사용";
					item.Target.gameObject.transform.FindChild("BG_g").FindChild("BG_w").FindChild("use button").GetComponent<UIButton>().isEnabled = true;
				}
			}
			item.Target.gameObject.SetActive(true);
			ItemList.Add(item);
		});
		if (UseItems.Count < 1) {
			transform.FindChild("NonItem").gameObject.SetActive(true);
		}
		transform.FindChild ("ListCards").GetComponent<UIDraggablePanel2> ().ResetPosition ();
	}
	public void setdelete(string name){
		char [] Name = name.ToCharArray ();
		string index = Name [Name.Length - 1].ToString ();
		transform.FindChild ("ListCards").GetComponent<UIDraggablePanel2> ().RemoveItem (ItemList[int.Parse(index)]);

	}

}