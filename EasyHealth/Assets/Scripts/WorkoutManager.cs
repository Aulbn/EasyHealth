using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Data;
using Mono.Data.Sqlite;

public class WorkoutManager : MonoBehaviour {
	public static WorkoutManager instance;

	private static string connectionString; 
	public List<Workout> workoutList = new List<Workout> ();
	private String[] exercises;

	void Awake(){
		instance = this;
		//Set the path of the database
		connectionString = "URI=file:" + Application.dataPath + "/Database/Database.db";
	}

	void Start () {
		List<string> tempList = LoadExerciseList();
		foreach (string s in tempList) {
			print (s);
		}
	}

	public List<string> LoadExerciseList (){
		List<string> tempList = new List<string> ();
		using (IDbConnection dbConnection = new SqliteConnection (connectionString)) {
			dbConnection.Open ();

			using (IDbCommand dbCmd = dbConnection.CreateCommand()) {
				string sqlQuery = String.Format("SELECT EName FROM Exercise");
				dbCmd.CommandText = sqlQuery;

				using (IDataReader reader = dbCmd.ExecuteReader ()) {
					while (reader.Read ()) {
						tempList.Add (reader.GetString (0));
					}
				}
			}
		}
		tempList.Sort ();
		return tempList;
	}

	public List<Workout> LoadWorkoutList (string exercise){
		int currentID = 0;
		Workout tempWorkout = new Workout ();
		List<Workout> tempList = new List<Workout> ();

		using (IDbConnection dbConnection = new SqliteConnection (connectionString)) {
			dbConnection.Open ();

			//Enter SQL query
			using (IDbCommand dbCmd = dbConnection.CreateCommand()) {
				string sqlQuery = String.Format("SELECT ID, Exercise, RestPerSet, Date, Reps, Weight FROM Workout W, WorkoutSet WS, WeightWorkout WW WHERE W.ID = WW.WorkoutID AND W.ID = WS.WorkoutID AND Exercise = \"{0}\"", exercise);
				dbCmd.CommandText = sqlQuery;

				//Read the data
				using (IDataReader reader = dbCmd.ExecuteReader()){
					while (reader.Read ()) {
						if (reader.GetInt32 (0) != currentID) {
							currentID = reader.GetInt32 (0);
//							print (currentID);
							tempWorkout = new Workout (reader.GetString (1), reader.GetInt32(2), reader.GetInt32(3));
							tempList.Add (tempWorkout);
						}
						tempWorkout.sets.Add (new Set (reader.GetInt32 (4), reader.GetDouble (5)));
					}
					reader.Close ();
					dbConnection.Close ();
				}
			}
		}
		return tempList;
	}

	public string GetConnectionString(){
		return connectionString;
	}

	private String Normalize(string input){
		input.Trim ();
		if (string.IsNullOrEmpty (input)) {
			return string.Empty;
		}
		return input.Substring(0,1).ToUpper() + input.Substring (1).ToLower();
	}

	public int CurrentEpochDate(){
		return (int)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
	}
	public string Epoch2String(int epoch) {
		return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(epoch).ToShortDateString(); 
	}
}
