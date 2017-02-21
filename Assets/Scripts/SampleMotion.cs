using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleMotion : MonoBehaviour {
    private float t = 0f;
    public float speed = 0.7f;

	void Start () {
        transform.position = new Vector3(0, 1, 0);		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(Mathf.Sin(2 * speed * t), 2 - 1 * Mathf.Cos(speed * t), 0);
        t += Time.deltaTime;
	}
}
