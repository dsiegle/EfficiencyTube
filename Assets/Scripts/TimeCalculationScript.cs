using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class Sessions {
	public TimeSpan session;
}

public class TimeCalculationScript : MonoBehaviour {

	public static TimeCalculationScript tcs;

	public DateTime startTime;			// When the app was started
	public DateTime currentTime;		// Latest time.
	public DateTime endTime;			// When our workday ends
	public DateTime endOverTime;		// Time to hit 100% in overtime
	public DateTime endTgtTime;			// Time when our Tgt marker will be hit

	public float secSinceStartup;
	public float secToEOD;				// # Seconds until the workday is over
	public float secToEOOT;				// Seconds until end of overtime

	public bool running = false;		// true when app is running
	public bool started = false;		// true when app started

	public float workLengthSec;
	public TimeSpan workSpan;			// length of workday in hours
	public TimeSpan runSpan;			// How long we've been running

	public float runSec;
	private Text startButtonText;

	public void StartStop()
	{
		if (running) { // Going from running to stop 
			Debug.Log ("TCS.StartStop() state stop");
			PlayerScript.playerScript.stopSession();
			startButtonText = GameObject.Find ("StartButtonText").GetComponent<Text> ();
			startButtonText.text = "Click here to continue work";

		} else {		// Going from stop to start
			Debug.Log ("TCS.StartStop() state running");
			PlayerScript.playerScript.startSession();
			startButtonText = GameObject.Find ("StartButtonText").GetComponent<Text> ();
			startButtonText.text = "Click here to take a break.";
		}
		running = !running;		// Toggle the running flag
	}

	//------------------------------------------------------------------------------------------
	public string toHMS(float timeSec)
	{
		int h = (int)(timeSec / 3600);
		int a = (int)(timeSec % 3600);
		int m = a / 60;
		int s = a % 60;
		return string.Format ("{0:d2}:{1:d2}:{2:d2}", h, m, s);
	}
	
	//------------------------------------------------------------------------------------------
	// Make sure we only have one of these.
	void Awake()
	{
		Debug.Log ("TCS.Awake() called.");
		if (tcs == null) 
		{
			Debug.Log ("Creating static TCS");
			DontDestroyOnLoad (gameObject);
			tcs = this;
		}
		else if (tcs != this)
		{
			Debug.Log ("Already have a TCS.  Destroying new one");
			Destroy(gameObject);
		}
		
	}

	//------------------------------------------------------------------------------------------
	// Gets called when the opening level "start work" button is pressed.
	public void InitializeAndRun(TimeSpan sliderTime)
	{
		Debug.Log ("TCS:InitializeAndRun() called");
		workSpan = sliderTime;
		startTime = System.DateTime.Now;
		currentTime = System.DateTime.Now;

		// The "Start Work!" button has been clicked and we are measuring.
		started = true;
		running = true;
		runSec = 0;
		runSpan = new TimeSpan (0, 0, 0);

		workLengthSec = (float)workSpan.TotalSeconds;	// constant after startup
		endTime = startTime + workSpan;
		endOverTime = endTime;

		secSinceStartup = 0;
		secToEOD = (int)(workSpan.TotalSeconds - secSinceStartup);
	}
	
	//------------------------------------------------------------------------------------------
	// This keeps all the time values for the app updated.
	void Update ()
	{
		//Debug.Log ("TCS.Update() called");
		if (!started) {
			return;
		}
		//Debug.Log ("TCS.Update() called");

		currentTime = System.DateTime.Now;
		if (running) {
			runSec = (runSec + Time.deltaTime);
			runSpan = new TimeSpan(0,0,(int)runSec);
		}
		secSinceStartup += Time.deltaTime;
		secToEOD = (int)(workSpan.TotalSeconds - secSinceStartup);
		secToEOOT = workLengthSec - runSec;
		//Debug.Log ("workLengthSec = " + workLengthSec);
		//Debug.Log ("secToEOOT = " + secToEOOT + "secToEOD = " + secToEOD);

		if (!running) {
			TimeSpan ts = new TimeSpan (0, 0, (int)secToEOOT);
			endOverTime = currentTime + ts;
		}
	}
}
