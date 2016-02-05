using UnityEngine;
using System.Collections;

public class sidewallCollider : MonoBehaviour {

    public int triggered;

    void Start()
    {
        triggered = 0;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
            triggered = 1;
        else
            triggered = -1;
    }

    void OnCollisionExit(Collision collisionInfo)
    {
        triggered = 0;
    }


}
