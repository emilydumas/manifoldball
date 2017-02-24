using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionResetter : MonoBehaviour {
    public OVRInput.Controller controller;
    private Vector3 initpos;
    private Quaternion initrot;

	Rigidbody rb;

	// Use this for initialization
	void Start () {
        initpos = transform.position;
        initrot = transform.rotation;

		rb = gameObject.GetComponent<Rigidbody>();    // Reduce number of GetComponent() calls in Update().
	}
	
	// Update is called once per frame
	void Update () {
		if (OVRInput.Get(OVRInput.RawButton.RThumbstick)) {
            transform.position = initpos;
            transform.rotation = initrot;
            
            if (rb != null)
            {
                rb.velocity = new Vector3(0f, 0f, 0f);
                rb.angularVelocity = new Vector3(0f, 0f, 0f);
            }
        }
    }
}
