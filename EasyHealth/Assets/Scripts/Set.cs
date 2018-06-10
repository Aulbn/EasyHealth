using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Set {
	public int reps { get; set; }
	public double weight { get; set; }
//	public double assistance { get; set; }

	public Set (int reps, double weight){
		this.reps = reps;
		this.weight = weight;
//		this.assistance = assistance;
	}

	public string print(){
		return "Reps: " + reps + " | Weight: " + weight;
	}

}
