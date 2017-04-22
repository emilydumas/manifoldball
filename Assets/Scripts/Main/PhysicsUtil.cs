
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsUtil : MonoBehaviour {
	// Game Objects
	public GameObject ball;
	public GameObject paddle;
	public GameObject glove;

	// Where the ball is put at the beginning
	public Vector3 ballInitialPosition;

	// Parameters for interactions
	public float collisionThreshold = 0.1f;
	public float ballBounciness = 0.9f;
	public float friction = 0.001f;
	public float grabRadius = 0.1f;
	public string inputAxis = "LGrab";

	// Renderers
	private Renderer ballRenderer;
	private Renderer paddleRenderer;
	private Renderer gloveRenderer;

	// Necessary variables for interactions
	private Vector4 ballVelocity = new Vector4(0, 0, 0, 0);
	private Vector4 ballCurrentPosition;
	private Vector4 paddleCurrentPosition;
	private Vector4 paddleLastPosition;
	private Vector4 gloveCurrentPosition;
	private Vector4 gloveLastPosition;

	// Check if the ball is being grabbed. If true, ignore collisions between ball & paddle
	private bool grabbing;

	public void Start() {
		// Get the renderers
		ballRenderer = ball.gameObject.GetComponent<Renderer> ();
		paddleRenderer = paddle.gameObject.GetComponent<Renderer> ();
		gloveRenderer = glove.gameObject.GetComponent<Renderer> ();

		// Position the ball
		paddleLastPosition = paddleRenderer.sharedMaterial.GetMatrix ("_objectpose").GetColumn(3);
		PositionSet (ballRenderer, ballInitialPosition);

		// Initiate grab feature
		gloveLastPosition = gloveRenderer.sharedMaterial.GetMatrix ("_objectpose").GetColumn(3);
		grabbing = false;
	}

	// Update is called once per frame
	void Update () {
		// Get state of the trigger
		float trigger = Input.GetAxis (inputAxis);

		// Get current positions of the objects
		gloveCurrentPosition = gloveRenderer.sharedMaterial.GetMatrix ("_objectpose").GetColumn(3);
		paddleCurrentPosition = paddleRenderer.sharedMaterial.GetMatrix ("_objectpose").GetColumn(3);

		if (grabbing && trigger == 1) {
			ballCurrentPosition = gloveCurrentPosition;
		} else {
			ballCurrentPosition = ballRenderer.sharedMaterial.GetMatrix ("_objectpose").GetColumn(3);
		}

		// Since the changes in positions of each object is very small, we need to amplify it to make interactions more realistic
		float coeff = 50f;

		// Check if the ball is being grabbed
		Debug.Log(grabbing + " " + trigger + " " + Vector4.Distance (gloveCurrentPosition, ballCurrentPosition) + "\n" + ballCurrentPosition + "\n" + gloveCurrentPosition);
		if (!grabbing && trigger == 1 && Vector4.Distance (gloveCurrentPosition, ballCurrentPosition) < grabRadius) {
			grabbing = true;
		} 
		if (grabbing && trigger < 1) {
			grabbing = false;
		}

		// If the ball is being grabbed, ignore all collisions between it and the paddle.
		if (grabbing) {
			// Set ball's position
			Matrix4x4 glovePose = gloveRenderer.sharedMaterial.GetMatrix ("_objectpose");
			SetObjectPose (ballRenderer, glovePose);
			// Set ball's velocity
			ballVelocity = gloveCurrentPosition * coeff - gloveLastPosition * coeff;

		} else {
			// Collision
			if (Vector4.Distance (ballCurrentPosition, paddleCurrentPosition) < collisionThreshold) {
				// Current velocity of paddle
				Vector4 paddleVelocity = paddleCurrentPosition * coeff - paddleLastPosition * coeff;
				// Update ball's velocity
				ballVelocity = (1 - ballBounciness) * ballVelocity + ballBounciness * paddleVelocity;
			} 

			// Move ball
			TranslateObjectPose (ballRenderer, ballVelocity * Time.deltaTime);
			// Ball slows down through time because of friction
			ballVelocity = ballVelocity * (1 - friction);
		} 

		// Update new positions
		paddleLastPosition = paddleCurrentPosition;
		gloveLastPosition = gloveCurrentPosition;
	}

	public void PositionSet(Renderer rend, Vector3 position) {
		Matrix4x4 deltapose = ballRenderer.sharedMaterial.GetMatrix ("_objectpose");
		deltapose.SetColumn (3, new Vector4(position.x, position.y, position.z, 1));
		ballRenderer.sharedMaterial.SetMatrix ("_objectpose", deltapose);
	}

	public void TranslateObjectPose(Renderer rend, Vector4 translationVector)
	{
		translationVector.w = 0;    // We don't need the w value of this vector
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
