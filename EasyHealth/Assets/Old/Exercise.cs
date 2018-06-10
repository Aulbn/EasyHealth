using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;

public class Exercise {

	[XmlAttribute("Name")]
	public string name;

	[XmlElement("Date")]
	public string date;

	[XmlElement("Sets")]
	public int sets;

	[XmlElement("Reps")]
	public int reps;

	[XmlElement("Weight")]
	public float weight;

}
