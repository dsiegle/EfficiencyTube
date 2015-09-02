using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PositionFutureBox : MonoBehaviour {

	public Slider slider;

	private ComputeAverages ca;
	private TimeCalculationScript to;

	//private int prevEfficiency = 0;
	private bool playedUp = false;
	private bool playedDown = false;

	//------------------------------------------------------------------------------------------
	public void SliderChanged()
	{
		// Reset the target audio triggers
		playedUp = false;
		playedDown = false;
	}

	//------------------------------------------------------------------------------------------
	// Use this for initialization
	void Start ()
	{
		//Debug.Log ("PositionFutureBox.Start() called");
		to = TimeCalculationScript.tcs;
		ca = GameObject.Find ("AvgComputer").GetComponent<ComputeAverages> ();
		slider = GameObject.Find ("TargetEfficiencySlider").GetComponent<Slider> ();
		//Debug.Log ("ca.efficiency = " + ca.efficiency + " slider.value = " + slider.value);
	}
	
	// Update is called once per frame
	//------------------------------------------------------------------------------------------
	void Update ()
	{
		// Sound ring check for when you pass by the target slider
		if (to.running && !playedUp) // We know its headed upward and hasn't played
		{
			if (ca.efficiency > slider.value) 
			{
				GetComponent<AudioSource>().Play ();
				//Debug.Log ("efficiency = " + efficiency + " prevEfficiency = " + prevEfficiency);
				//prevEfficiency = (int)ca.efficiency;
				playedUp = true;
				playedDown = false;
			}
		} 
		else  // we know its headed downward because its not running
		{
			if ( !playedDown && ca.efficiency < slider.value)
			{
				GetComponent<AudioSource>().Play ();
				//Debug.Log ("efficiency = " + efficiency + " prevEfficiency = " + prevEfficiency);
				//prevEfficiency = (int)ca.efficiency;
				playedUp = false;
				playedDown = true;
			}
			
		}

	}
}
