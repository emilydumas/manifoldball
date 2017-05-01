using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour {
    public OVRInput.Controller controller;

	// Update is called once per frame
	void Update () {
        transform.localRotation = OVRInput.GetLocalControllerRotation(controller);
        transform.localPosition = OVRInput.GetLocalControllerPosition(controller);

		// Quit on both thumbstick press
		if (OVRInput.Get (OVRInput.RawButton.LThumbstick) && OVRInput.Get (OVRInput.RawButton.RThumbstick)) {
			UnityEngine.Application.Quit ();
			Debug.LogError ("Quit selected.");
		}

    }
}
