using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BaseMonster : HPObject
{

    // Use this for initialization

    public float speed = 5;
    public float viewMinDistance = 5;
    public float viewMaxDistance = 10;
    protected Vector3 dir;
    public bool IsFindPlayer=false;
    public int atk =1;
   // public int flickPower = 5;
    protected bool isClose = false;


    protected void Start () {
        base.Start();
        hurtedCoolTime = 1;
    }
    // Update is called once per frame
    protected void Update() {
        base.Update();
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
            GameManager.playerAni.SetTrigger("GetHurted");
            collision.transform.GetComponent<Player>().Damage(atk);
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
        dir = GameManager.player.transform.position - transform.position; 
        if (dir.magnitude > viewMaxDistance)
            IsFindPlayer = false;
        else if (hurted || dir.magnitude < viewMinDistance) IsFindPlayer = true;
      //  else IsFindPlayer = false;
    }

    public void Damage(int a)
    {
        // if (!hurted) { Hp -= a; getHurted(); }
        hp -= a; getHurted();
        HpContorl.GetComponent<Slider>().value = hp;
    }
 
 


}
