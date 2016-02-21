using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class winCondition : MonoBehaviour {
    Text timer;
    float current_time;
    bool gameover;
    int size=4;
    public GameObject[] targets=new GameObject[4];

	// Use this for initialization
	void Awake () {
        timer = GetComponentInChildren<Text>();
        current_time = 0;
        gameover = false;
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
    }
}
