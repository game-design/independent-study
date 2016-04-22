using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class menuManager : MonoBehaviour {


    public Animator camera_animator;

    public Button credit;
    public Button back;
    public Button start;

    float waittime = 1.5f;

    GameObject billboard;
    Animation textRising;
    

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


    public void triggerBackAnimation()
    {
        StartCoroutine(back_func());
    }

  
    public IEnumerator credit_func()
    {
        credit.gameObject.SetActive(false);
        start.gameObject.SetActive(false);
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
    }



}
