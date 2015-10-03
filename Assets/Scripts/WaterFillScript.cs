using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class WaterFillScript : MonoBehaviour {
	
//	private float guiMinY;
//	private float guiMaxY;
//	private float guiDelta;

	private TimeCalculationScript to;
	private Text trEndDay;
	//private Text trMidday;
	
	// Use this for initialization
	void Start ()
	{
		//Debug.Log ("WaterFillScript.Start() called");
//		to = TimeCalculationScript.tcs;
//		trEndDay = GameObject.Find("TimeRunEOD").GetComponent<Text>();

//		guiMinY = -290;
//		guiMaxY = 350;
//		guiDelta = guiMaxY - guiMinY;
//		transform.localPosition = new Vector3 (0, guiMinY, 0);

		// Position the time markers on the beaker based upon how long the day is.
		//float eod = guiMinY + guiDelta;
		//Debug.Log ("eod = " + eod);
		//trEndDay.transform.localPosition = new Vector3 (0, eod+60, 0);
	}
	
	// Update is called once per frame
	void Update ()
	{
		// Position the 'water' within the beaker
		//float pos = guiMinY + (to.runSec/to.workSpan.TotalSeconds * guiDelta);
		//Debug.Log ("WaterFillScript.Update() to.runSec = " + to.runSec + " to.workSpan.TotalSeconds = " + to.workSpan.TotalSeconds);
		//transform.localPosition = new Vector3(0, pos, 0);

//		string e = to.endOverTime.ToString ("hh:mmtt\n");
//		trEndDay.text = e + to.toHMS (to.secToEOOT);
	}
}
