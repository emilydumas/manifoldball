using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuActions : MonoBehaviour {
    // Functions called by onClick events of main menu buttons.
	public void ActivateGame(string tilingName)
	{
		GlobalPreferences.tilingType = (TilingType)Enum.Parse (typeof(TilingType), tilingName);
        SceneManager.LoadScene("EuclideanGame");
    }
}
