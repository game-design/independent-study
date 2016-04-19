using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Gloggr_Tracker))]
public class Gloggr_Example_Event : MonoBehaviour {

	Gloggr_Tracker gTracker;

	// Use this for initialization
	void Awake () {
		gTracker = GetComponent<Gloggr_Tracker>();
	
	}
	
	// Update is called once per frame
	void Update () {
        //gTracker.CaptureEvent();
    }

}
