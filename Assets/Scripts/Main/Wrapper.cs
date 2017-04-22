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
	private PhysicsUtil physicsUtil;

	// Use this for initialization
	void Start () {
		ballRend = ball.gameObject.GetComponent<Renderer> ();
		physicsUtil = this.GetComponent<PhysicsUtil> ();
		Vector3 ballInitPos = physicsUtil.ballInitialPosition;
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
	
    Vector4 wrap(Vector4 v)
    {
        return new Vector4 (
			scalar_wrap(v.x, cubesize),
			scalar_wrap(v.y, cubesize),
			scalar_wrap(v.z, cubesize),
			1
        );
    }

	// Update is called once per frame
	void Update () {
		Vector4 col3 = ballRend.sharedMaterial.GetMatrix ("_objectpose").GetColumn (3);
		col3 = wrap (col3) + initial_diff;
		physicsUtil.PositionSet(ballRend, col3);
	}
}
