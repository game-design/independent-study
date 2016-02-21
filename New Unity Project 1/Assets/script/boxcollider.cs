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
    float len = 1;
    float moveMent = 1.5f;
    // Use this for initialization
    void Start()
    {
        n_x_flag = n_x.GetComponent<sidewallCollider>();
        n_z_flag = n_z.GetComponent<sidewallCollider>();
        p_x_flag = p_x.GetComponent<sidewallCollider>();
        p_z_flag = p_z.GetComponent<sidewallCollider>();   
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        //Debug.DrawRay(transform.position, forward, Color.red);
        Vector3 left = transform.TransformDirection(Vector3.left);
        //Debug.DrawRay(transform.position, left, Color.green);
        Vector3 right = transform.TransformDirection(Vector3.right);
        //Debug.DrawRay(transform.position, right, Color.yellow);
        Vector3 back = transform.TransformDirection(Vector3.back);
        //Debug.DrawRay(transform.position, back, Color.white);

        if (p_z_flag.triggered == 1 )
        {
            RaycastHit back_hit;
            if (!Physics.Raycast(transform.position, back, out back_hit, len))
            {
                Vector3 pos = transform.position;
                Vector3 newPosition = new Vector3(pos.x, pos.y, pos.z - moveMent);
                transform.position = newPosition;
            }
        }

        if (n_z_flag.triggered == 1)
        {
            RaycastHit forward_hit;
            if (!Physics.Raycast(transform.position, forward, out forward_hit, len))
            {
                Vector3 pos = transform.position;
                Vector3 newPosition = new Vector3(pos.x, pos.y, pos.z + moveMent);
                transform.position = newPosition;
            }
        }

        if (p_x_flag.triggered == 1)
        {
            RaycastHit left_hit;
            if (!Physics.Raycast(transform.position, left, out left_hit, len))
            {
                Vector3 pos = transform.position;
                Vector3 newPosition = new Vector3(pos.x- moveMent, pos.y, pos.z);
                transform.position = newPosition;
            }
        }
        if (n_x_flag.triggered == 1)
        {
            RaycastHit right_hit;
            if (!Physics.Raycast(transform.position, right, out right_hit, len))
            {
                Vector3 pos = transform.position;
                Vector3 newPosition = new Vector3(pos.x+ moveMent, pos.y, pos.z);
                transform.position = newPosition;
            }
        }
    }


}
