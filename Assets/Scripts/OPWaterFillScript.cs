using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class OPWaterFillScript : MonoBehaviour {
	
	private OpponentScript os;
	private float guiMinY;
	private float guiMaxY;
	private float guiDelta;

	private TimeCalculationScript to;
	private Text trEndDay;

	void Start ()
	{
		//Debug.Log ("OPWaterFillScript.Start() called");
		to = TimeCalculationScript.tcs;
		os = OpponentScript.opponentScript;

		guiMinY = -290;
		guiMaxY = 350;
		guiDelta = guiMaxY - guiMinY;
		transform.localPosition = new Vector3 (0, guiMinY, 0);
		
		// Position the time markers on the beaker based upon how long the day is.
		//float eod = guiMinY + (to.workSpan.TotalSeconds / 36000 * guiDelta);
		//float eod = guiMinY + guiDelta;
		//Debug.Log ("eod = " + eod);
	}
	
	// Update is called once per frame
	void Update ()
	{
		// Position the 'water' within the beaker
		float pos = guiMinY + (os.GetTimeWorkedSoFar()/(float)to.workSpan.TotalSeconds * guiDelta);
		//Debug.Log ("OPWaterFillScript.Update() to.runSec = " + to.runSec + " to.workSpan.TotalSeconds = " + to.workSpan.TotalSeconds);
		transform.localPosition = new Vector3(0, pos, 0);
		
	}
}

