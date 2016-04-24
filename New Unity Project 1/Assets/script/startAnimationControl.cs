using UnityEngine;
using System.Collections;

public class startAnimationControl : MonoBehaviour {

    Rigidbody playerRigidbody;
    Animation anim;
    Vector3 movement;
    public Transform leftPoint;
    public Transform rightPoint;
    bool left;

    float speed = 35f;

    public GameObject box;
    public GameObject fireball;

    // Use this for initialization
    void Start () {
        playerRigidbody = GetComponent<Rigidbody>();
        anim = GetComponent<Animation>();
        playerRigidbody.position = leftPoint.position;
        left = true;
        fireball.SetActive(false);
        box.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
        if (left)
        {
            movement.Set(1f, 0f, 0f);
            movement = movement.normalized * speed * Time.deltaTime;
            playerRigidbody.MovePosition(transform.position + movement);

            Quaternion newRotation = Quaternion.LookRotation(movement);
            playerRigidbody.MoveRotation(newRotation);
        }
        else
        {
            movement.Set(-1f, 0f, 0f);
            movement = movement.normalized * speed * Time.deltaTime;
            playerRigidbody.MovePosition(transform.position + movement);

            Quaternion newRotation = Quaternion.LookRotation(movement);
            playerRigidbody.MoveRotation(newRotation);
        }

        if (playerRigidbody.position.x < leftPoint.position.x)
        {
            left = true;
            fireball.SetActive(false);
            box.SetActive(true);
        }
        if (playerRigidbody.position.x > rightPoint.position.x)
        {
            left = false;
            fireball.SetActive(true);
            box.SetActive(false);
        }

	}
}
