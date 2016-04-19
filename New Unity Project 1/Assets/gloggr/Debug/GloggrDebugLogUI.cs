using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(UnityEngine.UI.Image))]
public class GloggrDebugLogUI : MonoBehaviour {

	public bool useDebugPanel = true;

	public bool panelVisible = false;

	public UnityEngine.UI.Text debugUI;
	UnityEngine.UI.Image panel;

	List<string> lines;
	[Range(1, 6)]
	public int capacity = 6;

	// Use this for initialization
	void Start () {
		panel = GetComponent<UnityEngine.UI.Image>();
		Gloggr.Instance.debugLogUpdate += DebugLog;
		lines = new List<string>();
		for (int i=0; i < capacity; i++)
			lines.Add ("-");
		UpdatePanel();
	}
	
	// Update is called once per frame
	void Update () {
		if (useDebugPanel)
		{
			if (Input.GetKeyDown(KeyCode.BackQuote))
			{
				panelVisible = !panelVisible;
			}
			panel.enabled = panelVisible;
			debugUI.enabled = panelVisible;

		}
	}

	public void DebugLog(string message) 
	{
		PushLineToPanel(message);
		UpdatePanel();
	}

	void PushLineToPanel(string line)
	{
		lines.Add (line);
		if (lines.Count > capacity)
			lines.RemoveAt (0);
	}

	void UpdatePanel()
	{
		if (debugUI != null)
			debugUI.text = GetUIText();
	}

	string GetUIText()
	{
		string output = "";
		foreach (string s in lines)
		{
			output += s;
			output += "\n";
		}
		return output;
	}

	void OnDestroy() {
		Gloggr.Instance.debugLogUpdate -= DebugLog;
	}


}
