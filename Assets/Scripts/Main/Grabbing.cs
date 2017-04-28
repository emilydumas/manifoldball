using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbing : MonoBehaviour {

	public float grabDistance;
	public static bool grabbed=false;

	private Matrix4x4 varBall,varHand;
	private Vector4 ballPose, handPose;
	private Material ball;
	private Material hand;
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
		grab = OVRInput.Get (OVRInput.RawAxis1D.LHandTrigger);
		varBall = ball.GetMatrix("_objectpose");
		varHand = hand.GetMatrix("_objectpose");
		ballPose = varBall.GetColumn (3);
		handPose = varHand.GetColumn (3);
		float distance = Vector4.Distance (ballPose, handPose);
		if (distance < grabDistance && grab > 0) {
			grabbed = true;
			varBall.SetColumn (3, handPose);
			ball.SetMatrix ("_objectpose", varBall);
		} else {
			grabbed = false;
			varBall.SetColumn (3, ballPose);
			ball.SetMatrix ("_objectpose", varBall);
		}
			
	}
}
