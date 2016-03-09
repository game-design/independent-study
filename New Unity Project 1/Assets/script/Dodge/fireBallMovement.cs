using UnityEngine;
using System.Collections;
//using System;

public class fireBallMovement : MonoBehaviour {

    public float velocity = 20f;
    public float velocity2 = 20f;
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
            this.transform.Translate(new Vector3(0, 0, -velocity2 * Time.deltaTime));
        }
	}

    void OnTriggerEnter(Collider other)
    {
        
        if(other.gameObject.name == "Cha_Knight")
        {
            //GameObject.Find("GameLogic").GetComponent<PauseMenu>().isDead = true;
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


    }
}
