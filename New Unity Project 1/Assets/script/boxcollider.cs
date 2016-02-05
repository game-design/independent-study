using UnityEngine;
using System.Collections;

public class boxcollider : MonoBehaviour
{

    public GameObject n_x;
    public GameObject p_x;
    public GameObject n_z;
    public GameObject p_z;
    sidewallCollider n_x_flag;
    sidewallCollider n_z_flag;
    sidewallCollider p_x_flag;
    sidewallCollider p_z_flag;
    Rigidbody boxbody;
    float force = 1;

    // Use this for initialization
    void Start()
    {
        n_x_flag = n_x.GetComponent<sidewallCollider>();
        n_z_flag = n_z.GetComponent<sidewallCollider>();
        p_x_flag = p_x.GetComponent<sidewallCollider>();
        p_z_flag = p_z.GetComponent<sidewallCollider>();
        boxbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (p_z_flag.triggered == 1 && n_z_flag.triggered == 0)
        {
            //Vector3 pos = transform.position;
            //newPosition = new Vector3(pos.x, pos.y, pos.z - 1);
            boxbody.AddForce(0,0,-1*force);
        }
        if (p_z_flag.triggered == 0 && n_z_flag.triggered == 1)
        {
            //Vector3 pos = transform.position;
            //Vector3 newPosition = new Vector3(pos.x, pos.y, pos.z +1);
            boxbody.AddForce(0, 0, force);
        }

        if (p_x_flag.triggered == 1 && n_x_flag.triggered == 0)
        {
            //Vector3 pos = transform.position;
            //Vector3 newPosition = new Vector3(pos.x-1, pos.y, pos.z);
            boxbody.AddForce(-1* force, 0, 0);
        }
        if (p_x_flag.triggered == 0 && n_x_flag.triggered == 1)
        {
            //Vector3 pos = transform.position;
            //Vector3 newPosition = new Vector3(pos.x + 1, pos.y, pos.z);
            boxbody.AddForce(force, 0, 0);
        }

        //rigidbody.velocity = (newPosition - pos) / Time.deltaTime;



    }


}
