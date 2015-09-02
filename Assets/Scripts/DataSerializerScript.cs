using UnityEngine;
using System.Collections;


// This class behaves as a singleton.  I will serialize all data assoiated with the game.
public class DataSerializerScript : MonoBehaviour {

	public static DataSerializerScript dataSerializerScript;

	void Awake()
	{
		if (dataSerializerScript == null) 
		{
			DontDestroyOnLoad (gameObject);
			dataSerializerScript = this;
		}
		else if (dataSerializerScript != this)
		{
			Destroy(gameObject);

		}

	}

}
