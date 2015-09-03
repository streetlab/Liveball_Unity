﻿using UnityEngine;
using System.Collections;

public class CoverFlowItems : MonoBehaviour {


	void OnDrag (Vector2 delta)
	{	
		//드래그 범위가 지정된 컬라이더 위치 재설정
		transform.parent.parent.FindChild ("BG").localPosition = new Vector3 (transform.parent.localPosition.x,transform.parent.parent.FindChild ("BG").localPosition.y,0);


	}
	public void Button(){

		//아이템 클릭시 경품페이지를 띄우고 데이터 입력
		transform.root.FindChild ("Scroll").FindChild ("Giveaway").gameObject.SetActive (true);


		if (LobbyGiftCommander.mGift != null) {
			transform.root.FindChild ("Scroll").FindChild ("Giveaway").FindChild("Scroll View")
				.FindChild("Item 0").GetComponent<UITexture>().mainTexture
					= LobbyGiftCommander.mGift.Textures[
					                                    transform.FindChild("openurl").GetComponent<UILabel>().text];
			for(int i = 0; i<LobbyGiftCommander.mGift.gift.Count;i++){
			
				if(transform.FindChild("openurl").GetComponent<UILabel>().text == LobbyGiftCommander.mGift.gift[i].image){
					for(int s = 0; s<6;s++){
//						transform.root.FindChild ("Scroll").FindChild ("Giveaway").FindChild("Scroll View")
//							.FindChild("Item " + s.ToString()).GetComponent<UITexture>().mainTexture
//								= LobbyGiftCommander.mGift.Textures[
//								                                    LobbyGiftCommander.mGift.gift[i].detail[s].image];
						transform.root.FindChild ("Scroll").FindChild ("Giveaway").FindChild("Scroll View")
							.FindChild("Item " + (s+1).ToString()).GetComponent<UITexture>().mainTexture
								= LobbyGiftCommander.mGift.Textures[
								                                    LobbyGiftCommander.mGift.gift[i].detail[s].image];
						Debug.Log("LobbyGiftCommander.mGift.gift[i].detail[s].image : " + LobbyGiftCommander.mGift.gift[i].detail[s].image);

						transform.root.FindChild ("Scroll").FindChild ("Giveaway").FindChild("Scroll View")
							.FindChild("Item " + (s+1).ToString()).FindChild("Sprite").FindChild("Label").GetComponent<UILabel>().text
								=  LobbyGiftCommander.mGift.gift[i].detail[s].text;




					

					

					}
					transform.root.FindChild ("Scroll").FindChild ("Giveaway").FindChild("Bots").FindChild("Sprite").FindChild("Gacha").FindChild("product").
						GetComponent<UILabel>().text = LobbyGiftCommander.mGift.gift[i].product;
					
					
					transform.root.FindChild ("Scroll").FindChild ("Giveaway").FindChild("Scroll View").FindChild("Info").FindChild("Label")
						.GetComponent<UILabel>().text = "-참여방법 : "+LobbyGiftCommander.mGift.gift[i].mileage+" 사용하여 참여\n-참여하신 마일리지는 환불되지 않습니다.\n-해당 경품은 당사 사정에 따라 사전 공지없이 변경 가능합니다.";


					transform.root.FindChild ("Scroll").FindChild ("Giveaway").FindChild("Bots").FindChild("Sprite").FindChild("Gacha")
						.FindChild("Label").GetComponent<UILabel>().text = LobbyGiftCommander.mGift.gift[i].mileage + " 참여하기 ";

				
				}

			
			}

		}
		//transform.gameObject.SetActive (true);



				
		transform.root.FindChild ("Scroll").FindChild ("Giveaway").gameObject.transform.localPosition = new Vector3(0,0,0);
		transform.root.FindChild ("Scroll").FindChild ("Giveaway").FindChild ("Scroll View").GetComponent<UIScrollView> ().ResetPosition ();
	}
	public void Off(){
		transform.root.FindChild ("Scroll").FindChild ("Giveaway").gameObject.transform.localPosition = new Vector3(-720,0,0);
		transform.root.FindChild ("Scroll").FindChild ("Giveaway").gameObject.SetActive (false);
	}
}
