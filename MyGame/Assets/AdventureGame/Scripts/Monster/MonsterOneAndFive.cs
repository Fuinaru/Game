using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterOneAndFive : BaseMonster {

    // Use this for initialization
    public bool hasBoom = false;
    protected void Start () {
        base.Start();
    }

    // Update is called once per frame
    protected void Update () {
        if (GameManager.IsTimePause()) { if(m_rigidbody!=null) m_rigidbody.velocity = Vector3.zero; return; }
        base.Update();
      if(_stopTime<=0) action();
    }

    protected void action() {
        moveToPlayer();
        if (hasBoom && dirFromPlayer.magnitude < 1.5f) { transform.GetChild(1).GetComponent<Boom>().isStart = true; transform.GetChild(1).GetComponent<Boom>().aliveTime=1.5f; hasBoom = false;speed *= 1.5f; }
    }

}
