using UnityEngine;
using System.Collections;

public class fireBallMovement : MonoBehaviour {

    float velocity = 20f;
    float velocity2 = 20f;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (this.name == "Fireball_1")
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
            Time.timeScale = 0;
            //Application.Quit();
        }

        if(other.gameObject.name == "airWall1")
        {
            this.transform.Translate(new Vector3(0, -120, 0));
        }

        if (other.gameObject.name == "airWall2")
        {
            this.transform.Translate(new Vector3(0, -90, 0));
        }


    }
}
