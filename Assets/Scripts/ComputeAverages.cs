using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

// Attached to the "AvgComputer" GameObject, which is the parent of the 3 efficiency slider items
// (AvgText, MinText, and MaxText).
// This script only fills in the text values for these 3 boxes.
public class ComputeAverages : MonoBehaviour {
	
	public Text avgText;
	public Text minText;
	public Text maxText;
	public Text targetPercentText;
	public Text sliderText;

	private DateTime sliderTime;

	public float efficiency;
	public float maxEfficiency;
	public float minEfficiency;

	public float sliderFloatSec;		// Time until reaching the target efficiency slider (seconds)

	private TimeCalculationScript to;
	private PositionFutureBox fb;

	//------------------------------------------------------------------------------------------
	void Start ()
	{
		// Get handles to all the text averages we are updating.
		avgText = GameObject.Find ("AvgText").GetComponent<Text>();
		minText = GameObject.Find ("MinText").GetComponent<Text>();
		maxText = GameObject.Find ("MaxText").GetComponent<Text>();

		efficiency = 0.0f;
		maxEfficiency = 0.0f;
		minEfficiency = 0.0f;

		to = TimeCalculationScript.tcs;
	}
	
	//------------------------------------------------------------------------------------------
	void Update ()
	{
		float rt = to.runSec;
		float sss = to.secSinceStartup;
		float seod = to.secToEOD;
		float seoot = to.secToEOOT;

		//Debug.Log ("rt = " + rt + " sss = " + sss + " seod = " + seod + " seoot = " + seoot);
		if (!to.started) return;

		if (seod >= 0) {	// In the normal part of the day
			efficiency = (rt) / sss * 100;
			maxEfficiency = ((rt + seod) / to.workLengthSec) * 100;
			minEfficiency = (rt / to.workLengthSec) * 100;
		} 
		else // Past the end of the normal day, i.e. into overtime.
		{	
			// TODO: In here is where you can display the final efficiency score
			efficiency = (rt) / (rt + seoot) * 100;
			//Debug.Log ("OVERTIME: efficiency = " + efficiency);
			// No longer update the maxEfficiency value.
			// Also no longer update the minEfficiency value and get rid of the text value.
			//minText.color = new Color(0,0,0,0);
			//to.audio.Play ();
		}
		if (efficiency > 100) 	// You've hit 100% efficiency and it's basially over.
		{
			efficiency = 100;
			to.started = false;
			to.running = false;
			//to.audio.Play ();
			//to.audio.Play ();
			//to.audio.Play ();
		}

		avgText.text = string.Format ("{0:f3}", efficiency);
		minText.text = string.Format ("{0:f3}", minEfficiency);
		maxText.text = string.Format ("{0:f3}", maxEfficiency);

		if (to.running) 
		{
			avgText.color = new Color(0,1,0,1);
		} else
		{
			avgText.color = new Color(1,0,0,1);
		}


	}
}
