using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataLoader : MonoBehaviour {

	public const string path = "Exercises.xml";

	// Use this for initialization
	void Start () {
		DataContainer dc = DataContainer.Load (path);
		foreach (Exercise exercise in dc.exercises) {
			print (exercise.name);
		}
	}

}
