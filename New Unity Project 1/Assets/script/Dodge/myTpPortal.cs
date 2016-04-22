using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

// this is for all portal in dodgeball missions
[RequireComponent(typeof(Gloggr_Tracker))]
public class myTpPortal : MonoBehaviour {

    bool showWin = false;
    bool missionComfirm = false;
    private Rect windowRect = new Rect(200, 200, 150, 100);
    public char curSituation;

    Gloggr_Tracker gTracker;

    // Use this for initialization
    void Awake()
    {
        gTracker = GetComponent<Gloggr_Tracker>();
    }

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
            showWin = false;
            //level 1 dodgeball


            if (curSituation == 'b' || curSituation == 'd' || curSituation == 'f' || curSituation == 'h' || curSituation == 'j')
            {
                string message = "Pass time: " + winControl_Dodge.current_time.ToString() + " death: " + winControl_Dodge.amountofDeath.ToString();
                gTracker.CaptureEvent(message);
                Gloggr.Instance.PostEvents();

                winControl_Dodge.gameover = true;
                switch (curSituation)
                {
                    case 'b':
                        PauseMenu.currentPosition = 1; break;
                    case 'd':
                        PauseMenu.currentPosition = 3; break;
                    case 'f':
                        PauseMenu.currentPosition = 5; break;
                    case 'h':
                        PauseMenu.currentPosition = 6; break;
                    case 'j':
                        PauseMenu.currentPosition = 7; break;

                }
                return;
            }
            else
            {
                string message = "Fail time: " + winControl_Dodge.current_time.ToString() + " death: " + winControl_Dodge.amountofDeath.ToString();
                gTracker.CaptureEvent(message);
                Gloggr.Instance.PostEvents();
                SceneManager.LoadScene("Initial Room");
            }
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
