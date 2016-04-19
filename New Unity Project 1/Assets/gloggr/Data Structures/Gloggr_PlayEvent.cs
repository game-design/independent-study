using UnityEngine;
using System.Collections;
//using Newtonsoft.Json;

using SimpleJSON;

//[System.Serializable]
public class Gloggr_PlayEvent {
	
	/*
		Gloggr PlayEvent Format v2:
		"play_events" :
		[ 
			{  "time": "2015-02-17T22:43:45-5:00", "event": "PowerUp.FireBall", "value": "1.0", "level": "1-1"},
			{  "time": "2015-02-17T22:45:45-5:00", "event": "PowerUp.Mushroom", "value": "2.0", "level": "1-1"}
		]

		First three are placed in the constructor. Level is added by the main manager (Gloggr) before storage.
	*/

	public string gTime; //ISO-8601 w/time-zone format
	public string gEvent; 
	public float? gValue; //nullable value declaration to allow for null checks to exclude this field from JSON when undefined
	public string gLevel; 
	public string gPosition;

	#pragma warning disable 0414
	Vector2 gPositionVector; //collects Vector2 that is converted to string format
	#pragma warning restore 0414

	/*
	public Gloggr_PlayEvent(string _event, string _time, Vector2 _pos = default(Vector2))
	{
		gEvent = ValidateEventInput(_event);
		gTime = _time;
		gValue = null; //Gloggr.DEFAULT_UNDEFINED_CODE;
		gLevel = Gloggr.DEFAULT_UNDEFINED_CODE;
		gPosition = _pos;
	}
	*/

	public Gloggr_PlayEvent(string _event, string _time, Vector2? _pos = null, float? _value = null)
	{
		gEvent = ValidateEventInput(_event);
		gTime = _time;
		gValue = _value;//.ToString();
		gLevel = string.Empty;	//level is updated on the Gloggr Component, which updates this value before processing

		if (_pos != null)
		{
			gPositionVector = (Vector2)_pos;
			gPosition = "(" + gPositionVector.x.ToString() + "," + gPositionVector.y.ToString() + ")";
		}
	}

	string ValidateEventInput(string e)
	{
		//string ePeriodSplit = e.Replace (':', '.'); //server expects period separated input
		string[] categories = e.Split('.');
		string validatedCategories = "";
		foreach (string category in categories)
		{
			if (category.Length > 0)
			{
				string output = category.Replace (' ', '_'); //server does not handle spaces
				validatedCategories += (output);
				validatedCategories += ".";
			}
		}
		string eventData = validatedCategories.TrimEnd ('.');
		return eventData;
	}
	
	public void AddLevel(string level)
	{
		gLevel = level;
	}
	
	
	public static JSONNode ToJSONObject(Gloggr_PlayEvent e)
	{
		JSONNode n = new JSONClass();
		
		n.Add ("time", new JSONData( e.gTime));
		n.Add("event", new JSONData(e.gEvent));
		n.Add("value", new JSONData(e.gValue));
		n.Add("position", new JSONData(e.gPosition));
		n.Add("level", new JSONData(e.gLevel));
		
		return n;
	}
	
	public static string ToJSON(Gloggr_PlayEvent e)
	{
//		string json = JsonConvert.SerializeObject(e);
//		json = FormatJSONKeys(json);
//		return json;

		JSONNode n = ToJSONObject(e);
		
		return n.ToString();
	}

//	public static string FormatJSONKeys(string serializedJson)
//	{
//		string json = serializedJson.Replace("gTime", "time");
//		json = json.Replace("gEvent", "event");
//		json = json.Replace("gValue", "value");
//		json = json.Replace("gPosition", "position");
//		json = json.Replace("gLevel", "level");
//		return json;
//	}


	/// <summary>
	/// Tells the JsonSerializer if it should serialize the value field. Only writes field if value was set in the constructor.
	/// </summary>
	/// <returns><c>true</c>, if value field was set, <c>false</c> otherwise.</returns>
	public bool ShouldSerializegValue()
	{
		return (gValue != null);//Gloggr.DEFAULT_UNDEFINED_CODE);
	}

	/// <summary>
	/// Tells the JsonSerializer if it should serialize the level field. Only writes field if AddLevel() was called.
	/// </summary>
	/// <returns><c>true</c>, if value field was set, <c>false</c> otherwise.</returns>
	public bool ShouldSerializegLevel()
	{
		return (gLevel != string.Empty);
	}

	/// <summary>
	/// Tells the JsonSerializer if it should serialize the value field. Only writes field if value was set in the constructor.
	/// </summary>
	/// <returns><c>true</c>, if value field was set, <c>false</c> otherwise.</returns>
	public bool ShouldSerializegPosition()
	{
		return (gPosition != null);//Gloggr.DEFAULT_UNDEFINED_CODE);
	}
}
