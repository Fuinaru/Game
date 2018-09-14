using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomItem : BagItem {

    // Use this for initialization
    private void Awake()
    {
        coolTime = 2f;
    }

    protected void Start () {
        base.Start();
       
	}

    // Update is called once per frame
    protected void Update () {
        base.Update();
	}
    public override void useItem()
    {if (!ItemCoolEnd()) return;
        GameObject go = Instantiate(GetItemObject(), PlayerLaunch.trans.position, PlayerLaunch.trans.rotation) as GameObject;
        go.GetComponent<Rigidbody>().AddRelativeForce(0, 0, 100);
        useUpdate();
    }
}
