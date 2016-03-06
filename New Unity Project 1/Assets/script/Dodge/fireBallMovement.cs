﻿using UnityEngine;
using System.Collections;

public class fireBallMovement : MonoBehaviour {

    float velocity = 15f;
    float velocity2 = 20f;
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
            this.transform.Translate(new Vector3(0, velocity2 * Time.deltaTime, 0));
        }
	}

    void OnTriggerEnter(Collider other)
    {
        
        if(other.gameObject.name == "Cha_Knight")
        {
            GameObject.Find("GameLogic").GetComponent<PauseMenu>().isDead = true;
            //Time.timeScale = 0;
        }

        if(other.gameObject.name == "airWall1")
        {
            this.transform.Translate(new Vector3(0, 0, 110));
        }

        if (other.gameObject.name == "airWall2")
        {
            this.transform.Translate(new Vector3(0, -90, 0));
        }


    }
}
