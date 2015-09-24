using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.IO;
using System.Collections.Generic;
//using SimpleJSON;
using Procurios.Public;


public class OpponentScript : MonoBehaviour {

	public static OpponentScript opponentScript;
	
	// These are the external GameObjects we update via this script.
	public Text op_name;
	public Text op_company;
	public Text op_position;
	public Text op_sessionTime;
	public Text op_quote;
	public Text op_numSes;
	public Text op_avgSes;
	public Text op_numBreaks; 	// Display the number of breaks
	public Text op_avgBreak;	// Display average time of all breaks
	public Text op_breakTime;	// Display length of current break
	public Text op_totalTime;
	public Image op_postItNote;
	public Text op_postItNoteText;
	private TimeCalculationScript to;

	Hashtable _op;			// Contains the opponent information from the JSON file.
	int _curIndex =  0;		// Current index into opponents work/break blocks
	int _numWorkBlocks = 0;	// Number of work blocks in the opponents day
	bool _working = true;	// Indicates if opponent is working or on break.

	DateTime _prevTime;		// When the currently running block started
	DateTime _blockEndTime;	// When the currently running block will end

	private Vector3 v = new Vector3 (0, -400, 0);	// Post it note position

//	class OpWorkTimeBlock {
//		public OpWorkTimeBlock(string t, TimeSpan ts, DateTime dt) {this.span = ts; this.dt = dt;}
//		public TimeSpan span;
//		public DateTime dt;
//	}

	// Our opponent uses one list for both work blocks and rest blocks
//	List<OpWorkTimeBlock> m_workBlocks = new List<OpWorkTimeBlock>();
//	List<OpBreakTimeBlock> m_breakBlocks = new List<OpWorkTimeBlock>();

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
		op_name = GameObject.Find ("OP_Name").GetComponent<Text>();
		op_position = GameObject.Find ("OP_Position").GetComponent<Text>();
		op_company = GameObject.Find ("OP_Company").GetComponent<Text>();
		op_sessionTime = GameObject.Find ("OP_SessionTime").GetComponent<Text>();
		op_quote = GameObject.Find ("OP_Quote").GetComponent<Text>();
		op_numSes = GameObject.Find ("OP_NumSes").GetComponent<Text>();
		op_avgSes = GameObject.Find ("OP_AvgSes").GetComponent<Text>();
		op_numBreaks = GameObject.Find ("OP_NumBreaks").GetComponent<Text>();
		op_avgBreak = GameObject.Find ("OP_AvgBreak").GetComponent<Text>();
		op_breakTime = GameObject.Find ("OP_BreakTime").GetComponent<Text>();
		op_postItNote = GameObject.Find ("OP_PostItNote").GetComponent<Image> ();
		op_postItNoteText = GameObject.Find ("OP_PostItNoteText").GetComponent<Text> ();
		op_totalTime = GameObject.Find ("OP_TotalTime").GetComponent<Text> ();

		string fname = ".\\Assets\\Data\\opponent2.json";

		StreamReader sr = new StreamReader (fname);
		string json = sr.ReadToEnd();
		_op = (Hashtable)JSON.JsonDecode(json);

		_numWorkBlocks = ((ArrayList)_op["workEvent"]).Count;
		_prevTime = to.currentTime;

		// Initialize the end time for the first work block.
		_blockEndTime = SetBlockEndTime ((ArrayList)_op["workEvent"], to.currentTime);

	}

	//------------------------------------------------------------
	void ShowPostIt(bool value) {
		if (value == true) {
			v.x = -150;
		} else {
			v.x = -650;
		}
		op_postItNote.transform.position = v; // Move post-it note into or out of view
	}

	//------------------------------------------------------------
	void UpdatePostItNote(ArrayList list, DateTime now)
	{
		Hashtable table = (Hashtable)(list[_curIndex]);
		string s = (string)table ["post_it_text"];

		TimeSpan bs = now - _prevTime;
		op_breakTime.text = string.Format ("Break Time: {0:d2}:{1:d2}:{2:d2}", bs.Hours, bs.Minutes, bs.Seconds);
		op_numBreaks.text = string.Format ("#Breaks: {0:d2}", _curIndex+1);
		op_postItNoteText.text = s;
		ShowPostIt(true);
	}
	
	//------------------------------------------------------------
	void UpdateBizCard(ArrayList list, DateTime now)
	{
		Hashtable table = (Hashtable)(list[_curIndex]);
		string s = (string)table ["post_it_text"];
		TimeSpan ws = now - _prevTime;

		op_sessionTime.text = string.Format ("Ses. Time: {0:d2}:{1:d2}:{2:d2}", ws.Hours, ws.Minutes, ws.Seconds);
		op_numSes.text = string.Format ("#Ses.: {0:d2}", _curIndex+1);
		op_quote.text = s;
		ShowPostIt(false);

//		UpdateSessionAverage();
//		UpdateQuote();
//		TriggerSpeechBubble();		
	}
	//------------------------------------------------------------
	void DisplayFinalMetrics()
	{
		ShowPostIt(true);
		op_breakTime.text = string.Format ("I'm out of here!");
	}

	//------------------------------------------------------------
	DateTime SetBlockEndTime(ArrayList list, DateTime now)
	{
		Hashtable table = (Hashtable)(list[_curIndex]);
		double secs = (double) table["length_in_secs"];
		TimeSpan ts = new TimeSpan(0,0,(int)secs);
		return to.currentTime + ts;
	}
	
	//------------------------------------------------------------
	// Update is called once per frame
	void Update () {

		DateTime now = to.currentTime;

		if (_curIndex < _numWorkBlocks) {
			if (_working) {
				UpdateBizCard ((ArrayList)_op ["workEvent"], now);
			} else {
				UpdatePostItNote ((ArrayList)_op ["breakEvent"], now);
			}

			if (now > _blockEndTime) {
				if (_working) {		// Switching to a "break"
					_prevTime = now;						// Initialize next start time
					_working = false;
					_blockEndTime = SetBlockEndTime ((ArrayList)_op ["breakEvent"], now);	// Set time when next block will end
				} else {		// Switching to "work"
					_curIndex += 1;		// Move to next work/break block ONLY when the break is done.
					_prevTime = now;
					_working = true;
					if (_curIndex < _numWorkBlocks)
						_blockEndTime = SetBlockEndTime ((ArrayList)_op ["workEvent"], now);
				}
			}
		} else {
			DisplayFinalMetrics ();	// When opponent is done for the day.	
		}
	}
}

