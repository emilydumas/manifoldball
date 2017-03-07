using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualBouncePhysics : MonoBehaviour {
    /// <summary>
    ///   Make this game object bounce from a given object (the "racquet") linked to
    ///   a touch controller, using collision detection and forces but avoiding the
    ///   built-in rigid body collision engine.
    /// </summary>

    public float forceMultiplier;
    public OVRInput.Controller racketController;
    public GameObject racketGameObject;
	private Rigidbody rigidBody;
    
	void Awake() {
		// Store the rigid body so GetComponent does not have to be called everytime a collision happens
		//rigidBody = GetComponent<Rigidbody> ();    
	}

    private void OnCollisionEnter(Collision col)
    {
		rigidBody = GetComponent<Rigidbody> ();  
        if (col.gameObject == racketGameObject)
        {
			if (rigidBody != null)
            	rigidBody.AddForce(OVRInput.GetLocalControllerVelocity(racketController) * forceMultiplier, ForceMode.VelocityChange);
        }
    }
}
