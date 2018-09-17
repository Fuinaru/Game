using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterTwo : BaseMonster {

    // Use this for initialization
    MonsterLaunch monsterLaunch;
    public Var.ItemType shootItem;
    protected void Start() {
        base.Start();
        monsterLaunch = transform.GetComponentInChildren<MonsterLaunch>();
    }

    // Update is called once per frame
    protected void Update() {
        if (GameManager.IsTimePause()) { if (m_rigidbody != null) m_rigidbody.velocity = Vector3.zero; return; }
        base.Update();
        if (_stopTime <= 0) Action();
    }
    protected void Action()
    {
        moveToPlayer();
        if (IsFindPlayer) Attack();
    }
    protected void Attack()
    {
       if (monsterLaunch.CoolEnd()&&!isClose) monsterLaunch.ShootItem(shootItem);
    }

}
