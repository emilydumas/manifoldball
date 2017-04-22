using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiling {
	private int N;
	private float cubesize;

	public Tiling(int N, float cubesize)
	{
		this.N = N;
		this.cubesize = cubesize;
	}

	// Iterate over transformations corresponding to the tiles
	public System.Collections.IEnumerator GetEnumerator()
	{
		for (int i = -N; i <= N; i++)
			for (int j = -N; j <= N; j++)
				for (int k = -N; k <= N; k++) {
					Matrix4x4 M = Matrix4x4.TRS (new Vector3 (cubesize * i, cubesize * j, cubesize * k), Quaternion.identity, new Vector3 (1, 1, 1));
					//Debug.Log ("Yielding " + M);
					yield return M;
				}
	}

	public Matrix4x4 TileContaining(Matrix4x4 P)
	{
		Vector3 pt = P.GetColumn(3);
		Vector3 delta = pt - wrap(pt);
		return Matrix4x4.TRS(delta,  Quaternion.identity, new Vector3(1,1,1));
 	}


	// ------------------ Internally used -------------------
	private float mymod(float p, float q)
	{
		return p - q * Mathf.Floor(p / q);
	}

	private float scalar_wrap(float p, float q)
	{
		return mymod(p + 0.5f * q, q) - 0.5f * q;
	}

	private Vector3 wrap(Vector3 v)
	{
		return new Vector3 (
			scalar_wrap(v.x, cubesize),
			scalar_wrap(v.y, cubesize),
			scalar_wrap(v.z, cubesize)
		);
	}
}