using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("ExerciseCollection")]
public class DataContainer {

	[XmlArray("Exercises")]
	[XmlArrayItem("Exercise")]
	public List<Exercise> exercises = new List<Exercise>();

	public static DataContainer Load(string path){
		TextAsset _xml = Resources.Load<TextAsset> (path);
		XmlSerializer serializer = new XmlSerializer (typeof(DataContainer));
		StringReader reader = new StringReader (_xml.text);
		DataContainer exercises = serializer.Deserialize (reader) as DataContainer;
		reader.Close();
		return exercises;
	}

}
