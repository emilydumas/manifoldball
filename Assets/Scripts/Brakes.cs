using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brakes : MonoBehaviour {
    private bool braking;
    public Rigidbody rb;
    private float olddrag;

    void startbraking()
    {
        olddrag = rb.drag;
        rb.drag = 3.0f;
        braking = true;
    }

    void stopbraking()
    {
        rb.drag = olddrag;
        braking = false;
    }

	void Update ()
    {
        bool pressed = OVRInput.Get(OVRInput.RawButton.A);

		if (pressed == true && braking == false) {
            startbraking();
        }

        if (pressed == false && braking == true)
        {
            stopbraking();
        }
	}
}
