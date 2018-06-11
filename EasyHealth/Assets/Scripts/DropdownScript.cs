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

	public void SetList(List<string> exercises){
		nameList = exercises;
		foreach (string s in nameList) {
			GameObject i = Instantiate (listItemPrefab, container);
			i.GetComponent<ExerciseListItem>().SetText (s);
		}
	}

//	public void ShowList(){
//		foreach (string s in nameList) {
//			GameObject i = Instantiate (listItemPrefab, container);
//			i.GetComponent<ExerciseListItem>().SetText (s);
//		}
//	}

	public void ShowList(bool show){
		list.SetActive (show); 
	}

	public void ToggleList(){
		list.SetActive (!list.activeSelf); 
	}

	public void ClearList(){
		foreach (GameObject g in container) {
			GameObject.Destroy (g);
		}
	}
}
