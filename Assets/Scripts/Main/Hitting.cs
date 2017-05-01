using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitting : MonoBehaviour {
	public GameObject ball;

	public float collisionThreshold = 0.1f;
	public float ballBounciness = 0.9f;
	public float friction = 0.001f;

	private Kinetics kball;
	private Renderer rball;
	private Renderer ren;

	private Vector3 racketLastPosition;

	public void Start() {
		kball = ball.GetComponent<Kinetics> ();
		rball = ball.GetComponent<Renderer> ();
		ren = gameObject.GetComponent<Renderer> ();

		racketLastPosition = mop.GetObjectPosition (ren);
	}

	void Update () {
		Vector3 ballCurrentPosition = mop.GetObjectPosition(rball);
		Vector3 racketCurrentPosition = mop.GetObjectPosition(ren);

		// Collision?
		if (Vector3.Distance (ballCurrentPosition, racketCurrentPosition) < collisionThreshold) {
			Vector3 racketVelocity = (racketCurrentPosition - racketLastPosition) / Time.deltaTime;
			// Set new ball velocity; TODO: Real elastic collision calculation!
			kball.velocity = (1 - ballBounciness)*kball.velocity + ballBounciness * racketVelocity;
		} 

		// Set friction/acceleration
		if (OVRInput.Get(OVRInput.RawButton.A))
			kball.friction = 0.05f;
		else
			kball.friction = 0.001f;
		
		if (OVRInput.Get (OVRInput.RawButton.B))
			kball.friction = -0.025f;

		// Update new position of the racket
		racketLastPosition = racketCurrentPosition;
	}
}
