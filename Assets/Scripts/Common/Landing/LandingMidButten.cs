using UnityEngine;
using System.Collections;

public class LandingMidButten : MonoBehaviour {
	GameObject G_M1,G_M2,G_M3,Bot,VS;
	Color S_Abled = new Color(242f/255f,242f/255f,242f/255f);
	Color S_Disabled = new Color(217f/255f,217f/255f,217f/255f);
	Color L_Abled = new Color(0,0,0);
	Color L_Disabled = new Color(157f/255f,157f/255f,157f/255f);

	void Start(){
		G_M1 = transform.FindChild ("M1").gameObject;
		G_M2 = transform.FindChild ("M2").gameObject;
		//G_M3 = transform.FindChild ("M3").gameObject;
		Bot = transform.FindChild ("Bot").gameObject;
		VS = transform.FindChild ("VS").gameObject;
		transform.FindChild ("<").GetComponent<UIButton> ().isEnabled = false;
	}
	public void LeftButten(){
		G_M1.transform.FindChild ("L").GetComponent<UILabel> ().text = "시즌타율";
		G_M2.transform.FindChild ("L").GetComponent<UILabel> ().text = "이닝별";
		//G_M3.transform.FindChild ("L").GetComponent<UILabel> ().text = "투수유형별";
		Bot.GetComponent<UILabel> ().text = "1,2루";
		VS.GetComponent<UILabel> ().text = "VS "+"Team" +" 시즌 타율 0.200 | "+ "Direction";
		transform.FindChild ("<").GetComponent<UIButton> ().isEnabled = false;
		transform.FindChild (">").GetComponent<UIButton> ().isEnabled = true;
	}
	public void RightButten(){
		G_M1.transform.FindChild ("L").GetComponent<UILabel> ().text = "주자상황";
		G_M2.transform.FindChild ("L").GetComponent<UILabel> ().text = "이닝";
		//G_M3.transform.FindChild ("L").GetComponent<UILabel> ().text = "투수유형";
		Bot.GetComponent<UILabel> ().text = "Direction";
		VS.GetComponent<UILabel> ().text = "VS "+"Team" +" 0 승 0 패 "+"평균자책 "+ "0.00 | " + "Direction";
		transform.FindChild ("<").GetComponent<UIButton> ().isEnabled = true;
		transform.FindChild (">").GetComponent<UIButton> ().isEnabled = false;
	}
	public void M1(){
		transform.parent.parent.parent.parent.GetComponent<LandingManager> ().M1 (null);
		G_M1.transform.FindChild ("S").GetComponent<UISprite> ().color = S_Abled;
		G_M2.transform.FindChild ("S").GetComponent<UISprite> ().color = S_Disabled;
		//G_M3.transform.FindChild ("S").GetComponent<UISprite> ().color = S_Disabled;
		
		G_M1.transform.FindChild ("L").GetComponent<UILabel> ().color = L_Abled;
		G_M2.transform.FindChild ("L").GetComponent<UILabel> ().color = L_Disabled;
		//G_M3.transform.FindChild ("L").GetComponent<UILabel> ().color = L_Disabled;
		
	}
	public void M2(){
		transform.parent.parent.parent.parent.GetComponent<LandingManager> ().M2 (null);
		G_M1.transform.FindChild ("S").GetComponent<UISprite> ().color = S_Disabled;
		G_M2.transform.FindChild ("S").GetComponent<UISprite> ().color = S_Abled;
		//G_M3.transform.FindChild ("S").GetComponent<UISprite> ().color = S_Disabled;
		
		G_M1.transform.FindChild ("L").GetComponent<UILabel> ().color = L_Disabled;
		G_M2.transform.FindChild ("L").GetComponent<UILabel> ().color = L_Abled;
		//G_M3.transform.FindChild ("L").GetComponent<UILabel> ().color = L_Disabled;
	}
	public void M3(){
		G_M1.transform.FindChild ("S").GetComponent<UISprite> ().color = S_Disabled;
		G_M2.transform.FindChild ("S").GetComponent<UISprite> ().color = S_Disabled;
		//G_M3.transform.FindChild ("S").GetComponent<UISprite> ().color = S_Abled;
		
		G_M1.transform.FindChild ("L").GetComponent<UILabel> ().color = L_Disabled;
		G_M2.transform.FindChild ("L").GetComponent<UILabel> ().color = L_Disabled;
		//G_M3.transform.FindChild ("L").GetComponent<UILabel> ().color = L_Abled;
	}
	public void MN1(){
		transform.parent.parent.parent.parent.GetComponent<LandingManager> ().M1 (Bot);
		G_M1.transform.FindChild ("S").GetComponent<UISprite> ().color = S_Abled;
		G_M2.transform.FindChild ("S").GetComponent<UISprite> ().color = S_Disabled;
		//G_M3.transform.FindChild ("S").GetComponent<UISprite> ().color = S_Disabled;
		
		G_M1.transform.FindChild ("L").GetComponent<UILabel> ().color = L_Abled;
		G_M2.transform.FindChild ("L").GetComponent<UILabel> ().color = L_Disabled;
		//G_M3.transform.FindChild ("L").GetComponent<UILabel> ().color = L_Disabled;
		
	}
	public void MN2(){
		transform.parent.parent.parent.parent.GetComponent<LandingManager> ().M2 (Bot);
		G_M1.transform.FindChild ("S").GetComponent<UISprite> ().color = S_Disabled;
		G_M2.transform.FindChild ("S").GetComponent<UISprite> ().color = S_Abled;
		//G_M3.transform.FindChild ("S").GetComponent<UISprite> ().color = S_Disabled;
		
		G_M1.transform.FindChild ("L").GetComponent<UILabel> ().color = L_Disabled;
		G_M2.transform.FindChild ("L").GetComponent<UILabel> ().color = L_Abled;
		//G_M3.transform.FindChild ("L").GetComponent<UILabel> ().color = L_Disabled;
	}
	public void MN3(){
		G_M1.transform.FindChild ("S").GetComponent<UISprite> ().color = S_Disabled;
		G_M2.transform.FindChild ("S").GetComponent<UISprite> ().color = S_Disabled;
		//G_M3.transform.FindChild ("S").GetComponent<UISprite> ().color = S_Abled;
		
		G_M1.transform.FindChild ("L").GetComponent<UILabel> ().color = L_Disabled;
		G_M2.transform.FindChild ("L").GetComponent<UILabel> ().color = L_Disabled;
		//G_M3.transform.FindChild ("L").GetComponent<UILabel> ().color = L_Abled;
	}
}
