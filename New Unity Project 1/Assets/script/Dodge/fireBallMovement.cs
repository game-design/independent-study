using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
//using System;

public class fireBallMovement : MonoBehaviour {

    public float velocity = 20f;
    public float velocity2 = 15f;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (this.name == "fireBall_1")
        {
            this.transform.Translate(new Vector3(0, 0, -velocity * Time.deltaTime));
        }

        if(this.name == "fireBall_2")
        {
            this.transform.Translate(new Vector3(0 ,0 , -velocity2 * Time.deltaTime));
        }
	}

    void OnTriggerEnter(Collider other)
    {
        
        if(other.gameObject.name == "Cha_Knight")
        {
            GameObject.FindGameObjectWithTag("Player").transform.position = new Vector3(-89, 0, -224);
            GameObject.Find("HUD").SendMessage("reStart");
        }

        if(other.gameObject.name == "airWall1")
        {
            this.velocity = Random.Range(19, 21);
            this.transform.Translate(new Vector3(0, 0, 105));
        }

        if (other.gameObject.name == "airWall2")
        {
            this.velocity = Random.Range(19, 21);
            this.transform.Translate(new Vector3(0, 0, 80));
        }

        if (other.gameObject.name == "airWall3")
        {
            this.velocity = Random.Range(19, 21);
            this.transform.Translate(new Vector3(0, 0, 125));
        }

        if (other.gameObject.name == "airWall4")
        {
            this.velocity = Random.Range(19, 21);
            this.transform.Translate(new Vector3(0, 0, 100));
        }

        if (other.gameObject.name == "airWall5")
        {
            this.velocity2 = Random.Range(13, 17);
            this.transform.Translate(new Vector3(0, 0, 68));
        }

        if (other.gameObject.name == "airWall6")
        {
            this.velocity2 = Random.Range(13, 17);
            this.transform.Translate(new Vector3(0, 0, 105));
        }

    }
}
