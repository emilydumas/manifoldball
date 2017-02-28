// Warning: This uses hard-coded values that need to agree with Duplicator
// FIX THIS

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wrapper : MonoBehaviour {
	protected float cellsize = 3.0f;    // WARNING: for some reason, removing the value 3.0f in this line will cause an error.
    private Vector3 initial_diff;

	// Use this for initialization
	void Start () {
        initial_diff = gameObject.transform.position - wrap(gameObject.transform.position);
		cellsize = GameObject.Find("Runner").GetComponent<SharedParameters>().cubeSize;
    }

    float mymod(float x, float y)
    {
        return x - y * Mathf.Floor(x / y);
    }

    float scalar_wrap(float x, float y)
    {
        return mymod(x + 0.5f * y,y) - 0.5f * y;
    }
	
    Vector3 wrap(Vector3 v)
    {
        return new Vector3(
            scalar_wrap(v.x, cellsize),
            scalar_wrap(v.y, cellsize),
            scalar_wrap(v.z, cellsize)
            );
    }

	// Update is called once per frame
	void Update () {
        gameObject.transform.position = wrap(gameObject.transform.position) + initial_diff;
	}
}
