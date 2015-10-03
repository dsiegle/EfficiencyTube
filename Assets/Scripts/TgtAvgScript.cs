//using UnityEngine;
//using UnityEngine.UI;
//using System.Collections;
//using System;

// Attached to the TgtAvg" GameObject.
// This displays the Avg: hh:mm to the left of the efficiency target slider,
// which is the Avg/ses time you will have if you keep working to the target.

// CURRENTLY DISABLED (AND NOT DEBUGGED!)
//public class TgtAvgScript : MonoBehaviour {
//	
//	private Text tgtAvg;
//	private TimeCalculationScript to;
//	private Slider slider;
//
//	// Use this for initialization
//	void Start () {
//		to = TimeCalculationScript.tcs;		// Get the global static tcs
//		slider = GameObject.Find ("TargetEfficiencySlider").GetComponent<Slider> ();
//		tgtAvg = GetComponent<Text> ();
//	}
//	
//	// Update is called once per frame
//	void Update () {
//		float secFromStartOfDayToSlider = to.workLengthSec * slider.value / 100.0f;
//		float secsToTgt = secFromStartOfDayToSlider - to.runSec;
//		float avg = secsToTgt / (sessions.Count + 1);
//		TimeSpan ts = new TimeSpan (0, 0, (int)(avg));
//		tgtAvg.text = string.Format ("Avg/Ses.: {0:d2}:{1:d2}:{2:d2}", ts.Hours, ts.Minutes, ts.Seconds);
//	}
//}
