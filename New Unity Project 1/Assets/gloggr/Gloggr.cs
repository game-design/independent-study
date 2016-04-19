using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Gloggr : MonoBehaviour {

	//TODO fix back to public 
	public bool isActive = true;

	public static Gloggr Instance
	{
		get 
		{
			return _instance;
		}
	}
	private static Gloggr _instance;

	bool _isInitialized = false;
	public bool isInitialized { get { return _isInitialized; } }

	public static string DEFAULT_UNDEFINED_CODE = "X_999_X";
	public static string NAME = @"Gloggr";

	#pragma warning disable 0649
	//add functions to the debugLogUpdate delegate to log errors across different views (Editor, WebPlayer debug)
	public delegate void DebugLogUpdate(string message);
	public DebugLogUpdate debugLogUpdate;
	#pragma warning restore 0649

	public bool initializeOnAwake = false;

	public enum ServerPostMode 
	{ 
		Off, 		// no data is sent
		Discrete,	// data is sent, but only when PostEvents() is called explicitly
		Periodic 	// data is sent automatically set periodically at the set interval defined below
	}

	public ServerPostMode postMode = ServerPostMode.Discrete;

	public float updateFrequency = 5.0f;

	string serverURL = @"http://actdev.imtc.gatech.edu/actlog/gameplay/";

	public string developmentURL = @"http://nodejs-dev.imtc.gatech.edu:8081/gameplay/";
	public string releaseURL = @"/actlog/gameplay/"; 


	public string gameID = "8809"; //(short) gameID as established through initial message
	//public string gameName = "Likert Invaders"; //friendly name

    public string secret = "M7RKdi-yyHPWQm1gfDpzxXCB1XiuTcUvRKlIHd9HItg";  // Secret token hashed with posted game events

	public string currentSession;
	public string currentPlayer;
	public string gameVersion;
	public string currentLevel;

	public string configurationID = "U"; //undefined
	//used to denote experimental condition, difficulty, defined by WebBridge
	//if the WebBridge is not active for some reason it will default to whatever is assigned above/inEditor

	public bool debugEventCapture = false;
	public bool debugJSON = false;

	List<Gloggr_PlayEvent> events;
	List<Gloggr_PlayEvent> processedEvents;

	int eventCount; //count of number of events sent to the server, embedded as Value in final Gloggr termination message

	public bool postToServerNow = false; //when true, this will send all currently accumulated data to the server immediately

	Gloggr_PlayEvent earlyTerminationEvent;

	protected float time;

	public static string NowTimeString()
	{
		return System.DateTime.UtcNow.ToString ("o");
	}

	public string configSessionHeader
	{
		get { return configurationID + "_" + currentSession + "."; } 
	}

	bool eventsArePosting = false;

	//TODO: Define events by Level of Detail in the Tracker, allow for more/less granular logging in the future.

	// Use this for initialization
	void Awake () {
		_instance = this;
		gameObject.name = NAME;
		if (Application.isEditor)
			serverURL = developmentURL;
		else
			serverURL = releaseURL;
		events = new List<Gloggr_PlayEvent>();
		processedEvents = new List<Gloggr_PlayEvent>();
		ClearProcessedEvents(); //used if length checks are used in the future on events sent to the server
		if (initializeOnAwake)
			Initialize();
	}

	public void Initialize() {
		_isInitialized = true;
		debugLogUpdate += DebugLog;

		currentSession = configurationID + "_" + System.Guid.NewGuid().ToString();

		debugLogUpdate ("Gloggr initialized: " + Gloggr.Instance.name);
		PostSystemEventNow("GloggrSystem.Start");
        
        time = Time.fixedTime;
		eventCount = 0;
		
	}
	
	// Update is called once per frame
	void Update () {
	
		

		if (!isInitialized || !isActive)
			return;

//		if ((postMode == ServerPostMode.Periodic) && !(IsInvoking("PostEvents")))
//			Invoke ("PostEvents", updateFrequency);

//		debugLogUpdate("gloggr activ/init");

		if(postMode == ServerPostMode.Periodic)
		{
			//checking 'eventsArePosting' delays the call until the current posting operation is finished
			//useful if PostEventNow() is called with a special one-time event and it has not completed its operation
			if(!eventsArePosting && (Time.fixedTime - time > updateFrequency)) 
			{
				time = Time.fixedTime;
				PostEvents();
			}
			
		}

		if (postToServerNow)
		{
			PostEvents();
			postToServerNow = false;
		}
	}

	public void AddEvent(Gloggr_PlayEvent g)
	{
		if (!isActive)
			return;
		/*
		if (!g.gEvent.Contains(configSessionHeader))
		    g.gEvent = configSessionHeader + g.gEvent;
		*/
		AddCurrentLevelToEvent(ref g);
		events.Add (g);
		if (debugEventCapture)
			DebugLog(Gloggr_PlayEvent.ToJSON(g));
	}

	public void AddCurrentLevelToEvent(ref Gloggr_PlayEvent gEvent)
	{
		if (!string.IsNullOrEmpty(currentLevel))
			gEvent.AddLevel(currentLevel);
	}

	public void PostSystemEventNow(string systemEventText, float? fValue = null)
	{
		string eventText = "GloggrSystem." + systemEventText;
		Gloggr_PlayEvent systemEvent = new Gloggr_PlayEvent(eventText, Gloggr.NowTimeString(), null, fValue);
		PostEventNow(systemEvent);
	}

	public void AddConfigurationEvent(string configEventName, float? fValue = null)
	{
		string eventText = "Configuration." + configEventName;
		Gloggr_PlayEvent configEvent = new Gloggr_PlayEvent(eventText, Gloggr.NowTimeString(), null, fValue);
		if (Application.isEditor)
			Debug.Log ("Configuration Event: " + Gloggr_PlayEvent.ToJSON(configEvent));
		AddEvent (configEvent);
	}

	public void AddSummaryEvent(string summaryEventName, float? fValue = null)
	{
		string eventText = "Summary." + summaryEventName;
		Gloggr_PlayEvent summaryEvent = new Gloggr_PlayEvent(eventText, Gloggr.NowTimeString(), null, fValue);
		if (Application.isEditor)
			Debug.Log ("Aggregate Event: " + Gloggr_PlayEvent.ToJSON(summaryEvent));
		AddEvent (summaryEvent);
	}
	
	public void PostEvents()
	{
		PushCurrentEventsToProcessedEvents();
		PostEvents (processedEvents);
	}

	public void PostEventsNow()
	{
		if(postMode == ServerPostMode.Periodic)
		{
			time = Time.fixedTime - updateFrequency; //this will force a push to the server on the next Update loop
		}
		else //if (postMode  == ServerPostMode.Discrete)
			PostEvents ();
	}

	public void PostEventNow(Gloggr_PlayEvent g)
	{
		/*
		if (!g.gEvent.Contains(configSessionHeader))
		    g.gEvent = configSessionHeader + g.gEvent;
		*/
		//Debug.Log ("Event posted now: " + Gloggr_PlayEvent.ToJSON(gEvent));
		AddCurrentLevelToEvent(ref g);
		List<Gloggr_PlayEvent> singleEventList = new List<Gloggr_PlayEvent>();
		singleEventList.Add (g);
		PostEvents (singleEventList);
	}

	public void PostEvents(List<Gloggr_PlayEvent> gEvents)
	{
		if (!isActive)
			return;
		eventsArePosting = true;
		Gloggr_Report report = new Gloggr_Report(GenerateSessionHeader(), gEvents);
		string eventData = Gloggr_Report.ToJSON(report);
		if (debugJSON)
			debugLogUpdate(eventData);
		/*
		Gloggr_SessionHeader header = GenerateSessionHeader();
		string headerData = Gloggr_SessionHeader.ToJSON(header);
		string eventData = GenerateEventData();
		Debug.Log (headerData);
		Debug.Log (eventData);
		*/
		if (postMode != ServerPostMode.Off)
			PostToServer(eventData);
		eventsArePosting = false;
	}

	Gloggr_SessionHeader GenerateSessionHeader()
	{
		return new Gloggr_SessionHeader(currentSession, currentPlayer, gameID, gameVersion);
	}

	string GenerateEventData()
	{
		string eventData = "[";
		for (int i=0; i < events.Count; i++)
		{
			string json = Gloggr_PlayEvent.ToJSON(events[i]);
			eventData += json;
			if ((i + 1) < events.Count)
			{
				eventData += ",";
			}
		}
		eventData += "]";
		
		debugLogUpdate (eventData);

		return eventData;

	}

	//TODO: possibly use RestSharp to send the data - http://restsharp.org/
	void PostToServer(string eventData)
	{
		System.Text.Encoding encoding = new System.Text.UTF8Encoding();
		Dictionary<string, string> postHeader = new Dictionary<string, string>();
		//create JSON object of current events and send to the server
		
		string url = serverURL + gameID;
		debugLogUpdate ("Sending data to " + url);

		postHeader.Add("Content-Type", "application/json");

		//throws error on web build
		//postHeader.Add("Content-Length", eventData.Length.ToString ());

        // custom auth scheme
        // create md5 hash of message + secret
        // send game id and hash in Authorization header
        MD5 md5 = new MD5CryptoServiceProvider();
        byte[] authData = encoding.GetBytes(eventData + secret);        
        byte[] authHash = md5.ComputeHash(authData);
        string authHashHex = "";
        foreach (byte b in authHash)
        {
            authHashHex += string.Format("{0:x2}", b);
        }
        postHeader.Add("Authorization", "mac " + gameID + ":" + authHashHex);

		WWW www = new WWW(url, encoding.GetBytes(eventData), postHeader); 
		StartCoroutine(WaitForRequest(www));
	}

	void PushCurrentEventsToProcessedEvents()
	{
		if (events.Count > 0)
		{
			processedEvents = events;
			events = new List<Gloggr_PlayEvent>();
		}
	}

	void ClearProcessedEvents()
	{
		eventCount += processedEvents.Count;
		processedEvents = new List<Gloggr_PlayEvent>();
	}

	IEnumerator WaitForRequest(WWW www)
	{
		yield return www;

		// check for errors
		if (www.error == null) {
			debugLogUpdate("WWW Ok!: " + www.text);
			ClearProcessedEvents();
		} 
		else {
			events.InsertRange(0, processedEvents);	//if unsuccessful, add the processed events back to the event queue
			ClearProcessedEvents();
			debugLogUpdate("WWW Error: "+ www.error);
			debugLogUpdate("WWW Error: "+ www.text);
		}    
	}

	void OnApplicationQuit() {
		if (postMode != ServerPostMode.Off)
		{
			PostSystemEventNow("Quit", eventCount);
			PostEvents ();
		}
	}

	public void DebugLog(string message) {
		if (Application.isEditor)
			Debug.Log (message);
	}
	
}
