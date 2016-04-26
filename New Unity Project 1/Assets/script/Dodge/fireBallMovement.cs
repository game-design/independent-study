 using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
//using System;

public class fireBallMovement : MonoBehaviour {

    public float velocity = 20f;
    public float velocity2 = 15f;


	// from 1 to 10 to determine which part of Bezier
    // all curve movements
	public int positionBezier;
	Vector3 positionB0 = new Vector3 (-67, 3, -7);
	Vector3 positionB1 = new Vector3 (-39, 3, -50);
	Vector3 positionB2 = new Vector3  (-19, 3, -50);
	Vector3 positionB3 = new Vector3 (10, 3, -10);
	Vector3 basePositionB = new Vector3 (-61,-1,-142);

	Vector3 positionB0_2 = new Vector3 (-29, 3, -11);
	Vector3 positionB1_2 = new Vector3 (-10, 3, -42);
	Vector3 positionB2_2 = new Vector3  (12, 3, -42);
	Vector3 positionB3_2 = new Vector3 (32, 3, -11);
	Vector3 basePositionB_2 = new Vector3 (-91,-1,-135);

	Vector3 positionB0_3 = new Vector3 (-25, 3, -13);
	Vector3 positionB1_3 = new Vector3 (-10, 3, -40);
	Vector3 positionB2_3 = new Vector3  (12, 3, -40);
	Vector3 positionB3_3 = new Vector3 (30, 3, -16);
	Vector3 basePositionB_3 = new Vector3 (-91,-1,-120);

    Vector3 positionB0_4 = new Vector3(-95, -11, -202);
    Vector3 positionB1_4 = new Vector3(-105, 16, -202);
    Vector3 positionB2_4 = new Vector3(-74, 16, -202);
    Vector3 positionB3_4 = new Vector3(-82, -11, -202);
	long timeBezier = System.DateTime.Now.Ticks;


    public double showTimer;
    public int showTimerController;
    Vector3 iterMove = new Vector3(0.2f, 0, -0.2f);
	// Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{

        if (this.name == "fireBall_1")
        {
            this.transform.Translate(new Vector3(0, 0, -velocity * Time.deltaTime));
        }

        if(this.name == "fireBall_2")
        {
            this.transform.Translate(new Vector3(0 ,0 , -velocity2 * Time.deltaTime));
        }

		if (this.name == "fireBall_3") 
		{
			//Debug.Log (((System.DateTime.Now.Ticks - timeBezier)%10000000)/100000000);
			this.transform.position = Bezier3(positionB0+basePositionB,positionB1+basePositionB,positionB2+basePositionB,positionB3+basePositionB,((System.DateTime.Now.Ticks - timeBezier + positionBezier * 10000000)%75000000)/75000000f);			
		}

		if (this.name == "fireBall_4") 
		{
			//Debug.Log (((System.DateTime.Now.Ticks - timeBezier)%10000000)/100000000);
			this.transform.position = Bezier3(positionB0_2+basePositionB_2,positionB1_2+basePositionB_2,positionB2_2+basePositionB_2,positionB3_2+basePositionB_2,((System.DateTime.Now.Ticks - timeBezier + positionBezier * 10000000)%100000000)/100000000f);			
		}

		if (this.name == "fireBall_5") 
		{
			//Debug.Log (((System.DateTime.Now.Ticks - timeBezier)%10000000)/100000000);
			this.transform.position = Bezier3(positionB0_3+basePositionB_3,positionB1_3+basePositionB_3,positionB2_3+basePositionB_3,positionB3_3+basePositionB_3,((System.DateTime.Now.Ticks - timeBezier + positionBezier * 10000000)%100000000)/100000000f);			
		}

        if (this.name == "fireBall_6")
        {
            showTimer = showTimer + Time.deltaTime;
            if(showTimer > 2.0){
                this.transform.Translate(new Vector3(0, -20 * showTimerController, 0));
                showTimer = showTimer % 2.0;
                showTimerController = -showTimerController;
            }
        }

        if (this.name == "fireBall_7")
        {
            showTimer = showTimer + Time.deltaTime;
            if (showTimer > 1.3)
            {
                this.transform.Translate(new Vector3(0, -20 * showTimerController, 0));
                showTimer = showTimer % 1.3;
                showTimerController = -showTimerController;
            }
        }
        if (this.name == "fireBall_8")
        {
            float myTemp = this.transform.position.x;
            if (myTemp < -104 || myTemp > -87)
            {
                iterMove = iterMove * -1;
            }
            this.transform.Translate(iterMove);
        }

        if (this.name == "fireBall_9")
        {
            float myTemp = this.transform.position.x;
            if (myTemp < -92  || myTemp > -76)
            {
                iterMove = iterMove * -1;
            }
            this.transform.Translate(iterMove);
        }

        if (this.name == "fireBall_10")
        {
            //Debug.Log (((System.DateTime.Now.Ticks - timeBezier)%10000000)/100000000);
            this.transform.position = Bezier3(positionB0_4, positionB1_4, positionB2_4, positionB3_4, ((System.DateTime.Now.Ticks - timeBezier + positionBezier * 10000000) % 100000000) / 100000000f);
        }

	}

	Vector3 Bezier3(Vector3 s,Vector3 st,Vector3 et,Vector3 e,float t)
	{
		return (((-s + 3*(st-et) + e)* t + (3*(s+et) - 6*st))* t + 3*(st-s))* t + s;
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
            this.transform.Translate(new Vector3(0, 0, 115));
        }

		if (other.gameObject.name == "airWall7")
		{
			this.velocity2 = Random.Range(12, 15);
			this.transform.Translate(new Vector3(0, 0, 80));
		}

    }
}
