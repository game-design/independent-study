using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class winCondition : MonoBehaviour {
    public Text timer;
    public Text score;
    float current_time;
    bool gameover;
    int size=4;
    public GameObject[] targets=new GameObject[4];
    AudioSource win;
    public static int step;
    Animation final;
	// Use this for initialization
	void Awake () {
        current_time = 0;
        gameover = false;
        win = GetComponent<AudioSource>();
        step = 0;
        final = GetComponent<Animation>();
    }
	
	// Update is called once per frame
	void Update () {
        gameover = true;
        for (int i = 0; i < 4; i++)
        {
            RaycastHit up_hit;
            Debug.DrawRay(targets[i].transform.position, Vector3.up, Color.red);
            if (!Physics.Raycast(targets[i].transform.position, Vector3.up, out up_hit, 1))//not hit
            {
                gameover = false;
                break;
            }
        }


        if (!gameover)
        {
            current_time += Time.deltaTime;
            int minutes = Mathf.FloorToInt(current_time / 60F);
            int seconds = Mathf.FloorToInt(current_time - minutes * 60);
            timer.text = string.Format("{0:0}:{1:00}", minutes, seconds);
        }
        else
        {
            StartCoroutine("load_level");
            StartCoroutine("Final_animation");
            score.text = "Step: " + step.ToString() + " Time: "+ timer.text;
            if (!win.isPlaying)
            {
                win.Play();
            }
            
        }
    }


    IEnumerator load_level()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("box_0");
    }
    IEnumerator Final_animation()
    {
        yield return null;
        final.Play();
        
    }


}
