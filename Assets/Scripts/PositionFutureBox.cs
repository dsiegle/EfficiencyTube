using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

// This script is attached to the Efficiency Slider.
// It updates the 3 text objects that are children of the slider.
public class PositionFutureBox : MonoBehaviour {

	public Slider slider;

	private ComputeAverages ca;
	private TimeCalculationScript to;
	

	private Text tgtAvg;

	//private int prevEfficiency = 0;
	private bool playedUp = false;
	private bool playedDown = false;



	//------------------------------------------------------------------------------------------
	// Use this for initialization
	void Start ()
	{
		to = TimeCalculationScript.tcs;		// Get the global static tcs
		
		slider = GetComponent<Slider> ();

		tgtAvg = GameObject.Find ("TgtAvg").GetComponent<Text> ();

		ca = GameObject.Find ("AvgComputer").GetComponent<ComputeAverages> ();
		//Debug.Log ("ca.efficiency = " + ca.efficiency + " slider.value = " + slider.value);
	}

	//------------------------------------------------------------------------------------------
	public void SliderChanged()
	{
		// Reset the target audio triggers
		playedUp = false;
		playedDown = false;
		
	}
	// Update is called once per frame
	//------------------------------------------------------------------------------------------
	void Update ()
	{
		// Sound ring check for when you pass by the target slider
		if (to.running && !playedUp) // We know its headed upward and hasn't played
		{
			if (ca.efficiency > slider.value) 
			{
				GetComponent<AudioSource>().Play ();
				//Debug.Log ("efficiency = " + efficiency + " prevEfficiency = " + prevEfficiency);
				//prevEfficiency = (int)ca.efficiency;
				playedUp = true;
				playedDown = false;
			}
		} 
		else  // we know its headed downward because its not running
		{
			if ( !playedDown && ca.efficiency < slider.value)
			{
				GetComponent<AudioSource>().Play ();
				//Debug.Log ("efficiency = " + efficiency + " prevEfficiency = " + prevEfficiency);
				//prevEfficiency = (int)ca.efficiency;
				playedUp = false;
				playedDown = true;
			}
			
		}

		// Update what the average will be if we work until the target times current value.
		float nsessions = (float)(PlayerScript.playerScript.sessions.Count+1);
		float avgsec = (to.runSec + (float)to.workSpan.TotalSeconds) / nsessions;
		TimeSpan ts = new TimeSpan(0,0,(int)avgsec);
		tgtAvg.text = string.Format ("Avg: {0:d2}:{1:d2}:{2:d2}", ts.Hours, ts.Minutes, ts.Seconds);	


	}
}
