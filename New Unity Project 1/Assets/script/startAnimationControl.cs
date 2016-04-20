using UnityEngine;
using System.Collections;

public class startAnimationControl : MonoBehaviour {

    Rigidbody playerRigidbody;
    Animation anim;
    Vector3 movement;
    public Transform leftPoint;
    public Transform rightPoint;
    bool left;
    // Use this for initialization
    void Start () {
        playerRigidbody = GetComponent<Rigidbody>();
        anim = GetComponent<Animation>();
        playerRigidbody.position = leftPoint.position;
        left = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (left)
        {
            movement.Set(1f, 0f, 0f);
            movement = movement.normalized * 45f * Time.deltaTime;
            playerRigidbody.MovePosition(transform.position + movement);

            Quaternion newRotation = Quaternion.LookRotation(movement);
            playerRigidbody.MoveRotation(newRotation);
        }
        else
        {
            movement.Set(-1f, 0f, 0f);
            movement = movement.normalized * 45f * Time.deltaTime;
            playerRigidbody.MovePosition(transform.position + movement);

            Quaternion newRotation = Quaternion.LookRotation(movement);
            playerRigidbody.MoveRotation(newRotation);
        }

        if (playerRigidbody.position.x < leftPoint.position.x)
            left = true;
        if (playerRigidbody.position.x > rightPoint.position.x)
            left = false;

	}
}
