using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour {
    public OVRInput.Controller controller;

	// Update is called once per frame
	void Update () {
        transform.localRotation = OVRInput.GetLocalControllerRotation(controller);
        transform.localPosition = OVRInput.GetLocalControllerPosition(controller);
    }
}
