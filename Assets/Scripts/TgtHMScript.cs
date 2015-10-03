using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

// Attached to the TgtHMS" GameObject.
// This displays the hh:mm:ss to the left of the efficiency target slider

public class TgtHMScript : MonoBehaviour {

	private Text tgtHMS;
	private TimeCalculationScript to;
	private Slider slider;

	// Use this for initialization
	void Start () {
		to = TimeCalculationScript.tcs;		// Get the global static tcs
		slider = GameObject.Find ("TargetEfficiencySlider").GetComponent<Slider> ();
		tgtHMS = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		float secFromStartOfDayToSlider = to.workLengthSec * slider.value / 100.0f;
		float secsToTgt = secFromStartOfDayToSlider - to.runSec;
		tgtHMS.text = to.toHMS(secsToTgt);
	}
}
