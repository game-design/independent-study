using UnityEngine;
using System.Collections;

public class CreateScrollingText : MonoBehaviour {
    public Transform creditTextObject;
    string scrollText = "Developer\nChutian Wang\nDingfeng Shao\nThanks\nJeff Wilson\nJeremy Johnson\nRobert Solomon";


    // Use this for initialization
    void Start () {
        string[] splitText = scrollText.Split('\n');
        int i = 0;
        foreach (string txtLine in splitText)
        {
            GameObject obj = (GameObject)Instantiate(creditTextObject.gameObject, Vector3.zero , Quaternion.Euler(0, 0, 0));
            obj.GetComponent<TextMesh>().text = txtLine;
            obj.transform.parent = this.transform;
            obj.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            obj.transform.localPosition = new Vector3(0, -i * 0.3f, 0);
            i++;
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
