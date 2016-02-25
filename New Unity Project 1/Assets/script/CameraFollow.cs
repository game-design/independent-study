using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	public Transform target;
	public float smoothing= 5f;
	Vector3 offset;
	void Start()
	{
		offset = transform.position - target.position;

	}
	void FixedUpdate()
	{
		Vector3 targetCamPos = target.position + offset;
		transform.position=Vector3.Lerp(transform.position,targetCamPos,smoothing*Time.deltaTime);
	}

    void zoomOut()
    {
        offset.x = offset.x * 2;
        offset.y = offset.y * 2;
        offset.z = offset.z * 2;
    }

    void zoomIn()
    {
        offset.x = offset.x / 2;
        offset.y = offset.y / 2;
        offset.z = offset.z / 2;
    }

}
