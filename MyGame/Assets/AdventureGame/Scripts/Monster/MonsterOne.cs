using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterOne : BaseMonster {

    // Use this for initialization
    protected void Start () {
        base.Start();
    }

    // Update is called once per frame
    protected void Update () {
        if (GameManager.IsTimePause()) { m_rigidbody.velocity = Vector3.zero; return; }
        base.Update();
      if(_stopTime<=0) action();
    }

    protected void action() {
        moveToPlayer();

    }

}
