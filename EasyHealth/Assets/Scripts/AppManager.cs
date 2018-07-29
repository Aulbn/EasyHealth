using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AppManager : MonoBehaviour {
	public static AppManager instance;

	public GameObject mainMenu;
	public WorkoutManager workoutManager;
	public AddWorkout addWorkout;
	public StatsManager statsManager;
	public GameObject backButton;


	void Awake(){
		instance = this;
	}

	void Start () {
		OpenMainMenu ();
//		OpenStatsPanel ();
	}

	private void CloseAllWindows(){
		mainMenu.SetActive (false);
		addWorkout.gameObject.SetActive (false);
		statsManager.gameObject.SetActive (false);
		backButton.SetActive (false);
	}

	public void OpenMainMenu(){
		CloseAllWindows ();
		mainMenu.SetActive (true);
	}

	public void OpenAddWorkout(){
		CloseAllWindows ();
		addWorkout.gameObject.SetActive (true);
		backButton.SetActive (true);
		//Load shit!
	}

	public void OpenStatsPanel(){
		CloseAllWindows ();
		statsManager.gameObject.SetActive (true);
		backButton.SetActive (true);
		statsManager.graphDisplay.ClearLineRenderer ();
//		statsManager.LoadExerciseList ();
	}

}
