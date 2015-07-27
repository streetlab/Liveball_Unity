using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;


/// <summary>

/// Creates a prefab from a selected game object.

/// </summary>
[CustomEditor(typeof(LobbyAddSubInSub))]
public class LobbyAddSubInSubEditor : Editor
	
	
	
{
	
	LobbyAddSubInSub _this;
	void OnEnable(){
		
		_this = target as LobbyAddSubInSub;
	}
	
	

	//List<SubItem> Lists = new List<SubItem>();
	public override void OnInspectorGUI(){
		//Lists.Clear ();
		base.DrawDefaultInspector ();

		if (_this.SubItemList.Count != _this.gameObject.GetComponent<LobbyAddSub> ().SubMenuName.Length) {
			_this.SubItemList.Clear();
			for(int i = 0; i<_this.gameObject.GetComponent<LobbyAddSub>().SubMenuName.Length;i++){
				//Debug.Log("I : " + i + " : " + _this.gameObject.GetComponent<LobbyAddSub> ().SubMenuValue[i]);
				SubItem sub = new SubItem();
				//sub = null;
				sub.SubItemName.Add(_this.gameObject.GetComponent<LobbyAddSub> ().SubMenuValue[i]);
				_this.SubItemList.Add(sub);

				//Debug.Log("I : " + i + " : " + _this.gameObject.GetComponent<LobbyAddSub> ().SubMenuValue[i]);
				//_this.SubItemList[i].SubItemName.Add(k);
				//_this.SubItemList[i].SubItemName.Clear();
			//_this.SubItemList[i].SubItemName.Add(_this.gameObject.GetComponent<LobbyAddSub> ().SubMenuValue[i]);
			}
		
		}
		if (_this.Width.Count != _this.gameObject.GetComponent<LobbyAddSub> ().SubMenuName.Length) {
			_this.Width.Add(0);
		}
//		if (GUILayout.Button ("Reset")) {
//			
//			_this.SubItemList.Clear();
//			for(int i = 0; i<_this.gameObject.GetComponent<LobbyAddSub>().SubMenuName.Length;i++){
//				//Debug.Log("I : " + i + " : " + _this.gameObject.GetComponent<LobbyAddSub> ().SubMenuValue[i]);
//				SubItem sub = new SubItem();
//				//sub = null;
//				sub.SubItemName.Add(_this.gameObject.GetComponent<LobbyAddSub> ().SubMenuValue[i]);
//				_this.SubItemList.Add(sub);
//
//			}}

	

			if (GUILayout.Button ("CreateAddInSub")) {
				
				_this.CreateAddInSub();
				
				
				
				//	_this.SunItemList= new SubItem[4];
				
			}
		if (GUILayout.Button ("DeleteAddInSub")) {
			
			_this.DeleteInSub();
			
			
			
			//	_this.SunItemList= new SubItem[4];
			
		}
	}
	
	
	
}