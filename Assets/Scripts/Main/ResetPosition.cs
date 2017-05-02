using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPosition : MonoBehaviour {
	private Renderer ren;
	private Kinetics kin;
	private Vector3? firstposition;

	public GameObject glove;

	public void Start() {
		ren = gameObject.GetComponent<Renderer> ();
		kin = gameObject.GetComponent<Kinetics> ();
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
	}
}
