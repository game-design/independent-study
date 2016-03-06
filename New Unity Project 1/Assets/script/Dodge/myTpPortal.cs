using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class myTpPortal : MonoBehaviour {

    bool showWin = false;
    bool missionComfirm = false;
    private Rect windowRect = new Rect(200, 200, 150, 100);


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
        if (showWin == true && !missionComfirm)
        {
            windowRect = GUI.Window(0, windowRect, WindowContain, "Are you sure about\ngoing back?");
        }
    }
    public void WindowContain(int windowID)
    {
        if (GUI.Button(new Rect(50, 40, 50, 20), "Sure"))
        {
            missionComfirm = true;
            showWin = false;
            SceneManager.LoadScene("Initial Room");
            //GameObject.Find("wallExitM1L").GetComponent<Collider>().isTrigger = false;
            //GameObject.Find("wallEnterM1L").GetComponent<Collider>().isTrigger = true;
            //GameObject.FindGameObjectWithTag("MainCamera").SendMessage("zoomIn");

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
        if(other.name == "Cha_Knight"){ showWin = false;
        GameObject.FindGameObjectWithTag("MainCamera").SendMessage("zoomIn");
        //Debug.Log(other.gameObject.transform.position.x);
    } }
}
