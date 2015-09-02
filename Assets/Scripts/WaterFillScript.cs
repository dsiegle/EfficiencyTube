using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class WaterFillScript : MonoBehaviour {
	
	private float guiMinY;
	private float guiMaxY;
	private float guiDelta;
	private float eodSec;

	private TimeCalculationScript to;
	private Text trEndDay;
	//private Text trMidday;
	
	// Use this for initialization
	void Start ()
	{
		//Debug.Log ("WaterFillScript.Start() called");
		to = TimeCalculationScript.tcs;
		trEndDay = GameObject.Find("TimeRunEOD").GetComponent<Text>();
		//trMidday = GameObject.Find("TimeRunMOD").GetComponent<Text>();

		guiMinY = -290;
		guiMaxY = 350;
		guiDelta = guiMaxY - guiMinY;
		transform.localPosition = new Vector3 (0, guiMinY, 0);

		// Position the time markers on the beaker based upon how long the day is.
		//float eod = guiMinY + (to.workDayLengthInSec / 36000 * guiDelta);
		float eod = guiMinY + guiDelta;
		Debug.Log ("eod = " + eod);
		trEndDay.transform.localPosition = new Vector3 (0, eod+60, 0);


		//float midday = guiMinY + (to.workDayLengthInSec / 72000 * guiDelta);
		//float midday = guiMinY + (guiDelta / 2);
		//Debug.Log ("midday = " + midday);
		//trMidday.transform.localPosition = new Vector3 (0, midday, 0);

	}
	
	// Update is called once per frame
	void Update ()
	{
		// Position the 'water' within the beaker
		//float pos = guiMinY + (to.runSec / 36000 * guiDelta);
		float pos = guiMinY + (to.runSec/to.workDayLengthInSec * guiDelta);
		//Debug.Log ("WaterFillScript.Update() to.runSec = " + to.runSec + " to.workDayLengthInSec = " + to.workDayLengthInSec);
		transform.localPosition = new Vector3(0, pos, 0);

		string e = to.endOverTime.ToString ("hh:mmtt\n");
		trEndDay.text = e + to.toHMS (to.secToEOOT);
		// Update time strings beside the beaker.
		//trEndDay.text = to.endOverTime.ToString("hh:mm tt");
		//e = to.midDay.ToString ("hh:mm:ss tt  ");
		//trMidday.text = e + to.toHMS (to.secToMidday);
		//trMidday.text = to.midDay.ToString ("hh:mm tt");
	}
}
