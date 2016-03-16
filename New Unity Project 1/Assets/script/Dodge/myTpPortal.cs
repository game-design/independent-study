using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

// this is for all portal in dodgeball missions
public class myTpPortal : MonoBehaviour {

    bool showWin = false;
    bool missionComfirm = false;
    private Rect windowRect = new Rect(200, 200, 150, 100);
    public char curSituation;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    void OnGUI()
    {
        if (showWin == true)
        {
            windowRect = GUI.Window(0, windowRect, WindowContain, "Are you sure about\ngoing initial room?");
        }
    }
    public void WindowContain(int windowID)
    {
        if (GUI.Button(new Rect(50, 40, 50, 20), "Sure"))
        {
            //level 1 dodgeball
            if(curSituation == 'b')
            {
                PauseMenu.currentPosition = 2;
                showWin = false;
                winControl_Dodge.gameover = true;
            }
            showWin = false;

            // level 2 dodgeball
            if (curSituation == 'd')
            {
                PauseMenu.currentPosition = 4;
                showWin = false;
                winControl_Dodge.gameover = true;
            }
            showWin = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Cha_Knight") {
            showWin = true;
            GameObject.FindGameObjectWithTag("MainCamera").SendMessage("zoomOut");
        }
    }
    void OnTriggerExit(Collider other)
    {
        if(other.name == "Cha_Knight"){
        showWin = false;
        GameObject.FindGameObjectWithTag("MainCamera").SendMessage("zoomIn");
    }
    }
}
