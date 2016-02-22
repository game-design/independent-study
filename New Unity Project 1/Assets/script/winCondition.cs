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
    bool final_play;
	// Use this for initialization
	void Awake () {
        current_time = 0;
        gameover = false;
        win = GetComponent<AudioSource>();
        step = 0;
        final = GetComponent<Animation>();
        final_play = false;
    }
	
	// Update is called once per frame
	void Update () {
        gameover = true;
        check_hit();

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
            score.text = "Step: " + step.ToString() + " Time: "+ timer.text;
            if (!win.isPlaying)
            {
                win.Play();
            }
            if (!final_play)
            {
                final.Play();
                final_play = true;
            }
            
        }
    }

    void check_hit()
    {
        RaycastHit[] up_hit=new RaycastHit[4];
        int i;
        for (i = 0; i < 4; i++)
        {
            Debug.DrawRay(targets[i].transform.position, Vector3.up, Color.red);
            if (!Physics.Raycast(targets[i].transform.position, Vector3.up, out up_hit[i], 3))//not hit
            {
                gameover = false;
            }
            else
            {
                Debug.Log(i+" "+up_hit[i].collider.gameObject.name);
                if (up_hit[i].collider.gameObject.name == "Quad")
                    gameover = false;
            }
            
        }


    }



    IEnumerator load_level()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("box_0");
    }
    


}
