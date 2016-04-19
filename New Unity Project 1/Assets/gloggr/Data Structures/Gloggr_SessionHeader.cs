using UnityEngine;
using System.Collections;
//using Newtonsoft.Json;

using SimpleJSON;



//[System.Serializable]
public class Gloggr_SessionHeader {
	
	/*
		Gloggr Session Header Format:
		"session": 
		{
			"id": "session1234",
			"player": "user123",
			"game": "game1",
			"version": "version 1.0"
		}

		All four fields are placed in the constructor.
	*/
	

	public string gSessionID;
	public string gPlayer;
	public string gGameName;
	public string gVersion;

	public Gloggr_SessionHeader(string _session)
	{
		gSessionID = _session;
		gPlayer = Gloggr.DEFAULT_UNDEFINED_CODE;
		gGameName = Gloggr.DEFAULT_UNDEFINED_CODE;
		gVersion = Gloggr.DEFAULT_UNDEFINED_CODE;
	}

	public Gloggr_SessionHeader(string _session, string _player, string _game, string _version)
	{
		gSessionID = _session;
		gPlayer = _player;
		gGameName = _game;
		gVersion = _version;
	}
	
	
	public static JSONNode ToJSONObject(Gloggr_SessionHeader e)
	{
		
		JSONNode n = new JSONClass();
		
		n.Add ("id", new JSONData( e.gSessionID));
		n.Add ("player", new JSONData(e.gPlayer));
		n.Add ("game", new JSONData(e.gGameName));
		n.Add ("version", new JSONData(e.gVersion));
		
		return n;
	}
		
	public static string ToJSON(Gloggr_SessionHeader e)
	{
	
		JSONNode n = ToJSONObject(e);
		
		return n.ToString();
		
	
//		string json = JsonConvert.SerializeObject(e);
//		json = FormatJSONKeys(json);
//		return json;
	}

//	public static string FormatJSONKeys(string serializedJSON)
//	{
//		string json = serializedJSON.Replace("gSessionID", "id");
//		json = json.Replace("gPlayer", "player");
//		json = json.Replace("gGameName", "game");
//		json = json.Replace("gVersion", "version");
//		return json;
//	}


	/// <summary>
	/// Tells the JsonSerializer if it should serialize the player field. Only writes field if four argument constructor called.
	/// </summary>
	/// <returns><c>true</c>, if player field was set, <c>false</c> otherwise.</returns>
	public bool ShouldSerializegPlayer()
	{
		return (gPlayer != Gloggr.DEFAULT_UNDEFINED_CODE);
		
	}

	/// <summary>
	/// Tells the JsonSerializer if it should serialize the gameName field. Only writes field if four argument constructor called.
	/// </summary>
	/// <returns><c>true</c>, if gameName field was set, <c>false</c> otherwise.</returns>
	public bool ShouldSerializegGameName()
	{
		return (gGameName != Gloggr.DEFAULT_UNDEFINED_CODE);
		
	}

	/// <summary>
	/// Tells the JsonSerializer if it should serialize the version field. Only writes field if four argument constructor called.
	/// </summary>
	/// <returns><c>true</c>, if version field was set, <c>false</c> otherwise.</returns>
	public bool ShouldSerializegVersion()
	{
		return (gVersion != Gloggr.DEFAULT_UNDEFINED_CODE);
		
	}
}
