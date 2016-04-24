using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class menuManager : MonoBehaviour {


    public Animator camera_animator;

    public Button credit;
    public Button back;
    public Button start;
    public Button quit;

    float waittime = 1.5f;

    GameObject billboard;
    Animation textRising;

    #if UNITY_WEBPLAYER
        public static string webplayerQuitURL = "http://google.com";
    #endif


    // Use this for initialization
    void Start () {
        back.gameObject.SetActive(false);
        billboard = GameObject.FindGameObjectWithTag("billboard");
        textRising = billboard.GetComponent<Animation>();
    }
	
	// Update is called once per frame
	void Update () {
       
	}


    
    public void triggerCreditAnimation()
    {
        StartCoroutine(credit_func());
    }

    public void triggerStartAnimation()
    {
        SceneManager.LoadScene("Initial Room");
    }

    public void triggerQuitApplication()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #elif UNITY_WEBPLAYER
                 Application.OpenURL(webplayerQuitURL);
        #else
                 Application.Quit();
        #endif
    }



    public void triggerBackAnimation()
    {
        StartCoroutine(back_func());
    }

  
    public IEnumerator credit_func()
    {
        credit.gameObject.SetActive(false);
        start.gameObject.SetActive(false);
        quit.gameObject.SetActive(false);
        
        camera_animator.SetTrigger("checkCredit");
        yield return new WaitForSeconds(waittime);
        back.gameObject.SetActive(true);
        if (!textRising.isPlaying)
            textRising.Play();
    }

    public IEnumerator back_func()
    {
        back.gameObject.SetActive(false);
        camera_animator.SetTrigger("backIntro");
        yield return new WaitForSeconds(waittime);       
        credit.gameObject.SetActive(true);
        start.gameObject.SetActive(true);
        quit.gameObject.SetActive(true);
    }



}
