using UnityEngine;
using System.Collections;

public class ButtonStartStopScript : MonoBehaviour {
    //private bool firstClick = true;
	public void ButtonClicked() {
        
		TimeCalculationScript.tcs.StartStop ();
	}

}
