using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterLittleBossTwo : BaseMonster
{

    public int ActionMode = 0;
    public MonsterLaunch launch;
    public float teleportDis = 8f;
    private bool isTeleport = false;

    public float leftOrRight = 1f;
    // Use this for initialization
    void Start()
    {
        if (GameManager.GM.hasFire) Destroy(gameObject);
        base.Start();
        launch = GetComponent<MonsterLaunch>();

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.IsTimePause()) { if (m_rigidbody != null) m_rigidbody.velocity = Vector3.zero; return; }
        base.Update();
        if (_stopTime <= 0)
        {
            if (IsFindPlayer) Action();
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

        switch (ActionMode)
        {
            case 0: ActionMode1(); break;
            case 1: ActionMode2(); break;
        }

    }
    protected void ActionMode1()
    {
        moveToPlayer();
        launch.coolTime = 4f;
        // Tools.LookAtOnlyYAxis(transform, GameManager.player.transform);
        if (IsFindPlayer)
        {
            if (launch.CoolEnd() && !isClose)
            {
                int a = Random.Range(-8, 20);
                if (a < 0)
                {
                    launch.ShootItemAtPos(Var.ItemType.FireBallItem, launch.transform.position + transform.forward + new Vector3(0, 0.5f, 0));
                    launch.ShootItemAtPos(Var.ItemType.FireBallItem, launch.transform.position + (transform.forward * 2 + transform.right).normalized + new Vector3(0, 0.5f, 0));
                    launch.ShootItemAtPos(Var.ItemType.FireBallItem, launch.transform.position + (transform.forward * 2 - transform.right).normalized + new Vector3(0, 0.5f, 0));
                }
                else if (a < 10)
                {
                    launch.ShootItemAtPos(Var.ItemType.FireBallItem, launch.transform.position + dirFromPlayer.normalized + new Vector3(0, 0.5f, 0));
                    StartCoroutine(launch.ShootItemAtPosWait(Var.ItemType.FireBallItem, launch.transform.position + dirFromPlayer.normalized + new Vector3(0, 0.5f, 0), 0.1f, 500, -1));
                    StartCoroutine(launch.ShootItemAtPosWait(Var.ItemType.FireBallItem, launch.transform.position + dirFromPlayer.normalized + new Vector3(0, 0.5f, 0), 0.2f, 500, -1));
                    StartCoroutine(launch.ShootItemAtPosWait(Var.ItemType.FireBallItem, launch.transform.position + dirFromPlayer.normalized + new Vector3(0, 0.5f, 0), 0.3f, 500, -1));
                }
                else if(a <=18){
                    Vector3 pos = launch.transform.position + new Vector3(0, 0.5f, 0)+transform.forward;
                    launch.ShootItemAtPosWithDir(Var.ItemType.FireBallItem, pos,500 ,transform.forward);
                    launch.ShootItemAtPosWithDir(Var.ItemType.FireBallItem, pos + transform.right * 1, 500, transform.forward);
                    launch.ShootItemAtPosWithDir(Var.ItemType.FireBallItem, pos + transform.right * 2, 500, transform.forward);
                  //  launch.ShootItemAtPosWithDir(Var.ItemType.FireBallItem, pos + transform.right * 3, 500, transform.forward);
                  //  launch.ShootItemAtPosWithDir(Var.ItemType.FireBallItem, pos + transform.right * 4, 500, transform.forward);
                    launch.ShootItemAtPosWithDir(Var.ItemType.FireBallItem, pos - transform.right * 1, 500, transform.forward);
                    launch.ShootItemAtPosWithDir(Var.ItemType.FireBallItem, pos - transform.right * 2, 500, transform.forward);
                  //  launch.ShootItemAtPosWithDir(Var.ItemType.FireBallItem, pos - transform.right * 3, 500, transform.forward);
                  //  launch.ShootItemAtPosWithDir(Var.ItemType.FireBallItem, pos - transform.right * 4, 500, transform.forward);

                }
          


                StartCoroutine(WaitSeconds(1.5f));
            }
        }

    }
    protected void ActionMode2()
    {
       
        moveAroundPlayer();
        launch.coolTime = 3f;
        TeleportByDis(-0.5f, 2f);
        if (launch.CoolEnd() && !isClose)
        {
            int a= Random.Range(-8, 30);
            if (a < 0)
            {
                launch.ShootItemAtPos(Var.ItemType.FireBallItem, launch.transform.position + transform.forward + new Vector3(0, 0.5f, 0));
                StartCoroutine(launch.ShootItemAtPosWait(Var.ItemType.FireBallItem, launch.transform.position + (3 * transform.forward - transform.right).normalized + new Vector3(0, 0.5f, 0), 0.1f, 500, -1));
                StartCoroutine(launch.ShootItemAtPosWait(Var.ItemType.FireBallItem, launch.transform.position + (3 * transform.forward + transform.right).normalized + new Vector3(0, 0.5f, 0), 0.1f, 500, -1));
                StartCoroutine(launch.ShootItemAtPosWait(Var.ItemType.FireBallItem, launch.transform.position + transform.forward + new Vector3(0, 0.5f, 0), 0.1f, 500, -1));
                StartCoroutine(launch.ShootItemAtPosWait(Var.ItemType.FireBallItem, launch.transform.position + (4 * transform.forward - transform.right).normalized + new Vector3(0, 0.5f, 0), 0.2f, 500, -1));
                StartCoroutine(launch.ShootItemAtPosWait(Var.ItemType.FireBallItem, launch.transform.position + (4 * transform.forward + transform.right).normalized + new Vector3(0, 0.5f, 0), 0.2f, 500, -1));
                StartCoroutine(launch.ShootItemAtPosWait(Var.ItemType.FireBallItem, launch.transform.position + (2 * transform.forward - transform.right).normalized + new Vector3(0, 0.5f, 0), 0.2f, 500, -1));
                StartCoroutine(launch.ShootItemAtPosWait(Var.ItemType.FireBallItem, launch.transform.position + (2 * transform.forward + transform.right).normalized + new Vector3(0, 0.5f, 0), 0.2f, 500, -1));
                StartCoroutine(launch.ShootItemAtPosWait(Var.ItemType.FireBallItem, launch.transform.position + transform.forward + new Vector3(0, 0.5f, 0), 0.2f, 500, -1));
 
            }
            else if (a < 10)
            {

                Vector3 pos = launch.transform.position + new Vector3(0, 0.5f, 0) + transform.forward;
                launch.ShootItemAtPosToPlayer(Var.ItemType.FireBallItem, pos, 600);
                launch.ShootItemAtPosToPlayer(Var.ItemType.FireBallItem, pos + transform.right * 2, 500);
                launch.ShootItemAtPosToPlayer(Var.ItemType.FireBallItem, pos + transform.right * 4, 400);
                launch.ShootItemAtPosToPlayer(Var.ItemType.FireBallItem, pos - transform.right * 2, 500);
                launch.ShootItemAtPosToPlayer(Var.ItemType.FireBallItem, pos - transform.right * 4, 400);
            }
            else if (a < 20) {

                StartCoroutine(launch.AroundPlayerShootAttackWaitAddForce(Var.ItemType.FireBallItem,0,45,500,2,1f));

            }
            else if(a<25)
            {

                StartCoroutine(launch.AroundPlayerShootAttackWaitAddForce(Var.ItemType.FireBallItem, 0, 30, 500, 2.5f, 1f));

            }
            StartCoroutine(WaitSeconds(1.5f));
            leftOrRight = -leftOrRight;
        }

    }
 
    protected IEnumerator WaitSeconds(float stoptime)
    {
        float a = speed;
        speed = 0;
        yield return new WaitForSeconds(stoptime);
        speed = a;
    }


    protected void Teleport(float n)
    {
        if (isTeleport) return;
        isTeleport = true;
        Tools.PlayParticletAtPosByName("SmokeEffect", transform);
        StartCoroutine(StartTeleport(n));
    }
    protected void TeleportByDis(float n, float dis)
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
        Vector3 dir = Vector3.zero;

        Ray ray1 = new Ray(transform.position + new Vector3(0, 0.4f, 0), n * transform.forward);
        RaycastHit HitInfo1;
        bool result1 = Physics.Raycast(ray1, out HitInfo1, Mathf.Abs(n) * teleportDis);
        if (result1)
        {
            Vector3 pos = HitInfo1.point;
            pos.y = 0;
            if (n > 0) dir += pos - transform.position - transform.forward;
            else dir += pos - transform.position + transform.forward;
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
            transform.position = pos - dir.normalized;
        }
        else transform.position += Mathf.Abs(n) * dir.normalized * teleportDis;

        // Debug.Log(HitInfo.collider.name);



        Tools.PlayParticletAtPosByName("SmokeEffect", transform);
        isTeleport = false;
    }

    public override void Damage(int a)
    {
        base.Damage(a);
        if (hp <= maxHp  / 2) ActionMode = 1;
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

    public override void GoDie()
    {
        if (!isDestory)
        {
            Tools.PlayFollowingParticletByName("SmokeEffect", transform);
            Destroy(gameObject.GetComponent<Collider>());
            Destroy(m_rigidbody);
            isDestory = true;
            if (!GameManager.GM.PlayerHaveKeyItem(Var.ItemType.FireBallItem))
            {
                GameManager.playerLaunch.GetItem(Var.ItemType.FireBallItem, 1);
                NPC.SetConver("天之声", "你获得了火球");
                TaskManager.taskManager.CompleteTask(1);
            }
        }

        HurtedTool.Color2B(GetComponentInChildren<Renderer>().material, new Color(1, 1, 1, 0), 30);
        if (HurtedTool.isChildrenColorB(transform, new Color(1, 1, 1, 0))) { GameManager.Monsters.Remove(this); Destroy(gameObject); }
    }

    protected void OnDestroy()
    {
        try
        {
            GameManager.Monsters.Remove(this);
            GameObject o = Instantiate(Tools.GetItemInSceneByStr("Teleport"));
            o.transform.position = new Vector3(10, 0, 18);
            o.transform.SetParent(GameManager.GM.monAndItemInScene.GetChild(2));
            if (hp <= 0)
            {
                GameManager.GM.DefeatBoss(Var.ItemType.FireBallItem, gameObject);
            }
        }
        catch { }
    }



}
