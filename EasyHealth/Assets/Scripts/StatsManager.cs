using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour {
	public static StatsManager instance;

	public GraphDisplay graphDisplay;
	public DropdownScript dropDown;

	void Awake(){
		instance = this;
	}

//	public void LoadExerciseList(){
//		dropDown.SetList (WorkoutManager.instance.LoadExerciseList ());
//	}

}
