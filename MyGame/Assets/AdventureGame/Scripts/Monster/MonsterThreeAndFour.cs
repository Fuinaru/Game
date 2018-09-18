using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterThreeAndFour : BaseMonster
{

    // Use this for initialization
    MonsterLaunch monsterLaunch;
    public Var.ItemType shootItem;
    TeleportAbility teleport = new TeleportAbility();
    protected void Start()
    {
        base.Start();
        monsterLaunch = transform.GetComponentInChildren<MonsterLaunch>();
    }

    // Update is called once per frame
    protected void Update()
    {
        if (GameManager.IsTimePause()) { if (m_rigidbody != null) m_rigidbody.velocity = Vector3.zero; return; }
        base.Update();
        if (_stopTime <= 0) Action();
    }
    protected void Action()
    {
       
        moveToPlayer();
        if (IsFindPlayer) {
            Attack();
            if (dirFromPlayer.magnitude > 10)if(Random.Range(-1,50)<0)StartCoroutine(teleport.StartTeleport(0.8f,transform));
            if(dirFromPlayer.magnitude < 2)  StartCoroutine(teleport.StartTeleport(-0.5f,transform));
        }
    }
    protected void Attack()
    {
        if (monsterLaunch.CoolEnd() && !isClose) monsterLaunch.ShootItem(shootItem);
    }
    public override void getHurted()
    {
        base.getHurted();
        StartCoroutine(teleport.StartTeleport(-0.5f, transform));
    }
}
