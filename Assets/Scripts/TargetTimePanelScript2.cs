using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

// Attached to "TargetTimePanel" GameObject on the startScreen level.

public class TargetTimePanelScript2 : MonoBehaviour {

	private Text t;
	public Slider s;
    private const int sliderUnitInMinutes = 1;
	//------------------------------------------------------------------------------------------
	// Gets called when the user selects how long they will work.  Starts timer as a side effect.
	public void SetWorkLength()
	{
		Debug.Log ("TargetTimePanelScript2.SetWorkLength() called");
        TimeSpan ts = new TimeSpan(0, (int)(s.value * sliderUnitInMinutes), 0);
        TimeCalculationScript.tcs.InitializeAndRun (ts);

		SceneManager.LoadScene (1);
	}
	
	//------------------------------------------------------------------------------------------
	// Use this for initialization
	void Start () 
	{
		s = GameObject.Find ("TargetTimeSlider").GetComponent<Slider> ();
		t = GameObject.Find ("TargetTimeText").GetComponent<Text> ();

        // Each unit on the slider is 15 minutes.
        TimeSpan ts = new TimeSpan(0, (int)(s.value * sliderUnitInMinutes), 0);
        t.text = string.Format ("{0:d2}:{1:d2}", ts.Hours, ts.Minutes);
	}
	
	//------------------------------------------------------------------------------------------
	// Update is called once per frame
	void Update () 
	{
        TimeSpan ts = new TimeSpan(0, (int)(s.value * sliderUnitInMinutes), 0);
        t.text = string.Format ("{0:d2}:{1:d2}", ts.Hours, ts.Minutes);
	}
}
