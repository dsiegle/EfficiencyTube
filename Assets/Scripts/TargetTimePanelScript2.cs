using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TargetTimePanelScript2 : MonoBehaviour {

	public Text t;
	public Slider s;

	//------------------------------------------------------------------------------------------
	// Gets called when the user selects how long they will work.  Starts timer as a side effect.
	public void SetWorkHours()
	{
		Debug.Log ("TargetTimePanelScript2.SetWorkHours() called");
		//to.workHours = s.value / 60.0f;
		TimeCalculationScript.tcs.InitializeAndRun (s.value/4);

		Application.LoadLevel (1);

	}
	
	//------------------------------------------------------------------------------------------
	// Use this for initialization
	void Start () 
	{
		s = GameObject.Find ("TargetTimeSlider").GetComponent<Slider> ();
		t = GameObject.Find ("TargetTimeText").GetComponent<Text> ();
		t.text = TimeCalculationScript.tcs.toHMS (s.value);
		//t.text = to.toHMS (s.value * 60 * 15);
		Debug.Log ("TTP2:Start() called");
	}
	
	//------------------------------------------------------------------------------------------
	// Update is called once per frame
	void Update () 
	{
		//Debug.Log ("TTP2:Update() called");

		//t.text = to.toHMS (s.value);
		t.text = TimeCalculationScript.tcs.toHMS (s.value * 60 * 15);
	}
}
