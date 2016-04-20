using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class menuManager : MonoBehaviour {


    public Animator camera_animator;

    public Button credit;
    public Button back;
    public Button start;
    

	// Use this for initialization
	void Start () {
        back.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
	
	}


    public void triggerCreditAnimation()
    {
        camera_animator.SetTrigger("checkCredit");
        back.gameObject.SetActive(true);
        credit.gameObject.SetActive(false);
        start.gameObject.SetActive(false);
    }

    public void triggerBackAnimation()
    {
        camera_animator.SetTrigger("backIntro");
        back.gameObject.SetActive(false);
        credit.gameObject.SetActive(true);
        start.gameObject.SetActive(true);
    }



}
