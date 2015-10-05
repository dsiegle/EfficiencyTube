using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.IO;
using System.Collections.Generic;
//using SimpleJSON;
using Procurios.Public;


// Attached to the OPBizCard GameObject
// This script is also responsible for updating the OP_PostItNote contents.

public class OpponentScript : MonoBehaviour {

	public static OpponentScript opponentScript;
	private PostItScript pis;

	// These are the external GameObjects we update via this script.
	private Text op_name;
	private Text op_company;
	private Text op_position;
	public Text op_sessionTime;
	public Text op_quote;
	public Text op_numSes;
	public Text op_avgSes;
	public Text op_numBreaks; 	// Display the number of breaks
	public Text op_backIn;		// Time until opponent returns
	public Text op_breakTime;	// Display length of current break
	public Text op_postItTotal;
	public Text op_bizTotal;
	public Image op_postItNote;
	public Text op_postItNoteText;
	private TimeCalculationScript to;
	private Image bizCardBackground;

	Hashtable _op;			// Contains the opponent information from the JSON file.
	int _curIndex =  0;		// Current index into opponents work/break blocks
	int _numWorkBlocks = 0;	// Number of work blocks in the opponents day
	private bool _working = true;	// Indicates if opponent is working or on break.
	bool _doneForDay = false;

	TimeSpan _workTotal;	// Total time worked
	TimeSpan _prevWorkTotal;

	TimeSpan _breakTotal;	// Time on break for this block
	TimeSpan _prevBreakTotal;

	DateTime _prevTime;		// When the currently running block started
	DateTime _blockEndTime;	// When the currently running block will end
	
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
		pis = PostItScript.pis;	// Get our static PostItScript object

		// Get all of the GameObjects we will be updating
		op_name = GameObject.Find ("OP_Name").GetComponent<Text>();
		op_position = GameObject.Find ("OP_Position").GetComponent<Text>();
		op_company = GameObject.Find ("OP_Company").GetComponent<Text>();
		op_sessionTime = GameObject.Find ("OP_SessionTime").GetComponent<Text>();
		op_quote = GameObject.Find ("OP_Quote").GetComponent<Text>();
		op_numSes = GameObject.Find ("OP_NumSes").GetComponent<Text>();
		op_avgSes = GameObject.Find ("OP_AvgSes").GetComponent<Text>();
		op_numBreaks = GameObject.Find ("OP_NumBreaks").GetComponent<Text>();
		op_backIn = GameObject.Find ("OP_BackIn").GetComponent<Text>();
		op_breakTime = GameObject.Find ("OP_BreakTime").GetComponent<Text>();
		op_postItNote = GameObject.Find ("OP_PostItNote").GetComponent<Image> ();
		op_postItNoteText = GameObject.Find ("OP_PostItNoteText").GetComponent<Text> ();
		op_postItTotal = GameObject.Find ("OP_PostItTotal").GetComponent<Text> ();

		string fname = ".\\Assets\\Data\\opponent3.json";

		StreamReader sr = new StreamReader (fname);
		string json = sr.ReadToEnd();
		Debug.Log ("json = " + json);
		_op = (Hashtable)JSON.JsonDecode(json);

		_numWorkBlocks = ((ArrayList)_op["workEvent"]).Count;
		_prevTime = to.currentTime;
		_workTotal = new TimeSpan (0, 0, 0);
		_prevWorkTotal = new TimeSpan (0, 0, 0);
		_breakTotal = new TimeSpan (0, 0, 0);
		_prevBreakTotal = new TimeSpan (0, 0, 0);

