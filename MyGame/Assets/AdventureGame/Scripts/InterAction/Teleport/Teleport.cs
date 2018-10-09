using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : PressKeyButton {

    public int stageNum;
    public Vector2Int size;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    protected override void InterAction()
    {
        if (stageNum!=1)
        {
            GameManager.GM.DestoryAllMonsters();
            GameManager.GM.DestoryAllItemsInScene();
            if (TwirlScripts.angle == 360f) TwirlScripts.angle = 0;
            MapLoad.mapload.LoadNextStage(stageNum, size.x, size.y);
            GameManager.GM.playerPos = GameManager.player.transform.position;
            GameManager.player.transform.position = new Vector3(10, 0, 5);
        }
        else {

            if (TwirlScripts.angle == 360f) TwirlScripts.angle = 0;
            MapLoad.mapload.LoadNextStage(1,5,5);
            GameManager.player.transform.position = GameManager.GM.playerPos;
            GameManager.GM.DestoryAllMonsters();
            GameManager.GM.DestoryAllItemsInScene();
            GameManager.GM.DestoryOther();
        }
    }
}
