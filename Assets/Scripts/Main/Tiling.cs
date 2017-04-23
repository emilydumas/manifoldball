using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tiling {
	// Iterate over transformations corresponding to the tiles
	public abstract System.Collections.IEnumerator GetEnumerator();

	// Return the tile containing a given pose
	public abstract Matrix4x4 TileContaining(Matrix4x4 P);
}