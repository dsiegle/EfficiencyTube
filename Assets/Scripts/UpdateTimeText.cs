using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

/* This script updates 3 values:
 * 1) The TimeText object in hh:mm:ss showing how long you've been actively working.
 * 2) The TgtFinishTime object giving time of day (hh:mmAM) when you will hit target percentage.
 * 3) The TgtWorkRemaining object in hh:mm:ss until you hit target percentage.
*/
public class UpdateTimeText : MonoBehaviour {

	private Text timeText;
	private Text tgtFinishTime;
	private Text tgtWorkRemaining;
	private Slider targetEfficiencySlider;
	private Text tgtAvg;

	private TimeCalculationScript to;	// external 'singleton' with all our time calculations

	// Use this for initialization
	void Start ()
	{
		//Debug.Log ("UpdateTimeText.Start() called");
		// Get handles to all the GameObjects we need
		targetEfficiencySlider = GameObject.Find ("TargetEfficiencySlider").GetComponent<Slider> ();
		timeText = GameObject.Find ("TimeText").GetComponent<Text>();
		tgtFinishTime = GameObject.Find ("TgtFinishTime").GetComponent<Text> ();
		tgtWorkRemaining = GameObject.Find ("TgtWorkRemaining").GetComponent<Text> ();
		tgtAvg = GameObject.Find ("TgtAvg").GetComponent<Text> ();

		to = TimeCalculationScript.tcs;
	}


	// Update is called once per frame
	void Update () {
		//Debug.Log ("UpdateTimeText.Update() called");

		timeText.text = to.toHMS(to.runSec);

		float workRemainingInSecs = (to.workDayLengthInSec * targetEfficiencySlider.value / 100)  - to.runSec;
		tgtWorkRemaining.text = to.toHMS (workRemainingInSecs); 

		TimeSpan ts = new TimeSpan(0,0,0,(int)(workRemainingInSecs));
		DateTime tgt = to.currentTime + ts;
		tgtFinishTime.text = tgt.ToString ("hh:mmtt");

		// Update what the average will be if we work until the target times current value.
		float nsessions = (float)(PlayerScript.playerScript.sessions.Count+1);
		float avgsec = (to.runSec + workRemainingInSecs) / nsessions;
		ts = new TimeSpan(0,0,0,(int)avgsec);
		tgtAvg.text = string.Format ("Avg: {0:d2}:{1:d2}:{2:d2}", ts.Hours, ts.Minutes, ts.Seconds);

	}
}
