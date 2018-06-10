using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddWorkout : MonoBehaviour {
	public static AddWorkout instance;

	public InputField exerciseInput;
	public InputField restInput;
	public List<GameObject> setList = new List<GameObject>();
	public GameObject scrollListContent;
	private GameObject currentAddButton;
	public GameObject setListObjectPrefab;
	public GameObject addButtonPrefab;

	void Awake(){
		instance = this;
		AddAddButton ();
	}

	void Start(){

	}

	public void AddSet(){
		GameObject.Destroy (currentAddButton);
		GameObject setObject = Instantiate(setListObjectPrefab, scrollListContent.transform) as GameObject;
		setList.Add (setObject);
		AddAddButton ();
	}

	public void AddAddButton(){
		currentAddButton = Instantiate (addButtonPrefab, scrollListContent.transform);
		currentAddButton.GetComponent<Button> ().onClick.AddListener (AddSet);
	}
		
	public void RemoveSet(GameObject setListObject){
		setList.Remove (setListObject);
		GameObject.Destroy (setListObject);
	}

	private string Normalize(string text){
		text = text.Trim ();
		if (!string.IsNullOrEmpty (text)) {
			text = text.Substring (0, 1).ToUpper () + text.Substring (1).ToLower ();
		}
		return text;
	}

	public void PrintWorkout(){
		if (setList.Count > 0) {
			int currentRep = 0;
			string exerciseText = Normalize (exerciseInput.text);


			if (string.IsNullOrEmpty(exerciseText)) {
				print ("No exercise name");
			} else {
				print (exerciseText);
			}
			foreach (GameObject s in setList) {
				SetManager manager = s.GetComponent<SetManager> ();
				string repsText = Normalize(manager.repsInput.text.Trim());
				string weightText = Normalize("" + manager.GetWeight ());
				currentRep++;

				if (string.IsNullOrEmpty (repsText))
					print (currentRep + "  No reps");
				else if (string.IsNullOrEmpty (weightText))
					print (currentRep + "  No weight");
				else {
					print (currentRep + ". Reps: " + repsText + "| Weight: " + weightText);
				}
			}
		}
	}

	public void AddWorkoutButton(){
		List<Set> sets = new List<Set>();
		int rest = 0;

		if (setList.Count > 0 && !string.IsNullOrEmpty (exerciseInput.text)) {

			if (restInput.text != "")
				rest = int.Parse (restInput.text);

			for (int i = 0; i < setList.Count; i++) {
				sets.Add (setList [i].GetComponent<SetManager> ().GetSet ());
			}

			Workout w = new Workout (exerciseInput.text, sets, rest);
			print ("Workout Created");
			print (WorkoutManager.instance.GetConnectionString ());
			w.StoreData (WorkoutManager.instance.GetConnectionString ());
			print ("Workout Saved");
			ResetInput ();
			AppManager.instance.ToMainMenu ();
		}
	}



	public void ResetInput(){
		exerciseInput.text = "";
		restInput.text = "";
		foreach (GameObject s in setList) {
			GameObject.Destroy (s);
		}
		setList.Clear ();
	}


	
}
