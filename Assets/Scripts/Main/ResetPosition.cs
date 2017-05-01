using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPosition : MonoBehaviour {
	private Renderer ren;
	private Kinetics kin;
	private Vector3? firstposition;
	private Grabbing grabbing;

	public GameObject glove;

	public void Start() {
		ren = gameObject.GetComponent<Renderer> ();
		kin = gameObject.GetComponent<Kinetics> ();
		grabbing = glove.GetComponent<Grabbing> ();
	}

	void Update () {
		if (!firstposition.HasValue) {
			firstposition = mop.GetObjectPosition (ren);
		}

		// On thumb stick press, reset position, velocity, accel
		if (OVRInput.Get(OVRInput.RawButton.LThumbstick)||OVRInput.Get(OVRInput.RawButton.RThumbstick)) 
		{
			mop.SetObjectPosition(ren,firstposition.Value);
			kin.makeStationary ();
		}
		if (grabbing.grabbed == true)
			kin.makeStationary ();
	}
}
