using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

[InitializeOnLoad]
class Gloggr_HierarchyIcon
{
	static Texture2D texture;
	static List<int> markedObjects;
	static List<int> parentObjects;
	
	static Gloggr_HierarchyIcon ()
	{
		// Init
		string[] path = AssetDatabase.FindAssets("GloggrIcon_16");
		//Debug.Log (path[0]);
		texture = AssetDatabase.LoadAssetAtPath (AssetDatabase.GUIDToAssetPath(path[0]), typeof(Texture2D)) as Texture2D;
		markedObjects = new List<int> (); //prevents null errors on first run
		EditorApplication.hierarchyWindowChanged += UpdateCB;
		EditorApplication.hierarchyWindowItemOnGUI += HierarchyItemCB;
		UpdateCB();
	}
	
	static void UpdateCB ()
	{
		// Check here
		Gloggr_Tracker[] gto = Object.FindObjectsOfType (typeof(Gloggr_Tracker)) as Gloggr_Tracker[];
		
		markedObjects = new List<int> ();
		parentObjects = new List<int> ();
		foreach (Gloggr_Tracker gt in gto) 
		{
			markedObjects.Add (gt.gameObject.GetInstanceID ());
			Transform parent = gt.gameObject.transform.parent;
			if (parent != null && parent != parent.parent)
			{
				parentObjects.Add (parent.gameObject.GetInstanceID());
			}
		}
		
	}
	
	static void HierarchyItemCB (int instanceID, Rect selectionRect)
	{
		// place the icoon to the right of the list:
		Rect r = new Rect (selectionRect); 
		r.x = r.width - 20;
		r.width = 18;
		
		if (markedObjects.Contains (instanceID)) 
		{
			// Draw the texture if it's a light (e.g.)
			GUI.Label (r, texture); 
		}

		else if (parentObjects.Contains (instanceID))
		{
			Color guiColor = GUI.color;
			GUI.color = new Color(1, 1, 1, 0.15f);
			GUI.Label (r, texture);
			GUI.color = guiColor;
		}
	}
	
}
