using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AppManager : MonoBehaviour {
	public static AppManager instance;

	public GameObject mainMenu;
	public GameObject addWorkout;
	public GameObject statsPanel;

	void Awake(){
		instance = this;
	}

	void Start () {
		ToMainMenu ();
	}

	public void ToMainMenu(){
		mainMenu.SetActive (true);
		addWorkout.SetActive (false);
		statsPanel.SetActive (false);
	}
}
