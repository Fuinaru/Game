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
        GameManager.GM.DestoryAllMonsters();
        GameManager.GM.DestoryAllItemsInScene();
        if (TwirlScripts.angle==360f) TwirlScripts.angle = 0;
        MapLoad.mapload.LoadNextStage(stageNum, size.x, size.y);

    }
}
