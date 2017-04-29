using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbing : MonoBehaviour {

	public float grabDistance;
	public static bool grabbed=false;
	public GameObject paddle;

	private Renderer paddleRenderer, ballRenderer;
	private Vector4 paddleLastPosition,paddleCurrentPosition,paddleVelocity;
	private bool left;
	private bool right;
	private Matrix4x4 varBall,varHand;
	private Vector4 ballPose, handPose;
	private Material ball;
	private Material hand;
	private float Lgrab;
	private float Rgrab;
	// Use this for initialization
	void Start () 
	{
		ballRenderer = GameObject.Find ("Ball").GetComponent<Renderer> ();
		ball = GameObject.Find ("Ball").GetComponent<Renderer> ().sharedMaterial;
		hand = GameObject.Find ("LHand").GetComponent<Renderer> ().sharedMaterial;
		paddleRenderer = paddle.gameObject.GetComponent<Renderer> ();
		paddleLastPosition = mop.GetObjectPosition (paddleRenderer);
	}
	
	// Update is called once per frame
	void Update () 
	{
		paddleCurrentPosition = mop.GetObjectPosition(paddleRenderer);
		right = GameObject.Find ("LHand").GetComponent<HandTracking> ().right;
		left = GameObject.Find ("RHand").GetComponent<LHandTracking> ().left;
		Rgrab = OVRInput.Get (OVRInput.RawAxis1D.RHandTrigger);
		Lgrab = OVRInput.Get (OVRInput.RawAxis1D.LHandTrigger);
		varBall = ball.GetMatrix("_objectpose");
		varHand = hand.GetMatrix("_objectpose");
		ballPose = varBall.GetColumn (3);
		handPose = varHand.GetColumn (3);
		float distance = Vector4.Distance (ballPose, handPose);
		if ((distance < grabDistance && Lgrab > 0 && right) ||(distance < grabDistance && Rgrab>0 && left)) {
			grabbed = true;
			varBall.SetColumn (3, handPose);
			ball.SetMatrix ("_objectpose", varBall);
			paddleVelocity = paddleCurrentPosition - paddleLastPosition;
		} else {
			grabbed = false;
			varBall.SetColumn (3, ballPose);
			ball.SetMatrix ("_objectpose", varBall);

		}
		/*if(!grabbed)
		mop.TranslateObjectPosition (ballRenderer, paddleVelocity * Time.deltaTime);*/	
	}
}
