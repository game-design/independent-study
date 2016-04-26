using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class winControl_Dodge : MonoBehaviour
{
    public Text timer;
    public Text score;
    public static float current_time;
    public static bool gameover;
    AudioSource win;
    public int step;
    Animation final;
    bool final_play;
    public static int amountofDeath = 0;

    Gloggr_Tracker gTracker;

    // Use this for initialization
    void Awake()
    {
        current_time = 0;
        gameover = false;
        win = GetComponent<AudioSource>();
        step = 0;
        final = GetComponent<Animation>();
        final_play = false;
        gTracker = GetComponent<Gloggr_Tracker>();
    }

    void reStart()
    {

        string message = "Dead";
        gTracker.CaptureEvent(message);
        Gloggr.Instance.PostEvents();

        gameover = false;
        step = 0;
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
            score.text = "Death: " + amountofDeath.ToString() + " Time: " + timer.text;
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
        SceneManager.LoadScene("Initial Room");
    }
}

