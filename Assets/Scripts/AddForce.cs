using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForce : MonoBehaviour {

    public OVRInput.Controller RightController;
	private Rigidbody rigidBody;

	void Start() {
		// Store the rigid body so GetComponent does not have to be called 
		// everytime a collision happens
		rigidBody = GetComponent<Rigidbody> ();    
	}

    private void OnCollisionEnter(Collision col)
    {
		if (col.gameObject.name == "Racket") rigidBody.AddForce(OVRInput.GetLocalControllerVelocity(RightController) * 1, ForceMode.VelocityChange);
    }
}
