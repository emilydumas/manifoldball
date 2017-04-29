
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsUtil : MonoBehaviour {
	public GameObject ball;
	public GameObject paddle;
	public float collisionThreshold = 0.1f;
	public float ballBounciness = 0.9f;
	public float friction = 0.001f;

	private Renderer ballRenderer;
	private Renderer paddleRenderer;
	private Vector4 ballVelocity = new Vector4(0, 0, 0, 0);
	private Vector4 ballCurrentPosition;
	private Vector4 paddleCurrentPosition;
	private Vector4 paddleLastPosition;
	private bool grabbed;
	private Grabbing grabbing;

	public void Start() {
		ballRenderer = ball.gameObject.GetComponent<Renderer> ();
		paddleRenderer = paddle.gameObject.GetComponent<Renderer> ();
		paddleLastPosition = mop.GetObjectPosition (paddleRenderer);
	}

	// Update is called once per frame
	void Update () {
		// Update the current position of ball & paddle
		ballCurrentPosition = mop.GetObjectPosition(ballRenderer);
		paddleCurrentPosition = mop.GetObjectPosition(paddleRenderer);

		grabbed = Grabbing.grabbed;

		// Since the changes in position of the ball and paddle are very tiny, we need to amplify them to make collisions realistic
		// TODO: Make this time-based instead of a fixed constant!
		float coeff = 50f;

		// Collision
		if (Vector4.Distance (ballCurrentPosition, paddleCurrentPosition) < collisionThreshold) {
			// Current velocity of paddle
			Vector4 paddleVelocity = paddleCurrentPosition * coeff - paddleLastPosition * coeff;
			// Update ball's velocity
			ballVelocity = (1 - ballBounciness) * ballVelocity + ballBounciness * paddleVelocity;
		} 
		if (OVRInput.Get(OVRInput.RawButton.LThumbstick)||OVRInput.Get(OVRInput.RawButton.RThumbstick)||grabbed==true)
			ballVelocity = new Vector3 (0, 0, 0);

		if (OVRInput.Get(OVRInput.RawButton.A))
			friction = 0.05f;
		else
			friction = 0.001f;
		if (OVRInput.Get (OVRInput.RawButton.B))
			friction -= 0.025f;

		// Move ball
		mop.TranslateObjectPosition(ballRenderer, ballVelocity * Time.deltaTime);
		// Ball slows down through time because of friction
		ballVelocity = ballVelocity * (1 - friction);
		// Update new position of the paddle
		paddleLastPosition = paddleCurrentPosition;
	}
}
