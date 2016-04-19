using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//helper class to allow for grabbing 2d positions faster
public static class Gloggr_ExtensionMethods {

	/// <summary>
	/// Extension Method. Allows for x,y 2d position to be obtained from Vector3 world position from Transform.
	/// </summary>
	/// <returns>x,y coordinates of world position</returns>
	/// <param name="t">Transform to get 2d world position of.</param>
	public static Vector2 GetPosition2D(this Transform t) {
		Vector3 pos = t.position;
		Vector2 pos2d = new Vector2(pos.x, pos.y);
		return pos2d;
	}

	// From http://answers.unity3d.com/questions/539390/check-if-a-trigger-object-implements-an-interface.html
	// Not currently used.

	/// <summary>
	/// GameObject Extension method. Returns an array of Monobehaviours on target GameObject implementing Interface <T>.
	/// </summary>
	/// <returns>Array of interface T Monobehaviours.</returns>
	/// <param name="objectToSearch">GameObject to search.</param>
	/// <typeparam name="T">Type of interface to search for.</typeparam>
	public static T[] GetInterfaces<T>(this GameObject objectToSearch) where T: class {
		MonoBehaviour[] list = objectToSearch.GetComponents<MonoBehaviour>();
		List<T> resultList = new List<T>();
		foreach(MonoBehaviour mb in list){
			if(mb is T){
				//found one
				resultList.Add((T)((System.Object)mb));
			}
		}
		return resultList.ToArray();
	}
}
