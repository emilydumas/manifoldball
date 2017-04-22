// Warning: This uses hard-coded values that need to agree with Duplicator
// FIX THIS

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wrapper : MonoBehaviour {
	public GameObject ball;
	public float cubesize = 3.0f;
    private Vector4 initial_diff;
	private Renderer ballRend;
	private InitialPoses IP;

	// Use this for initialization
	void Start () {
		ballRend = ball.gameObject.GetComponent<Renderer> ();
		IP = this.GetComponent<InitialPoses> ();

		Vector3 ballInitPos = IP.ballInitialPosition;
		Vector4 ballInititalPosition = new Vector4(ballInitPos.x, ballInitPos.y, ballInitPos.z, 1f);
		initial_diff = ballInititalPosition - wrap(ballInititalPosition);
    }

    float mymod(float p, float q)
    {
        return p - q * Mathf.Floor(p / q);
    }

    float scalar_wrap(float p, float q)
    {
        return mymod(p + 0.5f * q, q) - 0.5f * q;
    }
	
    Vector4 wrap(Vector3 v)
    {
        return new Vector3 (
			scalar_wrap(v.x, cubesize),
			scalar_wrap(v.y, cubesize),
			scalar_wrap(v.z, cubesize)
        );
    }

	// Update is called once per frame
	void Update () {
		Vector3 col3 = mop.GetObjectPosition (ballRend);
		col3 = wrap (col3) + initial_diff;
		mop.SetObjectPosition(ballRend, col3);
	}
}
