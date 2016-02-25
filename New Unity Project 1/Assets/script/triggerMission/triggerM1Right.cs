using UnityEngine;
using System.Collections;

public class triggerM1Right : MonoBehaviour {


    bool showWin = false;
    public bool missionComfirm = false;
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
            windowRect = GUI.Window(0, windowRect, WindowContain, " Are you sure about\ndoing Right? ");

            
        }
    }
    public void WindowContain(int windowID)
    {
        if (GUI.Button(new Rect(50, 40, 50, 20), "Sure"))
        {
            missionComfirm = true;
            showWin = false;
            GameObject.Find("wallExitM1R").GetComponent<Collider>().isTrigger = false;
            GameObject.Find("wallEnterM1R").GetComponent<Collider>().isTrigger = true;
            //GameObject.FindGameObjectWithTag("MainCamera").SendMessage("zoomIn");

        }
    }

    void OnTriggerEnter(Collider other)
    {
        showWin = true;
        GameObject.FindGameObjectWithTag("MainCamera").SendMessage("zoomOut");
        //Debug.Log(other.gameObject.transform.position.x);
    }
    void OnTriggerExit(Collider other)
    {
        showWin = false;
        GameObject.FindGameObjectWithTag("MainCamera").SendMessage("zoomIn");

        //Debug.Log(other.gameObject.transform.position.x);
    }
}
