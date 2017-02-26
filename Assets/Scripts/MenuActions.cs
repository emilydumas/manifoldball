using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuActions : MonoBehaviour {
    // Functions called by onClick events of main menu buttons.
    public void LoadScene(string name)
    {
        Debug.Log("Switching to scene: " + name);
        SceneManager.LoadScene(name);
    }
}
