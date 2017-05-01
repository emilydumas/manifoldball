using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualPhysics : MonoBehaviour {
	public GameObject ball;
	public GameObject racket;
	public GameObject glove;
	public float collisionThreshold = 0.1f;
	public float ballBounciness = 0.9f;
	public float friction = 0.001f;

	private Renderer rball;
	private Renderer rracket;

	private Vector3 ballVelocity = new Vector4(0, 0, 0, 0);
	private Vector3 ballCurrentPosition;
	private Vector3 racketCurrentPosition;
	private Vector3 racketLastPosition;
	private Grabbing gr;

	public void Start() {
		rball = ball.gameObject.GetComponent<Renderer> ();
		rracket = racket.gameObject.GetComponent<Renderer> ();
		gr = glove.GetComponent<Grabbing> ();
		racketLastPosition = mop.GetObjectPosition (rracket);
	}

	// Update is called once per frame
	void Update () {
		// Update the current position of ball & racket
		ballCurrentPosition = mop.GetObjectPosition(rball);
		racketCurrentPosition = mop.GetObjectPosition(rracket);

		bool grabbed = false;
		if (gr != null) {
			grabbed = gr.grabbed;
		}

		// Collision
		if (Vector3.Distance (ballCurrentPosition, racketCurrentPosition) < collisionThreshold) {
			// Current velocity of racket
			Vector3 racketVelocity = (racketCurrentPosition - racketLastPosition) / Time.deltaTime;
			// Update ball's velocity
			ballVelocity = (1 - ballBounciness) * ballVelocity + ballBounciness * racketVelocity;
		} 

		// Stop the ball on thumbstick or if grabbing
		if (grabbed || OVRInput.Get(OVRInput.RawButton.LThumbstick)||OVRInput.Get(OVRInput.RawButton.RThumbstick))
			ballVelocity = new Vector3 (0, 0, 0);

		// Set friction/acceleration
		if (OVRInput.Get(OVRInput.RawButton.A))
			friction = 0.05f;
		else
			friction = 0.001f;
		
		if (OVRInput.Get (OVRInput.RawButton.B))
			friction -= 0.025f;

		// Move ball
		mop.TranslateObjectPosition(rball, ballVelocity * Time.deltaTime);

		// Apply friction/acceleration
		ballVelocity = ballVelocity * (1 - friction);

		// Update new position of the racket
		racketLastPosition = racketCurrentPosition;
	}
}
