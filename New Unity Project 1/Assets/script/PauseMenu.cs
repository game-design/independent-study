using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public static int currentPosition = 0;
    public string levelToLoad;
    public bool paused = false;
    public string initial_level;
    int myClickTimer = 20;

	// Use this for initialization
	void Start () {
        Time.timeScale = 1;
        if (SceneManager.GetActiveScene().name == "Initial Room")
        {
            switch (PauseMenu.currentPosition){ 
                case 0:

                    break;
                case 1:
                    GameObject.FindGameObjectWithTag("Player").transform.Translate(5, 0, 65);
                    GameObject.FindGameObjectWithTag("MainCamera").transform.Rotate(new Vector3(-45, 0, 0));
                    GameObject.FindGameObjectWithTag("MainCamera").transform.Translate(5, 0, 65);
                    GameObject.FindGameObjectWithTag("MainCamera").transform.Rotate(new Vector3(45, 0, 0));
                    break;
                case 2:
                    GameObject.FindGameObjectWithTag("Player").transform.Translate(5, 0, 175);
                    GameObject.FindGameObjectWithTag("MainCamera").transform.Rotate(new Vector3(-45, 0, 0));
                    GameObject.FindGameObjectWithTag("MainCamera").transform.Translate(5, 0, 175);
                    GameObject.FindGameObjectWithTag("MainCamera").transform.Rotate(new Vector3(45, 0, 0));
                    break;
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Escape) && myClickTimer == 0)
        {
            myClickTimer = 20;
            paused = !paused;
        }
        if(myClickTimer > 0)    myClickTimer--;
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
                paused = false;
                SceneManager.LoadScene(initial_level);
            }
            if (GUI.Button(new Rect(Screen.width / 4 + 10, Screen.height / 4 + 3 * Screen.height / 10 + 10, Screen.width / 2 - 20, Screen.height / 10), "RESET"))
            {
                paused = false;
                //Application.LoadLevel(Application.loadedLevel);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }


}
