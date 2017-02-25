using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualBouncePhysics : MonoBehaviour {
    /// <summary>
    ///   Make this game object bounce from a given object (the "racquet") linked to
    ///   a touch controller, using collision detection and forces but avoiding the
    ///   built-in rigid body collision engine.
    /// </summary>

    public float forceMultiplier = 1.0f;
    public OVRInput.Controller racketController;
    public GameObject racketGameObject;
	private Rigidbody rigidBody;
    
	void Start() {
		// Store the rigid body so GetComponent does not have to be called 
		// everytime a collision happens
		rigidBody = GetComponent<Rigidbody> ();    
	}

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject == racketGameObject)
        {
            rigidBody.AddForce(OVRInput.GetLocalControllerVelocity(racketController) * forceMultiplier, ForceMode.VelocityChange);
        }
    }
}
