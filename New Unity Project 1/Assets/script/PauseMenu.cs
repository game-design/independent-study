using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public static int currentPosition = 0;
    public string levelToLoad;
    public bool paused = false;
    public string initial_level;
    int myClickTimer = 20;
    public static int array_size;
    public Transform[] positions=new Transform[array_size];

    Gloggr_Tracker gTracker;

    // Use this for initialization
    void Start () {

        gTracker = GetComponent<Gloggr_Tracker>();

        Time.timeScale = 1;
        if (SceneManager.GetActiveScene().name == "Initial Room")
        {
            Transform playerT = GameObject.Find("Cha_Knight").transform;
            Transform mCameraT = GameObject.Find("Main Camera").transform;
            playerT.position = positions[PauseMenu.currentPosition].position;
            mCameraT.GetComponent<CameraFollow>().setOffset();
            /*mCameraT.position = new Vector3(positions[PauseMenu.currentPosition].position.x, 
                                            positions[PauseMenu.currentPosition].position.y + 25, 
                                            positions[PauseMenu.currentPosition].position.z - 15);
            */
            //mCameraT.position = playerT.position + mCameraT.GetComponent<CameraFollow>().getOffset();

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


        if(PauseMenu.currentPosition==7)
            StartCoroutine("load_level");


    }


    IEnumerator load_level()
    {
        yield return new WaitForSeconds(3f);

        gTracker.CaptureEvent("Finish game");
        Gloggr.Instance.PostEvents();

        Debug.Log("capture event");
        SceneManager.LoadScene("startAnimation");
    }






    void OnGUI()
    {
        if (paused)
        {
            string message = "Pause game";
            gTracker.CaptureEvent(message);
            Gloggr.Instance.PostEvents();



            GUI.Box(new Rect(Screen.width / 4, Screen.height / 4, Screen.width / 2, Screen.height / 2), "PAUSED");
            if (GUI.Button(new Rect(Screen.width / 4 + 10, Screen.height / 4 + Screen.height / 10 + 10, Screen.width / 2 - 20, Screen.height / 10), "RESUME"))
            {
                message = "Resume game";
                gTracker.CaptureEvent(message);
                Gloggr.Instance.PostEvents();
                paused = false;
            }
            /*if (GUI.Button(new Rect(Screen.width / 4 + 10, Screen.height / 4 + 2 * Screen.height / 10 + 10, Screen.width / 2 - 20, Screen.height / 10), "RESTART"))
            {
                paused = false;
                SceneManager.LoadScene(initial_level);
            }*/
            if (GUI.Button(new Rect(Screen.width / 4 + 10, Screen.height / 4 + 3 * Screen.height / 10 + 10, Screen.width / 2 - 20, Screen.height / 10), "RESET"))
            {
                message = "Reset game";
                gTracker.CaptureEvent(message);
                Gloggr.Instance.PostEvents();
                paused = false;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }


}
