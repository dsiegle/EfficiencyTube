using UnityEngine;
using System.Collections;

public class GlobalCameraScript : MonoBehaviour {
	public static GlobalCameraScript gcs;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//------------------------------------------------------------------------------------------
	// Make sure we only have one of these.
	void Awake()
	{
		Debug.Log ("GlobalCameraScript.Awake() called.");
		if (gcs == null) 
		{
			Debug.Log ("Creating static Main Camera");
			DontDestroyOnLoad (gameObject);
			gcs = this;
		}
		else if (gcs != this)
		{
			Debug.Log ("Already have a GCS.  Destroying new one");
			Destroy(gameObject);
		}
		
	}
}
