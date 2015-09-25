using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Collections.Generic;

/* This class tracks 'sessions' and 'breaks', which are units of work stored as a list of TimeSpan objects
 */
public class PlayerScript : MonoBehaviour {

	public static PlayerScript playerScript;
	private PostItScript pis;
	//public Image postItNote;

	// These are the external GameObjects we update via this script.
	public Text sessionTime;
	public Text numSes;
	public Text avgSes;
	public Text numBreaks; 	// Display the number of breaks
	public Text avgBreak;	// Display average time of all breaks
	public Text breakTime;	// Display length of current break
	public Text totalTime;

	int numberOfBreaks = 0;
	
	// Our lists of sessions and breaks (which are simply TimeSpans)
	public List<TimeSpan> sessions = new List<TimeSpan>();
	public List<TimeSpan> breaks = new List<TimeSpan>();

	// TODO: I think these can be combined because we aren't using them at the same time
	private TimeSpan sessionTS;		// Current length of the session
	private TimeSpan breakTS;		// Current length of the break;
	private TimeSpan totalBreakTime; // Combined time for all breaks in the list + current one

	// TODO: I think these can be combined because we aren't using them at the same time
	public DateTime startDT;		// Time the current session started.
	public DateTime startBreakDT;	// Time the current break started.

	private TimeCalculationScript to;	// static global object that tracks all our time.
	//------------------------------------------------------------
	void Awake() {
		if (playerScript == null) {
			DontDestroyOnLoad (gameObject);
			playerScript = this;
		}
		else if (playerScript != this) {
			Destroy(gameObject);
		}
	}

	//------------------------------------------------------------
	void Start() {
		to = TimeCalculationScript.tcs;	// Get our static time information
		pis = PostItScript.pis;	// Get our static PostItScript object

		// Get all of the GameObjects we will be updating
		sessionTime = GameObject.Find ("SessionTime").GetComponent<Text>();
		numSes = GameObject.Find ("NumSes").GetComponent<Text>();
		avgSes = GameObject.Find ("AvgSes").GetComponent<Text>();
		numBreaks = GameObject.Find ("NumBreaks").GetComponent<Text>();
		avgBreak = GameObject.Find ("AvgBreak").GetComponent<Text>();
		breakTime = GameObject.Find ("BreakTime").GetComponent<Text>();
		totalTime = GameObject.Find ("TotalTime").GetComponent<Text> ();

		// Initialize this immediately because Update() updates the sessionTime string and requires this as input.
		startDT = System.DateTime.Now;

		// Initialize this since startSession() isn't called until the first time the user clicks for a break.
		numSes.text = string.Format ("Session #: {0:d2}", sessions.Count+1);


	}

	//------------------------------------------------------------
	// This only gets called *after* the user clicks to take a break.
	public void startSession() {
		startDT = System.DateTime.Now;
		//Debug.Log ("PS.Starting session #" + sessions.Count + " @ " + startDT);

		// Since we start the day working, we count the current session among the number of sessions.
		numSes.text = string.Format ("Session #: {0:d2}", sessions.Count+1);

		// If we get here, we have at least one break.
		if (numberOfBreaks > 0) {
			breaks.Add (breakTS);			// Add the break that just ended
			totalBreakTime += breakTS;
		}

		pis.ShowPostIt (false);
	}

	//------------------------------------------------------------
	public void stopSession() {
		//Debug.Log ("PS.stopSession() called");

		// We only add the session to our list when it stops.
		sessions.Add (sessionTS);	// Add this session to our list

		// Bump the number of breaks
		numberOfBreaks += 1;
		numBreaks.text = string.Format ("Break #: {0:d2}", numberOfBreaks);

		// Set the start of break time.
		startBreakDT = System.DateTime.Now;

		pis.ShowPostIt (true);
	}
	
	//------------------------------------------------------------
	void Update() {
		DateTime now = System.DateTime.Now;
		if (to.running) {	// The app is running
			sessionTS = now - startDT;
			//Debug.Log ("PS.Update(): startDt=" + startDT + "sessionTS=" + sessionTS);
			//totalTime.text = to.toHMS (to.runSec);

			// Update the time for this session.
			sessionTime.text = string.Format ("Ses. Time: {0:d2}:{1:d2}:{2:d2}", sessionTS.Hours, sessionTS.Minutes, sessionTS.Seconds);

			// We update the session averages every frame.
			float avg = to.runSec / (sessions.Count + 1);
			TimeSpan ts = new TimeSpan (0, 0, (int)(avg));
			avgSes.text = string.Format ("Avg/Ses.: {0:d2}:{1:d2}:{2:d2}", ts.Hours, ts.Minutes, ts.Seconds);

		} else {	// The app is NOT running
			// In here is where we dynamically update the break information.
			// We will be updating the avgBreak GameObject value.
			breakTS = now - startBreakDT;
			breakTime.text = string.Format ("Break Time: {0:d2}:{1:d2}:{2:d2}", breakTS.Hours, breakTS.Minutes, breakTS.Seconds);

			// Total time for all breaks here.
			TimeSpan ts2 = totalBreakTime + breakTS;	 	// Add the length of current break
			totalTime.text = string.Format ("Total: {0:d2}:{1:d2}:{2:d2}", ts2.Hours, ts2.Minutes, ts2.Seconds);

			int avgSecEachBreak = (int)(ts2.TotalSeconds / (breaks.Count+1));
			//Debug.Log ("PS.Update(): ts2=" + ts2 + " avgSecEachBreak=" + avgSecEachBreak);
			TimeSpan ts = new TimeSpan(0,0,avgSecEachBreak);
			avgBreak.text = string.Format ("Avg/Break: {0:d2}:{1:d2}", ts.Minutes, ts.Seconds);

		}
	}
}
