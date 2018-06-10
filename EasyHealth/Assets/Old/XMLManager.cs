using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

public class XMLManager : MonoBehaviour {

	public static XMLManager instance;

	void Awake(){
		if (instance == null)
			instance = this; 
		if (instance != this)
			Destroy (gameObject);
	}

	//list of data
	public ExerciseDatabase exDB;

}

[System.Serializable]
public class ExerciseEntry {
	public string exerciseName;
	public ExerciseType exerciseType;
	public int reps;
	public int sets;
	public float weight;
}

[System.Serializable]
public class ExerciseDatabase {
	public List<ExerciseEntry> list = new List<ExerciseEntry>();
}

public enum ExerciseType {
	Dips,
	Chins,
	Squats
}