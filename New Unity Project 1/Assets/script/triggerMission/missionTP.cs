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
            GUI.Box(new Rect(3*Screen.width / 8, 3*Screen.height / 8, Screen.width / 4, Screen.height / 8), "Are you sure about going to "+targetLevel+"?");
            if (GUI.Button(new Rect(7*Screen.width / 16, 7*Screen.height / 16, Screen.width / 8, Screen.height /16), "sure"))
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
        SceneManager.LoadScene(targetLevel);
    }
}
