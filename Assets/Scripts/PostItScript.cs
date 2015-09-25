using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PostItScript : MonoBehaviour {

	public static PostItScript pis;

	public Image postItNote;
	private Vector3 v = new Vector3 (206, -361, 0);	// Post it note position

	bool _show = false;

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
	}

	//------------------------------------------------------------
	public void ButtonClicked() {
		Debug.Log ("PostItScript: ButtonClicked() called\n");
		_show = (_show ? false : true);	// toggle flag each click
		ShowPostIt (_show);
	}

	//------------------------------------------------------------
	public void ShowPostIt(bool value) {
		if (value == true) {
			v.y = -170;
		} else {
			v.y = -362;
		}
		postItNote.transform.position = v; // Move post-it note into or out of view
	}

	//------------------------------------------------------------
	// Update is called once per frame
	void Update () {
	
	}
}
