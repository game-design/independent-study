using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Gloggr_Tracker : MonoBehaviour {

	float cacheCheckRateInSeconds = 1.0f;

	public bool enableTracking = true;

	bool trackingPossible = false;
	Gloggr g;

	List<Gloggr_PlayEvent> cachedEvents = new List<Gloggr_PlayEvent>();

	// Use this for initialization
	void Start () {
		//TODO: Allow for per-object disabling of logging
		//TODO: Apply a gizmo to each Tracker object for easier finding in the Editor

		g = Gloggr.Instance;
		if (g == null)
		{
			Debug.LogError("Could not find Gloggr object. Make sure one GameObject in the scene has the Gloggr component.");
			trackingPossible = false;
		}
		else
			trackingPossible = enableTracking;

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/// <summary>
	/// Capture event specified by the game.
	/// </summary>
	/// <param name="gameEvent">Game event (in period-delimited string).</param>
	/// <param name="pos">Optional world position of event (NOTE only xy 2D coordinates captured).</param>
	/// <param name="fValue">Optional value of this event.</param>
	public void CaptureEvent(string gameEvent, Vector3? position = null, float? fValue = null)
	{
		Vector2? pos2D = null;
		if (position != null)
		{
			Vector3 pos = (Vector3)position;
			pos2D = new Vector2(pos.x, pos.y);
		}
		CaptureEvent2D(gameEvent, pos2D, fValue);
	}

	void StartCaching()
	{
		if (!IsInvoking("SendCache"))
			InvokeRepeating("SendCache", cacheCheckRateInSeconds, cacheCheckRateInSeconds);
	}

	void SendCache()
	{
		if (trackingPossible && cachedEvents.Count > 0)
		{
			//int cacheSize = cachedEvents.Count;
			foreach (Gloggr_PlayEvent e in cachedEvents)
				g.AddEvent (e);
			cachedEvents = new List<Gloggr_PlayEvent>();
			//Debug.Log (cacheSize + " cached event(s) sent.");
			CancelInvoke("SendCache");
		}
	}


	/// <summary>
	/// Capture event specified by the game.
	/// </summary>
	/// <param name="gameEvent">Game event (in period-delimited string).</param>
	/// <param name="pos">Optional position of event (in 2D space).</param>
	/// <param name="fValue">Optional value of this event.</param>
	public void CaptureEvent2D(string gameEvent, Vector2? pos = null, float? fValue = null)
	{
		if (!enableTracking)
			return;

		Gloggr_PlayEvent gEvent = new Gloggr_PlayEvent(gameEvent, Gloggr.NowTimeString(), pos, fValue);
		
		if (trackingPossible)
		{
			g.AddEvent(gEvent);
		}
		else
		{
			StartCaching();
			cachedEvents.Add (gEvent);
			//Debug.Log ("Gloggr not ready. Caching event.");
			//Debug.LogError("Received tracking data in " + gameObject.name + ", but tracking was not enabled.");
		}
	}

	void OnDestroy()
	{
		SendCache ();
		CancelInvoke("SendCache");
	}

}
