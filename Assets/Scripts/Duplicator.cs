using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Duplicator : MonoBehaviour
{
	// Number of clones on each side of the player (above, below, front, behind, left, right)
    public int N = 3;

	// Size of each clone
	protected float cubesize;

	// Only duplicate objects which are assigned a specific tag
    public string targetTag = "Tiled";

	// Count number of validated clones (no rigidbody, no attached script, etc.). Also used
	// to assign name to each clone.
	protected int clonecount = 0;

    void Start()
    {
		cubesize = GameObject.Find("Runner").GetComponent<SharedParameters>().cubeSize;

        foreach (GameObject target in GameObject.FindGameObjectsWithTag(targetTag))
        {
            GameObject clonegroup = new GameObject(target.name + "-clones");
            clonegroup.transform.position = new Vector3(0, 0, 0);
            clonegroup.transform.localScale = new Vector3(1, 1, 1);
            clonegroup.transform.localRotation = new Quaternion();

            for (int i = -N; i <= N; i++)
                for (int j = -N; j <= N; j++)
                    for (int k = -N; k <= N; k++)
                    {
                        if (i == 0 && j == 0 && k == 0)
                            continue;
                        // float sx = ((i + j + k) % 2 == 0 ? 1.0f : -1.0f);
                        GameObject clone = TransformedClone(
                            target,
                            new Vector3(cubesize * i, cubesize * j, cubesize * k),
                            //                            new Vector3(sx, 1, 1),
                            new Vector3(1, 1, 1),
                            new Quaternion()
                        );
                        clone.transform.parent = clonegroup.transform;
                    }
        }
        
    }


	protected GameObject TransformedClone(GameObject target, Vector3 translation, Vector3 scale, Quaternion rotation)
    {
        // Make a full hierarchical clone of the input object and all components
        GameObject clone = Instantiate(target);
        clone.name = target.name + "-clone" + clonecount;

        // Disable all scripts on the clone
        // based on http://answers.unity3d.com/questions/292802
        MonoBehaviour[] scripts = clone.GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour script in scripts)
        {
            Destroy(script);
        }

        // Copies are not rigid bodies, either
        Rigidbody[] rigidBodies = clone.GetComponents<Rigidbody>();
        foreach (Rigidbody rb in rigidBodies)
        {
            Destroy(rb);
        }

        // Remove the colliders, too
        BoxCollider[] boxColliders = clone.GetComponents<BoxCollider>();
        foreach (BoxCollider bc in boxColliders)
        {
            Destroy(bc);
        }

        // Attach the TransformFollow script
        TransformFollower tf = clone.AddComponent<TransformFollower>() as TransformFollower;
        tf.translation = translation;
        tf.scale = scale;
        tf.rotation = rotation;
        tf.leader = target;

        clonecount++;
        return clone;
    }
}
