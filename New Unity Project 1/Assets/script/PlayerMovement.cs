using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float speed=18f;
	Vector3 movement, rotationY;
	Animation anim;
	Rigidbody playerRigidbody;
	int floorMask;
	float camRayLength=100;
	void Awake()
	{
		floorMask=LayerMask.GetMask("Floor");
		anim = GetComponent<Animation> ();
		playerRigidbody = GetComponent<Rigidbody> ();
	}

	void FixedUpdate()
	{
		float h=Input.GetAxisRaw("Horizontal");
		float v=Input.GetAxisRaw("Vertical");
		Move (h, v);
		Turning (h,v);
		Animating (h, v);
	}

	void Move(float h,float v)
	{

        movement.Set (h, 0f, v);
		movement = movement.normalized * speed * Time.deltaTime;
		playerRigidbody.MovePosition (transform.position + movement);
        
    }

	void Turning(float h,float v)
	{
        if (h > 0)
        {
            rotationY.Set(1f, 0f, 0f);
            Quaternion newRotation = Quaternion.LookRotation(rotationY);
            playerRigidbody.MoveRotation(newRotation);
        }
        else if (h < 0)
        {
            rotationY.Set(-1f, 0f, 0f);
            Quaternion newRotation = Quaternion.LookRotation(rotationY);
            playerRigidbody.MoveRotation(newRotation);
        }

        if (v > 0)
        {
            rotationY.Set(0f, 0f, 1f);
            Quaternion newRotation = Quaternion.LookRotation(rotationY);
            playerRigidbody.MoveRotation(newRotation);
        }
        else if (v < 0)
        {
            rotationY.Set(0f, 0f, -1f);
            Quaternion newRotation = Quaternion.LookRotation(rotationY);
            playerRigidbody.MoveRotation(newRotation);
        }

    }

	void Animating(float h,float v)
	{
		bool walking = (h != 0f || v != 0f);
        if (walking)
            anim.Play("Walk");
        else
            anim.Play("Wait");   
	}






}
