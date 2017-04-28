using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnMenu : MonoBehaviour {
	
	// Update is called once per frame
	void Update () 
	{
		if (OVRInput.GetDown (OVRInput.RawButton.Start))
			UnityEngine.Application.LoadLevel (0);
			//Application.LoadLevel("_Scenes/MenueScene");
	}
}
