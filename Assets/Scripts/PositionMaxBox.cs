using UnityEngine;
using System.Collections;

public class PositionMaxBox : MonoBehaviour {

	public float guiMinY;
	public float guiMaxY;
	public float guiDelta;
	
	private ComputeAverages computeAveragesScript;
	private RectTransform go;
	
	// Use this for initialization
	void Start ()
	{
		//Debug.Log ("PositionMaxBox.Start() called");

		computeAveragesScript = GameObject.Find ("AvgComputer").GetComponent<ComputeAverages>();

		guiMinY = -198;
		guiMaxY = 458;
		guiDelta = guiMaxY - guiMinY;
		transform.localPosition = new Vector3 (0, guiMaxY, 0);
	}
	
	// Update is called once per frame
	void Update ()
	{
		//Debug.Log ("PositionMaxBox.Update() called");

		float pos = guiMinY + (computeAveragesScript.maxEfficiency * guiDelta / 100);
		transform.localPosition = new Vector3(0, pos, 0);
	}
}
