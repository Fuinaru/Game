using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBoss : BaseMonster {

    public int ActionMode=0;
    public MonsterLaunch launch;

    public float teleportDis=8f;
    private bool isTeleport = false;

    public float leftOrRight = 1f;
    // Use this for initialization
    void Start () {
        base.Start();
        launch = GetComponent<MonsterLaunch>();

    }
	
	// Update is called once per frame
	void Update () {
        if (GameManager.IsTimePause()) { if (m_rigidbody != null) m_rigidbody.velocity = Vector3.zero; return; }
        base.Update();
        if (_stopTime <= 0)
        {
            if(IsFindPlayer)Action();
        }
    }
    public override void getHurted()
    {
        hurted = true;
        new WaitForSeconds(0.5f);
        Teleport(-0.5f);
    }

    protected void Action()
    {
        if (dirFromPlayer.magnitude > 12) Teleport(1);

        switch (ActionMode) {
            case 0:  ActionMode1(); break; 
            case 1: ActionMode2(); break;
            case 2: ActionMode3(); break;
        }
    
    }
    protected void ActionMode1() {
        moveToPlayer();
        launch.coolTime = 3f;
      
        if (IsFindPlayer) {
            if (launch.CoolEnd() && !isClose) {
                AroundShootAttack(Var.ItemType.FireBallItem, Random.Range(0, 30), 30);
                StartCoroutine(WaitSeconds(1.5f));
            }
        }

        }
    protected void ActionMode2()
    {
        moveAroundPlayer(); 
        TeleportByDis(-1,2);
        if (launch.CoolEnd() && !isClose) {
            float a = Random.Range(-1f, 1f);
            if (a >= 0)
            {
                AroundShootAttack(Var.ItemType.FireBallItem, Random.Range(0, 40), 40);
            }
            else { AroundShootAttack(Var.ItemType.IceBallItem, Random.Range(0,30), 30); }
            StartCoroutine(WaitSeconds(1.5f));
        }

    }
    protected void ActionMode3()
    {
        speed = 3;
        moveAroundPlayer();
        float b = Random.Range(-1f,-0.4f);
        TeleportByDis(b, 4);
        if (launch.CoolEnd() && !isClose) {
            float a = Random.Range(-1f, 1.2f);
            if (a > 1f)
            {
                AroundShootAttack(Var.ItemType.FireBallItem, 0, 40);
                AroundShootAttack(Var.ItemType.IceBallItem, 20, 40);
                StartCoroutine(AroundShootAttackWait(Var.ItemType.FireBallItem, 10, 40, 0.15f));
                StartCoroutine(AroundShootAttackWait(Var.ItemType.IceBallItem, 30, 40, 0.15f));
            }
            else
            if (a >= 0)
            {
                AroundShootAttack(Var.ItemType.FireBallItem, 0, 60);
                AroundShootAttack(Var.ItemType.IceBallItem, 30, 60);
                if(a>0.7) StartCoroutine(AroundShootAttackWait(Var.ItemType.IceBallItem, Random.Range(0, 60), 60, 0.15f));
            }
            else {
                AroundShootAttack(Var.ItemType.FireBallItem, 0, 40);
                StartCoroutine(AroundShootAttackWait(Var.ItemType.IceBallItem, 20,40,0.15f));
                if(a<-0.7) StartCoroutine(AroundShootAttackWait(Var.ItemType.FireBallItem, 0, 40, 0.15f));
            }
            StartCoroutine(WaitSeconds(1.5f));
        }

    }
    protected IEnumerator WaitSeconds(float stoptime) {
        float a = speed;
        speed = 0;
        yield return new WaitForSeconds(stoptime);
        speed = a;
    }

    protected void AroundShootAttack(Var.ItemType bullet,int start, int n) {
 
            for (float i = start; i < 360+start; i += n)
            {
                launch.ShootItemAtPos(bullet, transform.position + new Vector3(Mathf.Sin(i / 180 * Mathf.PI), 0.5f, Mathf.Cos(i / 180 * Mathf.PI))*1.5f);
             
            }
  
    }
    protected IEnumerator AroundShootAttackWait(Var.ItemType bullet, int start, int n,float waittime)
    {
        yield return new WaitForSeconds(waittime);
        for (float i = start; i < 360 + start; i += n)
        {
            launch.ShootItemAtPos(bullet, transform.position + new Vector3(Mathf.Sin(i / 180 * Mathf.PI), 0.5f, Mathf.Cos(i / 180 * Mathf.PI)) * 1.5f);

        }

    }
    protected void Teleport(float n) {
        if (isTeleport) return;
        isTeleport = true;
        Tools.PlayParticletAtPosByName("SmokeEffect", transform);
        StartCoroutine(StartTeleport(n));
    }
    protected void TeleportByDis(float n,float dis)
    {
        if (dirFromPlayer.magnitude >= dis) return;
        Teleport(n);
        leftOrRight = Random.Range(-1f, 1f);
    }
    protected IEnumerator StartTeleport(float n)
    {
        yield return new WaitForSeconds(0.4f);
        Tools.LookAtOnlyYAxis(transform, GameManager.player.transform);
        Vector3 position = transform.position;
        position.y = 0;
        transform.position = position;
        Vector3 dir=Vector3.zero;

        Ray ray1 = new Ray(transform.position + new Vector3(0, 0.4f, 0), n*transform.forward);
        RaycastHit HitInfo1;
        bool result1 = Physics.Raycast(ray1, out HitInfo1,Mathf.Abs(n)*teleportDis);
        if (result1) {
            Vector3 pos = HitInfo1.point;
            pos.y = 0;
            if (n > 0) dir += pos - transform.position-transform.forward;
            else dir+= pos - transform.position + transform.forward;
        }
        else dir += n * transform.forward * teleportDis;
        Ray ray2 = new Ray(transform.position + new Vector3(0, 0.4f, 0), Mathf.Abs(n) * transform.right);
        RaycastHit HitInfo2;
        bool result2 = Physics.Raycast(ray2, out HitInfo2, Mathf.Abs(n) * teleportDis);
        if (result2)
        {
            Vector3 pos = HitInfo2.point;
            pos.y = 0;
            dir += pos - transform.position - transform.right;
        }
        else dir += Mathf.Abs(n) * transform.right * teleportDis;

        Ray ray3 = new Ray(transform.position + new Vector3(0, 0.4f, 0), -Mathf.Abs(n) * transform.right);
        RaycastHit HitInfo3;
        bool result3 = Physics.Raycast(ray3, out HitInfo3, Mathf.Abs(n) * teleportDis);
        if (result3)
        {
            Vector3 pos = HitInfo3.point;
            pos.y = 0;
            dir += pos - transform.position + transform.right;
        }
        else dir -= Mathf.Abs(n) * transform.right * teleportDis;

        Ray ray4 = new Ray(transform.position + new Vector3(0, 0.4f, 0), dir);
        RaycastHit HitInfo4;
        bool result4 = Physics.Raycast(ray4, out HitInfo4, Mathf.Abs(n) * teleportDis);
        if (result4)
        {
            Vector3 pos = HitInfo4.point;
            pos.y = 0;
            transform.position= pos- dir.normalized;
        }
        else transform.position += Mathf.Abs(n) * dir.normalized * teleportDis;

            // Debug.Log(HitInfo.collider.name);

  

        Tools.PlayParticletAtPosByName("SmokeEffect", transform);
        isTeleport = false;
    }

    public override void Damage(int a)
    {
        base.Damage(a);
        if (hp <= maxHp * 2 / 3) ActionMode = 1;
        if (hp <= maxHp  / 3) ActionMode = 2;
    }

    protected void moveAroundPlayer()
    {
        if (IsFindPlayer && !isDestory)
        {
            Tools.LookAtOnlyYAxis(transform, GameManager.player.transform);
            dirFromPlayer.y = 0;
            Vector3 dir = transform.right * 2* leftOrRight + dirFromPlayer.normalized;
            if (m_rigidbody != null) m_rigidbody.velocity = dir.normalized* speed*0.5f ;
        }
    }


}
