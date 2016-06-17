using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class PositionTimeTextScript : MonoBehaviour {
	
	private const float guiMinY = -330;
	private const float guiMaxY = 310;
	private const float guiDelta = guiMaxY - guiMinY;
    private const int x = -155;

    private TimeCalculationScript to;
    private Text timeText;

    void Start ()
	{
		to = TimeCalculationScript.tcs;
        timeText = GetComponent<Text>();
        //guiMinY = -330;
		//guiMaxY = 310;
		//guiDelta = guiMaxY - guiMinY;
		transform.localPosition = new Vector3 (x, guiMinY, 0);	
	}
	
	// Update is called once per frame
	void Update ()
	{
		// Position the 'water' within the beaker
		float y = guiMinY + (to.runSec/(float)to.workSpan.TotalSeconds * guiDelta);
		transform.localPosition = new Vector3(x, y, 0);
        timeText.text = to.runSpan.ToString();
    }
}