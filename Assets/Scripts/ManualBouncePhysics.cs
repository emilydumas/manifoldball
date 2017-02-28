using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualBouncePhysics : MonoBehaviour {
    /// <summary>
    ///   Make this game object bounce from a given object (the "racquet") linked to
    ///   a touch controller, using collision detection and forces but avoiding the
    ///   built-in rigid body collision engine.
    /// </summary>

    public float forceMultiplier = 10000.0f;
    public OVRInput.Controller racketController;
    public GameObject racketGameObject;
	private Rigidbody rigidBody;
	Vector3 gravity=new Vector3(0,-0.5f,0);
	public float threshold = 7.0f;

    
	void Start() {
		// Store the rigid body so GetComponent does not have to be called 
		// everytime a collision happens
		rigidBody = GetComponent<Rigidbody> ();
		Physics.gravity = gravity;
	}

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject == racketGameObject)
        {			
			Vector3 controllerVelocity = OVRInput.GetLocalControllerVelocity (racketController);
			Vector3 extraVel = controllerVelocity * forceMultiplier;

			// The problem happens when either the ball or the paddle is moving at a very low velocity
			if (rigidBody.velocity.magnitude < threshold)
				extraVel = extraVel * 5f;

			//if (controllerVelocity.magnitude < threshold)
			//	extraVel = ( controllerVelocity + rigidBody.velocity ) * 0.5f; 


			rigidBody.AddForce(extraVel, ForceMode.VelocityChange);
        }
    }
}
