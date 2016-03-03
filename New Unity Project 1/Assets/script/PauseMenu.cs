using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public string levelToLoad;
    public bool paused = false;
    public string initial_level;

	// Use this for initialization
	void Start () {
        Time.timeScale = 1;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Escape))
        {
            if (!paused)
                paused = true;
        }
        if (paused)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;

	}

    void OnGUI()
    {
        if (paused)
        {
            GUI.Box(new Rect(Screen.width / 4, Screen.height / 4, Screen.width / 2, Screen.height / 2), "PAUSED");
            if (GUI.Button(new Rect(Screen.width / 4 + 10, Screen.height / 4 + Screen.height / 10 + 10, Screen.width / 2 - 20, Screen.height / 10), "RESUME"))
            {
                paused = false;
            }
            if (GUI.Button(new Rect(Screen.width / 4 + 10, Screen.height / 4 + 2 * Screen.height / 10 + 10, Screen.width / 2 - 20, Screen.height / 10), "RESTART"))
            {
                SceneManager.LoadScene(initial_level);
            }
        }
    }


}
