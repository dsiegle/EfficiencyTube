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
	public Text op_backIn;		// Time until opponent returns
	public Text op_breakTime;	// Display length of current break
	public Text op_postItTotal;
	public Text op_bizTotal;
	public Image op_postItNote;
	public Text op_postItNoteText;
	private TimeCalculationScript to;

	Hashtable _op;			// Contains the opponent information from the JSON file.
	int _curIndex =  0;		// Current index into opponents work/break blocks
	int _numWorkBlocks = 0;	// Number of work blocks in the opponents day
	bool _working = true;	// Indicates if opponent is working or on break.

	TimeSpan _workTotal;	// Total time worked
	TimeSpan _prevWorkTotal;

	TimeSpan _breakTotal;	// Time on break for this block
	TimeSpan _prevBreakTotal;

	DateTime _prevTime;		// When the currently running block started
	DateTime _blockEndTime;	// When the currently running block will end

	private Vector3 v = new Vector3 (-180, -375, 0);	// Post it note position
	
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
		op_backIn = GameObject.Find ("OP_BackIn").GetComponent<Text>();
		op_breakTime = GameObject.Find ("OP_BreakTime").GetComponent<Text>();
		op_postItNote = GameObject.Find ("OP_PostItNote").GetComponent<Image> ();
		op_postItNoteText = GameObject.Find ("OP_PostItNoteText").GetComponent<Text> ();
		op_postItTotal = GameObject.Find ("OP_PostItTotal").GetComponent<Text> ();
		op_bizTotal = GameObject.Find ("OP_BizTotal").GetComponent<Text> ();

		string fname = ".\\Assets\\Data\\opponent2.json";

		StreamReader sr = new StreamReader (fname);
		string json = sr.ReadToEnd();
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
	void ShowPostIt(bool value) {
		if (value == true) {
			v.y = -170;
		} else {
			v.y = -375;
		}
		op_postItNote.transform.position = v; // Move post-it note into or out of view
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
		ShowPostIt(true);
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
		op_bizTotal.text = string.Format ("Total Time: {0:d2}:{1:d2}:{2:d2}", _workTotal.Hours, _workTotal.Minutes, _workTotal.Seconds);
		op_quote.text = s;
		ShowPostIt(false);
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
					_prevWorkTotal = _workTotal;
					_working = false;
					_blockEndTime = SetBlockEndTime ((ArrayList)_op ["breakEvent"], now);	// Set time when next block will end
				} else {		// Switching to "work"
					_curIndex += 1;		// Move to next work/break block ONLY when the break is done.
					_prevTime = now;
					_prevBreakTotal = _breakTotal;
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

