using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

// Attached to the HMToEOD" GameObject
// Indicates the hh:MMAM time remaining until the end of the work day
public class HMToEODScript : MonoBehaviour {
	
	private TimeCalculationScript to;
	private Text t;
	
	// Use this for initialization
	void Start ()
	{
		to = TimeCalculationScript.tcs;
		t = GetComponent<Text>();		
	}
	
	// Update is called once per frame
	void Update ()
	{
		t.text = to.endOverTime.ToString ("hh:mmtt\n");

	}
}
