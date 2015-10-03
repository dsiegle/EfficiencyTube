using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WorkBarScript : MonoBehaviour {

	private TimeCalculationScript to;
	//private Text curTimeText;
	private Text overtimeText;
	private Slider startOfDaySlider;
	private float guiMinY;
	private float guiMaxY;
	private float guiDelta;
	private float secDelta;
	private Image workZone;
	private Image timeBar;
	private Image overTimeBar;
	private float distToTopOfWorkArea;
	private float overtimeoffset;

	// Use this for initialization
	void Start ()
	{
		//Debug.Log ("WorkBarScript.Start() called");

		// Updating the work bar requires usage of our Time calculation object
		//to = GameObject.Find ("TimeObject").GetComponent<TimeCalculationScript>();
		//curTimeText = GameObject.Find ("CurTime").GetComponent<Text>();
		//overtimeText = GameObject.Find ("OverflowTime").GetComponent<Text>();

		// Get handles to all the bar elements we are updating
		//workZone = GameObject.Find ("WorkDayZone").GetComponent<Image>();
		//timeBar = GameObject.Find ("CurrentTimeBar").GetComponent<Image>();
		//overTimeBar = GameObject.Find ("OverflowTimeBar").GetComponent<Image>();

		//guiMinY = -521;
		//guiMaxY = 494;
		//guiDelta = guiMaxY - guiMinY;
		//secDelta = guiDelta / 86400;	// Pixel distance in a single second on the work bar.

		// Position and scale the green work area (scale it so that it's 8 hours long).
		//distToTopOfWorkArea = guiMaxY - (secDelta * to.AppStartTime ());
		//overtimeoffset = distToTopOfWorkArea - (secDelta * to.workSpan.TotalSeconds);
		//workZone.transform.localPosition = new Vector3 (0, distToTopOfWorkArea, 0);
		//workZone.transform.localScale = new Vector3(1,to.workHours,1);
		//Debug.Log ("scale is " + to.workHours);

		//overtimeText.text = to.endTime.ToString("HH:mm:ss");
	}
	
	// Update is called once per frame
	void Update ()
	{
		//Debug.Log ("WorkBarScript.Update() called");

		// Don't do anything if the "Start Work!" button hasn't been pressed.
		//if (to.started == false) return;

		//curTimeText.text = to.currentTime.ToString("HH:mm:ss");
		//curTimeText.text = to.currentTime.ToString("HH:mm");

		// Position the time bar based upon the current time.
		//float nowdist = guiMaxY - (secDelta * to.AppCurrentTime ());
		//timeBar.transform.localPosition = new Vector3 (0, nowdist, 0);

		// Position the overtime bar based upon how much time you've wasted.
		//float leftovertime = to.secSinceStartup - to.runSec;
		//float overtime = overtimeoffset - (secDelta * leftovertime);
		//Debug.Log ("leftovertime = " + leftovertime + " secSinceStartup = " + to.secSinceStartup);
		//overTimeBar.transform.localPosition = new Vector3 (0, overtime, 0);
		//overtimeText.text = to.endOverTime.ToString("HH:mm:ss");
	}
}
