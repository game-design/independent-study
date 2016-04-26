using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class teleporterForBox : MonoBehaviour {

    bool showWin = false;
    private Rect windowRect = new Rect(200, 200, 150, 100);
    public Transform initial_point;
    public Transform end_point;
    public static bool ready_to_go;
    private SphereCollider sphere_collider;

    Gloggr_Tracker gTracker;

    // Use this for initialization
    void Start () {
        ready_to_go = false;
        transform.position = initial_point.position;
        sphere_collider = GetComponent<SphereCollider>();
        gTracker = GetComponent<Gloggr_Tracker>();

    }
	
	// Update is called once per frame
	void Update () {
        if (winCondition.gameover)
        {
            transform.position = end_point.position;
            sphere_collider.enabled = false;
        }
	}

    void OnGUI()
    {
        if (showWin == true&& !winCondition.gameover)
        {
            GUI.Box(new Rect(Screen.width / 3, 2*Screen.height / 5, Screen.width / 3, Screen.height / 5), "Are you sure about going back?");
            if (GUI.Button(new Rect(11*Screen.width /25, Screen.height / 2, 3*Screen.width/25, Screen.height/12), "Sure"))
            {
                string message = "Fail time: " + winCondition.current_time.ToString() + " step: " + winCondition.step.ToString();
                gTracker.CaptureEvent(message);
                Gloggr.Instance.PostEvents();
                Debug.Log("Wanna go back");
                showWin = false;
                SceneManager.LoadScene("Initial Room");
            }

        }
        if (showWin == true && winCondition.gameover)
        {
            GUI.Box(new Rect(Screen.width / 3, 2 * Screen.height / 5, Screen.width / 3, Screen.height / 5), "Ready to go to next level?");
            if (GUI.Button(new Rect(11 * Screen.width / 25, Screen.height / 2, 3 * Screen.width / 25, Screen.height / 12), "Sure"))
            {

                string message = "Pass time: " + winCondition.current_time.ToString() + " step: " + winCondition.step.ToString();
                gTracker.CaptureEvent(message);
                Gloggr.Instance.PostEvents();

                showWin = false;
                PauseMenu.currentPosition++;
                ready_to_go = true;

                Debug.Log("ready to go");
            }
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            showWin = true;
            GameObject.FindGameObjectWithTag("MainCamera").SendMessage("zoomOut");
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            showWin = false;
            GameObject.FindGameObjectWithTag("MainCamera").SendMessage("zoomIn");
        }
    }




}
