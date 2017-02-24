using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breaks : MonoBehaviour {

    private bool pressed;
    public Rigidbody rb;

	void Update ()
    {
        pressed = OVRInput.Get(OVRInput.RawButton.A);


		if (pressed == true)rb.drag=2;
        else rb.drag = 0.1f;

	}
}
