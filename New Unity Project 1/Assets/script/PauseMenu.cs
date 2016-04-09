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

        //Locate player according to current mission
        // 1 for before level 1 while 2 for finish level 1
        // 3 for before level 2 while 4 for finish level 2
        if (SceneManager.GetActiveScene().name == "Initial Room")
        {
            Transform playerT = GameObject.Find("Cha_Knight").transform;
            Transform mCameraT = GameObject.Find("Main Camera").transform;
            switch (PauseMenu.currentPosition){
                case 0:
                    playerT.position = new Vector3(-119, 0, -211);
                    mCameraT.position = new Vector3(-119, 25, -226);
                    break;
                case 1:
                    playerT.position = new Vector3(-119, 0, -146);
                    mCameraT.position = new Vector3(-119, 25, -161);
                    break;
                case 2:
                    playerT.position = new Vector3(-120, 0, -98);
                    mCameraT.position = new Vector3(-120, 25, -114);
                    break;
                case 3:
                    playerT.position = new Vector3(-112, 0, 2);
                    mCameraT.position = new Vector3(-112,25,-13);
                    break;
                case 4:
                    playerT.position = new Vector3(-90, 0, 34);
                    mCameraT.position = new Vector3(-90, 25, 19);
                    break;




                case 5:
                    playerT.position = new Vector3(-87, 0, 74);
                    mCameraT.position = new Vector3(-87, 25, 58);
                    break;
                case 6:
                    playerT.position = new Vector3(-87, 0, 74);
                    mCameraT.position = new Vector3(-87, 25, 58);
                    break;
                case 7:
                    playerT.position = new Vector3(-87, 0, 74);
                    mCameraT.position = new Vector3(-87, 25, 58);
                    break;
                case 8:
                    playerT.position = new Vector3(-87, 0, 74);
                    mCameraT.position = new Vector3(-87, 25, 58);
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
            /*if (GUI.Button(new Rect(Screen.width / 4 + 10, Screen.height / 4 + 2 * Screen.height / 10 + 10, Screen.width / 2 - 20, Screen.height / 10), "RESTART"))
            {
                paused = false;
                SceneManager.LoadScene(initial_level);
            }*/
            if (GUI.Button(new Rect(Screen.width / 4 + 10, Screen.height / 4 + 3 * Screen.height / 10 + 10, Screen.width / 2 - 20, Screen.height / 10), "RESET"))
            {
                paused = false;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }


}
