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
	//public DateTime midDay;				// Time until middle of the day
	//public float secToMidday;
	public float secSinceStartup;
	public float secToEOD;				// # Seconds until the workday is over
	public float secToEOOT;				// Seconds until end of overtime
	public float secToTgt;				// # Seconds until we hit our Tgt percentage

	public bool running = false;		// true when app is running
	public bool started = false;		// true when app started

	private TimeSpan workHoursSpan;		// TESTING TimeSpan object
	public float workHours;			// length of workday in hours
	public float workDayLengthInSec;
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
	public void InitializeAndRun(float sliderTime)
	{
		Debug.Log ("TCS:InitializeAndRun() called");
		workHours = sliderTime;
		startTime = System.DateTime.Now;

		// The "Start Work!" button has been clicked and we are measuring.
		started = true;
		running = true;
		runSec = 0;
		
		workDayLengthInSec = workHours * 3600;
		workHoursSpan = new TimeSpan (0, (int)workHours, 0, 0);
		endTime = startTime + workHoursSpan;
		endOverTime = endTime;

		//TimeSpan tgtSpan = new TimeSpan (0, (int)(workHours * efficiency), 0, 0);
		//endTgtTime = startTime + tgtSpan;
		
		secSinceStartup = 0;
		secToEOD = workDayLengthInSec - secSinceStartup;
	}
	
	//------------------------------------------------------------------------------------------
	// This keeps all the time values for the app updated.
	void Update ()
	{
		if (!started) {
			return;
		}
		//Debug.Log ("TCS.Update() called");

		currentTime = System.DateTime.Now;
		if (running) {
			runSec = (runSec + Time.deltaTime);
		}
		secSinceStartup += Time.deltaTime;
		//Debug.Log ("secSinceStartup = " + secSinceStartup);
		secToEOD = workDayLengthInSec - secSinceStartup;
		secToEOOT = workDayLengthInSec - runSec;

		if (!running) {
			TimeSpan ts = new TimeSpan (0, 0, 0, (int)(secToEOOT));
			endOverTime = currentTime + ts;
			//TimeSpan ts2 = new TimeSpan (0, 0, 0, (int)(secToMidday));
			//midDay = currentTime + (ts2);
		}
	}
}
