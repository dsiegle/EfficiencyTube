using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

// Attached to the "TimeRunEOD" gameobject.
// Indicates the HH:MM:SS until the end of the day
public class TimeRunEODScript : MonoBehaviour {

	private TimeCalculationScript to;
	private Text trEndDay;

	// Use this for initialization
	void Start ()
	{
		to = TimeCalculationScript.tcs;
		trEndDay = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		TimeSpan ts = new TimeSpan (0, 0, (int)to.secToEOOT);
		trEndDay.text = ts.ToString();
	}
}
