using UnityEngine;
using System.Collections;

//this is for mission introduction & general npc control
public class popupMissionIntro : MonoBehaviour {


    bool showWin = false;
    private Rect windowRect = new Rect(200, 200, 150, 100);
    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}


    void OnGUI()
    {
        if (showWin == true && this.name == "introMission1")
        {
			windowRect = GUI.Window(0, new Rect(100, 100, 150, 100), emptyWin, " Left is DODGEBALL \n Right is BOXING ");
			return;
        }
        if (showWin == true && this.name == "introMission2")
        {
			windowRect = GUI.Window(0, new Rect(100, 100, 300, 75), emptyWin, "Could you help me get my bag back ?\nYou can see it through right portal");
            //玩家的x，z与NPC的y作为一个新的vector3
            Transform playerT = GameObject.Find("Cha_Knight").transform;
            Transform npc1T = GameObject.Find("Char_2").transform;

            Vector3 v = new Vector3(playerT.position.x, npc1T.position.y, playerT.position.z);
            Quaternion rotation = Quaternion.LookRotation(v - npc1T.position);  //获取目标方向
            npc1T.rotation = Quaternion.Slerp(npc1T.rotation, rotation, Time.deltaTime * 1f);  // 差值  趋向目标
			return;
        }
        if (showWin == true && this.name == "introMission3")
        {
            windowRect = GUI.Window(0, new Rect(100, 100, 250, 175), WindowContain, "Nice to meet you again?\nDid you see my bag in there?");
            //玩家的x，z与NPC的y作为一个新的vector3
            Transform playerT = GameObject.Find("Cha_Knight").transform;
			Transform npc2T = GameObject.Find("Char_3").transform;

            Vector3 v = new Vector3(playerT.position.x, npc2T.position.y, playerT.position.z);
            Quaternion rotation = Quaternion.LookRotation(v - npc2T.position);  //获取目标方向
            npc2T.rotation = Quaternion.Slerp(npc2T.rotation, rotation, Time.deltaTime * 1f);  // 差值  趋向目标
			return;
        }
    }

	public void emptyWin(int windowID)
	{
	}

    public void WindowContain(int windowID)
    {
        if (GUI.Button(new Rect(0, 50, 250, 25), "Yes and I took it for you!"))
        {
            showWin = false;
        }
        if (GUI.Button(new Rect(0, 80, 250, 25), "Sorry, it was way too hard."))
        {
            showWin = false;
        }
        if (GUI.Button(new Rect(0, 110, 250, 25), "What bag?"))
        {
            showWin = false;
        }

    }

    void OnTriggerEnter(Collider other)
    {
		if(other.name == "Char_2" || other.name == "Char_3") return;
        showWin = true;
    }
    void OnTriggerExit(Collider other)
    {
        showWin = false;
    }

}
