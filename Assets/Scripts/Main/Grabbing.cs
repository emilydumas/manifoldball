using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbing : MonoBehaviour {

	public float grabDistance;

	private Matrix4x4 varBall,varHand;
	private Vector4 ballPose, handPose;
	private Material ball;
	private Material hand;
	private bool grabbed=false;
	private float grab;
	// Use this for initialization
	void Start () 
	{
		ball = GameObject.Find ("Ball").GetComponent<Renderer> ().sharedMaterial;
		hand = GameObject.Find ("LHand").GetComponent<Renderer> ().sharedMaterial;
	}
	
	// Update is called once per frame
	void Update () 
	{
		grab = Input.GetAxis ("LGrab");
		varBall = ball.GetMatrix("_objectpose");
		varHand = hand.GetMatrix("_objectpose");
		ballPose = varBall.GetColumn (3);
		handPose = varHand.GetColumn (3);
		float distance = Mathf.Sqrt (
				(ballPose.x - handPose.x) * (ballPose.x - handPose.x) +
				(ballPose.y - handPose.y) * (ballPose.y - handPose.y) +
				(ballPose.z - handPose.z) * (ballPose.z - handPose.z));
		if (distance < grabDistance && grab > 0) {
			varBall.SetColumn (3, handPose);
			ball.SetMatrix ("_objectpose", varBall);
		} else {
			varBall.SetColumn (3, ballPose);
			ball.SetMatrix ("_objectpose", varBall);
		}
			
	}
}
