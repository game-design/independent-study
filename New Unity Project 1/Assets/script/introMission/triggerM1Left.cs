using UnityEngine;
using System.Collections;

public class triggerM1Left : MonoBehaviour {

    bool showWin = false;
    public bool missionComfirm = false;
    private Rect windowRect = new Rect(200, 20, 200, 100);


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
            windowRect = GUI.Window(0, windowRect, WindowContain, " Are you sure about doing Left? ");
        }
    }
    public void WindowContain(int windowID)
    {
        if (GUI.Button(new Rect(70, 40, 100, 20), "Sure"))
        {
            missionComfirm = true;
            showWin = false;
            GameObject.Find("wallExitM1L").GetComponent<Collider>().isTrigger = false;
            GameObject.Find("wallEnterM1L").GetComponent<Collider>().isTrigger = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        showWin = true;
        //Debug.Log(other.gameObject.transform.position.x);
    }
    void OnTriggerExit(Collider other)
    {
        showWin = false;
        //Debug.Log(other.gameObject.transform.position.x);
    }
}
