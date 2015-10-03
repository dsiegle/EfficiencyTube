using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using Procurios.Public;

// Attached to the "OPTimeText" GameObject.
// Updates both the position and text content to show total amount your opponent has worked so far.
public class OPTimeTextScript : MonoBehaviour {
	
	private OpponentScript os;
	private float guiMinY;
	private float guiMaxY;
	private float guiDelta;
	private Text opTimeText;
	private TimeCalculationScript to;

	void Start ()
	{
		//Debug.Log ("OPTotalTimeScript.Start() called");
		to = TimeCalculationScript.tcs;
		os = OpponentScript.opponentScript;
		opTimeText = GetComponent<Text>();

		guiMinY = -283;
		guiMaxY = 355;
		guiDelta = guiMaxY - guiMinY;
		transform.localPosition = new Vector3 (51, guiMinY, 0);
		
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
		//Debug.Log ("pos = " + pos);
		//Debug.Log ("pos= " + pos +  " os.osGetTimeWorkedSoFar = " + os.GetTimeWorkedSoFar + " to.workSpan.TotalSeconds = " + to.workSpan.TotalSeconds);
		transform.localPosition = new Vector3(51, pos, 0);

		TimeSpan ts = new TimeSpan(0,0,(int)os.GetTimeWorkedSoFar());
		opTimeText.text = string.Format ("{0:d2}:{1:d2}:{2:d2}", ts.Hours, ts.Minutes, ts.Seconds);

	}
}

