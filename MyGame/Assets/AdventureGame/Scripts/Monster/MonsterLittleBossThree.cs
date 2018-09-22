using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterLittleBossThree : BaseMonster
{
 public int ActionMode = 0;
    public MonsterLaunch launch;
    public float teleportDis = 8f;
    private bool isTeleport = false;

    public float leftOrRight = 1f;
    // Use this for initialization
    void Start()
    {
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
        moveAroundPlayer();
        launch.coolTime = 4f;
        // Tools.LookAtOnlyYAxis(transform, GameManager.player.transform);
        if (IsFindPlayer)
        {
            if (launch.CoolEnd() && !isClose)
            {
                int a = Random.Range(-15, 20);
                if (a < 0)
                {
                    Vector3 pos = launch.transform.position + new Vector3(0, 0.5f, 0) + transform.forward;
                    StartCoroutine(launch.ShootAttackAtPosWaitAddForceTwice(Var.ItemType.IceBallItem, pos + transform.right, 400, 0.2f, 0.5f));
                    StartCoroutine(launch.ShootAttackAtPosWaitAddForceTwice(Var.ItemType.IceBallItem, pos - transform.right, 400, 0.6f, 0.5f));

                }
                else if (a < 10)
                {

                    Vector3 pos = launch.transform.position + new Vector3(0, 0.5f, 0) + transform.forward;
                    StartCoroutine(launch.ShootAttackAtPosWaitAddForce(Var.ItemType.IceBallItem, pos, 500, 1.2f));
                    StartCoroutine(launch.ShootAttackAtPosWaitAddForce(Var.ItemType.IceBallItem, pos + transform.right * 2, 400, 2.0f));
                    StartCoroutine(launch.ShootAttackAtPosWaitAddForce(Var.ItemType.IceBallItem, pos + transform.right * 4, 400, 1.6f));
                    StartCoroutine(launch.ShootAttackAtPosWaitAddForce(Var.ItemType.IceBallItem, pos - transform.right * 2, 400, 0.8f));
                    StartCoroutine(launch.ShootAttackAtPosWaitAddForce(Var.ItemType.IceBallItem, pos - transform.right * 4, 400, 0.4f));

                }
                else if (a < 18)
                {
                    Vector3 pos = launch.transform.position + new Vector3(0, 0.5f, 0) + transform.forward;
                    StartCoroutine(launch.ShootAttackAtPosWaitAddForceDirAndPlayerTwice(Var.ItemType.IceBallItem, pos + transform.right * 2, 400, transform.forward, 0, 0.5f));
                    StartCoroutine(launch.ShootAttackAtPosWaitAddForceDirAndPlayerTwice(Var.ItemType.IceBallItem, pos - transform.right * 2, 400, transform.forward, 0.5f, 0.5f));

                }
                else {
                    Vector3 pos = launch.transform.position + new Vector3(0, 0.5f, 0) + transform.forward;
                    StartCoroutine(launch.ShootAttackAtPosWaitAddForceTriple(Var.ItemType.PoisonItem, pos + transform.right, 400, 0.2f, 0.5f,0.5f));
                }
                leftOrRight = -leftOrRight;
                StartCoroutine(WaitSeconds(2f));
            }
        }

    }
    protected void ActionMode2()
    {

        moveAroundPlayer();
        launch.coolTime = 5f;
        TeleportByDis(-0.5f, 2f);
        if (launch.CoolEnd() && !isClose)
        {
            int a = Random.Range(-10, 20);
            if (a < 0)
            {
                Vector3 pos = launch.transform.position + new Vector3(0, 0.5f, 0) + transform.forward;
                StartCoroutine(launch.ShootAttackAtPosWaitAddForceTriple(Var.ItemType.IceBallItem, pos, 400, 0f, 0.5f, 0.5f));
                StartCoroutine(launch.ShootAttackAtPosWaitAddForceTriple(Var.ItemType.IceBallItem, pos + transform.right, 350, 0.5f, 0.5f, 0.5f));
                StartCoroutine(launch.ShootAttackAtPosWaitAddForceTriple(Var.ItemType.IceBallItem, pos - transform.right, 300, 1f, 0.5f, 0.5f));

            }
            else if (a < 12)
            {
                StartCoroutine(launch.AroundPlayerShootAttackWaitAddForceTwice(Var.ItemType.IceBallItem, Random.Range(0, 60), 60, 400, 3, 0.5f, 0.3f));

            }
            else {
                Vector3 pos = launch.transform.position + new Vector3(0, 0.5f, 0) + transform.forward;
                StartCoroutine(launch.ShootAttackAtPosWaitAddForceDirAndPlayerTwice(Var.ItemType.IceBallItem, pos , 400, transform.forward, 0, 0.6f));
                StartCoroutine(launch.ShootAttackAtPosWaitAddForceDirAndPlayerTwice(Var.ItemType.IceBallItem, pos + transform.right, 450, transform.forward + transform.right, 0, 0.4f));
                StartCoroutine(launch.ShootAttackAtPosWaitAddForceDirAndPlayerTwice(Var.ItemType.IceBallItem, pos- transform.right, 450, transform.forward - transform.right, 0, 0.4f));
                StartCoroutine(launch.ShootAttackAtPosWaitAddForceDirAndPlayerTwice(Var.ItemType.IceBallItem, pos + transform.right*2, 500, transform.forward + transform.right * 2, 0, 0.2f));
                StartCoroutine(launch.ShootAttackAtPosWaitAddForceDirAndPlayerTwice(Var.ItemType.IceBallItem, pos - transform.right*2, 500, transform.forward - transform.right * 2, 0, 0.2f));
            }
            StartCoroutine(WaitSeconds(2f));
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


}
