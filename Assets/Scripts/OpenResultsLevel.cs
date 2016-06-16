using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class OpenResultsLevel : MonoBehaviour {

	public void DoResults()
	{
		SceneManager.LoadScene(2);
	}
}
