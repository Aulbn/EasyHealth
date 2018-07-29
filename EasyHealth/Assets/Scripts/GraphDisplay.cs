using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Data;
using Mono.Data.Sqlite;
using UnityEngine.UI;

public class GraphDisplay : MonoBehaviour {
	public InputField inputField;

//	private List<Workout> workouts = new List<Workout>();
	private List<Vector3> positions = new List<Vector3>();

	private int startDate;
	private int endDate;

	[Header("Graph")]
	public float graphBot = 0;
	public float graphHeight = 3;
	public float graphWidth = 5f;

	private LineRenderer lr;

	void Start(){
		lr = GetComponent<LineRenderer> ();
//		MakeGraph ("Dips");
	}

	public void MakeGraph(string workout){
		List<Workout> workouts = WorkoutManager.instance.LoadWorkoutList(workout);
//		foreach (Workout w in workouts)
//			print ("ID: " + w.GetWorkoutID(WorkoutManager.instance.GetConnectionString()));
		SetPositions (workouts);
//		foreach (Vector3 v in positions) {
//			print (v);
//		}
	}

//	private void FakeValues(){
//		List<Set> tempList = new List<Set> ();
//		List<Set> tempList2 = new List<Set> ();
//		List<Set> tempList3 = new List<Set> ();
//		tempList.Add (new Set(8, 60));
//		tempList.Add (new Set(10, 60));
//		workouts.Add (new Workout ("Chins", tempList, 20));
//		tempList2.Add (new Set(10, 62));
//		tempList2.Add (new Set(12, 62));
//		workouts.Add (new Workout ("Chins", tempList2, 30));
//		tempList3.Add (new Set(12, 50));
//		tempList3.Add (new Set(13, 50));
//		workouts.Add (new Workout ("Chins", tempList3, 30));
//	}

	private void SetPositions(List<Workout> workouts) {
		positions.Clear ();
		if (workouts.Count > 0) {
			List<Workout> sortedList = SortWorkouts (workouts);
			double lowestValue = sortedList [0].GetMeanValue ();
			double highestValue = sortedList [sortedList.Count - 1].GetMeanValue ();
			
			lr.positionCount = workouts.Count;
			for (int i = 0; i < workouts.Count; i++) {
				int index = (i > 0 ? 1 : 0);
				positions.Add (new Vector3 ((float)(index * (graphWidth * (i + 1)/workouts.Count)), (float)(((workouts [i].GetMeanValue () - lowestValue) * (graphHeight / (highestValue - lowestValue)))) + graphBot, 0));
			}

			lr.SetPositions (positions.ToArray());
		}
	}

	private List<Workout> SortWorkouts(List<Workout> list){//-------SORTERAR BARA RÄTT FÖRSTA GÅNGEN
		Workout[] tempArr = new Workout[list.Count];
		list.CopyTo (tempArr);
		List<Workout> tempList = new List<Workout> (tempArr);

//		List<Workout> tempList = new List<Workout> ();
//		foreach (Workout w in list)
//			tempList.Add (w);

		if (tempList.Count > 0) {
			tempList.Sort(delegate(Workout a, Workout b) {
				return (a.GetMeanValue()).CompareTo(b.GetMeanValue());
			});
		}

//		for (int i = 0; i < workouts.Count; i++) {
//			print (i + 1 + ": " + workouts[i].GetMeanValue());
//		}
//		foreach (Workout w in oldList) {
//			print ("oldList: " + w.GetMeanValue());
//		}
//		foreach (Workout w in tempList) {
//			print ("tempLists: " + w.GetMeanValue());
//		}
		return tempList;
	}

	public void ClearLineRenderer(){
		lr.positionCount = 0;
	}
}
