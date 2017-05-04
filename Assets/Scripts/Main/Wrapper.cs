using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wrapper : MonoBehaviour {
	public GameObject ball;
	private Tiling tiling;
    //private Vector4 initial_diff;
	private Renderer ballRend;
	private InitialPoses IP;

	// Use this for initialization
	void Start () {
		Registrar reg = this.GetComponent<Registrar> ();
		tiling = reg.tiling;

		ballRend = ball.gameObject.GetComponent<Renderer> ();
		IP = this.GetComponent<InitialPoses> ();

		//Vector3 ballInitPos = IP.ballInitialPosition;
		//Vector4 ballInititalPosition = new Vector4(ballInitPos.x, ballInitPos.y, ballInitPos.z, 1f);
		//initial_diff = ballInititalPosition - wrap(ballInititalPosition);
    }

	// Update is called once per frame
	void Update () {
		Matrix4x4 P = mop.GetObjectPose (ballRend);
		Matrix4x4 T = tiling.TileContaining (P);
		mop.SetObjectPose(ballRend, T.inverse * P);
		//col3 = wrap (col3) + initial_diff;
		//mop.SetObjectPosition(ballRend, col3);
	}
}
