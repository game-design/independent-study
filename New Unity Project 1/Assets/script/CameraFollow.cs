﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CameraFollow : MonoBehaviour {
	public Transform target;
	public float smoothing= 2f;
	static Vector3 initRoom_Offset;
    Vector3 offset;
    public static bool initCam = false;
	void Start()
	{
        //这样只有在游戏开始时来决定在init room中摄像机的位置，而不是每次从mission中返回时
        if (!initCam && SceneManager.GetActiveScene().name == "Initial Room")
        {
            initCam = true;
            initRoom_Offset = transform.position - target.position;
            setOffset();
        }
        if (SceneManager.GetActiveScene().name != "Initial Room")
        {
            offset = transform.position - target.position;
        }
	}

    public Vector3 getOffset(){
        return initRoom_Offset;
    }

    public void setOffset()
    {
        offset =  initRoom_Offset;
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
