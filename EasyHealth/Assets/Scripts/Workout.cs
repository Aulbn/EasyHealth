using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Data;
using Mono.Data.Sqlite;

public class Workout {
	public string exercise { get; set; }
	public List<Set> sets = new List<Set> ();
	public int rest { get; set; }
	private int date;

	public Workout (string exercise, List<Set> sets, int rest){
		this.exercise = exercise;
		this.sets = sets;
		this.rest = rest;
		date = WorkoutManager.instance.CurrentEpochDate();
	}

	public Workout (string exercise, List<Set> sets, int rest, int date){
		this.exercise = exercise;
		this.sets = sets;
		this.rest = rest;
		this.date = date;
	}

	public Workout (string exercise, int rest, int date){
		this.exercise = exercise;
		this.rest = rest;
		this.date = date;
	}

	public Workout (){
		this.exercise = "Exercise";
		this.rest = 0;
		this.date = WorkoutManager.instance.CurrentEpochDate();
	}


	public double GetMeanValue(){
		//Genomsnittssetet
		double value = 0;
		foreach (Set s in sets) {
			value += s.weight * s.reps;
		}
		return value / sets.Count;
	}

	public void StoreData(string connectionString){
		bool workoutExists = WorkoutExists (connectionString);
		bool exerciseExists = ExerciseExists (connectionString);

			//Start the connection to the database
		using (IDbConnection dbConnection = new SqliteConnection (connectionString)) {
				dbConnection.Open ();

			if (!exerciseExists){
				//Store Exercise
				using (IDbCommand dbCmd = dbConnection.CreateCommand ()) {
					string sqlQuery = String.Format ("INSERT INTO Exercise(Ename) VALUES(\"{0}\")", exercise);
					dbCmd.CommandText = sqlQuery;
					dbCmd.ExecuteScalar ();
					Debug.Log ("Exercise Stored");
				}
			}

			if (!workoutExists) {
				//Store Workout
				using (IDbCommand dbCmd = dbConnection.CreateCommand ()) {
					string sqlQuery = String.Format ("INSERT INTO Workout(Exercise, Date) VALUES(\"{0}\",\"{1}\")", exercise, date);
					dbCmd.CommandText = sqlQuery;
					dbCmd.ExecuteScalar ();
					Debug.Log ("Workout Stored");
				}
			}
				dbConnection.Close ();
			}

		//Get Workout ID
		int id = GetWorkoutID (connectionString);

		//Start the connection to the database
		using (IDbConnection dbConnection = new SqliteConnection (connectionString)) {
			dbConnection.Open ();

			using (IDbCommand dbCmd = dbConnection.CreateCommand ()) {

				if (!workoutExists) {
					//Store WeightWorkout
					string sqlQuery = String.Format ("INSERT INTO WeightWorkout(WorkoutID, RestPerSet) VALUES(\"{0}\",\"{1}\")", id, rest);
					dbCmd.CommandText = sqlQuery;
					dbCmd.ExecuteScalar ();
					Debug.Log ("WeightWorkout Stored");
				}

				//Store Sets
				foreach (Set s in sets) {
					string sqlQuery = String.Format("INSERT INTO WorkoutSet(WorkoutID, Reps, Weight) VALUES(\"{0}\",\"{1}\",\"{2}\")", id, s.reps, s.weight);
					dbCmd.CommandText = sqlQuery;
					dbCmd.ExecuteScalar ();
					Debug.Log ("WorkoutSet Stored");
				}
			}
			dbConnection.Close ();
		}
	}


	public int GetWorkoutID(string connectionString){
		int id = 0;
		//Start the connection to the database
		using (IDbConnection dbConnection = new SqliteConnection (connectionString)) {
			dbConnection.Open ();

			//Enter SQL query
			using (IDbCommand dbCmd = dbConnection.CreateCommand()) {

				string sqlQuery = String.Format("SELECT DISTINCT ID, Date FROM Workout WHERE Exercise = \"{0}\" AND Date BETWEEN \"{1}\" AND \"{2}\"", exercise, date - 43200000, date + 43200000);
				dbCmd.CommandText = sqlQuery;

				//Read the data
				using (IDataReader reader = dbCmd.ExecuteReader()){
					while (reader.Read ()) {
//						Debug.Log ("COMPARE DATES: " + WorkoutManager.instance.Epoch2String (date) + " = " + WorkoutManager.instance.Epoch2String(reader.GetInt32(1)));
//						Debug.Log ("COMPARE DATES: " + date + " = " + reader.GetInt32(1));
						if (WorkoutManager.instance.Epoch2String(date).Equals(WorkoutManager.instance.Epoch2String(reader.GetInt32(1)))){
							id = reader.GetInt32(0);
//							Debug.Log("ID is: " + id);
						}
					}
					dbConnection.Close ();
					reader.Close ();
				}
			}
		}
		return id;
	}

	private bool WorkoutExists(string connectionString){
		//Start the connection to the database
		using (IDbConnection dbConnection = new SqliteConnection (connectionString)) {
			dbConnection.Open ();

			//Enter SQL query
			using (IDbCommand dbCmd = dbConnection.CreateCommand()) {

				string sqlQuery = String.Format("SELECT DISTINCT ID, Date FROM Workout WHERE Exercise = \"{0}\" AND Date BETWEEN \"{1}\" AND \"{2}\"", exercise, date - 43200000, date + 43200000);
				dbCmd.CommandText = sqlQuery;

				//Read the data
				using (IDataReader reader = dbCmd.ExecuteReader()){
					while (reader.Read ()) {
						if (WorkoutManager.instance.Epoch2String(date).Equals(WorkoutManager.instance.Epoch2String(reader.GetInt32(1))))
							return true;
					}
					dbConnection.Close ();
					reader.Close ();
				}
			}
		}
		return false;
	}

	private bool ExerciseExists(string connectionString){
		//Start the connection to the database
		using (IDbConnection dbConnection = new SqliteConnection (connectionString)) {
			dbConnection.Open ();

			//Enter SQL query
			using (IDbCommand dbCmd = dbConnection.CreateCommand()) {

				string sqlQuery = String.Format("SELECT EName FROM Exercise WHERE EName = \"{0}\"", exercise);
				dbCmd.CommandText = sqlQuery;

				//Read the data
				using (IDataReader reader = dbCmd.ExecuteReader()){
					while (reader.Read ()) {
						Debug.Log ("Exercise already exists");
						return true;
					}
					dbConnection.Close ();
					reader.Close ();
				}
			}
		}
		return false;
	}

//	private string GetDate(){
//		return "" + System.DateTime.Now.ToString ("yy-MM-dd");
//	}

	public string Print(){
		return "Ex: " + exercise + ". Date: " + date;
	}
}
