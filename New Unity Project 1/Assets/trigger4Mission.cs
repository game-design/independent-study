using UnityEngine;
using System.Collections;

public class trigger4Mission : MonoBehaviour {

    public bool windowSwitch = false;
    private Rect windowRect = new Rect(200, 20, 200, 100);

    void OnGUI()
    {
        if (windowSwitch == true)
        {
            windowRect = GUI.Window(0, windowRect, WindowContain, "  Left is AA \n Right is BB");
        }
    }
    public void WindowContain(int windowID)
    {
        /*if (GUI.Button(new Rect(70, 40, 100, 20), "关闭视窗"))
        {
            windowSwitch = false;
        }*/
    }





    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        windowSwitch = true;
        //Debug.Log(other.gameObject.transform.position.x);
    }
    void OnTriggerExit(Collider other)
    {
        windowSwitch = false;
        //Debug.Log(other.gameObject.transform.position.x);
    }

}
