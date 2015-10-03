using UnityEngine;
using System.Collections;

// Attached to the "AvgMixBox" gameobject
// Only positions it on the efficiency slider

public class PositionMinBox : MonoBehaviour {

	public float guiMinY;
	public float guiMaxY;
	public float guiDelta;
	
	private ComputeAverages computeAveragesScript;
	private RectTransform go;
	
	// Use this for initialization
	void Start ()
	{
		//Debug.Log ("PositionMinBox.Start() called");
		computeAveragesScript = GameObject.Find ("AvgComputer").GetComponent<ComputeAverages>();

		guiMinY = -198;
		guiMaxY = 458;
		guiDelta = guiMaxY - guiMinY;
		transform.localPosition = new Vector3 (0, guiMaxY, 0);
	}
	
	// Update is called once per frame
	void Update ()
	{
		//Debug.Log ("PositionMinBox.Update() called");

		float pos = guiMinY + (computeAveragesScript.minEfficiency * guiDelta / 100);
		transform.localPosition = new Vector3(0, pos, 0);

	}
}
