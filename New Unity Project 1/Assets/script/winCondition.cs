using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class winCondition : MonoBehaviour {
    public Text timer;
    public Text score;
    float current_time;
    public static bool gameover;
    int size=4;
    public GameObject[] targets=new GameObject[4];
    AudioSource win;
    public static int step;
    Animation final;
    bool final_play;

    public int currentLevel;


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
            if (final_play)
            {
                PauseMenu.currentPosition = currentLevel;
                if (teleporterForBox.ready_to_go)
                {
                    SceneManager.LoadScene("Initial Room");
                }
            }
            
        }
    }

    void check_hit()
    {
        RaycastHit up_hit=new RaycastHit();
        int i;
        for (i = 0; i < 4; i++)
        {
            Debug.DrawRay(targets[i].transform.position, Vector3.up, Color.red);
            if (!Physics.Raycast(targets[i].transform.position, Vector3.up, out up_hit, 3)||up_hit.collider.gameObject.tag != "box")//not hit
            {
                gameover = false;
                if (targets[i].GetComponent<ParticleSystem>().isStopped)
                    targets[i].GetComponent<ParticleSystem>().Play();
            }
            else
            {
                targets[i].GetComponent<ParticleSystem>().Stop();
            }
            
            
        }


    }

}
