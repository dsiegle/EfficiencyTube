using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PostItScript : MonoBehaviour {

	public static PostItScript pis;

	public Image postItNote;
	public Button op_postItNote;

	private Vector3 vPlayer = new Vector3 (206, -361, -5);		// Players Post it note hidden position
	private Vector3 vOpponent = new Vector3 (-124, -362, -5);	// Opponents Post it note hidden position

	bool _showPlayer = false;
	bool _showOpponent = false;

	void Awake() {
		Debug.Log ("PostItScript.Awake() called.");
		if (pis == null) {
			Debug.Log ("Creating static pis");
			DontDestroyOnLoad (gameObject);
			pis = this;
		}
		else if (pis != this) {
			Debug.Log ("Already have a pis.  Destroying new one");
			Destroy(gameObject);
		}
		
	}

	//------------------------------------------------------------
	// Use this for initialization
	void Start () {
		postItNote = GameObject.Find ("PostItNote").GetComponent<Image> ();
		op_postItNote = GameObject.Find ("OP_PostItNote").GetComponent<Button> ();
	}

	//------------------------------------------------------------
	public void PlayerPostItClicked() {
		Debug.Log ("PlayerPostItClicked():");
		_showPlayer = (_showPlayer ? false : true);	// toggle flag each click
		ShowPostIt (_showPlayer);
	}

	//------------------------------------------------------------
	public void OpponentPostItClicked() {
		_showOpponent = (_showOpponent ? false : true);	// toggle flag each click
		ShowOpponentPostIt (_showOpponent);
		Debug.Log ("OpponentPostItClicked(): _showOpponent = " + _showOpponent);
	}

	//------------------------------------------------------------
	public void ShowPostIt(bool value) {
		if (value == true) {
			vPlayer.z = -20;
		} else {
			vPlayer.z = -5;
		}
		postItNote.transform.position = vPlayer; // Move post-it note into or out of view
		_showPlayer = value;
	}
	//------------------------------------------------------------
	public void ShowOpponentPostIt(bool value) {
		if (value == true) {
			vOpponent.z = -20;
		} else {
			vOpponent.z = -5;
		}
		op_postItNote.transform.position = vOpponent; // Move post-it note into or out of view
		_showOpponent = value;
	}

	//------------------------------------------------------------
	// Update is called once per frame
	void Update () {
	
	}
}
