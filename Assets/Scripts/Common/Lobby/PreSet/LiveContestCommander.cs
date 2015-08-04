using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class LiveContestCommander : MonoBehaviour {

	public GameObject PresetContestItem1;
	public GameObject LiveContestItem;
	public int PCCount1;
	public List<int> PCCount2 = new List<int>();
	public float PCGap;
	public void CreatItem(){
		int Count = transform.FindChild ("Scroll View").FindChild ("Position").childCount;
		for(int i = 0 ; i<Count;i++){
			DestroyImmediate(transform.FindChild("Scroll View").FindChild("Position").GetChild(0).gameObject);
		}
		for (int i = 0; i<PCCount1; i++) {
			GameObject Item1 = (GameObject)Instantiate(PresetContestItem1);
			Item1.transform.parent = transform.FindChild("Scroll View").FindChild("Position");
			Item1.transform.localScale = new Vector3(1,1,1);
			float Y = -((float)i*PCGap)+556f-(PCGap*0.5f)-14f;
			if( i== 0){
			Item1.transform.localPosition = new Vector3 (0, Y);
			}else{
				float Sum=0;
				for(int a = 0; a<i;a++){
					Sum += PCCount2[a];
				}
				Item1.transform.localPosition = new Vector3 (0, Y-(Sum*(150)));
			}
		
			Item1.name = "Item " + i.ToString();
		
			for(int a = 0; a<PCCount2[i];a++){
				GameObject Item2 = (GameObject)Instantiate(PresetContestItem1);
				Item2.transform.parent = Item1.transform;
				Item2.transform.localScale = new Vector3(1,1,1);
				Y = -(PCGap*1f)-(a*150);
				Item2.transform.localPosition = new Vector3(0,Y,0);

				Item2.name = "Item " + i.ToString() + " Sub " + a.ToString();

			}
		}
	}

}
