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
        offset.x = offset.x * 1.5f;
        offset.y = offset.y * 1.5f;
        offset.z = offset.z * 1.5f;
    }

    void zoomIn()
    {
        offset.x = offset.x / 1.5f;
        offset.y = offset.y / 1.5f;
        offset.z = offset.z / 1.5f;
    }

}
