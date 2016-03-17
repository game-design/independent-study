using UnityEngine;
using System.Collections;

public class objWobble : MonoBehaviour {

    float direction = 0.1f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Mathf.Abs(this.transform.position.y) < 7.7 && Mathf.Abs(this.transform.position.y) > 2.7) {
            this.transform.Translate(new Vector3(0,direction,0));
        }
        else
        {
            direction = -direction;
            this.transform.Translate(new Vector3(0, direction, 0));
        }
	}

    void OnTriggerEnter(Collider other)
    {
        Destroy(this);
    }
}
