using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletItem : BagItem
{

    // Use this for initialization
    protected void Start()
    {
        base.Start();

    }

    // Update is called once per frame
    protected void Update()
    {
        base.Update();
    }
    public override void useItem()
    {
        GameObject go = Instantiate(PlayerLaunch.SBullet, PlayerLaunch.trans.position, PlayerLaunch.trans.rotation) as GameObject;
        go.GetComponent<Rigidbody>().AddRelativeForce(0, 0, 1200);
        itemData.itemNum--;
        updateText();
    }
}