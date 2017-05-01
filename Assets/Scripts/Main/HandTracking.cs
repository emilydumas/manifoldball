using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HandTracking : MonoBehaviour {

	public OVRInput.Controller controller;
	private Renderer ren;

	void Start () 
	{
		ren = gameObject.GetComponent<Renderer> ();
	}

	void Update () 
	{
		Quaternion handRot = OVRInput.GetLocalControllerRotation (controller);
		Vector3 handPos = OVRInput.GetLocalControllerPosition (controller);

		mop.SetObjectPose (ren, Matrix4x4.TRS (handPos, handRot, new Vector3 (1, 1, 1)));

		if (OVRInput.GetDown (OVRInput.RawButton.Start))
			SceneManager.LoadScene ("MenuScene");
	}
}