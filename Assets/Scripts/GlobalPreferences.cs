using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TilingType { Torus, Boro, MirrorCube };

public static class GlobalPreferences {
	// Set by MenuScene, read by EuclideanGame
	static public TilingType tilingType = TilingType.Torus;
}
