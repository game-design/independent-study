using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

// this is for portals in initial room

[RequireComponent(typeof(Gloggr_Tracker))]
public class missionTP : MonoBehaviour {

    Gloggr_Tracker gTracker;

    bool showWin = false;
    public bool missionComfirm = false;
    private Rect windowRect = new Rect(200, 200, 150, 100);
    public string targetLevel;


    void Awake()
    {
        gTracker = GetComponent<Gloggr_Tracker>();
    }

    // Use this for initialization
    void Start(){}
    // Update is called once per frame
    void Update(){}


    void OnGUI()
    {
        if (showWin == true)// && !missionComfirm)
        {
            GUI.Box(new Rect(Screen.width / 2-150, Screen.height / 2-35, 300, 70), "Are you sure about going to "+targetLevel+"?");
            if (GUI.Button(new Rect(Screen.width / 2 - 40, Screen.height/2, 80, 30), "sure"))
            {
                Debug.Log("go to" + targetLevel);
                StartCoroutine("load_level");
            }
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        showWin = true;
    }
    void OnTriggerExit(Collider other)
    {
        showWin = false;
    }
    
    IEnumerator load_level()
    {
        yield return new WaitForSeconds(1f);
        gTracker.CaptureEvent("enter "+targetLevel);
        Debug.Log("capture event");
        SceneManager.LoadScene(targetLevel);
    }
}
