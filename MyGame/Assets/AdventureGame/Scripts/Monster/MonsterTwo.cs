using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterTwo : BaseMonster {

    // Use this for initialization
    MonsterLaunch monsterLaunch;
    protected void Start() {
        base.Start();
        monsterLaunch = transform.GetComponentInChildren<MonsterLaunch>();
    }

    // Update is called once per frame
    protected void Update() {
        if (GameManager.isTimePause) { m_rigidbody.velocity = Vector3.zero; return; }
        base.Update();
        Action();
    }
    protected void Action()
    {
        moveToPlayer();
        if (IsFindPlayer) Attack();
    }
    protected void Attack()
    {
       if (monsterLaunch.BulletCoolEnd()&&!isClose) monsterLaunch.Shoot();
    }

}
