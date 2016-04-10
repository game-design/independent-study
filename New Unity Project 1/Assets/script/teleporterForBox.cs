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


    // Use this for initialization
    void Start () {
        ready_to_go = false;
        transform.position = initial_point.position;
        sphere_collider = GetComponent<SphereCollider>();
        
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
            windowRect = GUI.Window(0, windowRect, WindowContain, "Are you sure about\ngoing back?");
        }
        if (showWin == true && winCondition.gameover)
        {
            windowRect = GUI.Window(0, windowRect, WindowContain, "Ready to go to next level?");
        }
    }
    public void WindowContain(int windowID)
    {
        if (!winCondition.gameover)
        {
            if (GUI.Button(new Rect(50, 40, 50, 20), "Sure"))
            {
                Debug.Log("Wanna go back");
                showWin = false;
                SceneManager.LoadScene("Initial Room");
            }
        }
        else
        {
            if (GUI.Button(new Rect(50, 40, 50, 20), "Sure"))
            {
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
