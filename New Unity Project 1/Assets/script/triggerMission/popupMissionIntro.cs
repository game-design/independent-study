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
            windowRect = GUI.Window(0, new Rect(200, 200, 150, 100), WindowContain, " Left is DODGEBALL \n Right is BOXING ");
        }
        if (showWin == true && this.name == "introMission2")
        {
            windowRect = GUI.Window(0, new Rect(200, 200, 300, 75), WindowContain, "Could you help me get my bag back ?\nYou can see it through right portal");
            //玩家的x，z与NPC的y作为一个新的vector3
            Transform playerT = GameObject.Find("Cha_Knight").transform;
            Transform npc1T = GameObject.Find("NPC_1").transform;

            Vector3 v = new Vector3(playerT.position.x, npc1T.position.y, playerT.position.z);
            Quaternion rotation = Quaternion.LookRotation(v - npc1T.position);  //获取目标方向
            npc1T.rotation = Quaternion.Slerp(npc1T.rotation, rotation, Time.deltaTime * 1f);  // 差值  趋向目标
        }
    }
    public void WindowContain(int windowID)
    {
    }

    void OnTriggerEnter(Collider other)
    {
        showWin = true;
    }
    void OnTriggerExit(Collider other)
    {
        showWin = false;
    }

}
