using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterLittleBossOne : BaseMonster
{

    public int ActionMode = 0;
    public MonsterLaunch launch;

    public float leftOrRight = 1f;


    private int isDash=-1;

    private float setTime=0;
    private float _setTime = 1f;

    private bool isHitSth = false;
    // Use this for initialization
    void Start()
    {
        base.Start();
        setTime = _setTime;
        launch = GetComponent<MonsterLaunch>();

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.IsTimePause()) { if (m_rigidbody != null) m_rigidbody.velocity = Vector3.zero; return; }
        base.Update();
        if (setTime <= 0) setTime = _setTime;

        if (m_rigidbody != null && isDash == 1) speed += Time.deltaTime*12; m_rigidbody.velocity = transform.forward * speed * 0.5f;
        if (_stopTime <= 0)
        {
            if (IsFindPlayer) Action();
        }
        setTime -= Time.deltaTime;
    }


    protected void Action()
    {

        switch (ActionMode)
        {
            case 0: ActionMode1(); break;
            case 1: ActionMode2(); break;
        }

    }
    protected void ActionMode1()
    {
        if (isDash==-1)
        {
            moveAroundPlayer();
            launch.coolTime = 5f;
            if (launch.CoolEnd() && !isClose)
            {
                int a= Random.Range(-1, 15);
                if (a < 0) launch.AroundShootAttack(Var.ItemType.BoomItem, Random.Range(0, 30), 60, 100, -1);
                else if(a>10){
                    launch.ShootItemAtPos(Var.ItemType.BoomItem, transform.forward, 200);
                    launch.ShootItemAtPos(Var.ItemType.BoomItem, 2*transform.forward+transform.right, 200);
                    launch.ShootItemAtPos(Var.ItemType.BoomItem, 2 * transform.forward - transform.right, 200);
                }
                else launch.AroundShootAttack(Var.ItemType.BulletItem, Random.Range(0,10), 20);
            }
            if (Random.Range(-1, 300) < 0) StartCoroutine(Dash());
        }
        else if(isDash==0) Tools.LookAtOnlyYAxis(transform, GameManager.player.transform);
        if (isDash == 1 && setTime == _setTime&& !isHitSth) launch.AroundShootAttack(Var.ItemType.BoomItem, 30, 60, 50, -1);

    }
    protected void ActionMode2()
    {
        if (isDash == -1)
        {
            moveAroundPlayer();
            launch.coolTime = 2f;
            if (launch.CoolEnd() && !isClose)
            {
                int a = Random.Range(-1, 10);
                if (Random.Range(-10, 10) < 0) launch.AroundShootAttack(Var.ItemType.BoomItem, 30, 60, 100, 0.5f);
                else
                {
                    launch.ShootItemAtPos(Var.ItemType.BoomItem, transform.forward, 200, 0.5f);
                    launch.ShootItemAtPos(Var.ItemType.BoomItem, 2 * transform.forward + transform.right, 200, 0.5f);
                    launch.ShootItemAtPos(Var.ItemType.BoomItem, 2 * transform.forward - transform.right, 200, 0.5f);
                }


            }
            if (Random.Range(-1, 150) < 0) StartCoroutine(Dash());
        }
        if (isDash == 1 && setTime >= _setTime &&! isHitSth) launch.AroundShootAttack(Var.ItemType.BoomItem, 30, 60,50,0.5f);
    }


    protected IEnumerator Dash() {
        isDash = 0;
        float a = speed;
        speed = 0;
        yield return new WaitForSeconds(1f);
        int b = atk;
        atk = 5;
        speed = 20;
        isDash = 1;
        yield return new WaitForSeconds(1.5f);
        speed = 0;
        atk = b;
        yield return new WaitForSeconds(1f);
        speed=a;
        isDash = -1;
        leftOrRight = -leftOrRight;
    }

    public override void getHurted()
    {
        hurted = true;
        if(ActionMode==0)if(Random.Range(-1,1)<0) launch.AroundShootAttack(Var.ItemType.BoomItem, 30, 60, 50, 1f);
        if(ActionMode ==1)if (Random.Range(-2, 1) < 0) launch.AroundShootAttack(Var.ItemType.BoomItem, 30, 60, 50, 0.8f);
    }


    protected IEnumerator WaitSeconds(float stoptime)
    {
        float a = speed;
        speed = 0;
        yield return new WaitForSeconds(stoptime);
        speed = a;
    }
    public override void Damage(int a,string str)
    {
        if (str == "Boom") return;
        base.Damage(a);
        if (hp <= maxHp  / 2) ActionMode = 1;
    }
    public override void Damage(int a)
    {
        base.Damage(a);
        if (hp <= maxHp / 2) ActionMode = 1;
    }


    protected void moveAroundPlayer()
    {
        if (IsFindPlayer && !isDestory)
        {
            Tools.LookAtOnlyYAxis(transform, GameManager.player.transform);
            dirFromPlayer.y = 0;
            Vector3 dir = transform.right * 2 * leftOrRight + dirFromPlayer.normalized;
            if (m_rigidbody != null) m_rigidbody.velocity = dir.normalized * speed * 0.5f;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        isHitSth = true;
    }
    private void OnCollisionExit(Collision collision)
    {
        isHitSth = false;
    }
}
