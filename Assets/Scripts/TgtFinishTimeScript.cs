using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

// Attached to the "TgtFinishTime" GameObject.
// This displays the hh:mmAM string to the left of the Target slider

public class TgtFinishTimeScript : MonoBehaviour {

	private Text tgtFinishTime;
	private TimeCalculationScript to;
	private Slider slider;

	// Use this for initialization
	void Start () {
		to = TimeCalculationScript.tcs;		// Get the global static tcs
		slider = GameObject.Find ("TargetEfficiencySlider").GetComponent<Slider> ();
		tgtFinishTime = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	// TODO: This thing should update with a single call to the Times object.
	void Update () {
		// tgtFinishTime.text = times.FinishTimeAsString();
		float secFromStartOfDayToSlider = to.workLengthSec * slider.value / 100.0f;
		float secsToTgt = secFromStartOfDayToSlider - to.runSec;
		TimeSpan ts = new TimeSpan(0,0,(int)(secsToTgt));
		DateTime tgt = to.currentTime + ts;
		tgtFinishTime.text = tgt.ToString ("hh:mmtt\n");
	}
}
