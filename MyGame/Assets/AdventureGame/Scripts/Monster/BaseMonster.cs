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
            Tools.LookAtOnlyYAxis(transform,GameManager.player.transform);
            dirFromPlayer.y=0;
           if(m_rigidbody!=null)  m_rigidbody.velocity = dirFromPlayer.normalized * speed*0.5f;
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
    protected IEnumerator  StopForSeconds(float a)
    {
        float _speed = speed;
        speed = 0;
        yield return new WaitForSeconds(a);
        speed = _speed;

    }

}
