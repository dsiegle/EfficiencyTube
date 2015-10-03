using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

// This script is attached to the "TimeText" object.
// It sets it's time and position.
public class TimeTextScript : MonoBehaviour {

	private float guiMinY;
	private float guiMaxY;
	private float guiDelta;
	private Text timeText;
	private TimeCalculationScript to;	// external 'singleton' with all our time calculations

	// Use this for initialization
	void Start ()
	{
		to = TimeCalculationScript.tcs;
		timeText = GetComponent<Text>();

		guiMinY = -283;
		guiMaxY = 355;
		guiDelta = guiMaxY - guiMinY;
		transform.localPosition = new Vector3 (-37, guiMinY, 0);

	}
	
	// Update is called once per frame
	void Update () {
		float pos = guiMinY + (to.runSec/(float)to.workSpan.TotalSeconds * guiDelta);
		transform.localPosition = new Vector3(-37, pos, 0);
		timeText.text = to.runSpan.ToString ();
	}
}
