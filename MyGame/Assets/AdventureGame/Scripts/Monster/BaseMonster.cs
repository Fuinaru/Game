using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BaseMonster : HPObject
{

    // Use this for initialization
    protected Player player;
    protected Transform m_Player;
    public float speed = 5;
    public float viewMinDistance = 5;
    public float viewMaxDistance = 10;
    protected Vector3 dir;
    public bool IsFindPlayer=false;
    public int atk =1;
    public int flickPower = 5;
    protected bool isClose = false;


    protected void Start () {
        base.Start();
        if (player == null) m_Player = GameObject.FindWithTag("Player").transform;
        else m_Player = player.transform;

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
            transform.LookAt(m_Player);
            if (!isClose) m_rigidbody.velocity = dir.normalized * 10 * speed * Time.deltaTime;
        }
    }


    protected void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player") {
            collision.transform.GetComponent<Rigidbody>().velocity = dir.normalized * flickPower;
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
        dir = m_Player.position - transform.position; 
        if (dir.magnitude > viewMaxDistance)
            IsFindPlayer = false;
        else if (hurted || dir.magnitude < viewMinDistance) IsFindPlayer = true;
      //  else IsFindPlayer = false;
    }

    public void Damage(int a)
    {
        // if (!hurted) { Hp -= a; getHurted(); }
        Hp -= a; getHurted();
        HpContorl.GetComponent<Slider>().value = Hp;
    }
 
 


}
