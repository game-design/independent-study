using UnityEngine;
using System.Collections;

public class popupMissionIntro : MonoBehaviour {


    bool showWin = false;
    private Rect windowRect = new Rect(200, 200, 150, 100);


    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    void OnGUI()
    {
        if (showWin == true)
        {
            windowRect = GUI.Window(0, windowRect, WindowContain, " Left is AA \n Right is BB ");
        }
    }
    public void WindowContain(int windowID)
    {
        /*if (GUI.Button(new Rect(70, 40, 100, 20), "关闭视窗"))
        {
            windowSwitch = false;
        }*/
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
