using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class TargetTimePanelScript2 : MonoBehaviour {

	public Text t;
	public Slider s;

	//------------------------------------------------------------------------------------------
	// Gets called when the user selects how long they will work.  Starts timer as a side effect.
	public void SetWorkHours()
	{
		Debug.Log ("TargetTimePanelScript2.SetWorkHours() called");
		//to.workHours = s.value / 60.0f;
		TimeSpan ts = new TimeSpan(0,(int)(s.value*15),0);
		TimeCalculationScript.tcs.InitializeAndRun (ts);

		Application.LoadLevel (1);

	}
	
	//------------------------------------------------------------------------------------------
	// Use this for initialization
	void Start () 
	{
		s = GameObject.Find ("TargetTimeSlider").GetComponent<Slider> ();
		t = GameObject.Find ("TargetTimeText").GetComponent<Text> ();

		// Each unit on the slider is 15 minutes.
		TimeSpan ts = new TimeSpan (0, (int)(s.value * 15), 0);
		t.text = ts.ToString ();

		// return string.Format ("{0:d2}:{1:d2}:{2:d2}", h, m, s);
		//t.text = TimeCalculationScript.tcs.toHMS (s.value);
		//t.text = to.toHMS (s.value * 60 * 15);
		//Debug.Log ("TTP2:Start() called");
	}
	
	//------------------------------------------------------------------------------------------
	// Update is called once per frame
	void Update () 
	{
		//Debug.Log ("TTP2:Update() called");

		//t.text = to.toHMS (s.value);
		TimeSpan ts = new TimeSpan (0, (int)(s.value * 15), 0);
		t.text = ts.ToString ();

		//t.text = TimeCalculationScript.tcs.toHMS (s.value * 60 * 15);
	}
}
