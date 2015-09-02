using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Collections.Generic;


public class OpponentScript : MonoBehaviour {

	public static OpponentScript opponentScript;
	
	// These are the external GameObjects we update via this script.
	public Text op_sessionTime;
	public Text op_numSes;
	public Text op_avgSes;
	public Text op_numBreaks; 	// Display the number of breaks
	public Text op_avgBreak;	// Display average time of all breaks
	public Text op_breakTime;	// Display length of current break
	public Text op_totalTime;
	public Image op_postItNote;
	private TimeCalculationScript to;

	int currentBlock = 0;		// The number of blocks our opponent has completed
	DateTime prevTime;	// When the currently running block started

	private Vector3 v = new Vector3 (0, -400, 0);	// Post it note position

	class OpTimeBlock {
		public OpTimeBlock(char t, TimeSpan ts, DateTime dt) { this.tag = t; this.span = ts; this.dt = dt; }
		public TimeSpan span;
		public char tag; // Either 'w' or 'b'
		public DateTime dt;
	}

	// Our opponent uses one list for both work blocks and rest blocks
	List<OpTimeBlock> blocks = new List<OpTimeBlock>();

	//------------------------------------------------------------
	void Awake() {
		//Debug.Log ("OS.Awake() called");
		if (opponentScript == null) {
			DontDestroyOnLoad (gameObject);
			opponentScript  = this;
		}
		else if (opponentScript != this) {
			Destroy(gameObject);
		}

	}

	//------------------------------------------------------------
	// Use this for initialization
	void Start () {
		Debug.Log ("OS.Start() called");
		to = TimeCalculationScript.tcs;	// Get our static time information
		
		// Get all of the GameObjects we will be updating
		op_sessionTime = GameObject.Find ("OP_SessionTime").GetComponent<Text>();
		op_numSes = GameObject.Find ("OP_NumSes").GetComponent<Text>();
		op_avgSes = GameObject.Find ("OP_AvgSes").GetComponent<Text>();
		op_numBreaks = GameObject.Find ("OP_NumBreaks").GetComponent<Text>();
		op_avgBreak = GameObject.Find ("OP_AvgBreak").GetComponent<Text>();
		op_breakTime = GameObject.Find ("OP_BreakTime").GetComponent<Text>();
		op_postItNote = GameObject.Find ("OP_PostItNote").GetComponent<Image> ();
		op_totalTime = GameObject.Find ("OP_TotalTime").GetComponent<Text> ();

		// Put a couple of works and breaks into our blocks
		//blocks.Add (new OpTimeBlock('w',new TimeSpan(0,0,0), new DateTime()));
		blocks.Add (new OpTimeBlock('w',new TimeSpan(0,10,0), new DateTime()));
		blocks.Add (new OpTimeBlock('b',new TimeSpan(0,5,0), new DateTime()));
		blocks.Add (new OpTimeBlock('w',new TimeSpan(0,15,0), new DateTime()));
		blocks.Add (new OpTimeBlock('b',new TimeSpan(0,5,0), new DateTime()));
		blocks.Add (new OpTimeBlock('w',new TimeSpan(0,25,0), new DateTime()));
		blocks.Add (new OpTimeBlock('b',new TimeSpan(0,10,0), new DateTime()));
		blocks.Add (new OpTimeBlock('w',new TimeSpan(0,30,0), new DateTime()));
		blocks.Add (new OpTimeBlock('b',new TimeSpan(0,10,0), new DateTime()));
		blocks.Add (new OpTimeBlock('w',new TimeSpan(0,25,0), new DateTime()));
		blocks.Add (new OpTimeBlock('b',new TimeSpan(0,10,0), new DateTime()));
		blocks.Add (new OpTimeBlock('w',new TimeSpan(0,40,0), new DateTime()));
		blocks.Add (new OpTimeBlock('b',new TimeSpan(0,15,0), new DateTime()));
		blocks.Add (new OpTimeBlock('w',new TimeSpan(0,10,0), new DateTime()));
		blocks.Add (new OpTimeBlock('b',new TimeSpan(0,5,0), new DateTime()));
		blocks.Add (new OpTimeBlock('w',new TimeSpan(0,35,0), new DateTime()));
		blocks.Add (new OpTimeBlock('b',new TimeSpan(0,15,0), new DateTime()));
		Debug.Log ("blocks.Count = " + blocks.Count);
		// Fill out our ending times based off of the actual start time.
		DateTime baseTime = to.startTime;
		foreach (OpTimeBlock b in blocks) {
			b.dt = baseTime + b.span;	// Compute actual DateTime from the span
			baseTime = b.dt;
			//Debug.Log("b.dt = " + b.dt + "baseTime = " + baseTime + "b.span = " + b.span);
		}
		prevTime = to.currentTime;  // Initialize for the first time Update() is called.
		Debug.Log ("prevTime = " + prevTime);
	}

	//------------------------------------------------------------
	void ShowPostIt(bool value) {
		if (value == true) {
			v.x = -150;
		} else {
			v.x = -650;
		}
		op_postItNote.transform.position = v; // Move post it note into or out of view
	}

	//------------------------------------------------------------
	// Update is called once per frame
	void Update () {

		// This is where we will keep track of the opponents time
		// Our opponent is still work/breaking

		if (currentBlock < blocks.Count) {
			OpTimeBlock ob = blocks[currentBlock];

			if (ob.tag == 'w') {
				//Debug.Log(" work cb = " + currentBlock);
				TimeSpan ws = to.currentTime - prevTime;
				op_sessionTime.text = string.Format ("Ses. Time: {0:d2}:{1:d2}:{2:d2}", ws.Hours, ws.Minutes, ws.Seconds);
				ShowPostIt(false);

			}
			else if (ob.tag == 'b') {
				//Debug.Log(" break cb = " + currentBlock);
				TimeSpan bs = to.currentTime - prevTime;
				op_breakTime.text = string.Format ("Break Time: {0:d2}:{1:d2}:{2:d2}", bs.Hours, bs.Minutes, bs.Seconds);
				ShowPostIt(true);

			}
			if (to.currentTime > ob.dt) {
				currentBlock += 1;	// Move to next block
				prevTime = to.currentTime;
			}

		} else {  	// Opponent is done for the day
			ShowPostIt(true);
			op_breakTime.text = string.Format ("I'm out of here!");
		}
	}
}

