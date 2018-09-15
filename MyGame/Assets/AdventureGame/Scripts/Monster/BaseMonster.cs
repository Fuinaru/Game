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
    protected Vector3 dir;
    public bool IsFindPlayer = false;
    public int atk = 1;
    // public int flickPower = 5;
    protected bool isClose = false;
    public float stopTime=1;
    public static bool saveMonsters=false; 
    protected float _stopTime = 0;
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
        FindingPlayer();
        if (IsFindPlayer && !isDestory)
        {
            // Debug.Log("faxian");
            // Tools.LookAt(transform, dir, 5);
            Tools.LookAtOnlyYAxis(transform,GameManager.player.transform);
            dir.y=0;
            if (!isClose) m_rigidbody.velocity = dir.normalized * speed*0.5f;
        }
    }


    protected void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player") {
            //   collision.transform.GetComponent<Rigidbody>().velocity = dir.normalized * flickPower;

            GameManager.player.DamageWithAni(atk, transform);
            isClose = true;

            _stopTime = stopTime;

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
        dir = GameManager.player.transform.position - transform.position; 
        if (dir.magnitude > viewMaxDistance)
            IsFindPlayer = false;
        else if (hurted || dir.magnitude < viewMinDistance) IsFindPlayer = true;
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


}
