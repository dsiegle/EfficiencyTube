using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UpdateStartTime : MonoBehaviour {

	private Text timeStartText;
	private TimeCalculationScript to;

	// Use this for initialization
	void Start () {
		timeStartText = GameObject.Find ("TimeStartText").GetComponent<Text> ();
		to = TimeCalculationScript.tcs;
		timeStartText.text = "Start: " + to.startTime.ToString ("hh:mmtt\n");
	}
	
	// Update is called once per frame
	void Update () {
	}
}
