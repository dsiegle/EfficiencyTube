using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class PositionTimeTextScript : MonoBehaviour {
	
	private float guiMinY;
	private float guiMaxY;
	private float guiDelta;
	private TimeCalculationScript to;
	
	void Start ()
	{
		//Debug.Log ("OPTotalTimeScript.Start() called");
		to = TimeCalculationScript.tcs;

		guiMinY = -283;
		guiMaxY = 355;
		guiDelta = guiMaxY - guiMinY;
		transform.localPosition = new Vector3 (-37, guiMinY, 0);
		
		// Position the time markers on the beaker based upon how long the day is.
		//float eod = guiMinY + (to.workSpan.TotalSeconds / 36000 * guiDelta);
		//float eod = guiMinY + guiDelta;
		//Debug.Log ("eod = " + eod);
	}
	
	// Update is called once per frame
	void Update ()
	{
		// Position the 'water' within the beaker
		float pos = guiMinY + (to.runSec/(float)to.workSpan.TotalSeconds * guiDelta);
		//Debug.Log ("OPTotalTimeScript.Update() to.runSec = " + to.runSec + " to.workSpan.TotalSeconds = " + to.workSpan.TotalSeconds);
		transform.localPosition = new Vector3(-37, pos, 0);
		
	}
}