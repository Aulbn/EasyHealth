using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetManager : MonoBehaviour {

	public InputField repsInput;
	public InputField weightInput;
	public InputField assistInput;

	public int GetReps(){
		return int.Parse (repsInput.text);
	}
	public double GetWeight(){
		if (string.IsNullOrEmpty(assistInput.text))
			return double.Parse (weightInput.text);
		return double.Parse (weightInput.text) - float.Parse(assistInput.text);
	}


	public Set GetSet(){
		return new Set (GetReps(), GetWeight());
	}
		
	public void RemoveSet(){
		AddWorkout.instance.RemoveSet (this.gameObject);
	}
}
