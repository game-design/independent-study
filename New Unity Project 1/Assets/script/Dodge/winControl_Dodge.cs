using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class winControl_Dodge : MonoBehaviour
{
    public Text timer;
    public Text score;
    float current_time;
    public static bool gameover;
    AudioSource win;
    public static int step;
    Animation final;
    bool final_play;
    int amountofDeath = 0;

    public int currentLevel;


    // Use this for initialization
    void Awake()
    {
        current_time = 0;
        gameover = false;
        win = GetComponent<AudioSource>();
        step = 0;
        final = GetComponent<Animation>();
        final_play = false;
    }

    void reStart()
    {
        current_time = 0;
        gameover = false;
        //win = GetComponent<AudioSource>();
        step = 0;
        //final = GetComponent<Animation>();
        final_play = false;
        amountofDeath++;
    }

    // Update is called once per frame
    void Update()
    {
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
            score.text = "Step: " + step.ToString() + " Time: " + timer.text;
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

IEnumerator load_level()
    {
        yield return new WaitForSeconds(3f);
        PauseMenu.currentPosition = currentLevel;
        //GameObject.Find("GameLogic").GetComponent<PauseMenu>().
        SceneManager.LoadScene("Initial Room");
    }
}

