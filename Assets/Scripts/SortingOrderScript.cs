using UnityEngine;
using System.Collections;

public class SortingOrderScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Renderer r = gameObject.GetComponent<Renderer>();
		Debug.Log ("SortingOrderScript: r = " + r);
		r.sortingLayerName = "Front_Layer";
		r.sortingOrder = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
