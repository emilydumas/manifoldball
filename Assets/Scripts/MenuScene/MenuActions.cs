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

	public void ActivateMenu ()
	{
		SceneManager.LoadScene("MenuScene");
	}

	public void ActivateAbout ()
	{
		SceneManager.LoadScene ("AboutScene");
	}

	public void LaunchMCLHome()
	{
		Application.OpenURL ("http://mcl.math.uic.edu/");
	}
}
