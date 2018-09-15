using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : PressKeyButton {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    protected override void InterAction()
    {
        SavaAndLoad.saveload.SaveGame();
    }
}