		// Initialize the end time for the first work block.
		_blockEndTime = SetBlockEndTime ((ArrayList)_op["workEvent"], to.currentTime);

	}

	//------------------------------------------------------------
	public float GetTimeWorkedSoFar()
	{
		return (float)_workTotal.TotalSeconds;
	}

	//------------------------------------------------------------
	void UpdatePostItNote(ArrayList list, DateTime now)
	{
		Hashtable table = (Hashtable)(list[_curIndex]);
		string s = (string)table ["post_it_text"];

		TimeSpan bs = now - _prevTime;
		_breakTotal = (now - _prevTime) + _prevBreakTotal;	// current block + previous blocks

		TimeSpan break_over = _blockEndTime - now;
		op_breakTime.text = string.Format ("Break Time: {0:d2}:{1:d2}", bs.Minutes, bs.Seconds);
		op_numBreaks.text = string.Format ("#Breaks: {0:d2}", _curIndex+1);
		op_backIn.text = string.Format ("Back in: {0:d2}:{1:d2}", break_over.Minutes, break_over.Seconds);
		op_postItTotal.text = string.Format ("Total: {0:d2}:{1:d2}:{2:d2}", _breakTotal.Hours, _breakTotal.Minutes, _breakTotal.Seconds);
		op_postItNoteText.text = s;
	}

	//------------------------------------------------------------
	void UpdateBizCard(ArrayList list, DateTime now)
	{
		Hashtable table = (Hashtable)(list[_curIndex]);
		string s = (string)table ["post_it_text"];

		TimeSpan ws = now - _prevTime;
		_workTotal = (now - _prevTime) + _prevWorkTotal;	// current block + previous blocks

		double avgSes = _workTotal.TotalSeconds / (_curIndex+1);
		TimeSpan ts = new TimeSpan (0, 0, (int)avgSes);

		op_sessionTime.text = string.Format ("Ses. Time: {0:d2}:{1:d2}:{2:d2}", ws.Hours, ws.Minutes, ws.Seconds);
		op_numSes.text = string.Format ("#Ses.: {0:d2}", _curIndex+1);
		op_avgSes.text = string.Format ("Avg./Ses: {0:d2}:{1:d2}:{2:d2}", ts.Hours, ts.Minutes, ts.Seconds);
		op_quote.text = s;
	}

//	//------------------------------------------------------------
//	void UpdateSpeechBubbleText(ArrayList list) {
//		string s = (string)((Hashtable)(list[_curIndex])) ["post_it_text"];
//		return s;
//	}

	public bool IsWorking() {
		return _working;
	}

	//------------------------------------------------------------
	void DisplayFinalMetrics()
	{
		pis.ShowOpponentPostIt(true);
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

		if (_doneForDay) return;	// Skip the whole dang thing.	

		DateTime now = to.currentTime;

		if (_curIndex < _numWorkBlocks) {
			if (_working) {
				UpdateBizCard ((ArrayList)_op ["workEvent"], now);
				TimeSpan ts = _blockEndTime - now;
				op_breakTime.text = string.Format ("Next Break: {0:d2}:{1:d2}:{2:d2}", ts.Hours,ts.Minutes,ts.Seconds);;
			} else {
				UpdatePostItNote ((ArrayList)_op ["breakEvent"], now);
			}

			if (now > _blockEndTime) {
				if (_working) {		// Switching to a "break"
					_prevTime = now;						// Initialize next start time
					_prevWorkTotal = _workTotal;
					_working = false;
					_blockEndTime = SetBlockEndTime ((ArrayList)_op ["breakEvent"], now);	// Set time when next block will end
					//UpdateSpeechBubbleText((ArrayList)_op ["breakEvent"]);
					pis.ShowOpponentPostIt(true);
				} else {		// Switching to "work"
					_curIndex += 1;		// Move to next work/break block ONLY when the break is done.
					_prevTime = now;
					_prevBreakTotal = _breakTotal;
					_working = true;
					if (_curIndex < _numWorkBlocks) {
						_blockEndTime = SetBlockEndTime ((ArrayList)_op ["workEvent"], now);
						//UpdateSpeechBubbleText((ArrayList)_op ["workEvent"]);
					}
					pis.ShowOpponentPostIt(false);
				}
			}
		} else {
			DisplayFinalMetrics ();	// When opponent is done for the day.
			_working = false;
			_doneForDay = true;
		}
	}
}

