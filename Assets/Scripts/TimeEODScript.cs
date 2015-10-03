using UnityEngine;
using UnityEngine.UI;
//using System.Collections;

public class TimeEODScript : MonoBehaviour {

	private TimeCalculationScript to;
	private Text t;

	// Use this for initialization
	void Start () {
		to = TimeCalculationScript.tcs;
		t = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		string e = to.endOverTime.ToString ("hh:mmtt\n");
		t.text = e + to.toHMS (to.secToEOOT);
	}
}