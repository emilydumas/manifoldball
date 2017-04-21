
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsUtil : MonoBehaviour {
	public GameObject ball;
	public GameObject paddle;
	public Vector3 ballInitialPosition;
	public float collisionThreshold = 0.1f;
	public float ballBounciness = 0.9f;
	public float friction = 0.001f;

	private Renderer ballRenderer;
	private Renderer paddleRenderer;
	private Vector4 ballVelocity = new Vector4(0, 0, 0, 0);
	private Vector4 ballCurrentPosition;
	private Vector4 paddleCurrentPosition;
	private Vector4 paddleLastPosition;

	public void Start() {
		ballRenderer = ball.gameObject.GetComponent<Renderer> ();
		paddleRenderer = paddle.gameObject.GetComponent<Renderer> ();
		paddleLastPosition = paddleRenderer.sharedMaterial.GetMatrix ("_objectpose").GetColumn(3);
		PositionSet (ballRenderer, ballInitialPosition);
	}

	// Update is called once per frame
	void Update () {
		// Update the current position of ball & paddle
		ballCurrentPosition = ballRenderer.sharedMaterial.GetMatrix ("_objectpose").GetColumn(3);
		paddleCurrentPosition = paddleRenderer.sharedMaterial.GetMatrix ("_objectpose").GetColumn(3);

		// Since the changes in position of the ball and paddle are very tiny, we need to amplify them to make collisions realistic
		float coeff = 50f;

		// Collision
		if (Vector4.Distance (ballCurrentPosition, paddleCurrentPosition) < collisionThreshold) {
			// Create a short delay to avoid overlapping
			//System.Timers.Timer ();
			// Current velocity of paddle
			Vector4 paddleVelocity = paddleCurrentPosition * coeff - paddleLastPosition * coeff;
			// Update ball's velocity
			ballVelocity = (1 - ballBounciness) * ballVelocity + ballBounciness * paddleVelocity;
		} 

		// Move ball
		TranslateObjectPose (ballRenderer, ballVelocity * Time.deltaTime);
		// Ball slows down through time because of friction
		ballVelocity = ballVelocity * (1 - friction);
		// Update new position of the paddle
		paddleLastPosition = paddleCurrentPosition;
	}

	public void PositionSet(Renderer rend, Vector3 position) {
		// Ball
		Matrix4x4 deltapose = ballRenderer.sharedMaterial.GetMatrix ("_objectpose");
		deltapose.SetColumn (3, new Vector4(position.x, position.y, position.z, 1));
		ballRenderer.sharedMaterial.SetMatrix ("_objectpose", deltapose);
	}

	public void TranslateObjectPose(Renderer rend, Vector4 translationVector)
	{
		translationVector.w = 0;
		Matrix4x4 objPose = rend.sharedMaterial.GetMatrix ("_objectpose");
		Vector4 currentPosition = objPose.GetColumn (3);
		objPose.SetColumn (3, currentPosition + translationVector);
		rend.sharedMaterial.SetMatrix ("_objectpose", objPose);
	}

	public void SetObjectPose(Renderer rend, Matrix4x4 deltapose)
	{
		rend.sharedMaterial.SetMatrix("_objectpose", deltapose);
	}
}
