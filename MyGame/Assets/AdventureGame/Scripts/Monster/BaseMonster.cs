using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BaseMonster : HPObject
{

    // Use this for initialization
    public int monsterNum = 1;
    public float speed = 5;
    public float viewMinDistance = 5;
    public float viewMaxDistance = 10;
    protected Vector3 dirFromPlayer;
    public bool IsFindPlayer = false;
    public int atk = 1;
    // public int flickPower = 5;
    protected bool isClose = false;
    public float stopTime=1;
    public static bool saveMonsters=false; 
    protected float _stopTime = 0;

    public float destoryTime = 100f;
    [HideInInspector]
    public static Transform nearestMonster;


    public MonsterData GetMonsterData() {
    MonsterData monsterData = new MonsterData();
        monsterData.monsterNum = monsterNum;
        monsterData.Hp = hp;
        monsterData.monsterPos[0] = transform.position.x;
        monsterData.monsterPos[1] = transform.position.y;
        monsterData.monsterPos[2] = transform.position.z;
        return monsterData;
    }

    protected void Start() {
        base.Start();
        GameManager.Monsters.Add(this);
    }
    // Update is called once per frame
    protected void Update() {
        base.Update();
        FindTheNearestMonster();
        FindingPlayer();
        if (dirFromPlayer.magnitude > 50 && monsterNum > 0) {
            GetComponent<Drop>().dropOrNot = false;
           Destroy(gameObject);
        }
        if (_stopTime > 0) { _stopTime -= Time.deltaTime;  }
    }

    protected void FindTheNearestMonster()
    {
        if (nearestMonster == null) nearestMonster = transform;
        else
        {
            Vector3 disDir = transform.position - GameManager.player.transform.position;
            Vector3 neardisDir = nearestMonster.position - GameManager.player.transform.position;
            if (disDir.magnitude <= neardisDir.magnitude) nearestMonster = transform;
        }
    }
    protected void moveToPlayer() {

        //  Debug.Log(dir.magnitude);
        if (IsFindPlayer && !isDestory)
        {
            // Debug.Log("faxian");
            // Tools.LookAt(transform, dir, 5);

            //    Tools.LookAtOnlyYAxis(transform,GameManager.player.transform);
            // dirFromPlayer.y=0;
            transform.eulerAngles = new Vector3(0, Mathf.Atan2(CaculateDir().normalized.x, CaculateDir().normalized.z) * 180 / Mathf.PI, 0);
           if (m_rigidbody!=null)  m_rigidbody.velocity = CaculateDir().normalized * speed*0.5f;
        }
    }


    protected void OnCollisionStay(Collision collision)
    {
        if (collision.transform.tag == "Player") {
            //   collision.transform.GetComponent<Rigidbody>().velocity = dir.normalized * flickPower;

            GameManager.player.DamageWithAni(atk, transform);
            isClose = true;
        }
    }
    protected void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {

            isClose = false;
        }
    }
    protected void FindingPlayer() {
        dirFromPlayer = GameManager.player.transform.position - transform.position;
        dirFromPlayer.y = 0;
        if (dirFromPlayer.magnitude > viewMaxDistance)
            IsFindPlayer = false;
        else if (hurted || dirFromPlayer.magnitude < viewMinDistance) IsFindPlayer = true;
      //  else IsFindPlayer = false;
    }


    public override void Damage(int a)
    {
       if (!hurted) { hp -= a; getHurted(); }
        HpContorl.GetComponent<Slider>().value = hp;
        _stopTime = stopTime;

    }
    public override void GoDie()
    {
        if (!isDestory)
        {
            Tools.PlayFollowingParticletByName("SmokeEffect", transform);
            Destroy(gameObject.GetComponent<Collider>());
            Destroy(m_rigidbody);
            isDestory = true;
        }

        HurtedTool.Color2B(GetComponentInChildren<Renderer>().material, new Color(1, 1, 1, 0), 30);
        if (HurtedTool.isChildrenColorB(transform, new Color(1, 1, 1, 0))) { GameManager.Monsters.Remove(this); Destroy(gameObject); }
    }

    protected void OnDestroy()
    {
        try { GameManager.Monsters.Remove(this); }
        catch { }
    }

    protected IEnumerator  StopForSeconds(float a)
    {
        float _speed = speed;
        speed = 0;
        yield return new WaitForSeconds(a);
        speed = _speed;

    }

    protected Vector3 CaculateDir() {
     //   Tools.LookAtOnlyYAxis(transform, GameManager.player.transform);
        Vector3 position = transform.position;
        position.y = 0;
        transform.position = position;
        Vector3 dir = Vector3.zero;

        //Ray ray1 = new Ray(transform.position + new Vector3(0, 0.4f, 0), transform.forward);
        //RaycastHit HitInfo1;
        //bool result1 = Physics.Raycast(ray1, out HitInfo1, 2);
        //if (result1 && HitInfo1.collider.tag == "Untagged")
        //{
        //    Vector3 pos = HitInfo1.point;
        //    pos.y = 0;
        //    dir += pos - transform.position - transform.forward;
        //}
        //else
            dir += dirFromPlayer.normalized * 2;

        Vector3 dirleftright = new Vector3(-dirFromPlayer.normalized.z, 0, dirFromPlayer.normalized.x);

        Ray ray2 = new Ray(transform.position + new Vector3(0, 0.05f, 0), dirleftright);
        RaycastHit HitInfo2;
        bool result2 = Physics.Raycast(ray2, out HitInfo2,  2);
        if (result2)
        {
            Vector3 pos = HitInfo2.point;
            pos.y = 0;
            dir += pos - transform.position - dirleftright;
        }
        else dir += dirleftright * 2;

        Ray ray3 = new Ray(transform.position + new Vector3(0, 0.05f, 0), -dirleftright);
        RaycastHit HitInfo3;
        bool result3 = Physics.Raycast(ray3, out HitInfo3,  2);
        if (result3)
        {
            Vector3 pos = HitInfo3.point;
            pos.y = 0;
            dir += pos - transform.position + dirleftright;
        }
        else dir -= dirleftright * 2;

        return dir;

    }
}
