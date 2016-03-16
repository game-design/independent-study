using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

// this is for portals in initial room
public class missionTP : MonoBehaviour {

    bool showWin = false;
    public bool missionComfirm = false;
    private Rect windowRect = new Rect(200, 200, 150, 100);
    public string targetLevel;


    // Use this for initialization
    void Start(){}
    // Update is called once per frame
    void Update(){}


    void OnGUI()
    {
        if (showWin == true)// && !missionComfirm)
        {
            windowRect = GUI.Window(0, windowRect, WindowContain, "Are you sure about\ngoing " + targetLevel);
        }
    }
    public void WindowContain(int windowID)
    {
        if (GUI.Button(new Rect(50, 40, 50, 20), "Sure"))
        {
            if (targetLevel.Equals("box_0") || targetLevel.Equals("dodgeBall_0"))
            {
                PauseMenu.currentPosition = 1;
            }

            if (targetLevel.Equals("box_1") || targetLevel.Equals("dodgeBall_1"))
            {
                PauseMenu.currentPosition = 3;
            }

            StartCoroutine("load_level");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        showWin = true;
        //GameObject.FindGameObjectWithTag("MainCamera").SendMessage("zoomOut");
        //Debug.Log(other.gameObject.transform.position.x);
    }
    void OnTriggerExit(Collider other)
    {
        showWin = false;
        //GameObject.FindGameObjectWithTag("MainCamera").SendMessage("zoomIn");
        //Debug.Log(other.gameObject.transform.position.x);
    }
    
    IEnumerator load_level()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(targetLevel);
    }
}
