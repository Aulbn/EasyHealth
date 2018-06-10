using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Data;
using Mono.Data.Sqlite;

public class GraphDisplay : MonoBehaviour {

	private List<Workout> workouts = new List<Workout>();
	private List<Vector3> positions = new List<Vector3>();

	private int startDate;
	private int endDate;

	public float graphBot = 0;
	public float graphHeight = 3;
	public float graphWidth = 5f;

	private LineRenderer lr;

	void Start(){
		lr = GetComponent<LineRenderer> ();
		MakeGraph ("Chins");
	}

	public void MakeGraph(string workout){
		workouts = WorkoutManager.instance.LoadWorkoutList(workout);
//		foreach (Workout w in workouts)
//			print ("ID: " + w.GetWorkoutID(WorkoutManager.instance.GetConnectionString()));
		SetPositions ();
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

	private void SetPositions() {
		if (workouts.Count > 0) {
			List<Workout> sortedList = sortWorkouts (workouts);
//			List<Workout> sortedList = workouts;
			double lowestValue = sortedList [0].GetMeanValue ();
			double highestValue = sortedList [sortedList.Count - 1].GetMeanValue ();

			lr.positionCount = workouts.Count;
			for (int i = 0; i < workouts.Count; i++) {
				positions.Add (new Vector3 ((float)(i * (graphWidth / (sortedList.Count - 1)) - graphWidth / 2), (float)(((workouts [i].GetMeanValue () - lowestValue) * (graphHeight / (highestValue - lowestValue)))) + graphBot, 0));
//				Debug.Log ((((workouts [i].GetMeanValue () - lowestValue) * (graphHeight / (highestValue - lowestValue)))));
			}

			lr.SetPositions (positions.ToArray());
		}
	}

	private List<Workout> sortWorkouts(List<Workout> list){
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
}
