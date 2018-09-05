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
        if (GameManager.isTimePause) { m_rigidbody.velocity = Vector3.zero; return; }
        base.Update();
        action();
    }

    protected void action() {
        moveToPlayer();

    }

}
