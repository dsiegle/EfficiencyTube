using UnityEngine;
using System.Collections;

public class PositionAvgBox : MonoBehaviour {

	public float guiMinY;
	//public float guiMaxY;
	public float guiDelta;

	private ComputeAverages computeAveragesScript;

	// Use this for initialization
	void Start ()
	{
		//Debug.Log ("PositionAvgBox.Start() called");

		computeAveragesScript = GameObject.Find ("AvgComputer").GetComponent<ComputeAverages>();

		guiMinY = -198;
		float guiMaxY = 458;
		guiDelta = guiMaxY - guiMinY;
		transform.localPosition = new Vector3 (0, guiMinY, 0);
	}
	
	// Update is called once per frame
	void Update ()
	{

		float pos = guiMinY + (computeAveragesScript.efficiency * guiDelta / 100);
		//Debug.Log ("PositionAvgBox.Update() pos=" + pos);

		transform.localPosition = new Vector3(0, (float)pos, 0);
	}
}
