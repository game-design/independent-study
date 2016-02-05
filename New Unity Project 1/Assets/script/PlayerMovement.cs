using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float speed=18f;
	Vector3 movement;
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
		Turning ();
		Animating (h, v);
	}

	void Move(float h,float v)
	{
		movement.Set (h, 0f, v);
		movement = movement.normalized * speed * Time.deltaTime;
		playerRigidbody.MovePosition (transform.position + movement);
        /*
        // keep character stand~ No x,z rotation.
        float CurRoaX = transform.rotation.x;
        float CurRoaZ = transform.rotation.z;
        float CurRoaY = transform.rotation.y;
        float CurRoaW = transform.rotation.w;
        
        transform.rotation.Set(-CurRoaX/2, CurRoaY, -CurRoaZ/2, CurRoaW);
        */
	}

	void Turning()
	{
		Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit floorHit;
		if (Physics.Raycast (camRay, out floorHit, camRayLength, floorMask)) 
		{
			Vector3 playerToMouse=floorHit.point-transform.position;
			playerToMouse.y=0f;
			Quaternion newRotation=Quaternion.LookRotation(playerToMouse);
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
