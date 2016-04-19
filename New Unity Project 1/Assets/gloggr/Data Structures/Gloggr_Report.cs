using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//using Newtonsoft.Json;

using SimpleJSON;

[System.Serializable]
public class Gloggr_Report {

	public Gloggr_SessionHeader session;
	public List<Gloggr_PlayEvent> play_events;

	public Gloggr_Report(Gloggr_SessionHeader _session, List<Gloggr_PlayEvent> _play_events)
	{
		session = _session;
		play_events = _play_events;
	}

	//JSON Format:

	/*
	{
		"session": {
			"id": "session1234",
			"player": "user123",
			"game": "game1",
			"version": "version 1.0"
		},
		
		"play_events" :
		[ 
		{  "time": "2015-02-17T22:43:45-5:00", "event": "PowerUp.FireBall", "value": "1.0", "level": "1-1"},
		{  "time": "2015-02-17T22:45:45-5:00", "event": "PowerUp.Mushroom", "value": "2.0", "level": "1-1"}
		 ]
	}
	*/

	public static string ToJSON(Gloggr_Report r)
	{
	
		JSONNode n = new JSONClass();
		
		
		
		n.Add ("session",  Gloggr_SessionHeader.ToJSONObject(r.session)  );
		
		JSONArray a = new JSONArray();
		
		foreach(Gloggr_PlayEvent e in r.play_events)
		{
			a.Add(Gloggr_PlayEvent.ToJSONObject(e));
		}
		
		n.Add ("play_events", a);
		
		return n.ToString();	
	
//		string json = JsonConvert.SerializeObject(e, Formatting.Indented);
//		//from Gloggr_SessionHeader.ToJSON
//		//json = Gloggr_SessionHeader.FormatJSONKeys(json);
//		//from Gloggr_PlayEvent.ToJSON
//		//json = Gloggr_PlayEvent.FormatJSONKeys(json);
//		return json;
	}

}
