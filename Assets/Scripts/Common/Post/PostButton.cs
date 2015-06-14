using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PostButton : MonoBehaviour {
	public CheckMailbox CheckMail;
	public int GachaCount = 0;
	GetMailEvent Mail;
	List<Mailinfo> Mails = new List<Mailinfo>();
	public GameObject GachaAnim;

	public void getitem (int mailseq,int attachseq){
	
	}
	void Start(){
		Mail = new GetMailEvent (new EventDelegate (this, "getdata"));
		NetMgr.GetUserMailBox (UserMgr.UserInfo.memSeq,Mail);
	}
	public void on(){

//		Mail = new GetMailEvent (new EventDelegate (this, "Setdata"));
//		NetMgr.GetUserMailBox (UserMgr.UserInfo.memSeq,Mail);
	//	transform.FindChild ("TF_Post").gameObject.SetActive (true);
		Setdata();	


	}
	public void off(){
		Mail = new GetMailEvent (new EventDelegate (this, "getdata"));
		NetMgr.GetUserMailBox (UserMgr.UserInfo.memSeq,Mail);
		StartCoroutine(Down (transform.FindChild ("TF_Post").gameObject));
	

			
	
	}
	void getdata(){
		Mails = Mail.Response.data;
		if (Mail.Response.data != null) {
			if (Mail.Response.data.Count > 0) {
				transform.FindChild ("Background").FindChild ("on").gameObject.SetActive (true);
			} else {
				transform.FindChild ("Background").FindChild ("on").gameObject.SetActive (false);
			}
		}
	}
	void Setdata(){
		Debug.Log (transform.FindChild ("TF_Post").FindChild ("List").childCount);
		if (transform.FindChild ("TF_Post").FindChild ("List").childCount > 1) {
			for(int i = 1; i < transform.FindChild ("TF_Post").FindChild ("List").childCount;i++){
				Destroy(transform.FindChild ("TF_Post").FindChild ("List").GetChild(i).gameObject);
			}
		}
	//	Mails = Mail.Response.data;
		GameObject origin = transform.FindChild ("TF_Post").FindChild ("List").FindChild ("bar origin").gameObject;
		Vector3 po;
		if (Mails != null) {
			for(int i = 0; i<Mails.Count; i++){

				if(Mails[i].attach[0].limitDateTime!=null){
				
					if(Mails[i].attach[0].limitDateTime.Length>-1)
				{

				GameObject temp = (GameObject)Instantiate(origin,new Vector2(1,1),origin.transform.localRotation);
				po = new Vector3(0,i*(110),0);
				temp.transform.parent = origin.transform.parent;
	        	temp.transform.localScale = new Vector3(1,1,1);
				temp.transform.localPosition = origin.transform.localPosition;
				temp.transform.localPosition -= po;
					temp.name = "Mail " + i.ToString();
				temp.transform.FindChild("Name").GetComponent<UILabel>().text = Mails[i].mailTitle;
				string limit = Mails[i].attach[0].limitDateTime;
				char [] limitlist;
				List<string> result = new List<string>();
				if(limit.Length>10){
				limitlist = limit.ToCharArray();
				for(int q = 0; i<limitlist.Length-2;q++){
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
				limit = string.Join("",result.ToArray());


				temp.transform.FindChild("Lmite").GetComponent<UILabel>().text = limit;
				}
					//+ " " + Mails[i].attach[0].attachValue.ToString();
				Debug.Log(temp.transform.FindChild("Name").GetComponent<UILabel>().text + " status : " + Mails[i].mailStatus);
				temp.transform.FindChild("mailseq").GetComponent<UILabel>().text = Mails[i].mailSeq.ToString();
						temp.transform.FindChild("attachseq").GetComponent<UILabel>().text = Mails[i].attach[0].attachSeq.ToString();
						temp.transform.FindChild("Code").GetComponent<UILabel>().text = Mails[i].attach[0].attachCode.ToString();
						temp.gameObject.SetActive(true);

					}
			}
			}
	}
		StartCoroutine (Up(transform.FindChild ("TF_Post").gameObject));
	}
	IEnumerator Up(GameObject G){
		G.SetActive (true);
		if(G.name == "TF_Lineup"){
			G.transform.FindChild("Scroll View").gameObject.SetActive(true);
			G.transform.FindChild("Scroll View").GetComponent<UIScrollView>().ResetPosition();
		}
		for (int i =0; i<5; i++) {
			if(G.transform.localPosition.y!=-565.5){
				G.transform.localPosition += new Vector3(0,1334.5f/5f,0);
				
				yield return new WaitForSeconds(0.01f);
			}else{
				//UiRoot.transform.FindChild("TF_Items").FindChild("TF_Items").FindChild("category 1").GetComponent<UIScrollView>().ResetPosition();
				break;
			}
		}
	}
	IEnumerator Down(GameObject G){
		if(G.name == "TF_Lineup"){
			G.transform.FindChild("Scroll View").gameObject.SetActive(false);
			G.transform.FindChild("Scroll View 1").gameObject.SetActive(false);
		}
		for (int i =0; i<5; i++) {
			if(G.transform.localPosition.y!=-1900){
				G.transform.localPosition -= new Vector3(0,1334.5f/5f,0);
				yield return new WaitForSeconds(0.02f);
			}else{
				break;
			}
		}

			G.SetActive(false);
		Debug.Log (GachaCount);
		if (GachaCount > 0&&Application.loadedLevelName.Equals("SceneCards")) {
			AutoFade.LoadLevel("SceneCards", 0f, 1f);
			
		}

	}
	public void GachaOK(){
	
		transform.FindChild ("PostDialogue").gameObject.SetActive (false);
		PostgetButton.anim.SetActive (false);
		transform.FindChild ("PostDialogue").FindChild ("Panel").FindChild ("Sprite").GetComponent<BoxCollider2D> ().enabled = false;
		Destroy (PostgetButton.anim);
		if (CheckMail.gacha.itemType >= 6) {
			DialogueMgr.ShowDialogue ("경품 확인", CheckMail.gacha.itemName+ " 지급 완료 되었습니다.\n인벤토리를 확인해주세요.", DialogueMgr.DIALOGUE_TYPE.Alert, null);
		} else {
			DialogueMgr.ShowDialogue ("경품 확인", CheckMail.gacha.itemName + " 지급 완료 되었습니다.", DialogueMgr.DIALOGUE_TYPE.Alert, null);
		}
	}

}
