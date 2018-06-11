using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExerciseListItem : MonoBehaviour {
	public Text text;

	public void SetText(string text){
		this.text.text = text;
	}

	public void LoadGraphData(){
		StatsManager.instance.graphDisplay.MakeGraph (text.text);
		StatsManager.instance.dropDown.ShowList (false);
	}

}
