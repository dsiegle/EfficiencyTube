using UnityEngine;
using System.Collections;

public class ButtonStartStopScript : MonoBehaviour {

	public void ButtonClicked() {
		TimeCalculationScript.tcs.StartStop ();
	}

}
