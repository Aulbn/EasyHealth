using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropdownScript : MonoBehaviour {

	[Header("References")]
	public GameObject list;
	public Transform container;
	[Header("List")]
	public GameObject listItemPrefab;
	private List<string> nameList = new List<string> ();

	public void ShowList(){
		foreach (string s in nameList) {
			GameObject g = Instantiate (listItemPrefab, container);
			//g.text = s
		}
	}

	public void ClearList(){
		foreach (GameObject g in container) {
			GameObject.Destroy (g);
		}
	}
}
